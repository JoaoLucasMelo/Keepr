CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name TEXT NOT NULL COMMENT 'Keep Name',
  description TEXT NOT NULL COMMENT 'Keep Description',
  img TEXT NOT NULL,
  views INT NOT NULL DEFAULT 0,
  shares INT NOT NULL DEFAULT 0,
  keeps INT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name TEXT NOT NULL COMMENT 'Vault Name',
  description TEXT NOT NULL COMMENT 'Vault Description',
  isPrivate TINYINT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO keeps
(name, description, img, creatorId)
VALUES
("new keep", "description new keep", "aushuashsua", "619afdda45834453d3dac4f8");



      SELECT
      k.*,
      vk.id AS VaultKeepId,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      JOIN vaultkeeps vk ON vk.vaultId = 166
      WHERE vk.vaultId = 166;


      UPDATE keeps
      SET keeps = keeps+ 1
      WHERE
      id = 217;

      UPDATE keeps
      SET keeps = keeps-1
      WHERE
      id = 182;

ALTER TABLE
vaults
ADD COLUMN imgUrl VARCHAR(5000) DEFAULT "https://images.unsplash.com/photo-1517423440428-a5a00ad493e8?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=449&q=80";


      SELECT
      k.*,
      k.id AS keepId,
      vk.id AS VaultKeepId,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      JOIN vaultkeeps vk ON vk.vaultId = 9 AND vk.keepId = k.id
      WHERE vk.vaultId = 9
      ;


      SELECT
      k.*,
      a.*
      FROM
      keeps k
      JOIN accounts a ON k.creatorId = a.id AND k.name LIKE "%name%"
      ;

      DELETE FROM vaults WHERE name LIKE "%test%";