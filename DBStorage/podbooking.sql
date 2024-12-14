CREATE DATABASE  IF NOT EXISTS `podbooking` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `podbooking`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: podbooking
-- ------------------------------------------------------
-- Server version	8.0.38

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
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20241102184010_Update','8.0.10');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `amenityservices`
--

DROP TABLE IF EXISTS `amenityservices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `amenityservices` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` float NOT NULL,
  `Status` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `amenityservices`
--

LOCK TABLES `amenityservices` WRITE;
/*!40000 ALTER TABLE `amenityservices` DISABLE KEYS */;
INSERT INTO `amenityservices` VALUES ('0fbd5640-5cc2-46cc-81d3-edd8b84839c8','Water','Drink',8000,'Available','2024-10-18 02:35:06.020581','2024-10-18 02:35:06.020582'),('13704365-7df4-459d-a0f2-55532d814003','Pizza','Food',50000,'Available','2024-10-18 02:35:27.695144','2024-10-18 02:35:27.695144'),('16b282f4-d444-4f0e-accb-9bb4badf6cc5','Orange Juice','Drink',20000,'Available','2024-10-18 02:33:59.130781','2024-10-18 02:33:59.130783'),('286c6f55-bca7-4ec0-a946-8a34d5e18a23','Tivi','Amenity',50000,'Available','2024-10-18 02:35:44.433119','2024-10-18 02:35:44.433120'),('480443d7-ff53-4cc3-8eda-716c63857af8','White Board','Amenity',30000,'Available','2024-10-18 02:32:13.472181','2024-10-18 02:32:13.472182'),('5b7637c6-2d63-412f-b53d-dbb85acef765',' Bread','Food',20000,'Available','2024-10-18 02:33:35.717798','2024-10-18 02:33:35.717799'),('7012d81d-e7e3-46cf-85d2-00ff7647f451','Coca','Drink',20000,'Available','2024-10-18 02:34:11.642735','2024-10-18 02:34:11.642737'),('7ddfac91-f2a3-444e-a9da-f2ed186f65bf','Rice','Food',5000,'Available','2024-10-18 02:34:42.776638','2024-10-18 02:34:42.776640');
/*!40000 ALTER TABLE `amenityservices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `areas`
--

DROP TABLE IF EXISTS `areas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `areas` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LocationId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Areas_LocationId` (`LocationId`),
  CONSTRAINT `FK_Areas_Locations_LocationId` FOREIGN KEY (`LocationId`) REFERENCES `locations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `areas`
--

LOCK TABLES `areas` WRITE;
/*!40000 ALTER TABLE `areas` DISABLE KEYS */;
INSERT INTO `areas` VALUES ('955f5d74-e849-40a0-ac08-cd5b0eeaff95','WorkChill Thao Dien','WorkChill - Một không gian vừa có cà phê ngon vừa có giải pháp về không gian làm việc hiệu quả vừa khai trương cửa hàng đầu tiên ở khu Thảo Điền, Q.2, quá nhiều những thứ hay ho nên tất nhiên là không quên chụp lại chút hình ảnh đồng thời chia sẻ với mọi người về trải nghiệm của mình ở đây.','215119f8-6e1c-4d3a-85d0-25580374307d','2024-11-05 05:07:51.116878','2024-11-05 05:07:51.116879'),('cba5f6b5-0d57-425a-8d6a-cd1d23b86588','WorkChill Hai Ba Chung','Mọi thứ ở WorkChill là sự kết hợp hài hòa giữa một quán cafe và co-working space, bao hàm nhiều ưu điểm của cả 2 hình thức này: Kiểu cách decor toát lên vibe thoải mái tự nhiên, cà phê ngon, cho tới các tiện ích đặc biệt như kén (pod) làm việc, phòng họp, văn phòng ảo.','9c5a9c93-e2bb-4525-a00c-f30cda1e3cab','2024-10-05 20:06:27.750705','2024-10-05 20:06:27.750707');
/*!40000 ALTER TABLE `areas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `baseentity`
--

DROP TABLE IF EXISTS `baseentity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `baseentity` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `baseentity`
--

LOCK TABLES `baseentity` WRITE;
/*!40000 ALTER TABLE `baseentity` DISABLE KEYS */;
/*!40000 ALTER TABLE `baseentity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bookingitems`
--

DROP TABLE IF EXISTS `bookingitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bookingitems` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `AmountItems` int NOT NULL,
  `Total` float NOT NULL,
  `Status` int NOT NULL,
  `BookingId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `AmenityServiceId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ServiceDetailId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_BookingItems_AmenityServiceId` (`AmenityServiceId`),
  KEY `IX_BookingItems_BookingId` (`BookingId`),
  KEY `IX_BookingItems_ServiceDetailId` (`ServiceDetailId`),
  CONSTRAINT `FK_BookingItems_AmenityServices_AmenityServiceId` FOREIGN KEY (`AmenityServiceId`) REFERENCES `amenityservices` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_BookingItems_Bookings_BookingId` FOREIGN KEY (`BookingId`) REFERENCES `bookings` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_BookingItems_ServiceDetails_ServiceDetailId` FOREIGN KEY (`ServiceDetailId`) REFERENCES `servicedetails` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bookingitems`
--

LOCK TABLES `bookingitems` WRITE;
/*!40000 ALTER TABLE `bookingitems` DISABLE KEYS */;
/*!40000 ALTER TABLE `bookingitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bookings`
--

DROP TABLE IF EXISTS `bookings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bookings` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TimeBooking` time(6) NOT NULL,
  `DateBooking` datetime(6) NOT NULL,
  `Total` float NOT NULL,
  `Status` int NOT NULL,
  `IsPay` tinyint(1) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `RoomId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `MembershipUserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  `IsCheckIn` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Bookings_MembershipUserId` (`MembershipUserId`),
  KEY `IX_Bookings_RoomId` (`RoomId`),
  KEY `IX_Bookings_UserId` (`UserId`),
  CONSTRAINT `FK_Bookings_MembershipUsers_MembershipUserId` FOREIGN KEY (`MembershipUserId`) REFERENCES `membershipusers` (`Id`),
  CONSTRAINT `FK_Bookings_Rooms_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `rooms` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Bookings_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bookings`
--

LOCK TABLES `bookings` WRITE;
/*!40000 ALTER TABLE `bookings` DISABLE KEYS */;
/*!40000 ALTER TABLE `bookings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `depositwithdraws`
--

DROP TABLE IF EXISTS `depositwithdraws`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `depositwithdraws` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Amount` float NOT NULL,
  `Type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Method` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_DepositWithdraws_UserId` (`UserId`),
  CONSTRAINT `FK_DepositWithdraws_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `depositwithdraws`
--

LOCK TABLES `depositwithdraws` WRITE;
/*!40000 ALTER TABLE `depositwithdraws` DISABLE KEYS */;
/*!40000 ALTER TABLE `depositwithdraws` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `devicecheckings`
--

DROP TABLE IF EXISTS `devicecheckings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `devicecheckings` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `BookingItemsId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `StaffId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_DeviceCheckings_BookingItemsId` (`BookingItemsId`),
  KEY `IX_DeviceCheckings_StaffId` (`StaffId`),
  CONSTRAINT `FK_DeviceCheckings_BookingItems_BookingItemsId` FOREIGN KEY (`BookingItemsId`) REFERENCES `bookingitems` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_DeviceCheckings_Users_StaffId` FOREIGN KEY (`StaffId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `devicecheckings`
--

LOCK TABLES `devicecheckings` WRITE;
/*!40000 ALTER TABLE `devicecheckings` DISABLE KEYS */;
/*!40000 ALTER TABLE `devicecheckings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `favourites`
--

DROP TABLE IF EXISTS `favourites`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `favourites` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `RoomId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Favourites_RoomId` (`RoomId`),
  KEY `IX_Favourites_UserId` (`UserId`),
  CONSTRAINT `FK_Favourites_Rooms_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `rooms` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Favourites_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `favourites`
--

LOCK TABLES `favourites` WRITE;
/*!40000 ALTER TABLE `favourites` DISABLE KEYS */;
/*!40000 ALTER TABLE `favourites` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `images`
--

DROP TABLE IF EXISTS `images`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `images` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Url` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoomId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `AreaId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `AmenityServiceId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Images_AmenityServiceId` (`AmenityServiceId`),
  UNIQUE KEY `IX_Images_UserId` (`UserId`),
  KEY `IX_Images_AreaId` (`AreaId`),
  KEY `IX_Images_RoomId` (`RoomId`),
  CONSTRAINT `FK_Images_AmenityServices_AmenityServiceId` FOREIGN KEY (`AmenityServiceId`) REFERENCES `amenityservices` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Images_Areas_AreaId` FOREIGN KEY (`AreaId`) REFERENCES `areas` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Images_Rooms_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `rooms` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Images_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `images`
--

LOCK TABLES `images` WRITE;
/*!40000 ALTER TABLE `images` DISABLE KEYS */;
INSERT INTO `images` VALUES ('0ba60360-b1ff-4736-97ad-07a8471422ce','https://storage.googleapis.com/podbooking-508d3.appspot.com/services/Rice.jpg',NULL,NULL,NULL,'7ddfac91-f2a3-444e-a9da-f2ed186f65bf','2024-10-18 02:34:43.008827','2024-10-18 02:34:43.008828'),('0c0a1d3d-3381-413e-83e1-24b8bce0d84d','https://storage.googleapis.com/podbooking-508d3.appspot.com/Area/WorkChill_Hai_Ba_Chung.jpg',NULL,'cba5f6b5-0d57-425a-8d6a-cd1d23b86588',NULL,NULL,'2024-10-05 20:06:28.705999','2024-10-05 20:06:28.706001'),('125dd9f8-c7cc-4707-8ed0-15fb3605906b','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Meeting_POD_1/3.jpg','41ba7082-47f8-4292-a7f7-854ad324bf34',NULL,NULL,NULL,'2024-10-05 22:05:19.319533','2024-10-05 22:05:19.319534'),('1e158a61-c27c-4402-bbf7-a1b69f99577a','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Fourth_POD_2/0.jpg','86c773d3-0b7f-4e26-85ca-08fa2ceab927',NULL,NULL,NULL,'2024-11-05 05:20:49.885317','2024-11-05 05:20:49.885318'),('1fed4d76-c3f7-4198-8c61-3029876dc6d0','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Fourth_POD_2/3.jpg','86c773d3-0b7f-4e26-85ca-08fa2ceab927',NULL,NULL,NULL,'2024-11-05 05:20:52.319200','2024-11-05 05:20:52.319201'),('25b96e0c-3c40-4c8e-89b9-af7070a8f1d4','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Fourth_POD_2/1.jpg','86c773d3-0b7f-4e26-85ca-08fa2ceab927',NULL,NULL,NULL,'2024-11-05 05:20:50.602985','2024-11-05 05:20:50.602986'),('2eb92096-a7ee-4bc0-a802-c19c031ad0e6','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Double_POD_2/2.webp','46aeab2e-7c2d-4172-9318-93c9fe79eea8',NULL,NULL,NULL,'2024-11-05 05:17:46.027578','2024-11-05 05:17:46.027579'),('2f067418-e733-4a64-8ac9-358bd98ea6ca','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Double_POD_2/3.webp','46aeab2e-7c2d-4172-9318-93c9fe79eea8',NULL,NULL,NULL,'2024-11-05 05:17:46.867863','2024-11-05 05:17:46.867864'),('357afc36-f1d0-4170-9247-15d693ab21ff','https://storage.googleapis.com/podbooking-508d3.appspot.com/services/Tivi.jpg',NULL,NULL,NULL,'286c6f55-bca7-4ec0-a946-8a34d5e18a23','2024-10-18 02:35:44.662612','2024-10-18 02:35:44.662613'),('3f7dbd51-08ce-4fab-91d1-c63f92137f6c','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Fourth_POD_2/2.jpg','86c773d3-0b7f-4e26-85ca-08fa2ceab927',NULL,NULL,NULL,'2024-11-05 05:20:51.466727','2024-11-05 05:20:51.466728'),('45804c8d-a4e8-4a7a-af3b-095a8a5038a1','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Fourth_POD_1/3.jpg','78248ca9-a175-4f2a-9a01-c38cca183a92',NULL,NULL,NULL,'2024-10-05 22:21:24.363152','2024-10-05 22:21:24.363152'),('4738957e-b530-4ed1-8731-f4078a9965bd','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Meeting_POD_2/3.jpg','6dcabf1d-a057-4e08-9999-c9bf8f84f587',NULL,NULL,NULL,'2024-11-05 05:24:21.319361','2024-11-05 05:24:21.319362'),('50bf080c-300f-4e85-9856-9d6c8188071c','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Fourth_POD_1/0.png','78248ca9-a175-4f2a-9a01-c38cca183a92',NULL,NULL,NULL,'2024-10-05 22:21:22.232732','2024-10-05 22:21:22.232734'),('536d5e56-459f-4c10-aa08-ba9590f3efef','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Single_POD_2/2.webp','1de83d38-c239-4135-929e-cef39f00c6f2',NULL,NULL,NULL,'2024-11-05 05:14:18.959547','2024-11-05 05:14:18.959548'),('572d5b6a-0e53-4ffb-9d8d-2d82ab07d258','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Double_POD_2/1.webp','46aeab2e-7c2d-4172-9318-93c9fe79eea8',NULL,NULL,NULL,'2024-11-05 05:17:45.423326','2024-11-05 05:17:45.423328'),('59d0c091-6b91-4397-97cf-1e4d1a3f8dd9','https://storage.googleapis.com/podbooking-508d3.appspot.com/services/ Bread.jpg',NULL,NULL,NULL,'5b7637c6-2d63-412f-b53d-dbb85acef765','2024-10-18 02:33:35.994594','2024-10-18 02:33:35.994596'),('5a38665b-127e-488d-98b6-47fe3a62b7de','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Fourth_POD_1/1.jpg','78248ca9-a175-4f2a-9a01-c38cca183a92',NULL,NULL,NULL,'2024-10-05 22:21:22.942088','2024-10-05 22:21:22.942089'),('5b804540-5a8e-4cc5-903a-ae534b271d64','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Meeting_POD_2/1.avif','6dcabf1d-a057-4e08-9999-c9bf8f84f587',NULL,NULL,NULL,'2024-11-05 05:24:20.464480','2024-11-05 05:24:20.464481'),('6685c8ed-45d8-4714-84dd-213fa0b4846a','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Single_POD_1/2.png','a3f26dd6-b769-476a-8acd-6a60d01b8c9e',NULL,NULL,NULL,'2024-10-05 22:42:55.982336','2024-10-05 22:42:55.982337'),('6f8f7c64-654d-4142-aee1-3b694a732eed','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Single_POD_2/3.webp','1de83d38-c239-4135-929e-cef39f00c6f2',NULL,NULL,NULL,'2024-11-05 05:14:19.536121','2024-11-05 05:14:19.536122'),('71ab6c4b-e3f2-4eda-9b8b-5b3bafd54015','https://storage.googleapis.com/podbooking-508d3.appspot.com/Area/WorkChill_Thao_Dien.png',NULL,'955f5d74-e849-40a0-ac08-cd5b0eeaff95',NULL,NULL,'2024-11-05 05:07:52.597286','2024-11-05 05:07:52.597287'),('7cd04564-b1a7-4136-91ee-15fea89fec3a','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Double_POD_1/0.jpg','44c2711f-ae5b-472c-94c4-d076a4068080',NULL,NULL,NULL,'2024-10-05 22:34:29.924673','2024-10-05 22:34:29.924674'),('7cd66a0b-3270-4fa3-b98d-5f2e459ac6f3','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Double_POD_2/0.webp','46aeab2e-7c2d-4172-9318-93c9fe79eea8',NULL,NULL,NULL,'2024-11-05 05:17:44.761449','2024-11-05 05:17:44.761450'),('8550779d-233b-4dd4-92fc-3c8943c5017d','https://storage.googleapis.com/podbooking-508d3.appspot.com/User/a.png',NULL,NULL,'b0b529c5-0a38-4e43-b82e-2d8e0b9a17d3',NULL,'2024-10-17 01:10:57.173661','2024-10-17 01:10:57.173665'),('8a31fba3-b430-47af-b38f-8202687c7f8b','https://storage.googleapis.com/podbooking-508d3.appspot.com/services/Pizza.jpg',NULL,NULL,NULL,'13704365-7df4-459d-a0f2-55532d814003','2024-10-18 02:35:27.994058','2024-10-18 02:35:27.994059'),('8f5477e1-5448-4908-b570-5bb64364fb0b','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Single_POD_1/0.jpg','a3f26dd6-b769-476a-8acd-6a60d01b8c9e',NULL,NULL,NULL,'2024-10-05 22:42:54.782646','2024-10-05 22:42:54.782646'),('914ce082-779d-42a5-89ab-c36ea95d6d94','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Single_POD_2/0.webp','1de83d38-c239-4135-929e-cef39f00c6f2',NULL,NULL,NULL,'2024-11-05 05:14:17.671418','2024-11-05 05:14:17.671418'),('95f06d9b-3318-4f56-a888-929b5db1f3e6','https://storage.googleapis.com/podbooking-508d3.appspot.com/services/Coca.jpg',NULL,NULL,NULL,'7012d81d-e7e3-46cf-85d2-00ff7647f451','2024-10-18 02:34:11.952734','2024-10-18 02:34:11.952736'),('98297cf2-adc5-401f-a481-5a7b5950f84c','https://storage.googleapis.com/podbooking-508d3.appspot.com/services/Orange Juice.jpg',NULL,NULL,NULL,'16b282f4-d444-4f0e-accb-9bb4badf6cc5','2024-10-18 02:33:59.398299','2024-10-18 02:33:59.398301'),('9c2deb5f-1eb2-4dcb-9e4c-6f8e13a5e9e2','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Meeting_POD_1/2.jpg','41ba7082-47f8-4292-a7f7-854ad324bf34',NULL,NULL,NULL,'2024-10-05 22:05:18.697238','2024-10-05 22:05:18.697239'),('a34b5d02-5f81-491c-88f7-449393f46383','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Fourth_POD_1/2.jpg','78248ca9-a175-4f2a-9a01-c38cca183a92',NULL,NULL,NULL,'2024-10-05 22:21:23.608203','2024-10-05 22:21:23.608204'),('a4320cca-7ef8-495d-9c2d-bd6bbc438d1b','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Meeting_POD_1/0.jpg','41ba7082-47f8-4292-a7f7-854ad324bf34',NULL,NULL,NULL,'2024-10-05 22:05:17.342517','2024-10-05 22:05:17.342518'),('b8397f70-2c06-424c-a8cb-68913b75ac47','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Meeting_POD_2/2.avif','6dcabf1d-a057-4e08-9999-c9bf8f84f587',NULL,NULL,NULL,'2024-11-05 05:24:20.799253','2024-11-05 05:24:20.799254'),('b9a4577d-17ef-4444-abe3-66801c9b35b9','https://storage.googleapis.com/podbooking-508d3.appspot.com/services/White Board.jpg',NULL,NULL,NULL,'480443d7-ff53-4cc3-8eda-716c63857af8','2024-10-18 02:32:13.724672','2024-10-18 02:32:13.724673'),('c1f2c8bc-ccac-48a0-833d-82c45fa2244e','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Meeting_POD_1/1.jpg','41ba7082-47f8-4292-a7f7-854ad324bf34',NULL,NULL,NULL,'2024-10-05 22:05:18.011084','2024-10-05 22:05:18.011085'),('c84d47db-e4e0-4ab2-800d-750e19a7b36a','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Double_POD_1/3.jpg','44c2711f-ae5b-472c-94c4-d076a4068080',NULL,NULL,NULL,'2024-10-05 22:34:31.894521','2024-10-05 22:34:31.894522'),('cc7a0cf8-ef3f-4e5d-8598-52c4b6ac18f3','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Meeting_POD_2/0.avif','6dcabf1d-a057-4e08-9999-c9bf8f84f587',NULL,NULL,NULL,'2024-11-05 05:24:20.092114','2024-11-05 05:24:20.092115'),('e28e1745-39e9-4991-9278-70f4684fc262','https://storage.googleapis.com/podbooking-508d3.appspot.com/services/Water.jpg',NULL,NULL,NULL,'0fbd5640-5cc2-46cc-81d3-edd8b84839c8','2024-10-18 02:35:06.376967','2024-10-18 02:35:06.376968'),('e29c8f92-09f7-4e81-bc3c-c3989aef3406','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Thao_Dien/Single_POD_2/1.webp','1de83d38-c239-4135-929e-cef39f00c6f2',NULL,NULL,NULL,'2024-11-05 05:14:18.336157','2024-11-05 05:14:18.336158'),('e572cf70-b83d-42bb-b9da-22345b0bd0a4','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Single_POD_1/1.png','a3f26dd6-b769-476a-8acd-6a60d01b8c9e',NULL,NULL,NULL,'2024-10-05 22:42:55.380988','2024-10-05 22:42:55.380989'),('e574f331-4fa1-4342-bada-cb8f310b5caf','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Single_POD_1/3.webp','a3f26dd6-b769-476a-8acd-6a60d01b8c9e',NULL,NULL,NULL,'2024-10-05 22:42:56.664339','2024-10-05 22:42:56.664340'),('e65416af-e539-404f-adbb-140767817b55','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Double_POD_1/2.jpg','44c2711f-ae5b-472c-94c4-d076a4068080',NULL,NULL,NULL,'2024-10-05 22:34:31.249928','2024-10-05 22:34:31.249929'),('f6ad4641-86be-432c-9395-342e3030dc9b','https://storage.googleapis.com/podbooking-508d3.appspot.com/Room/WorkChill_Hai_Ba_Chung/Double_POD_1/1.jpg','44c2711f-ae5b-472c-94c4-d076a4068080',NULL,NULL,NULL,'2024-10-05 22:34:30.603840','2024-10-05 22:34:30.603841');
/*!40000 ALTER TABLE `images` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `locations`
--

DROP TABLE IF EXISTS `locations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `locations` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Address` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Longitude` float NOT NULL,
  `Latitude` float NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `locations`
--

LOCK TABLES `locations` WRITE;
/*!40000 ALTER TABLE `locations` DISABLE KEYS */;
INSERT INTO `locations` VALUES ('215119f8-6e1c-4d3a-85d0-25580374307d','39 Đ. Nguyễn Duy Hiệu, Thảo Điền, Quận 2, Hồ Chí Minh',106.736,10.8028,'2024-11-05 05:07:49.833333','2024-11-05 05:07:49.833406'),('9c5a9c93-e2bb-4525-a00c-f30cda1e3cab','195/3 Hai Bà Trưng, Q3, TP.HCM',106.694,10.7877,'2024-10-05 20:06:27.030958','2024-10-05 20:06:27.031026');
/*!40000 ALTER TABLE `locations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `memberships`
--

DROP TABLE IF EXISTS `memberships`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `memberships` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Discount` float NOT NULL,
  `TimeLeft` int NOT NULL,
  `Price` float NOT NULL,
  `Rank` int NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `memberships`
--

LOCK TABLES `memberships` WRITE;
/*!40000 ALTER TABLE `memberships` DISABLE KEYS */;
INSERT INTO `memberships` VALUES ('0899d872-d428-4210-9e66-c3b7dcb53297','Gold',0.15,90,150000,3,'2024-11-05 05:32:32.137719','2024-11-05 05:32:32.137720'),('1172b910-7a14-42e8-b597-a22905e3a344','Silver',0.1,90,100000,2,'2024-11-05 05:32:10.762569','2024-11-05 05:32:10.762571'),('d9f062e8-e79e-472a-8f15-ffc1765bed44','Bronze',0.05,90,50000,1,'2024-11-05 05:31:50.729866','2024-11-05 05:31:50.729867');
/*!40000 ALTER TABLE `memberships` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `membershipusers`
--

DROP TABLE IF EXISTS `membershipusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `membershipusers` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Status` tinyint(1) NOT NULL,
  `MembershipId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_MembershipUsers_MembershipId` (`MembershipId`),
  KEY `IX_MembershipUsers_UserId` (`UserId`),
  CONSTRAINT `FK_MembershipUsers_Memberships_MembershipId` FOREIGN KEY (`MembershipId`) REFERENCES `memberships` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_MembershipUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `membershipusers`
--

LOCK TABLES `membershipusers` WRITE;
/*!40000 ALTER TABLE `membershipusers` DISABLE KEYS */;
/*!40000 ALTER TABLE `membershipusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notifications`
--

DROP TABLE IF EXISTS `notifications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notifications` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Title` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Notifications_UserId` (`UserId`),
  CONSTRAINT `FK_Notifications_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notifications`
--

LOCK TABLES `notifications` WRITE;
/*!40000 ALTER TABLE `notifications` DISABLE KEYS */;
/*!40000 ALTER TABLE `notifications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `paymentrefunds`
--

DROP TABLE IF EXISTS `paymentrefunds`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `paymentrefunds` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Type` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Total` float NOT NULL,
  `PointBonus` int NOT NULL,
  `PaymentType` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Status` tinyint(1) NOT NULL,
  `IsRefundReturnRoom` tinyint(1) DEFAULT NULL,
  `BookingId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_PaymentRefunds_BookingId` (`BookingId`),
  CONSTRAINT `FK_PaymentRefunds_Bookings_BookingId` FOREIGN KEY (`BookingId`) REFERENCES `bookings` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paymentrefunds`
--

LOCK TABLES `paymentrefunds` WRITE;
/*!40000 ALTER TABLE `paymentrefunds` DISABLE KEYS */;
/*!40000 ALTER TABLE `paymentrefunds` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ratingfeedbacks`
--

DROP TABLE IF EXISTS `ratingfeedbacks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ratingfeedbacks` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Feedback` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RatingStar` int NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `RoomId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_RatingFeedbacks_RoomId` (`RoomId`),
  KEY `IX_RatingFeedbacks_UserId` (`UserId`),
  CONSTRAINT `FK_RatingFeedbacks_Rooms_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `rooms` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_RatingFeedbacks_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ratingfeedbacks`
--

LOCK TABLES `ratingfeedbacks` WRITE;
/*!40000 ALTER TABLE `ratingfeedbacks` DISABLE KEYS */;
/*!40000 ALTER TABLE `ratingfeedbacks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `refunditems`
--

DROP TABLE IF EXISTS `refunditems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `refunditems` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `AmountItems` int NOT NULL,
  `Total` float NOT NULL,
  `PaymentRefundId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `BookingItemId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_RefundItems_BookingItemId` (`BookingItemId`),
  KEY `IX_RefundItems_PaymentRefundId` (`PaymentRefundId`),
  CONSTRAINT `FK_RefundItems_BookingItems_BookingItemId` FOREIGN KEY (`BookingItemId`) REFERENCES `bookingitems` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_RefundItems_PaymentRefunds_PaymentRefundId` FOREIGN KEY (`PaymentRefundId`) REFERENCES `paymentrefunds` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `refunditems`
--

LOCK TABLES `refunditems` WRITE;
/*!40000 ALTER TABLE `refunditems` DISABLE KEYS */;
/*!40000 ALTER TABLE `refunditems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES ('42feaeb5-fc53-4163-98b5-d28cfceafa7c','Manager',NULL,NULL),('5a4226d9-e58a-42c4-a786-dba8369b234b','Staff',NULL,NULL),('6489cb2a-f4df-4020-bf31-56f2a19d30c3','Admin',NULL,NULL),('8f02a88d-24d3-43ee-8020-40b9dc94e4cb','Customer',NULL,NULL);
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rooms`
--

DROP TABLE IF EXISTS `rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rooms` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TypeRoom` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` float NOT NULL,
  `Status` int NOT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AreaId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Rooms_AreaId` (`AreaId`),
  CONSTRAINT `FK_Rooms_Areas_AreaId` FOREIGN KEY (`AreaId`) REFERENCES `areas` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rooms`
--

LOCK TABLES `rooms` WRITE;
/*!40000 ALTER TABLE `rooms` DISABLE KEYS */;
INSERT INTO `rooms` VALUES ('1de83d38-c239-4135-929e-cef39f00c6f2','Single','Single POD 2',45000,0,'The meeting rooms at WorkChill can be reserved online, accommodating up to 1 people in a spacious, verdant setting, complete with all essential equipment provided.','955f5d74-e849-40a0-ac08-cd5b0eeaff95','2024-11-05 05:14:16.971000','2024-11-05 05:14:16.971001'),('41ba7082-47f8-4292-a7f7-854ad324bf34','Meeting','Meeting POD 1',210000,0,'The meeting rooms at WorkChill can be reserved online, accommodating up to 10 people in a spacious, verdant setting, complete with all essential equipment provided.','cba5f6b5-0d57-425a-8d6a-cd1d23b86588','2024-10-05 22:05:16.666548','2024-10-05 22:05:16.666548'),('44c2711f-ae5b-472c-94c4-d076a4068080','Double','Double POD 1',80000,0,'The meeting rooms at WorkChill can be reserved online, accommodating up to 2 people in a spacious, verdant setting, complete with all essential equipment provided.','cba5f6b5-0d57-425a-8d6a-cd1d23b86588','2024-10-05 22:34:29.277475','2024-10-05 22:34:29.277475'),('46aeab2e-7c2d-4172-9318-93c9fe79eea8','Double','Double POD 2',85000,0,'The meeting rooms at WorkChill can be reserved online, accommodating up to 1 people in a spacious, verdant setting, complete with all essential equipment provided.','955f5d74-e849-40a0-ac08-cd5b0eeaff95','2024-11-05 05:17:44.049536','2024-11-05 05:17:44.049536'),('6dcabf1d-a057-4e08-9999-c9bf8f84f587','Meeting','Meeting POD 2',200000,0,'The meeting rooms at WorkChill can be reserved online, accommodating up to 1 people in a spacious, verdant setting, complete with all essential equipment provided.','955f5d74-e849-40a0-ac08-cd5b0eeaff95','2024-11-05 05:24:19.636885','2024-11-05 05:24:19.636885'),('78248ca9-a175-4f2a-9a01-c38cca183a92','Fourth','Fourth POD 1',120000,0,'The meeting rooms at WorkChill can be reserved online, accommodating up to 4 people in a spacious, verdant setting, complete with all essential equipment provided.','cba5f6b5-0d57-425a-8d6a-cd1d23b86588','2024-10-05 22:21:21.537604','2024-10-05 22:21:21.537605'),('86c773d3-0b7f-4e26-85ca-08fa2ceab927','Fourth','Fourth POD 2',125000,0,'The meeting rooms at WorkChill can be reserved online, accommodating up to 1 people in a spacious, verdant setting, complete with all essential equipment provided.','955f5d74-e849-40a0-ac08-cd5b0eeaff95','2024-11-05 05:20:49.139281','2024-11-05 05:20:49.139281'),('a3f26dd6-b769-476a-8acd-6a60d01b8c9e','Single','Single POD 1',50000,0,'The meeting rooms at WorkChill can be reserved online, accommodating up to 1 people in a spacious, verdant setting, complete with all essential equipment provided.','cba5f6b5-0d57-425a-8d6a-cd1d23b86588','2024-10-05 22:42:54.311284','2024-10-05 22:42:54.311285');
/*!40000 ALTER TABLE `rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roomutility`
--

DROP TABLE IF EXISTS `roomutility`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roomutility` (
  `RoomsId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UtilitiesId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  PRIMARY KEY (`RoomsId`,`UtilitiesId`),
  KEY `IX_RoomUtility_UtilitiesId` (`UtilitiesId`),
  CONSTRAINT `FK_RoomUtility_Rooms_RoomsId` FOREIGN KEY (`RoomsId`) REFERENCES `rooms` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_RoomUtility_Utilities_UtilitiesId` FOREIGN KEY (`UtilitiesId`) REFERENCES `utilities` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roomutility`
--

LOCK TABLES `roomutility` WRITE;
/*!40000 ALTER TABLE `roomutility` DISABLE KEYS */;
INSERT INTO `roomutility` VALUES ('41ba7082-47f8-4292-a7f7-854ad324bf34','097bc3a4-7f3c-4e56-8e4e-b03be507a9fb'),('44c2711f-ae5b-472c-94c4-d076a4068080','097bc3a4-7f3c-4e56-8e4e-b03be507a9fb'),('78248ca9-a175-4f2a-9a01-c38cca183a92','097bc3a4-7f3c-4e56-8e4e-b03be507a9fb'),('a3f26dd6-b769-476a-8acd-6a60d01b8c9e','097bc3a4-7f3c-4e56-8e4e-b03be507a9fb'),('1de83d38-c239-4135-929e-cef39f00c6f2','2e74a575-16a9-4281-acb3-5cc0046997af'),('46aeab2e-7c2d-4172-9318-93c9fe79eea8','2e74a575-16a9-4281-acb3-5cc0046997af'),('6dcabf1d-a057-4e08-9999-c9bf8f84f587','2e74a575-16a9-4281-acb3-5cc0046997af'),('86c773d3-0b7f-4e26-85ca-08fa2ceab927','2e74a575-16a9-4281-acb3-5cc0046997af'),('1de83d38-c239-4135-929e-cef39f00c6f2','4c756c56-6145-4137-9b62-7ec9276db800'),('41ba7082-47f8-4292-a7f7-854ad324bf34','4c756c56-6145-4137-9b62-7ec9276db800'),('44c2711f-ae5b-472c-94c4-d076a4068080','4c756c56-6145-4137-9b62-7ec9276db800'),('46aeab2e-7c2d-4172-9318-93c9fe79eea8','4c756c56-6145-4137-9b62-7ec9276db800'),('6dcabf1d-a057-4e08-9999-c9bf8f84f587','4c756c56-6145-4137-9b62-7ec9276db800'),('78248ca9-a175-4f2a-9a01-c38cca183a92','4c756c56-6145-4137-9b62-7ec9276db800'),('86c773d3-0b7f-4e26-85ca-08fa2ceab927','4c756c56-6145-4137-9b62-7ec9276db800'),('a3f26dd6-b769-476a-8acd-6a60d01b8c9e','4c756c56-6145-4137-9b62-7ec9276db800'),('41ba7082-47f8-4292-a7f7-854ad324bf34','55265be9-d1c1-4295-99ae-1f3991c71c20'),('1de83d38-c239-4135-929e-cef39f00c6f2','9b7eb2d4-7f94-41f4-acf6-86707d5d60c7'),('46aeab2e-7c2d-4172-9318-93c9fe79eea8','9b7eb2d4-7f94-41f4-acf6-86707d5d60c7'),('6dcabf1d-a057-4e08-9999-c9bf8f84f587','9b7eb2d4-7f94-41f4-acf6-86707d5d60c7'),('86c773d3-0b7f-4e26-85ca-08fa2ceab927','9b7eb2d4-7f94-41f4-acf6-86707d5d60c7'),('41ba7082-47f8-4292-a7f7-854ad324bf34','a68d1a53-dfeb-4470-ada8-788e6b95a619'),('44c2711f-ae5b-472c-94c4-d076a4068080','a68d1a53-dfeb-4470-ada8-788e6b95a619'),('78248ca9-a175-4f2a-9a01-c38cca183a92','a68d1a53-dfeb-4470-ada8-788e6b95a619'),('a3f26dd6-b769-476a-8acd-6a60d01b8c9e','a68d1a53-dfeb-4470-ada8-788e6b95a619'),('1de83d38-c239-4135-929e-cef39f00c6f2','a8428255-b950-497d-a9b7-e5e351a340c4'),('46aeab2e-7c2d-4172-9318-93c9fe79eea8','a8428255-b950-497d-a9b7-e5e351a340c4'),('6dcabf1d-a057-4e08-9999-c9bf8f84f587','a8428255-b950-497d-a9b7-e5e351a340c4'),('86c773d3-0b7f-4e26-85ca-08fa2ceab927','a8428255-b950-497d-a9b7-e5e351a340c4'),('41ba7082-47f8-4292-a7f7-854ad324bf34','acb81410-f3b5-4a55-863f-9ea01aca0619'),('78248ca9-a175-4f2a-9a01-c38cca183a92','acb81410-f3b5-4a55-863f-9ea01aca0619'),('1de83d38-c239-4135-929e-cef39f00c6f2','b59ba942-1425-4bcb-9098-fae4582a5ad6'),('46aeab2e-7c2d-4172-9318-93c9fe79eea8','b59ba942-1425-4bcb-9098-fae4582a5ad6'),('6dcabf1d-a057-4e08-9999-c9bf8f84f587','b59ba942-1425-4bcb-9098-fae4582a5ad6'),('78248ca9-a175-4f2a-9a01-c38cca183a92','b59ba942-1425-4bcb-9098-fae4582a5ad6'),('86c773d3-0b7f-4e26-85ca-08fa2ceab927','b59ba942-1425-4bcb-9098-fae4582a5ad6');
/*!40000 ALTER TABLE `roomutility` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servicedetails`
--

DROP TABLE IF EXISTS `servicedetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `servicedetails` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsNormal` tinyint(1) NOT NULL,
  `IsInUse` tinyint(1) NOT NULL,
  `AmenitySerivceId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ServiceDetails_AmenitySerivceId` (`AmenitySerivceId`),
  CONSTRAINT `FK_ServiceDetails_AmenityServices_AmenitySerivceId` FOREIGN KEY (`AmenitySerivceId`) REFERENCES `amenityservices` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicedetails`
--

LOCK TABLES `servicedetails` WRITE;
/*!40000 ALTER TABLE `servicedetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `servicedetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TransactionType` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Total` float NOT NULL,
  `PaymentRefundId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `MembershipUserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `DepositWithdrawId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Transactions_DepositWithdrawId` (`DepositWithdrawId`),
  UNIQUE KEY `IX_Transactions_MembershipUserId` (`MembershipUserId`),
  UNIQUE KEY `IX_Transactions_PaymentRefundId` (`PaymentRefundId`),
  KEY `IX_Transactions_UserId` (`UserId`),
  CONSTRAINT `FK_Transactions_DepositWithdraws_DepositWithdrawId` FOREIGN KEY (`DepositWithdrawId`) REFERENCES `depositwithdraws` (`Id`) ON DELETE SET NULL,
  CONSTRAINT `FK_Transactions_MembershipUsers_MembershipUserId` FOREIGN KEY (`MembershipUserId`) REFERENCES `membershipusers` (`Id`) ON DELETE SET NULL,
  CONSTRAINT `FK_Transactions_PaymentRefunds_PaymentRefundId` FOREIGN KEY (`PaymentRefundId`) REFERENCES `paymentrefunds` (`Id`) ON DELETE SET NULL,
  CONSTRAINT `FK_Transactions_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions`
--

LOCK TABLES `transactions` WRITE;
/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userareamanagements`
--

DROP TABLE IF EXISTS `userareamanagements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userareamanagements` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `AreaId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_UserAreaManagements_AreaId` (`AreaId`),
  KEY `IX_UserAreaManagements_UserId` (`UserId`),
  CONSTRAINT `FK_UserAreaManagements_Areas_AreaId` FOREIGN KEY (`AreaId`) REFERENCES `areas` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_UserAreaManagements_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userareamanagements`
--

LOCK TABLES `userareamanagements` WRITE;
/*!40000 ALTER TABLE `userareamanagements` DISABLE KEYS */;
/*!40000 ALTER TABLE `userareamanagements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Username` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `DOB` datetime(6) DEFAULT NULL,
  `Wallet` float NOT NULL,
  `Status` int NOT NULL,
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Users_RoleId` (`RoleId`),
  CONSTRAINT `FK_Users_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('1e50e62d-136d-4331-ad2b-a5475e43bb99',NULL,'1234567891','user3@gmail.com','391552c099c101b131feaf24c5795a6a15bc8ec82015424e0d2b4274a369a0bf','User3',NULL,0,0,'8f02a88d-24d3-43ee-8020-40b9dc94e4cb','2024-11-05 05:27:34.557008','2024-11-05 05:27:34.557008'),('2892893f-34f2-4ca2-8390-a5f4c1800b19',NULL,'1234567891','user2@gmail.com','391552c099c101b131feaf24c5795a6a15bc8ec82015424e0d2b4274a369a0bf','User2',NULL,0,0,'8f02a88d-24d3-43ee-8020-40b9dc94e4cb','2024-11-05 05:27:24.869874','2024-11-05 05:27:24.869875'),('43bb9cb6-a28f-44ea-91f9-7ec664014ea4',NULL,'1234567891','dat@gmail.com','391552c099c101b131feaf24c5795a6a15bc8ec82015424e0d2b4274a369a0bf','DatLmao',NULL,0,0,'42feaeb5-fc53-4163-98b5-d28cfceafa7c','2024-11-05 05:26:32.596020','2024-11-05 05:26:32.596020'),('54e9ed77-4c10-4c19-8596-9a32b7bfa0bd',NULL,'1234567891','user1@gmail.com','391552c099c101b131feaf24c5795a6a15bc8ec82015424e0d2b4274a369a0bf','User1',NULL,0,0,'8f02a88d-24d3-43ee-8020-40b9dc94e4cb','2024-11-05 05:27:17.033430','2024-11-05 05:27:17.033430'),('639e1841-a3e8-431f-b63a-d06b8035604a',NULL,'1234567891','phuong@gmail.com','391552c099c101b131feaf24c5795a6a15bc8ec82015424e0d2b4274a369a0bf','PhuongLmao',NULL,0,0,'5a4226d9-e58a-42c4-a786-dba8369b234b','2024-11-05 05:26:55.609038','2024-11-05 05:26:55.609038'),('b0b529c5-0a38-4e43-b82e-2d8e0b9a17d3','Manh Duy','0981041426','duynmse173649@fpt.edu.vn','391552c099c101b131feaf24c5795a6a15bc8ec82015424e0d2b4274a369a0bf','OtakuIsMe','2024-10-06 00:46:13.000000',0,0,'6489cb2a-f4df-4020-bf31-56f2a19d30c3','2024-10-06 00:46:13.469565','2024-10-06 00:46:13.469567');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `utilities`
--

DROP TABLE IF EXISTS `utilities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `utilities` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreateAt` datetime(6) DEFAULT NULL,
  `UpdateAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utilities`
--

LOCK TABLES `utilities` WRITE;
/*!40000 ALTER TABLE `utilities` DISABLE KEYS */;
INSERT INTO `utilities` VALUES ('097bc3a4-7f3c-4e56-8e4e-b03be507a9fb','TV',NULL,NULL),('09dbc964-4755-414b-9e73-3229cd97f8ec','Curtains',NULL,NULL),('2e74a575-16a9-4281-acb3-5cc0046997af','Service benefits',NULL,NULL),('4c756c56-6145-4137-9b62-7ec9276db800','High-speed Wi-Fi',NULL,NULL),('54cdf248-846c-4d8e-98e1-7d0a48a1982e','Adjustable lights',NULL,NULL),('55265be9-d1c1-4295-99ae-1f3991c71c20','Whiteboard',NULL,NULL),('9b7eb2d4-7f94-41f4-acf6-86707d5d60c7','Shared utilities',NULL,NULL),('9e87c47c-e91e-4adc-947d-9d5f17723523','Standard sockets',NULL,NULL),('a68d1a53-dfeb-4470-ada8-788e6b95a619','Hot water dispenser',NULL,NULL),('a8428255-b950-497d-a9b7-e5e351a340c4','Premium table desk',NULL,NULL),('acb81410-f3b5-4a55-863f-9ea01aca0619','Air conditioning',NULL,NULL),('b59ba942-1425-4bcb-9098-fae4582a5ad6','Ergonomic chair',NULL,NULL);
/*!40000 ALTER TABLE `utilities` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-05  5:54:37
