-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Počítač: 127.0.0.1
-- Vytvořeno: Pon 14. zář 2020, 19:41
-- Verze serveru: 10.4.11-MariaDB
-- Verze PHP: 7.4.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Databáze: `ehr`
--
CREATE DATABASE IF NOT EXISTS `ehr` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `ehr`;

-- --------------------------------------------------------

--
-- Struktura tabulky `address`
--

CREATE TABLE `address` (
  `ID` int(11) NOT NULL,
  `street_name` varchar(32) NOT NULL,
  `street_number` int(8) NOT NULL,
  `city` varchar(32) NOT NULL,
  `postal_code` int(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `address`
--

INSERT INTO `address` (`ID`, `street_name`, `street_number`, `city`, `postal_code`) VALUES
(7, 'Nová', 2635, 'Ústí nad Labem', 36501),
(8, 'Nová', 2596, 'Přerov', 36592),
(10, 'Třebízského', 1236, 'Praha', 43601),
(11, 'Nová', 2653, 'Opava', 23564),
(12, 'Nová', 2659, 'Opava', 23564),
(13, 'Váňova', 3226, 'Kladno', 27201),
(14, 'Váňova', 3227, 'Kladno', 27201),
(19, 'Nová', 2635, 'Ústí nad Labem', 26501);

-- --------------------------------------------------------

--
-- Struktura tabulky `examination`
--

CREATE TABLE `examination` (
  `ID` int(11) NOT NULL,
  `patientID` int(16) NOT NULL,
  `weight` double NOT NULL,
  `height` double NOT NULL,
  `pressure_dis` int(8) NOT NULL,
  `pressure_sys` int(8) NOT NULL,
  `saturation` int(2) NOT NULL,
  `BPM` int(11) NOT NULL,
  `date` date NOT NULL,
  `today_examination` mediumtext NOT NULL,
  `exam_code_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `examination`
--

INSERT INTO `examination` (`ID`, `patientID`, `weight`, `height`, `pressure_dis`, `pressure_sys`, `saturation`, `BPM`, `date`, `today_examination`, `exam_code_ID`) VALUES
(1, 22, 86, 195, 67, 120, 93, 99, '2020-09-23', 'Dne:11.09.2020 17:31:19 Váha=86 Výška=195 BMI= 22,6166995397765\nDiastolický tlak= 67 Systolický tlak= 120 Saturace= 93\nDnešní vyšetření:', 10),
(2, 26, 75, 186, 98, 125, 94, 89, '2020-09-17', 'Dne:11.09.2020 17:37:56 Váha=75 Výška=186 BMI= 21,6788067984738\nDiastolický tlak= 98 Systolický tlak= 125 Saturace= 94\nDnešní vyšetření:Dnes prisel pacient blabla je mu blabla aha.', 10),
(8, 22, 755, 75, 75, 75, 75, 58, '2020-02-19', 'Dne:12.00.2020 Váha=755 Výška=75 BMI= 1342,22222222222\nDiastolický tlak= 75 Systolický tlak= 75 Saturace= 75\nDnešní vyšetření:hjhj', 10),
(12, 22, 45, 45, 45, 45, 45, 45, '2020-09-12', 'Dne:2020.09.12 Váha=45 Výška=45 BMI= 222,222222222222\nDiastolický tlak= 45 Systolický tlak= 45 Saturace= 45\nDnešní vyšetření:sfasf', 10),
(13, 22, 187, 54, 87, 120, 96, 85, '2020-09-12', 'Dne:2020.09.12 Váha=187 Výška=54 BMI= 641,289437585734\nDiastolický tlak= 87 Systolický tlak= 120 Saturace= 96\nDnešní vyšetření:Dnes je mu lepe', 10),
(14, 22, 789, 79, 79, 79, 79, 89, '2020-09-12', 'Dne:2020.09.12 Váha=789 Výška=79 BMI= 1264,22047748758\nDiastolický tlak= 79 Systolický tlak= 79 Saturace= 79\nDnešní vyšetření:Je to naprd', 10),
(15, 26, 46, 466, 54, 16, 13, 95, '2020-09-12', 'Dne:2020.09.12 Váha=46 Výška=466 BMI= 2,1182928401702\nDiastolický tlak= 54 Systolický tlak= 16 Saturace= 13\nDnešní vyšetření:testuju', 14),
(16, 22, 89, 48, 45, 24, 95, 64, '2020-09-12', 'Dne:2020.09.12 Váha=89 Výška=48 BMI= 386,284722222222\nDiastolický tlak= 45 Systolický tlak= 24 Saturace= 95\nDnešní vyšetření:Je to mongol.', 14);

-- --------------------------------------------------------

--
-- Struktura tabulky `examination_codes`
--

CREATE TABLE `examination_codes` (
  `ID` int(11) NOT NULL,
  `name` varchar(64) NOT NULL,
  `loinc_code` varchar(11) NOT NULL,
  `snomed_code` varchar(11) NOT NULL,
  `mkn10_code` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `examination_codes`
--

INSERT INTO `examination_codes` (`ID`, `name`, `loinc_code`, `snomed_code`, `mkn10_code`) VALUES
(10, 'test', '12525', '65543', '122356'),
(11, 'Asthma NS', '82674-3', '195967001', 'J45.9'),
(12, 'Chřipka způsobená identifikovaným sezónním chřipkovým virem', '85476', '6142004', 'J10'),
(13, 'Infekce Salmonelami NS', '59846-6', '110378009', 'A02.9'),
(14, 'Downův syndrom', 'LP74785-4', '41040004', 'Q90');

-- --------------------------------------------------------

--
-- Struktura tabulky `insurance`
--

CREATE TABLE `insurance` (
  `ID` int(11) NOT NULL,
  `code` int(16) NOT NULL,
  `name` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `insurance`
--

INSERT INTO `insurance` (`ID`, `code`, `name`) VALUES
(1, 111, 'VZP'),
(2, 201, 'Vojenská zdravotní pojišťovna'),
(3, 205, 'Česká průmyslová zdravotní pojiš'),
(7, 209, 'Zaměstnanecká pojišťovna Škoda');

-- --------------------------------------------------------

--
-- Struktura tabulky `medication`
--

CREATE TABLE `medication` (
  `ID` int(11) NOT NULL,
  `name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_czech_ci NOT NULL,
  `sukl_code` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `medication`
--

INSERT INTO `medication` (`ID`, `name`, `sukl_code`) VALUES
(1, 'CODEIN SLOVAKOFARMA', 207940),
(2, 'DIAZEPAM SLOVAKOFARMA', 230422),
(3, 'LORATADIN RATIOPHARM', 40662),
(4, 'IBUPROFEN ', 241992),
(5, 'STOPEX NA SUCHÝ KAŠEL', 204004);

-- --------------------------------------------------------

--
-- Struktura tabulky `medication_link`
--

CREATE TABLE `medication_link` (
  `patientID` int(11) NOT NULL,
  `medicationID` int(11) NOT NULL,
  `examinationID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `medication_link`
--

INSERT INTO `medication_link` (`patientID`, `medicationID`, `examinationID`) VALUES
(26, 2, 2),
(22, 3, 2),
(22, 3, 12),
(22, 5, 12),
(22, 2, 12),
(22, 2, 12),
(22, 1, 12);

-- --------------------------------------------------------

--
-- Struktura tabulky `patient`
--

CREATE TABLE `patient` (
  `ID` int(11) NOT NULL,
  `name` varchar(64) NOT NULL,
  `surname` varchar(64) NOT NULL,
  `sexID` int(11) NOT NULL,
  `addressID` int(8) NOT NULL,
  `tel_number` int(16) NOT NULL,
  `anamnesis` mediumtext NOT NULL,
  `insuranceID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `patient`
--

INSERT INTO `patient` (`ID`, `name`, `surname`, `sexID`, `addressID`, `tel_number`, `anamnesis`, `insuranceID`) VALUES
(22, 'Antonín', 'Němeček', 1, 12, 65326546, '\n11.09.2020 16:08:37\nDnesni zaznam\nDne:11.09.2020 17:02:22 Váha=85 Výška=178 BMI= 0,00268274207802045\nDiastolický tlak= 89 Systolický tlak= 110 Saturace= 93\n Dnešní vyšetření:\n\nDne:2020.09.12 Váha=187 Výška=54 BMI= 641,289437585734\nDiastolický tlak= 87 Systolický tlak= 120 Saturace= 96\nDnešní vyšetření:Dnes je mu lepe\n\nPacientovi jsou předepsané následující léčiva: LORATADIN RATIOPHARM, STOPEX NA SUCHÝ KAŠEL, \n\nDne:2020.09.12 Váha=789 Výška=79 BMI= 1264,22047748758\nDiastolický tlak= 79 Systolický tlak= 79 Saturace= 79\nDnešní vyšetření:Je to naprd\n\nPacientovi jsou předepsané následující léčiva: DIAZEPAM SLOVAKOFARMA, \n\nDne:2020.09.12 Váha=89 Výška=48 BMI= 386,284722222222\nDiastolický tlak= 45 Systolický tlak= 24 Saturace= 95\nDnešní vyšetření:Je to mongol.\n\nPacientovi jsou předepsané následující léčiva: DIAZEPAM SLOVAKOFARMA, CODEIN SLOVAKOFARMA, \n', 1),
(26, 'Karel', 'Novák', 1, 10, 72564126, 'Karel Novák se dostavil v ranních hodinách se zdravotními problémy. Bolelo ho břicho. Dopporučuji jej poslat na sono, abychom mohli zjistit více.\n11.09.2020 16:03:52\nTest data navstevy.\n11.09.2020 16:07:37\nTest dva', 1);

-- --------------------------------------------------------

--
-- Struktura tabulky `sex`
--

CREATE TABLE `sex` (
  `ID` int(11) NOT NULL,
  `name` varchar(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `sex`
--

INSERT INTO `sex` (`ID`, `name`) VALUES
(1, 'muž'),
(2, 'žena');

-- --------------------------------------------------------

--
-- Struktura tabulky `user_staff`
--

CREATE TABLE `user_staff` (
  `ID` int(11) NOT NULL,
  `nickname` varchar(16) NOT NULL,
  `pwd` varchar(32) NOT NULL,
  `name` varchar(16) NOT NULL,
  `surname` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `user_staff`
--

INSERT INTO `user_staff` (`ID`, `nickname`, `pwd`, `name`, `surname`) VALUES
(1, 'test', 'heslo', 'Petr', 'Pavel');

--
-- Klíče pro exportované tabulky
--

--
-- Klíče pro tabulku `address`
--
ALTER TABLE `address`
  ADD PRIMARY KEY (`ID`);

--
-- Klíče pro tabulku `examination`
--
ALTER TABLE `examination`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `patientID` (`patientID`),
  ADD KEY `exam_code` (`exam_code_ID`);

--
-- Klíče pro tabulku `examination_codes`
--
ALTER TABLE `examination_codes`
  ADD PRIMARY KEY (`ID`);

--
-- Klíče pro tabulku `insurance`
--
ALTER TABLE `insurance`
  ADD PRIMARY KEY (`ID`);

--
-- Klíče pro tabulku `medication`
--
ALTER TABLE `medication`
  ADD PRIMARY KEY (`ID`);

--
-- Klíče pro tabulku `medication_link`
--
ALTER TABLE `medication_link`
  ADD KEY `patientID` (`patientID`),
  ADD KEY `medicationID` (`medicationID`),
  ADD KEY `examinationID` (`examinationID`);

--
-- Klíče pro tabulku `patient`
--
ALTER TABLE `patient`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `address` (`addressID`),
  ADD KEY `insuranceID` (`insuranceID`),
  ADD KEY `sexID` (`sexID`);

--
-- Klíče pro tabulku `sex`
--
ALTER TABLE `sex`
  ADD PRIMARY KEY (`ID`);

--
-- Klíče pro tabulku `user_staff`
--
ALTER TABLE `user_staff`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `nickname` (`nickname`);

--
-- AUTO_INCREMENT pro tabulky
--

--
-- AUTO_INCREMENT pro tabulku `address`
--
ALTER TABLE `address`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT pro tabulku `examination`
--
ALTER TABLE `examination`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT pro tabulku `examination_codes`
--
ALTER TABLE `examination_codes`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT pro tabulku `insurance`
--
ALTER TABLE `insurance`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT pro tabulku `medication`
--
ALTER TABLE `medication`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT pro tabulku `patient`
--
ALTER TABLE `patient`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pro tabulku `sex`
--
ALTER TABLE `sex`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pro tabulku `user_staff`
--
ALTER TABLE `user_staff`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Omezení pro exportované tabulky
--

--
-- Omezení pro tabulku `examination`
--
ALTER TABLE `examination`
  ADD CONSTRAINT `examination_ibfk_1` FOREIGN KEY (`patientID`) REFERENCES `patient` (`ID`),
  ADD CONSTRAINT `examination_ibfk_2` FOREIGN KEY (`exam_code_ID`) REFERENCES `examination_codes` (`ID`);

--
-- Omezení pro tabulku `medication_link`
--
ALTER TABLE `medication_link`
  ADD CONSTRAINT `medication_link_ibfk_1` FOREIGN KEY (`medicationID`) REFERENCES `medication` (`ID`),
  ADD CONSTRAINT `medication_link_ibfk_2` FOREIGN KEY (`patientID`) REFERENCES `patient` (`ID`),
  ADD CONSTRAINT `medication_link_ibfk_3` FOREIGN KEY (`examinationID`) REFERENCES `examination` (`ID`);

--
-- Omezení pro tabulku `patient`
--
ALTER TABLE `patient`
  ADD CONSTRAINT `patient_ibfk_1` FOREIGN KEY (`addressID`) REFERENCES `address` (`ID`),
  ADD CONSTRAINT `patient_ibfk_2` FOREIGN KEY (`insuranceID`) REFERENCES `insurance` (`ID`),
  ADD CONSTRAINT `patient_ibfk_3` FOREIGN KEY (`sexID`) REFERENCES `sex` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
