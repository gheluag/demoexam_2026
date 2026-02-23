CREATE DATABASE IF NOT EXISTS OfficeDB;
USE OfficeDB;

CREATE TABLE Departments (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Employees (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(150) NOT NULL,
    Position VARCHAR(100),
    Salary DECIMAL(10,2),
    DepartmentId INT,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE SET NULL
);

CREATE TABLE Tasks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    Deadline DATE,
    EmployeeId INT,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE SET NULL
);

INSERT INTO Departments (Name) VALUES
('HR'),
('IT'),
('Бухгалтерия');

INSERT INTO Employees (FullName, Position, Salary, DepartmentId) VALUES
('Анна Иванова', 'HR-менеджер', 3500, 1),
('Петр Сидоров', 'Разработчик ПО', 4500, 2),
('Иван Петров', 'Бухгалтер', 3200, 3);

INSERT INTO Tasks (Title, Deadline, EmployeeId) VALUES
('Подготовить ежемесячный отчет', '2026-02-25', 3),
('Обновить сайт', '2026-02-20', 2),
('Провести собеседования', '2026-02-22', 1);