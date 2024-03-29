CREATE SCHEMA IF NOT EXISTS ObjectiveTest;

USE ObjectiveTest;

CREATE TABLE IF NOT EXISTS Mail (
    Address VARCHAR(255) NOT NULL,
    Id CHAR(36) PRIMARY KEY,
    ChatId BIGINT NOT NULL
);

CREATE TABLE IF NOT EXISTS Apartment (
    Id CHAR(36) PRIMARY KEY,
    CharId BIGINT NOT NULL,
    UrlApart VARCHAR(255) NOT NULL
);