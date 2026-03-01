-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: shoeshop
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `idProd` int NOT NULL AUTO_INCREMENT,
  `Article` varchar(255) NOT NULL,
  `ProdName` int NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Sale` int NOT NULL,
  `Count` int NOT NULL,
  `Descrip` text,
  `Image` text,
  `SupId` int NOT NULL,
  `ManufId` int NOT NULL,
  `CatId` int NOT NULL,
  PRIMARY KEY (`idProd`),
  KEY `SupId` (`SupId`),
  KEY `ProdName` (`ProdName`),
  KEY `ManufId` (`ManufId`),
  KEY `CatId` (`CatId`),
  CONSTRAINT `products_ibfk_1` FOREIGN KEY (`SupId`) REFERENCES `suppliers` (`idSup`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `products_ibfk_2` FOREIGN KEY (`ProdName`) REFERENCES `prodnames` (`idProdName`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `products_ibfk_3` FOREIGN KEY (`ManufId`) REFERENCES `manufacrurers` (`idManuf`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `products_ibfk_4` FOREIGN KEY (`CatId`) REFERENCES `categories` (`idCat`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (1,'А112Т4',1,4990.00,3,6,'Женские Ботинки демисезонные kari','1.jpg',1,1,1),(2,'F635R4',1,3244.00,2,13,'Ботинки Marco Tozzi женские демисезонные, размер 39, цвет бежевый','2.jpg',2,2,1),(3,'H782T5',2,4499.00,4,5,'Туфли kari мужские классика MYZ21AW-450A, размер 43, цвет: черный','3.jpg',1,1,2),(4,'G783F5',1,5900.00,2,8,'Мужские ботинки Рос-Обувь кожаные с натуральным мехом','4.jpg',1,3,2),(5,'J384T6',1,3800.00,2,16,'B3430/14 Полуботинки мужские Rieker','5.jpg',2,4,2),(6,'D572U8',3,4100.00,3,6,'129615-4 Кроссовки мужские','6.jpg',2,3,2),(7,'F572H7',2,2700.00,2,14,'Туфли Marco Tozzi женские летние, размер 39, цвет черный','7.jpg',1,2,1),(8,'D329H3',4,1890.00,4,4,'Полуботинки Alessio Nesca женские 3-30797-47, размер 37, цвет: бордовый','8.jpg',2,5,1),(9,'B320R5',2,4300.00,2,6,'Туфли Rieker женские демисезонные, размер 41, цвет коричневый','9.jpg',1,4,1),(10,'G432E4',2,2800.00,3,15,'Туфли kari женские TR-YR-413017, размер 37, цвет: черный','10.jpg',1,1,1),(11,'S213E3',4,2156.00,3,6,'407700/01-01 Полуботинки мужские CROSBY','',2,6,2),(12,'E482R4',4,1800.00,2,14,'Полуботинки kari женские MYZ20S-149, размер 41, цвет: черный','',1,1,1),(13,'S634B5',5,5500.00,3,0,'Кеды Caprice мужские демисезонные, размер 42, цвет черный','',2,6,2),(14,'K345R4',4,2100.00,2,3,'407700/01-02 Полуботинки мужские CROSBY','',2,6,2),(15,'O754F4',2,5400.00,4,18,'Туфли женские демисезонные Rieker артикул 55073-68/37','',2,4,1),(16,'G531F4',1,6600.00,12,9,'Ботинки женские зимние ROMER арт. 893167-01 Черный','',1,1,1),(17,'J542F5',6,500.00,13,0,'Тапочки мужские Арт.70701-55-67син р.41','',1,1,2),(18,'B431R5',1,2700.00,2,5,'Мужские кожаные ботинки/мужские ботинки','',2,4,2),(19,'P764G4',2,6800.00,15,15,'Туфли женские, ARGO, размер 38','',1,6,1),(20,'C436G5',1,10200.00,15,9,'Ботинки женские, ARGO, размер 40','',1,5,1),(21,'F427R5',1,11800.00,15,11,'Ботинки на молнии с декоративной пряжкой FRAU','',2,4,1),(22,'N457T5',4,4600.00,3,13,'Полуботинки Ботинки черные зимние, мех','',1,6,1),(23,'D364R4',2,12400.00,16,5,'Туфли Luiza Belly женские Kate-lazo черные из натуральной замши','',1,1,1),(24,'S326R5',6,9900.00,17,15,'Мужские кожаные тапочки \"Профиль С.Дали\"\" \"','',2,6,2),(25,'L754R4',4,1700.00,2,7,'Полуботинки kari женские WB2020SS-26, размер 38, цвет: черный','',1,1,1),(26,'M542T5',3,2800.00,18,3,'Кроссовки мужские TOFA','',2,4,2),(27,'D268G5',2,4399.00,3,12,'Туфли Rieker женские демисезонные, размер 36, цвет коричневый','',2,4,1),(28,'T324F5',7,4699.00,2,5,'Сапоги замша Цвет: синий','',1,6,1),(29,'K358H6',6,599.00,20,2,'Тапочки мужские син р.41','',1,4,2),(30,'H535R5',1,2300.00,2,7,'Женские Ботинки демисезонные','',2,4,1);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-02-15 18:36:54
