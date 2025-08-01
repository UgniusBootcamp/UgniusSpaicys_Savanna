# Savanna Simulation Game

This repository contains a full-stack web application that simulates a savanna ecosystem. The application features a .NET backend and a React frontend, allowing users to register, log in, and interact with real-time simulations. Animals in the savanna, such as lions, tigers, hyenas, zebras, and antelopes, exhibit behaviors like hunting, fleeing, and reproducing.

The project also includes a standalone console version of the simulation.

## Features

- **User Authentication**: Secure user registration and login functionality using JWT for session management.
- **Real-Time Simulation**: Utilizes SignalR for live, real-time updates of the savanna ecosystem directly to the client.
- **Interactive Gameplay**:
    - **Create & Customize**: Start new games with user-defined map dimensions.
    - **Save & Load**: Persist game states to Azure Blob Storage and load them later to continue a session.
    - **Pause & Resume**: Control the flow of the simulation at any time.
    - **Dynamic Animal Spawning**: Add new animals to the simulation on the fly.
- **Extensible Animal System**: The backend features a plugin-based architecture, allowing new animal types to be added by simply dropping a new DLL into a designated folder.
- **Dual Interfaces**: Includes both a rich, interactive web interface built with React and a classic console application for the simulation.
- **Modern Tech Stack**:
    - **Backend**: ASP.NET Core 8, Entity Framework Core, SignalR, Azure Blob Storage.
    - **Frontend**: React, Vite, Tailwind CSS, and Material-UI.

## Architecture

The application is built on a clean, decoupled architecture, separating the backend logic from the frontend presentation.

### Backend

The backend is built with .NET and is divided into several projects, promoting separation of concerns and maintainability.

-   **`SavannaApp.Api`**: The main web API project built with ASP.NET Core. It provides RESTful endpoints for user authentication and game management, and a SignalR hub (`/game`) for real-time communication.
-   **`SavannaApp.Business`**: Contains the core business logic for the simulation. This includes services for animal creation, game state management (`WebGameRunner`), animal movement strategies, and reproduction mechanics.
-   **`SavannaApp.Data`**: The data access layer. It defines entities (`Animal`, `User`, `Game`), DTOs, the Entity Framework `SavannaDbContext`, repositories for database interaction, and custom exceptions.
-   **`SavannaApp.Animals`**: A unique, plugin-style project containing concrete animal implementations (e.g., `Lion`, `Zebra`). The application dynamically loads animal types from DLLs in the `Plugins` directory at runtime.
-   **`SavannaApp`**: A standalone console application that runs the simulation, demonstrating the engine's independence from the web UI.
-   **`SavannaApp.Tests`**: A suite of MSTest unit tests ensuring the reliability of the core business logic and entities.

### Frontend

The frontend is a modern single-page application (SPA) built with React and Vite.

-   **`SavannaWeb`**: The main project directory.
-   **`src/api`**: Houses the API communication logic, including `axios` clients for REST endpoints and a `SignalRService` for managing the real-time WebSocket connection.
-   **`src/pages`**: Contains the primary views of the application, such as `Home`, `Login`, `Register`, and the main `Savanna` game interface.
-   **`src/components`**: A collection of reusable UI components. Key components include `Game.jsx` for rendering the simulation grid and `GameCreationForm.jsx` for starting or loading a game.
-   **`src/auth`**: Implements authentication state management using React Context (`AuthContext` and `AuthProvider`).
-   **Styling**: A combination of Tailwind CSS for utility-first styling and Material-UI for pre-built components like modals and date pickers.

## Getting Started

To run this project locally, you will need .NET 8 SDK and Node.js installed.

### Backend Setup

1.  **Configure Settings**:
    In `Backend/SavannaApp.Api/appsettings.json`, provide the necessary values for:
    -   `ConnectionStrings`: Your SQL Server connection string.
    -   `Jwt`: Your JWT issuer, audience, and secret key.
    -   `AzureBlob`: Your Azure Blob Storage connection key.
    You can use `.env` files for development secrets.

2.  **Restore Dependencies**:
    Navigate to the `Backend/` directory and run:
    ```bash
    dotnet restore
    ```

3.  **Apply Database Migrations**:
    Ensure the EF Core tools are installed (`dotnet tool install --global dotnet-ef`). Then, from the `Backend/SavannaApp.Data/` directory, run:
    ```bash
    dotnet ef database update --startup-project ../SavannaApp.Api
    ```

4.  **Run the API**:
    From the `Backend/` directory, run the API project:
    ```bash
    dotnet run --project SavannaApp.Api
    ```
    The API will be available at `https://localhost:7164` and `http://localhost:5010`.

### Frontend Setup

1.  **Configure Environment Variables**:
    In the `Frontend/SavannaWeb/` directory, create a `.env` file and define the backend URLs:
    ```
    VITE_API_BASE_URL=https://localhost:7164/api
    VITE_API_GAME_URL=https://localhost:7164/game
    ```

2.  **Install Dependencies**:
    Navigate to the `Frontend/SavannaWeb/` directory and run:
    ```bash
    npm install
    ```

3.  **Start the Development Server**:
    ```bash
    npm run dev
    ```
    The application will be available at `http://localhost:59752`.

## How It Works

-   **Simulation Loop**: For each active game, the backend runs an asynchronous loop. In each iteration (tick), all animals perform an action based on their state and environment.
-   **Animal Behavior**:
    -   **Movement**: Animals move based on their `speed` and `vision` attributes. Hunters move towards the nearest prey within their vision, while prey flee from the nearest hunter. If no threat or target is nearby, they move randomly.
    -   **Interaction**: When a hunter catches a prey, the prey dies, and the hunter's health increases.
    -   **Health**: Animals lose a small amount of health with every move and die if their health reaches zero.
    -   **Reproduction**: If two animals of the same species stay within close proximity for a set number of rounds, a new animal of that species is born nearby.
-   **Persistence**: Game sessions can be serialized into JSON format and saved to a user-specific container in Azure Blob Storage. These can be loaded back to resume the simulation from the saved state.
