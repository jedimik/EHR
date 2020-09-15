-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Počítač: 127.0.0.1
-- Vytvořeno: Stř 16. zář 2020, 01:32
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
(19, 'Nová', 2635, 'Ústí nad Labem', 26501),
(20, 'V zahradach', 2736, 'Kutna Hora', 12365);

-- --------------------------------------------------------

--
-- Struktura tabulky `examination`
--

CREATE TABLE `examination` (
  `ID` int(11) NOT NULL,
  `weight` double NOT NULL,
  `height` double NOT NULL,
  `pressure_dis` int(8) NOT NULL,
  `pressure_sys` int(8) NOT NULL,
  `saturation` int(2) NOT NULL,
  `BPM` int(11) NOT NULL,
  `today_examination` mediumtext NOT NULL,
  `exam_code_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `examination`
--

INSERT INTO `examination` (`ID`, `weight`, `height`, `pressure_dis`, `pressure_sys`, `saturation`, `BPM`, `today_examination`, `exam_code_ID`) VALUES
(2, 88, 195, 95, 145, 98, 89, 'Dne:11.09.2020 17:37:56 Váha=85 Výška=195 BMI= 23,1426692965155\r\nDiastolický tlak= 95 Systolický tlak= 145 Saturace= 94\r\nIntervence:\r\nVyšetření:Dnes prisel pacient blabla je mu blabla aha.', 12),
(13, 86, 175, 94, 123, 92, 112, 'Dne:12.09.2020 0:00:00 Váha=86 Výška=175 BMI= 28,0816326530612\nDiastolický tlak= 94 Systolický tlak= 123 Saturace= 92BPM:112\nIntervence:Vyšetření:Dnes je mu hodne', 14),
(17, 82, 184, 67, 120, 95, 89, 'Dne:19.09.2020 0:00:00 Váha=82 Výška=184 BMI= 24,2202268431002\nDiastolický tlak= 67 Systolický tlak= 120 Saturace= 95\nIntervence: Odběr plodové vody Vyšetření: TEstovaci vysetreni nove', 14),
(18, 102, 187, 87, 112, 93, 99, 'Dne:18.09.2020 0:00:00 Váha=102 Výška=187 BMI= 29,1686922702965\nDiastolický tlak= 87 Systolický tlak= 112 Saturace= 93\nIntervence: EEG vyšetření Vyšetření: Testujeme hnusny datum, ktere stale blbne.', 11),
(19, 89, 176, 68, 130, 94, 59, 'Dne:19.09.2020 0:00:00 Váha=89 Výška=176 BMI= 28,7319214876033\nDiastolický tlak= 68 Systolický tlak= 130 Saturace= 94\nIntervence: EEG vyšetření Diagnoza: Asthma NS\nVyšetření: Neco', 11),
(20, 100, 180, 87, 130, 94, 95, 'Dne:25.09.2020 0:00:00 Váha=100 Výška=180 BMI= 30,8641975308642\nDiastolický tlak= 87 Systolický tlak= 130 Saturace= 94BPM:95\nIntervence:Odběr plodové vodyVyšetření: Dnes k nam na kliniku prisla tato dama, z vysledku z odberu plodove vody ma jeji plod trizomii 21. chromozomu.', 11),
(21, 87, 192, 45, 100, 24, 56, 'Dne:07.09.2020 0:00:00 Váha=87 Výška=192 BMI= 23,6002604166667\nDiastolický tlak= 45 Systolický tlak= 100 Saturace= 24BPM:56\nIntervence:EEG vyšetřeníVyšetření:Test \n', 11),
(22, 87, 192, 45, 100, 24, 56, 'Dne:07.09.2020 0:00:00 Váha=87 Výška=192 BMI= 23,6002604166667\nDiastolický tlak= 45 Systolický tlak= 100 Saturace= 24\nIntervence: EEG vyšetření Diagnoza: Infekce Salmonelami NS\nVyšetření: Test test i leku\nPacientovi jsou předepsané následující léčiva: LORATADIN RATIOPHARM, IBUPROFEN , \n', 13),
(23, 101, 195, 87, 110, 95, 65, 'Dne:01.09.2020 0:00:00 Váha=101 Výška=195 BMI= 26,5614727153189\nDiastolický tlak= 87 Systolický tlak= 110 Saturace= 95\nIntervence: EEG vyšetření Diagnoza: Chřipka způsobená identifikovaným sezónním chřipkovým virem\nVyšetření: Finalni vysetreniPacientovi jsou předepsané následující léčiva: STOPEX NA SUCHÝ KAŠEL, CODEIN SLOVAKOFARMA, ', 12);

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
-- Struktura tabulky `intervention`
--

CREATE TABLE `intervention` (
  `ID` int(11) NOT NULL,
  `name` varchar(64) NOT NULL,
  `snomed_code` varchar(16) NOT NULL,
  `loinc_code` varchar(16) NOT NULL,
  `mkn10_code` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `intervention`
--

INSERT INTO `intervention` (`ID`, `name`, `snomed_code`, `loinc_code`, `mkn10_code`) VALUES
(1, 'Odběr vzorku krve', '119297000', '66746-9', 'Y84.7'),
(2, 'EEG vyšetření', '54550000', 'LP6239-0', 'R94'),
(3, 'Odběr plodové vody', '119373006', '29253-2', 'O41.9');

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
  `medicationID` int(11) NOT NULL,
  `visitID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `medication_link`
--

INSERT INTO `medication_link` (`medicationID`, `visitID`) VALUES
(3, 1),
(3, 2),
(2, 2),
(1, 2),
(4, 2),
(4, 1),
(1, 1),
(1, 6),
(2, 6),
(4, 6),
(3, 7),
(4, 7),
(5, 9),
(1, 9);

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
  `insuranceID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `patient`
--

INSERT INTO `patient` (`ID`, `name`, `surname`, `sexID`, `addressID`, `tel_number`, `insuranceID`) VALUES
(22, 'Antonín', 'Němeček', 1, 12, 65326546, 1),
(26, 'Karel', 'Novák', 1, 10, 72564126, 1),
(31, 'Helena', 'Novakova', 2, 20, 456456987, 1);

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

-- --------------------------------------------------------

--
-- Struktura tabulky `visit`
--

CREATE TABLE `visit` (
  `ID` int(11) NOT NULL,
  `examinationID` int(11) NOT NULL,
  `interventionID` int(11) NOT NULL,
  `patientID` int(11) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `visit`
--

INSERT INTO `visit` (`ID`, `examinationID`, `interventionID`, `patientID`, `date`) VALUES
(1, 13, 1, 26, '2020-09-12'),
(2, 2, 1, 22, '2020-09-11'),
(4, 18, 2, 26, '2020-09-18'),
(5, 19, 2, 22, '2020-09-19'),
(6, 20, 3, 31, '2020-09-25'),
(7, 21, 2, 31, '2020-09-07'),
(8, 22, 2, 31, '2020-09-07'),
(9, 23, 2, 26, '2020-09-01');

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
-- Klíče pro tabulku `intervention`
--
ALTER TABLE `intervention`
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
  ADD KEY `medicationID` (`medicationID`),
  ADD KEY `examinationID` (`visitID`);

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
-- Klíče pro tabulku `visit`
--
ALTER TABLE `visit`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `examinationID` (`examinationID`),
  ADD KEY `interventionID` (`interventionID`),
  ADD KEY `patientID` (`patientID`);

--
-- AUTO_INCREMENT pro tabulky
--

--
-- AUTO_INCREMENT pro tabulku `address`
--
ALTER TABLE `address`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT pro tabulku `examination`
--
ALTER TABLE `examination`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

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
-- AUTO_INCREMENT pro tabulku `intervention`
--
ALTER TABLE `intervention`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pro tabulku `medication`
--
ALTER TABLE `medication`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT pro tabulku `patient`
--
ALTER TABLE `patient`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

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
-- AUTO_INCREMENT pro tabulku `visit`
--
ALTER TABLE `visit`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Omezení pro exportované tabulky
--

--
-- Omezení pro tabulku `examination`
--
ALTER TABLE `examination`
  ADD CONSTRAINT `examination_ibfk_2` FOREIGN KEY (`exam_code_ID`) REFERENCES `examination_codes` (`ID`);

--
-- Omezení pro tabulku `medication_link`
--
ALTER TABLE `medication_link`
  ADD CONSTRAINT `medication_link_ibfk_1` FOREIGN KEY (`medicationID`) REFERENCES `medication` (`ID`),
  ADD CONSTRAINT `medication_link_ibfk_3` FOREIGN KEY (`visitID`) REFERENCES `visit` (`ID`);

--
-- Omezení pro tabulku `patient`
--
ALTER TABLE `patient`
  ADD CONSTRAINT `patient_ibfk_1` FOREIGN KEY (`addressID`) REFERENCES `address` (`ID`),
  ADD CONSTRAINT `patient_ibfk_2` FOREIGN KEY (`insuranceID`) REFERENCES `insurance` (`ID`),
  ADD CONSTRAINT `patient_ibfk_3` FOREIGN KEY (`sexID`) REFERENCES `sex` (`ID`);

--
-- Omezení pro tabulku `visit`
--
ALTER TABLE `visit`
  ADD CONSTRAINT `visit_ibfk_1` FOREIGN KEY (`examinationID`) REFERENCES `examination` (`ID`),
  ADD CONSTRAINT `visit_ibfk_2` FOREIGN KEY (`interventionID`) REFERENCES `intervention` (`ID`),
  ADD CONSTRAINT `visit_ibfk_4` FOREIGN KEY (`patientID`) REFERENCES `patient` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
