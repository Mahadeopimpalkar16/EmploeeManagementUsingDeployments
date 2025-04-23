### **EmploeeManagementUsingDeployments**

### Description
The Employee Management System is a secure and scalable full-stack CRUD application designed to manage employee data efficiently. It integrates Angular for the frontend and ASP.NET Core Web API for the backend, with deployment automation using Docker and Kubernetes. This project emphasizes clean architecture, modularity, and automation for performance and maintainability.

### Features
- CRUD Operations: Manage employee data securely.
- Real-Time Updates: Angular ensures dynamic UI responsiveness.
- RESTful API Integration: Backend communication using ASP.NET Core Web API.
- Microservices Architecture: Modular and scalable design.
- Containerization: Applications containerized with Docker.
- Orchestration and Deployment: Automated scaling and deployment with Kubernetes.
- CI/CD Automation: Jenkins streamlines continuous integration and deployment processes.

### Tech Stack
- Frontend: Angular, HTML5, CSS 
- Backend: ASP.NET Core Web API, Entity Framework 
- Database: SQL Server 
- DevOps: Docker, Kubernetes, Jenkins Tools: Git

### Installation and Setup
Follow these steps to set up and run the project locally:

### Frontend Setup 
git clone https://github.com/Mahadeopimpalkar16/EmploeeManagementUsingDeployments.git
cd frontend 
npm install 
npm start  

### Backend Setup
cd backend  
dotnet run  

### Docker Setup

# Build Docker images for frontend and backend  
- docker build -t frontend-app ./frontend  
- docker build -t backend-app ./backend  
# Start containers  
- docker-compose up  

### API Documentation

- GET /api/employees - Fetch all employee records.

- POST /api/employees - Create a new employee record.

- PUT /api/employees/{id} - Update an existing employee record.

- DELETE /api/employees/{id} - Delete an employee record.


### Learning Goals
This project is designed to strengthen my skills in:
- Full-stack application development.
- Clean architecture and modular design.
- Microservices implementation.
- Containerization with Docker and Kubernetes.
- Automation of deployment pipelines using Jenkins.






