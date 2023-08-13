CREATE DATABASE apicarrosdb;

USE apicarrosdb;

CREATE TABLE Categorias(
	CategoriaId int NOT NULL AUTO_INCREMENT,
    NomeCategoria VARCHAR(80),
    PRIMARY KEY(CarroId)
);

CREATE TABLE Carros(
	CarroId int NOT NULL AUTO_INCREMENT,
    Modelo VARCHAR(80) NOT NULL,
    Descricao VARCHAR(100),
	CategoriaId int NOT NULL,
    PRIMARY KEY(CarroId),
    FOREIGN KEY(CategoriaId) REFERENCES Categorias(CategoriaId)
);

INSERT INTO Categorias(NomeCategoria) VALUES ('Hatch');
INSERT INTO Categorias(NomeCategoria) VALUES ('Sedan');
INSERT INTO Categorias(NomeCategoria) VALUES ('SUV');

SELECT * FROM Categorias;


INSERT INTO Carros(Modelo,Descricao,CategoriaId) VALUES('HB20','Carro para uso urbano',1);
INSERT INTO Carros(Modelo,Descricao,CategoriaId) VALUES('Logan','Carro para quem deseja ter conforto',2);
INSERT INTO Carros(Modelo,Descricao,CategoriaId) VALUES('HRV','Carro para diversos usos',3);

SELECT * FROM Carros;


SELECT Modelo,NomeCategoria
FROM Carros AS CR
INNER JOIN Categorias as CT
ON CR.CategoriaId = CT.CategoriaID;