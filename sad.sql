-- --------------------------------------------------------
-- Хост:                         192.168.0.94
-- Версия сервера:               8.0.20 - MySQL Community Server - GPL
-- Операционная система:         Linux
-- HeidiSQL Версия:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных is-31-orlova
DROP DATABASE IF EXISTS `is-31-orlova`;
CREATE DATABASE IF NOT EXISTS `is-31-orlova` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `is-31-orlova`;

-- Дамп структуры для таблица is-31-orlova.Category
DROP TABLE IF EXISTS `Category`;
CREATE TABLE IF NOT EXISTS `Category` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Дамп данных таблицы is-31-orlova.Category: ~3 rows (приблизительно)
DELETE FROM `Category`;
/*!40000 ALTER TABLE `Category` DISABLE KEYS */;
INSERT INTO `Category` (`ID`, `Name`) VALUES
	(1, 'Телек'),
	(2, 'Никита'),
	(3, 'Z');
/*!40000 ALTER TABLE `Category` ENABLE KEYS */;

-- Дамп структуры для таблица is-31-orlova.Customer
DROP TABLE IF EXISTS `Customer`;
CREATE TABLE IF NOT EXISTS `Customer` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `NameFull` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Surname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Otchestvo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Дамп данных таблицы is-31-orlova.Customer: ~0 rows (приблизительно)
DELETE FROM `Customer`;
/*!40000 ALTER TABLE `Customer` DISABLE KEYS */;
INSERT INTO `Customer` (`ID`, `NameFull`, `Surname`, `Name`, `Otchestvo`) VALUES
	(1, 'Обухов', 'Обухов', 'Никита', 'Олегович'),
	(2, 'Никита', 'Емк', 'Опа', 'ЫВ');
/*!40000 ALTER TABLE `Customer` ENABLE KEYS */;

-- Дамп структуры для таблица is-31-orlova.Product
DROP TABLE IF EXISTS `Product`;
CREATE TABLE IF NOT EXISTS `Product` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL DEFAULT '',
  `Category` int NOT NULL,
  `Price` decimal(10,0) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK__Category` (`Category`),
  CONSTRAINT `FK__Category` FOREIGN KEY (`Category`) REFERENCES `Category` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Дамп данных таблицы is-31-orlova.Product: ~2 rows (приблизительно)
DELETE FROM `Product`;
/*!40000 ALTER TABLE `Product` DISABLE KEYS */;
INSERT INTO `Product` (`ID`, `Name`, `Category`, `Price`) VALUES
	(1, 'sad', 3, 1200),
	(2, 'say', 2, 3222);
/*!40000 ALTER TABLE `Product` ENABLE KEYS */;

-- Дамп структуры для таблица is-31-orlova.Sale
DROP TABLE IF EXISTS `Sale`;
CREATE TABLE IF NOT EXISTS `Sale` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Date` date NOT NULL,
  `CustomerID` int NOT NULL DEFAULT '0',
  `SalesmanID` int NOT NULL DEFAULT '0',
  `ProductID` int NOT NULL DEFAULT '0',
  `Quantity` int NOT NULL DEFAULT '0',
  `Sum` decimal(20,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`ID`),
  KEY `FK__Product` (`ProductID`) USING BTREE,
  KEY `FK__Customer` (`CustomerID`) USING BTREE,
  KEY `FK__Salesman` (`SalesmanID`) USING BTREE,
  CONSTRAINT `FK_Sale_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `Customer` (`ID`),
  CONSTRAINT `FK_Sale_Product` FOREIGN KEY (`ProductID`) REFERENCES `Product` (`ID`),
  CONSTRAINT `FK_Sale_Salesman` FOREIGN KEY (`SalesmanID`) REFERENCES `Salesman` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Дамп данных таблицы is-31-orlova.Sale: ~3 rows (приблизительно)
DELETE FROM `Sale`;
/*!40000 ALTER TABLE `Sale` DISABLE KEYS */;
INSERT INTO `Sale` (`ID`, `Date`, `CustomerID`, `SalesmanID`, `ProductID`, `Quantity`, `Sum`) VALUES
	(2, '2021-12-13', 1, 1, 1, 23, 27600.00),
	(3, '2021-12-16', 2, 1, 1, 33, 39600.00),
	(4, '2021-10-17', 1, 1, 2, 5002, 16116444.00);
/*!40000 ALTER TABLE `Sale` ENABLE KEYS */;

-- Дамп структуры для таблица is-31-orlova.Salesman
DROP TABLE IF EXISTS `Salesman`;
CREATE TABLE IF NOT EXISTS `Salesman` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `NameFull` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0',
  `Surname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Otchestvo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Дамп данных таблицы is-31-orlova.Salesman: ~0 rows (приблизительно)
DELETE FROM `Salesman`;
/*!40000 ALTER TABLE `Salesman` DISABLE KEYS */;
INSERT INTO `Salesman` (`ID`, `NameFull`, `Surname`, `Name`, `Otchestvo`) VALUES
	(1, 'Орлов .А.А', 'Орлов', 'Артем', 'Алексеевич');
/*!40000 ALTER TABLE `Salesman` ENABLE KEYS */;

-- Дамп структуры для таблица is-31-orlova.Users
DROP TABLE IF EXISTS `Users`;
CREATE TABLE IF NOT EXISTS `Users` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Login` varchar(50) NOT NULL DEFAULT '',
  `Password` varchar(50) NOT NULL DEFAULT '',
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Login` (`Login`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Дамп данных таблицы is-31-orlova.Users: ~0 rows (приблизительно)
DELETE FROM `Users`;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` (`ID`, `Login`, `Password`, `Name`) VALUES
	(1, 'Admin', 'qwerty', 'Администратор');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
