-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: localhost    Database: sls
-- ------------------------------------------------------
-- Server version	8.0.28

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
-- Table structure for table `sls_sql_final`
--

DROP TABLE IF EXISTS `sls_sql_final`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sls_sql_final` (
  `Student_id` int DEFAULT NULL,
  `Product_id` text,
  `Status` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sls_sql_final`
--

LOCK TABLES `sls_sql_final` WRITE;
/*!40000 ALTER TABLE `sls_sql_final` DISABLE KEYS */;
INSERT INTO `sls_sql_final` VALUES (424255,'B00964ou','Paa lager'),(524252,'f00964ou','Utlaant'),(737363,'B50964oP','Forfall'),(929282,'N00964ou','Paa lager');
/*!40000 ALTER TABLE `sls_sql_final` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sls_sql_product_id`
--

DROP TABLE IF EXISTS `sls_sql_product_id`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sls_sql_product_id` (
  `Product_id` text,
  `Product_name` text,
  `Product_brand` text,
  `Rom_nr` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sls_sql_product_id`
--

LOCK TABLES `sls_sql_product_id` WRITE;
/*!40000 ALTER TABLE `sls_sql_product_id` DISABLE KEYS */;
INSERT INTO `sls_sql_product_id` VALUES ('B00964ou','24\'skjerm','Hp',1206),('f00964ou','Mega','Arduino',1229),('B50964oP','Uno','Arduino',2261),('N00964ou','Headphones','Koss',1229);
/*!40000 ALTER TABLE `sls_sql_product_id` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sls_sql_student_id`
--

DROP TABLE IF EXISTS `sls_sql_student_id`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sls_sql_student_id` (
  `Student_id` int DEFAULT NULL,
  `Fornavn` text,
  `Etternavn` text,
  `telefon_nr` int DEFAULT NULL,
  `email` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sls_sql_student_id`
--

LOCK TABLES `sls_sql_student_id` WRITE;
/*!40000 ALTER TABLE `sls_sql_student_id` DISABLE KEYS */;
INSERT INTO `sls_sql_student_id` VALUES (424255,'John ','Smith',9816434,'j.s@gmail.com'),(524252,'Ali','Baba',6524311,'a.baba@outlook.com'),(737363,'Will','Smith',5252525,'iluvchrisrock@gmail.com'),(929282,'LeBron','James',4424242,'le.James@hotmail.com');
/*!40000 ALTER TABLE `sls_sql_student_id` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-10-12  8:10:06
