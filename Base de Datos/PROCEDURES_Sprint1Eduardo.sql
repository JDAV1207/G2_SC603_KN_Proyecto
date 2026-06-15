USE DB_Orion_Fit;

-- STORED PROCEDURES: CLIENTES (Editar y Eliminar)


DROP PROCEDURE IF EXISTS sp_EditarCliente;
DROP PROCEDURE IF EXISTS sp_EliminarCliente;
DROP PROCEDURE IF EXISTS sp_ObtenerWODs;
DROP PROCEDURE IF EXISTS sp_AgregarWOD;
DROP PROCEDURE IF EXISTS sp_ObtenerEjercicios;

DELIMITER $$

-- Editar datos personales de un cliente
-- Historia: Modificar datos del cliente

CREATE PROCEDURE sp_EditarCliente(
    IN pIdCliente     INT,
    IN pNombre        VARCHAR(100),
    IN pCedula        VARCHAR(20),
    IN pTelefono      VARCHAR(20),
    IN pCorreo        VARCHAR(100),
    IN pFechaNacimiento DATE,
    IN pEstado        VARCHAR(20)
)
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Cliente WHERE id_cliente = pIdCliente) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El cliente no existe en el sistema.';
    ELSE
        UPDATE Cliente
        SET nombre           = pNombre,
            cedula           = pCedula,
            telefono         = pTelefono,
            correo           = pCorreo,
            fecha_nacimiento = pFechaNacimiento,
            estado           = pEstado
        WHERE id_cliente = pIdCliente;
    END IF;
END$$


-- Eliminar cliente inactivo
-- Historia: Eliminar clientes inactivos

CREATE PROCEDURE sp_EliminarCliente(
    IN pIdCliente INT
)
BEGIN
    DECLARE clienteEstado VARCHAR(20);
    DECLARE clienteIdUsuario INT;

    SELECT estado, id_usuario
    INTO clienteEstado, clienteIdUsuario
    FROM Cliente
    WHERE id_cliente = pIdCliente;

    IF clienteEstado IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El cliente no existe en el sistema.';
    ELSEIF clienteEstado != 'Inactivo' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Solo se pueden eliminar clientes con estado Inactivo.';
    ELSE
        DELETE FROM Cliente_Membresia WHERE id_cliente = pIdCliente;
        DELETE FROM Cliente_Rutina    WHERE id_cliente = pIdCliente;
        DELETE FROM Asistencia        WHERE id_cliente = pIdCliente;
        DELETE FROM Cliente           WHERE id_cliente = pIdCliente;
        DELETE FROM Usuario           WHERE id_usuario = clienteIdUsuario;
    END IF;
END$$


-- Obtener todos los WODs con sus ejercicios
-- Historia: Publicar entrenamiento diario

CREATE PROCEDURE sp_ObtenerWODs()
BEGIN
    SELECT
        r.id_rutina      AS IdRutina,
        r.nombre         AS Nombre,
        r.objetivo       AS Objetivo,
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


-- Obtener lista de ejercicios disponibles
-- Para el dropdown del modal WOD

CREATE PROCEDURE sp_ObtenerEjercicios()
BEGIN
    SELECT
        id_ejercicio   AS IdEjercicio,
        nombre         AS Nombre,
        grupo_muscular AS GrupoMuscular
    FROM Ejercicio
    ORDER BY nombre ASC;
END$$


-- Agregar WOD (rutina + ejercicios)
-- Historia: Publicar entrenamiento diario

CREATE PROCEDURE sp_AgregarWOD(
    IN pIdEntrenador INT,
    IN pNombre       VARCHAR(100),
    IN pObjetivo     VARCHAR(255),
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

    INSERT INTO Rutina (id_entrenador, nombre, objetivo)
    VALUES (pIdEntrenador, pNombre, pObjetivo);

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


-- valides de datos
SELECT * FROM db_orion_fit.cliente;

-- Primero verificá si el SP existe
SHOW PROCEDURE STATUS WHERE Db = 'DB_Orion_Fit' AND Name = 'sp_ObtenerClientesResumen';

CALL sp_ObtenerClientesResumen();
INSERT INTO usuario (username, contrasena, rol) VALUES ('cliente1', SHA2('cliente1', 256), 'USER');
INSERT INTO cliente (id_usuario, nombre, cedula, telefono, correo, fecha_nacimiento, estado) VALUES (LAST_INSERT_ID(), 'Ana Rodríguez Mora', '112233445', '88881111', 'ana@correo.com', '1995-03-15', 'Activo');
INSERT INTO usuario (username, contrasena, rol) VALUES ('cliente2', SHA2('cliente2', 256), 'USER');
INSERT INTO cliente (id_usuario, nombre, cedula, telefono, correo, fecha_nacimiento, estado) VALUES (LAST_INSERT_ID(), 'Carlos Jiménez Vega', '998877665', '77772222', 'carlos@correo.com', '1990-07-20', 'Inactivo');
SELECT * FROM cliente;




USE DB_Orion_Fit;

INSERT INTO Ejercicio (nombre, grupo_muscular, descripcion) VALUES
('Burpees', 'Cuerpo completo', 'Ejercicio de alta intensidad'),
('Air Squat', 'Piernas', 'Sentadilla sin peso');

SELECT id_ejercicio, nombre, grupo_muscular FROM Ejercicio;


-- Ejecutar en orden ya que me da este error (Error al publicar el WOD: Cannot add or update a child row: a foreign key constraint fails (`db_orion_fit`.`rutina_ejercicio`, CONSTRAINT `FK_Reserva_Ejercicio` FOREIGN KEY (`id_reserva`) REFERENCES `reserva` (`id_reserva`)))
USE DB_Orion_Fit;

-- Primero hacemos la columna id_reserva opcional en la tabla
ALTER TABLE Rutina_Ejercicio 
    DROP FOREIGN KEY FK_Reserva_Ejercicio;

ALTER TABLE Rutina_Ejercicio 
    MODIFY COLUMN id_reserva INT NULL;

ALTER TABLE Rutina_Ejercicio
    ADD CONSTRAINT FK_Reserva_Ejercicio 
    FOREIGN KEY (id_reserva) REFERENCES Reserva(id_reserva)
    ON DELETE SET NULL;

-- Recrear el SP sin el id_reserva hardcodeado
DROP PROCEDURE IF EXISTS sp_AgregarWOD;

DELIMITER $$
CREATE PROCEDURE sp_AgregarWOD(
    IN pIdEntrenador INT,
    IN pNombre       VARCHAR(100),
    IN pObjetivo     VARCHAR(255),
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

    INSERT INTO Rutina (id_entrenador, nombre, objetivo)
    VALUES (pIdEntrenador, pNombre, pObjetivo);

    SET nuevaRutinaId = LAST_INSERT_ID();
    SET totalEjercicios = JSON_LENGTH(pEjercicios);

    WHILE indice < totalEjercicios DO
        SET pIdEjercicio  = JSON_UNQUOTE(JSON_EXTRACT(pEjercicios, CONCAT('$[', indice, '].IdEjercicio')));
        SET pSeries       = JSON_UNQUOTE(JSON_EXTRACT(pEjercicios, CONCAT('$[', indice, '].Series')));
        SET pRepeticiones = JSON_UNQUOTE(JSON_EXTRACT(pEjercicios, CONCAT('$[', indice, '].Repeticiones')));
        SET pDescanso     = JSON_UNQUOTE(JSON_EXTRACT(pEjercicios, CONCAT('$[', indice, '].Descanso')));

        -- id_reserva va NULL porque un WOD no requiere reserva
        INSERT INTO Rutina_Ejercicio (id_rutina, id_reserva, id_ejercicio, series, repeticiones, descanso)
        VALUES (nuevaRutinaId, NULL, pIdEjercicio, pSeries, pRepeticiones, pDescanso);

        SET indice = indice + 1;
    END WHILE;
END$$
DELIMITER ;
