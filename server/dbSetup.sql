-- SQLBook: Code
-- Active: 1715613992165@@127.0.0.1@3306@happy_skeleton_c7a82b_db

-- SQLBook: Code
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture',
    coverImg varchar(1000) COMMENT 'User Cover Image'
) default charset utf8mb4 COMMENT '';

ALTER TABLE accounts
ADD COLUMN IF NOT EXISTS coverImg VARCHAR(1000) NULL COMMENT 'User Cover Image';
-- SECTION: KEEPS ------------------------------------------

CREATE TABLE keeps (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    views INT NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

DROP TABLE keeps;

-- SECTION: VAULTS ------------------------------------------
CREATE TABLE vaults (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    isPrivate BOOLEAN NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

DROP TABLE vaults;

-- SECTION: VAULTKEEPS ------------------------------------------
CREATE TABLE vaultKeeps (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    keepId INT NOT NULL,
    vaultId INT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE,
    FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

SELECT * FROM vaultKeeps;

DROP TABLE vaultKeeps;