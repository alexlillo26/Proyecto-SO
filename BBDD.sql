  mysql -u root -p

mysql

DROP DATABASE IF EXISTS juego;
CREATE DATABASE juego;

CREATE TABLE jugador (
	ID INTEGER,
	usuario TEXT,
	contrasena TEXT,
	nombre TEXT,
	edad INTEGER,
	puntos INTEGER,
	PRIMARY KEY (ID)
)ENGINE=InnoDB;

CREATE TABLE partidas (
	ID INTEGER,
	usuario TEXT,
	puntosganador INTEGER,
	superficie INTEGER,
	duracion INTEGER,
	fecha INTEGER,
	horafinalizacion INTEGER,
	ganador TEXT,
	PRIMARY KEY (ID)
)ENGINE=InnoDB;

CREATE TABLE campeonato (
	ID_J INTEGER,
	ID_P INTEGER,
	PUNTOS INTEGER,
	FOREIGN KEY (ID_J) REFERENCES jugador(ID),
	FOREIGN KEY (ID_P) REFERENCES partidas(ID)

)ENGINE=InnoDB;

INSERT INTO jugador (ID, usuario, contrasena, nombre, edad, puntos) VALUES ('1', 'luis44','contrasenya','Luis','48','233');
INSERT INTO jugador (ID, usuario, contrasena, nombre, edad, puntos) VALUES ('2', 'Roberto245','123456789','Roberto','18','187');
INSERT INTO jugador (ID, usuario, contrasena, nombre, edad, puntos) VALUES ('3', 'pikachu33','12345','Sofia','46','453');
INSERT INTO jugador (ID, usuario, contrasena, nombre, edad, puntos) VALUES ('4', 'sistemasoperativos','password','Luis','34','56');

INSERT INTO partidas (ID, usuario, puntosganador, superficie, duracion, fecha, horafinalizacion, ganador) VALUES ('1', 'luis44, pikachu33', '233', '300', '4', DATE('2023-12-03'),TIME('12:54'), 'luis44');
INSERT INTO partidas (ID, usuario, puntosganador, superficie, duracion, fecha, horafinalizacion, ganador) VALUES ('2','luis44, Roberto245','256','200','5',DATE('2023-5-12'),TIME('6:54'),'luis44');
INSERT INTO partidas (ID, usuario, puntosganador, superficie, duracion, fecha, horafinalizacion, ganador) VALUES ('3','luis44, sistemasoperativos','356','500','7',DATE('2023-7-7'),TIME('16:56'),'luis44');

INSERT INTO campeonato VALUES ('1','3','233');
INSERT INTO campeonato VALUES ('2','2','488');
INSERT INTO campeonato VALUES ('3','1','367');

/*Cuantas personas menores de 18 años tienen más de 40 puntos */

SELECT jugador.usuario FROM (jugador, partidas) WHERE jugador.edad < '19' AND partidas.puntosganador > '40';
SELECT jugador.puntos FROM (jugador, partidas) WHERE jugador.usuario = 'Luis44' AND partidas.ganador = 'Luis' AND partidas.duracion < 5;






	
