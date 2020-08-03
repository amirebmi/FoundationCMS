CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Donors` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Gender` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Street` longtext CHARACTER SET utf8mb4 NULL,
    `City` longtext CHARACTER SET utf8mb4 NULL,
    `Zip` longtext CHARACTER SET utf8mb4 NULL,
    `CellPhone` longtext CHARACTER SET utf8mb4 NOT NULL,
    `HomePhone` longtext CHARACTER SET utf8mb4 NULL,
    `Email1` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Email2` longtext CHARACTER SET utf8mb4 NULL,
    `IsActive` tinyint(1) NOT NULL,
    `MemberSince` datetime(6) NOT NULL,
    CONSTRAINT `PK_Donors` PRIMARY KEY (`Id`)
);

CREATE TABLE `Events` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EventName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreationDate` datetime(6) NOT NULL,
    `StartsAt` datetime(6) NOT NULL,
    `EndsAt` datetime(6) NOT NULL,
    `Location` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Events` PRIMARY KEY (`Id`)
);

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserName` varchar(15) CHARACTER SET utf8mb4 NOT NULL,
    `Password` varchar(18) CHARACTER SET utf8mb4 NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Gender` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Street` longtext CHARACTER SET utf8mb4 NOT NULL,
    `City` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Zip` char(5) NULL,
    `CellPhone` longtext CHARACTER SET utf8mb4 NOT NULL,
    `HomePhone` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    `IsAdmin` tinyint(1) NOT NULL,
    `UserNotes` varchar(1000) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
);

CREATE TABLE `Contributions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ContributionDate` datetime(6) NOT NULL,
    `Amount` decimal(65,30) NOT NULL,
    `PaymentMethod` longtext CHARACTER SET utf8mb4 NULL,
    `DonorId` int NOT NULL,
    CONSTRAINT `PK_Contributions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Contributions_Donors_DonorId` FOREIGN KEY (`DonorId`) REFERENCES `Donors` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `EventDonors` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `InvitationDate` datetime(6) NOT NULL,
    `EventDate` datetime(6) NOT NULL,
    `IsPresent` tinyint(1) NOT NULL,
    `EventId` int NOT NULL,
    `DonorId` int NOT NULL,
    CONSTRAINT `PK_EventDonors` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_EventDonors_Donors_DonorId` FOREIGN KEY (`DonorId`) REFERENCES `Donors` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EventDonors_Events_EventId` FOREIGN KEY (`EventId`) REFERENCES `Events` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `EventManager` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `EventId` int NOT NULL,
    CONSTRAINT `PK_EventManager` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_EventManager_Events_EventId` FOREIGN KEY (`EventId`) REFERENCES `Events` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EventManager_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Contributions_DonorId` ON `Contributions` (`DonorId`);

CREATE INDEX `IX_EventDonors_DonorId` ON `EventDonors` (`DonorId`);

CREATE INDEX `IX_EventDonors_EventId` ON `EventDonors` (`EventId`);

CREATE INDEX `IX_EventManager_EventId` ON `EventManager` (`EventId`);

CREATE INDEX `IX_EventManager_UserId` ON `EventManager` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200727202605_InitialSchema', '3.1.6');

