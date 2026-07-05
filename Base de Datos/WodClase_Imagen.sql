USE DB_Orion_Fit;

/* Imagen del tipo de entrenamiento */
ALTER TABLE Rutina ADD COLUMN imagen VARCHAR(255) NULL;

/* Vinculo opcional entre Clase y WOD */
ALTER TABLE Clase ADD COLUMN id_rutina INT NULL;

ALTER TABLE Clase
    ADD CONSTRAINT FK_Clase_Rutina FOREIGN KEY (id_rutina) REFERENCES Rutina(id_rutina);

DROP PROCEDURE IF EXISTS sp_ObtenerWODs;
DROP PROCEDURE IF EXISTS sp_AgregarWOD;

DELIMITER $$

CREATE PROCEDURE sp_ObtenerWODs()
BEGIN
    SELECT
        r.id_rutina      AS IdRutina,
        r.nombre         AS Nombre,
        r.objetivo       AS Objetivo,
        r.imagen         AS Imagen,
        e.id_entrenador  AS IdEntrenador,
        e.nombre         AS NombreEntrenador,
        re.id_rutina_ejercicio AS IdRutinaEjercicio,
        ej.nombre        AS NombreEjercicio,
        re.series        AS Series,
        re.repeticiones  AS Repeticiones,
        re.descanso      AS Descanso
    FROM Rutina r
    INNER JOIN Entrenador e  ON r.id_entrenador = e.id_entrenador
    LEFT JOIN Rutina_Ejercicio re ON r.id_rutina = re.id_rutina
    LEFT JOIN Ejercicio ej   ON re.id_ejercicio = ej.id_ejercicio
    ORDER BY r.id_rutina DESC;
END$$

CREATE PROCEDURE sp_AgregarWOD(
    IN pIdEntrenador INT,
    IN pNombre       VARCHAR(100),
    IN pObjetivo     VARCHAR(255),
    IN pImagen       VARCHAR(255),
    IN pEjercicios   LONGTEXT
)
BEGIN
    DECLARE nuevaRutinaId INT;
    DECLARE totalEjercicios INT;
    DECLARE indice INT DEFAULT 0;
    DECLARE pIdEjercicio INT;
    DECLARE pSeries INT;
    DECLARE pRepeticiones INT;
    DECLARE pDescanso INT;

    IF pNombre IS NULL OR TRIM(pNombre) = '' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El nombre del entrenamiento es obligatorio.';
    END IF;

    IF JSON_LENGTH(pEjercicios) = 0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Debe incluir al menos un ejercicio en el WOD.';
    END IF;

    INSERT INTO Rutina (id_entrenador, nombre, objetivo, imagen)
    VALUES (pIdEntrenador, pNombre, pObjetivo, pImagen);

    SET nuevaRutinaId = LAST_INSERT_ID();
    SET totalEjercicios = JSON_LENGTH(pEjercicios);

    WHILE indice < totalEjercicios DO
        SET pIdEjercicio  = JSON_UNQUOTE(JSON_EXTRACT(pEjercicios, CONCAT('$[', indice, '].IdEjercicio')));
        SET pSeries       = JSON_UNQUOTE(JSON_EXTRACT(pEjercicios, CONCAT('$[', indice, '].Series')));
        SET pRepeticiones = JSON_UNQUOTE(JSON_EXTRACT(pEjercicios, CONCAT('$[', indice, '].Repeticiones')));
        SET pDescanso     = JSON_UNQUOTE(JSON_EXTRACT(pEjercicios, CONCAT('$[', indice, '].Descanso')));

        INSERT INTO Rutina_Ejercicio (id_rutina, id_reserva, id_ejercicio, series, repeticiones, descanso)
        VALUES (nuevaRutinaId, 1, pIdEjercicio, pSeries, pRepeticiones, pDescanso);

        SET indice = indice + 1;
    END WHILE;
END$$

DELIMITER ;
