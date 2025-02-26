CREATE TABLE Clients(
   Identifiant_unique INT,
   Prenom VARCHAR(50) NOT NULL,
   Nom VARCHAR(50) NOT NULL,
   Rue VARCHAR(50) NOT NULL,
   Numero_de_rue VARCHAR(50) NOT NULL,
   Code_Postal BIGINT NOT NULL,
   Ville VARCHAR(50) NOT NULL,
   Tel BIGINT NOT NULL,
   Email VARCHAR(50) NOT NULL,
   Metro_le_plus_proche VARCHAR(100) NOT NULL,
   PRIMARY KEY(Identifiant_unique)
);

CREATE TABLE Cuisiniers(
   Identifiant_unique INT,
   Nom VARCHAR(50) NOT NULL,
   Prenom VARCHAR(50) NOT NULL,
   Rue VARCHAR(50) NOT NULL,
   Numéro_de_rue INT NOT NULL,
   Code_Postal BIGINT NOT NULL,
   Ville VARCHAR(50) NOT NULL,
   Tel BIGINT NOT NULL,
   Email VARCHAR(50) NOT NULL,
   Metro_le_plus_proche VARCHAR(50) NOT NULL,
   PRIMARY KEY(Identifiant_unique)
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
   Régime VARCHAR(50),
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
