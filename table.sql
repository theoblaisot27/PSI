CREATE TABLE Clients(
   Identifiant_unique_client INT,
   Prenom VARCHAR(50) NOT NULL,
   Nom VARCHAR(50) NOT NULL,
   Rue VARCHAR(50) NOT NULL,
   Numero_de_rue VARCHAR(50) NOT NULL,
   Code_Postal BIGINT NOT NULL,
   Ville VARCHAR(50) NOT NULL,
   Tel BIGINT NOT NULL,
   Email VARCHAR(50) NOT NULL,
   Metro_le_plus_proche VARCHAR(100) NOT NULL,
   PRIMARY KEY(Identifiant_unique_client)
);

CREATE TABLE Cuisiniers(
   Identifiant_unique_cuisinier INT,
   Nom VARCHAR(50) NOT NULL,
   Prenom VARCHAR(50) NOT NULL,
   Rue VARCHAR(50) NOT NULL,
   Numéro_de_rue INT NOT NULL,
   Code_Postal BIGINT NOT NULL,
   Ville VARCHAR(50) NOT NULL,
   Tel BIGINT NOT NULL,
   Email VARCHAR(50) NOT NULL,
   Metro_le_plus_proche VARCHAR(50) NOT NULL,
   PRIMARY KEY(Identifiant_unique_cuisiner)
);

CREATE TABLE Commandes(
   Numero_Commande INT,
   Numero_client INT NOT NULL,
   Numero_cuisinier INT NOT NULL,
   Identifiant_unique INT NOT NULL,
   Identifiant_unique_1 INT NOT NULL,
   PRIMARY KEY(Numero_Commande),
   FOREIGN KEY(Identifiant_unique) REFERENCES Cuisiniers(Identifiant_unique),
   FOREIGN KEY(Identifiant_unique_1) REFERENCES Clients(Identifiant_unique)
);

CREATE TABLE Plats(
   Nom_plat VARCHAR(50),
   Prix DOUBLE NOT NULL,
   Date_de_fabrication DATE NOT NULL,
   Date_de_peremption DATE NOT NULL,
   Type VARCHAR(50) NOT NULL,
   Regime VARCHAR(50),
   Nature VARCHAR(50) NOT NULL,
   PRIMARY KEY(Nom_plat)
);

CREATE TABLE Ingredients(
   Nom_ingredient VARCHAR(50),
   PRIMARY KEY(Nom_ingredient)
);

CREATE TABLE Commande_Plat(
   Numero_Commande INT,
   Nom_plat VARCHAR(50),
   Nombre INT NOT NULL,
   PRIMARY KEY(Numero_Commande, Nom_plat),
   FOREIGN KEY(Numero_Commande) REFERENCES Commandes(Numero_Commande),
   FOREIGN KEY(Nom_plat) REFERENCES Plats(Nom_plat)
);

CREATE TABLE Plat_Ingredient(
   Nom_plat VARCHAR(50),
   Nom_ingredient VARCHAR(50),
   Volume VARCHAR(50) NOT NULL,
   PRIMARY KEY(Nom_plat, Nom_ingredient),
   FOREIGN KEY(Nom_plat) REFERENCES Plats(Nom_plat),
   FOREIGN KEY(Nom_ingredient) REFERENCES Ingredients(Nom_ingredient)
);
INSERT INTO Clients (Identifiant_unique, Prenom, Nom, Rue, Numero_de_rue, Code_Postal, Ville, Tel, Email, Metro_le_plus_proche)
VALUES
(1, 'Medhy', 'Durand', 'Rue Cardinet', 15, 75017, 'Paris', 1234567890, 'Mdurand@gmail.com', 'Cardinet');
INSERT INTO Cuisiniers (Identifiant_unique, Prenom, Nom, Rue, Numéro_de_rue, Code_Postal, Ville, Tel, Email, Metro_le_plus_proche)
VALUES
(1, 'Marie', 'Dupond', 'Rue de la République', 30, 75011, 'Paris', 1234567890, 'Mdupond@gmail.com', 'République');
INSERT INTO Commandes (Numero_Commande, Numero_client, Numero_cuisinier, Identifiant_unique_client, Identifiant_unique_cuisinier)
VALUES
(1, 1, 1, 1, 1),
(2, 1, 1, 1, 1);
INSERT INTO Plats (Nom, Prix, Type, Regime, Nature)
VALUES
('Raclette', 10.0, 'Plat', NULL, 'Française'),
('Salade de fruit', 5.0, 'Dessert', 'Végétarien', 'Indifférent');
INSERT INTO Ingredients (Nom)
VALUES
('kiwi'),
('fraise'),
('jambon'),
('pommes_de_terre'),
('raclette fromage'),
('cornichon'),
('sucre');
INSERT INTO Commande_Plat (Numero_Commande, Nom_plat, Nombre)
VALUES
(1, 'Raclette', 6),
(2, 'Salade de fruit', 6);
INSERT INTO plat_ingredient (Nom_plat, Nom_ingredient, Volume)
VALUES
('Raclette', 'raclette fromage', '250g'),
('Raclette', 'pommes_de_terre', '200g'),
('Raclette', 'jambon', '200g'),
('Raclette', 'cornichon', '3p'),
('Salade de fruit', 'fraise', '100g'),
('Salade de fruit', 'kiwi', '100g'),
('Salade de fruit', 'sucre', '10g');
SELECT * FROM Clients;
SELECT Nom, Prenom FROM Cuisiniers;
SELECT Numero_Commande, Date_fabrication FROM Commandes;
SELECT Nom_plat, Prix FROM Plats;
SELECT Nom_ingredient FROM ingredients;
SELECT Numero_Commande, Nom_plat, Nombre FROM Commande_Plat;
SELECT Nom_plat, Nom_ingredient, Volume FROM Plat_Ingredient;