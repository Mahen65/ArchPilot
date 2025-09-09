const CACHE_NAME = 'archpilot-blazor-v1.0';
const WASM_CACHE = 'archpilot-wasm-v1.0';

// Files to cache immediately - only essential files that definitely exist
const ESSENTIAL_CACHE_FILES = [
    '/',
    '/_framework/blazor.webassembly.js'
];

// Optional files to cache if they exist
const OPTIONAL_CACHE_FILES = [
    '/index.html',
    '/manifest.json',
    '/css/app.css',
    '/css/bootstrap/bootstrap.min.css',
    '/icon-192.png',
    '/icon-512.png'
];

// Helper function to safely cache files
async function safeCacheFiles(cache, urls) {
    const cachePromises = urls.map(async url => {
        try {
            const response = await fetch(url);
            if (response.status === 200) {
                await cache.put(url, response);
                console.log('Successfully cached:', url);
            } else {
                console.log('Skipping cache for', url, '- Status:', response.status);
            }
        } catch (error) {
            console.log('Failed to cache', url, ':', error.message);
        }
    });

    await Promise.all(cachePromises);
}

// Install event - cache static files
self.addEventListener('install', event => {
    console.log('Service Worker installing...');
    event.waitUntil(
        (async () => {
            try {
                const cache = await caches.open(CACHE_NAME);

                // Cache essential files first (these must succeed)
                console.log('Caching essential files');
                await cache.addAll(ESSENTIAL_CACHE_FILES);

                // Cache optional files (these can fail without breaking the install)
                console.log('Caching optional files');
                await safeCacheFiles(cache, OPTIONAL_CACHE_FILES);

                console.log('Service Worker installation completed');
                await self.skipWaiting();
            } catch (error) {
                console.error('Service Worker installation failed:', error);
                // Still skip waiting even if some caching failed
                await self.skipWaiting();
            }
        })()
    );
});

// Activate event - clean up old caches
self.addEventListener('activate', event => {
    console.log('Service Worker activating...');
    event.waitUntil(
        caches.keys().then(cacheNames => {
            return Promise.all(
                cacheNames.map(cacheName => {
                    if (cacheName !== CACHE_NAME && cacheName !== WASM_CACHE) {
                        console.log('Deleting old cache:', cacheName);
                        return caches.delete(cacheName);
                    }
                })
            );
        }).then(() => self.clients.claim())
    );
});

// Fetch event - implement caching strategy
self.addEventListener('fetch', event => {
    const url = new URL(event.request.url);

    // Cache WebAssembly files with long-term caching
    if (url.pathname.includes('_framework/') &&
        (url.pathname.endsWith('.dll') ||
            url.pathname.endsWith('.wasm') ||
            url.pathname.endsWith('.pdb') ||
            url.pathname.endsWith('.dat'))) {

        event.respondWith(
            caches.open(WASM_CACHE).then(cache => {
                return cache.match(event.request).then(cachedResponse => {
                    if (cachedResponse) {
                        console.log('Serving WASM file from cache:', url.pathname);
                        return cachedResponse;
                    }

                    return fetch(event.request).then(networkResponse => {
                        if (networkResponse.status === 200) {
                            console.log('Caching WASM file:', url.pathname);
                            cache.put(event.request, networkResponse.clone());
                        }
                        return networkResponse;
                    });
                });
            })
        );
        return;
    }

    // Cache static assets with cache-first strategy
    if (url.pathname === '/' ||
        url.pathname.endsWith('.css') ||
        url.pathname.endsWith('.js') ||
        url.pathname.endsWith('.html') ||
        url.pathname.endsWith('.json') ||
        url.pathname.endsWith('.png') ||
        url.pathname.endsWith('.ico')) {

        event.respondWith(
            caches.match(event.request).then(cachedResponse => {
                if (cachedResponse) {
                    return cachedResponse;
                }
                return fetch(event.request).then(networkResponse => {
                    if (networkResponse.status === 200) {
                        const responseClone = networkResponse.clone();
                        caches.open(CACHE_NAME).then(cache => {
                            cache.put(event.request, responseClone);
                        });
                    }
                    return networkResponse;
                }).catch(error => {
                    console.log('Network request failed for static asset:', url.pathname);
                    // Return a proper error response or try cache again
                    return caches.match(event.request);
                });
            })
        );
        return;
    }

    // For API calls to your backend, bypass service worker completely
    if (url.origin !== location.origin || url.pathname.startsWith('/api/')) {
        // Let the browser handle API calls directly without service worker interference
        return;
    }

    // Default: try network first, then cache
    event.respondWith(
        fetch(event.request).catch(() => {
            return caches.match(event.request);
        })
    );
});

// Handle service worker updates
self.addEventListener('message', event => {
    if (event.data && event.data.type === 'SKIP_WAITING') {
        self.skipWaiting();
    }
});