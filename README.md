# Library Management System

The Library Management System is a comprehensive .NET Core and .NET 7.0 project developed to simplify the management and exploration of an extensive collection of books, DVDs, and CDs.

## Design Documentation
[Design Document](https://deanprogramming.github.io/CV/Library%20System%20Design%20Doc.pdf) 

## Key Features

### 1. Search and Filter Capabilities
Efficiently search through the database using various criteria such as title, publication year, author, and type (book, DVD, CD). This feature makes it easy to locate specific items in the library's vast collection.

### 2. Availability Check
Real-time availability checks allow users to quickly identify whether a desired book, DVD, or CD is currently in stock, providing a seamless user experience.

### 3. Secure Net Identity Login
Prioritizing security, the system integrates Net Identity, ensuring a robust and secure authentication system for user login.

### 4. User Functionality
Regular users can explore the database and book items. A dedicated menu allows them to view a list of items they have booked, providing a convenient overview of their library activity.

### 5. Admin Privileges
Administrators, equipped with special roles, can access advanced functionalities. They can view booked items of all users, apply filters based on various criteria, and confirm the return of items, making them available for the next users.

### 6. CRUD Operations
Admins have the power to create, read, update, and delete elements in the system, ensuring efficient management of the library's inventory. A complete book history is available, revealing usage patterns and previous users of each item.

### 7. Future Enhancements
The project is designed with future scalability in mind. While currently utilizing a local database, there are plans to expand capabilities by deploying on cloud platforms such as Microsoft Azure. This will provide enhanced scalability, reliability, and additional cloud-based features for the library management application.

## Getting Started

1. **Prerequisites**
   - Install [.NET Core](https://dotnet.microsoft.com/download) 

2. **Installation**
   - Clone the repository: `git clone https://github.com/DeanProgramming/NetCoreLibrarySystem.git`
   - Navigate to the project directory: `cd NetCoreLibrarySystem`

3. **Database Setup**
   - On first load a database is created for you **locally** with test accounts and a set of books/dvd/cds

4. **Run the Application**
   - Run the application using the following command: `dotnet run`

5. **Access the Application**
   - Open your web browser and go to `http://localhost:5000` to access the Library Management System.

