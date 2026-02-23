CREATE DATABASE IF NOT EXISTS ArtGalleryDB;
USE ArtGalleryDB;

CREATE TABLE Artists (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(150) NOT NULL,
    Country VARCHAR(100),
    BirthYear INT
);

CREATE TABLE Artworks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    YearCreated INT,
    Price DECIMAL(10,2),
    ArtistId INT NOT NULL,
    FOREIGN KEY (ArtistId) REFERENCES Artists(Id) ON DELETE CASCADE
);

CREATE TABLE Exhibitions (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    StartDate DATE,
    EndDate DATE
);

CREATE TABLE ExhibitionArtworks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ExhibitionId INT NOT NULL,
    ArtworkId INT NOT NULL,
    FOREIGN KEY (ExhibitionId) REFERENCES Exhibitions(Id) ON DELETE CASCADE,
    FOREIGN KEY (ArtworkId) REFERENCES Artworks(Id) ON DELETE CASCADE
);

CREATE TABLE Curators (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(150) NOT NULL,
    Email VARCHAR(150)
);

INSERT INTO Artists (FullName, Country, BirthYear) VALUES
('Клод Моне', 'Франция', 1840),
('Винсент Ван Гог', 'Нидерланды', 1853),
('Пабло Пикассо', 'Испания', 1881),
('Фрида Кало', 'Мексика', 1907);

INSERT INTO Artworks (Title, YearCreated, Price, ArtistId) VALUES
('Водяные лилии', 1906, 5000000, 1),
('Звездная ночь', 1889, 8000000, 2),
('Герника', 1937, 12000000, 3),
('Две Фриды', 1939, 4000000, 4);

INSERT INTO Exhibitions (Name, StartDate, EndDate) VALUES
('Мастера импрессионизма', '2026-01-01', '2026-03-01'),
('Революция современного искусства', '2026-02-01', '2026-04-01');

INSERT INTO ExhibitionArtworks (ExhibitionId, ArtworkId) VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4);

INSERT INTO Curators (Name, Email) VALUES
('Анна Смирнова', 'anna@gallery.com'),
('Иван Петров', 'ivan@gallery.com');