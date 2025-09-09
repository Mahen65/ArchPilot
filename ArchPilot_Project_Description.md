# ArchPilot Project Description

## 1. Overview

ArchPilot is a software architecture recommendation engine. It helps users make informed decisions about their software architecture by providing tailored recommendations based on their specific project requirements. The application takes a user through a questionnaire to gather information about their project, and then uses a rule-based engine to generate a detailed architecture recommendation.

## 2. Core Components

The project is divided into several layers, following the principles of Clean Architecture:

*   **Domain:** Contains the core business logic and entities of the application. This includes the `ProjectRequirements` and `ArchitectureRecommendation` entities, as well as various enums that define the possible values for the questionnaire answers.
*   **Application:** Contains the application logic, including services, interfaces, and DTOs. The `IRecommendationEngine` interface is defined here.
*   **Infrastructure:** Contains the implementation of the services defined in the Application layer. This includes the `RecommendationEngine`, which is the core of the recommendation logic.
*   **Presentation:** Contains the user interface and the API. The project includes a Blazor WebAssembly front-end for the user to interact with the questionnaire, and an ASP.NET Core Web API to serve the recommendations.

## 3. How it Works

1.  **Questionnaire:** The user fills out a questionnaire in the Blazor WebAssembly application. The questionnaire covers various aspects of the project, such as project type, scale, expected users, team experience, budget, and more.
2.  **Recommendation Engine:** The project requirements are sent to the `RecommendationEngine`. This engine uses a set of rules to determine the most suitable architecture pattern, technology stack, database, infrastructure, and security recommendations.
3.  **Recommendation Output:** The engine generates a detailed `ArchitectureRecommendation` object. This object includes not only the recommendations themselves, but also a justification, trade-offs, alternatives, an implementation roadmap, and estimated cost and timeline.
4.  **Display:** The recommendation is then displayed to the user in the Blazor application.

## 4. Technology Stack

*   **Backend:** ASP.NET Core
*   **Frontend:** Blazor WebAssembly
*   **Database:** The recommendation engine can recommend various databases, but the default is PostgreSQL.
*   **Architecture:** Clean Architecture

## 5. Key Features

*   **Questionnaire-based requirements gathering:** A comprehensive questionnaire ensures that all relevant aspects of the project are considered.
*   **Rule-based recommendation engine:** The engine provides consistent and repeatable recommendations based on a predefined set of rules.
*   **Detailed recommendations:** The recommendations are not just a list of technologies, but a complete architectural plan with justifications, trade-offs, and implementation details.
*   **Multiple recommendations:** The engine can generate multiple recommendations, allowing the user to compare different approaches.
