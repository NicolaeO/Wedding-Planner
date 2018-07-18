-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema WeddingPlanner
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `WeddingPlanner` ;

-- -----------------------------------------------------
-- Schema WeddingPlanner
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `WeddingPlanner` DEFAULT CHARACTER SET utf8 ;
USE `WeddingPlanner` ;

-- -----------------------------------------------------
-- Table `WeddingPlanner`.`Users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeddingPlanner`.`Users` ;

CREATE TABLE IF NOT EXISTS `WeddingPlanner`.`Users` (
  `UserID` INT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(45) NULL,
  `LastName` VARCHAR(45) NULL,
  `Email` VARCHAR(100) NULL,
  `Password` TEXT NULL,
  `CreatedAt` DATETIME NULL,
  `UpdatedAt` DATETIME NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC),
  UNIQUE INDEX `Email_UNIQUE` (`Email` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeddingPlanner`.`Weddings`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeddingPlanner`.`Weddings` ;

CREATE TABLE IF NOT EXISTS `WeddingPlanner`.`Weddings` (
  `WeddingID` INT NOT NULL AUTO_INCREMENT,
  `Wedder1` VARCHAR(45) NULL,
  `Wedder2` VARCHAR(45) NULL,
  `WeddingDate` DATETIME NULL,
  `CreatedAt` DATETIME NULL,
  `UpdatedAt` DATETIME NULL,
  `Location` TEXT NULL,
  `UserID` INT NOT NULL,
  PRIMARY KEY (`WeddingID`),
  INDEX `fk_Weddings_Users1_idx` (`UserID` ASC),
  CONSTRAINT `fk_Weddings_Users1`
    FOREIGN KEY (`UserID`)
    REFERENCES `WeddingPlanner`.`Users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeddingPlanner`.`UserWedding`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeddingPlanner`.`UserWedding` ;

CREATE TABLE IF NOT EXISTS `WeddingPlanner`.`UserWedding` (
  `UserWeddingID` INT NOT NULL AUTO_INCREMENT,
  `UserID` INT NOT NULL,
  `WeddingID` INT NOT NULL,
  PRIMARY KEY (`UserWeddingID`),
  INDEX `fk_UserWedding_Users_idx` (`UserID` ASC),
  INDEX `fk_UserWedding_Weddings1_idx` (`WeddingID` ASC),
  CONSTRAINT `fk_UserWedding_Users`
    FOREIGN KEY (`UserID`)
    REFERENCES `WeddingPlanner`.`Users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_UserWedding_Weddings1`
    FOREIGN KEY (`WeddingID`)
    REFERENCES `WeddingPlanner`.`Weddings` (`WeddingID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
