DELIMITER $$

CREATE PROCEDURE SP_EditarEquipo(
    IN p_id_equipo INT,
    IN p_nombre VARCHAR(100),
    IN p_estado VARCHAR(30),
    IN p_fecha_compra DATE,
    IN p_costo DECIMAL(10,2)
)
BEGIN
    UPDATE equipo
    SET
        nombre = p_nombre,
        estado = p_estado,
        fecha_compra = p_fecha_compra,
        costo = p_costo
    WHERE id_equipo = p_id_equipo;
END $$

DELIMITER ;

DELIMITER $$

CREATE PROCEDURE SP_AgregarEquipo(
    IN p_nombre VARCHAR(100),
    IN p_estado VARCHAR(30),
    IN p_fecha_compra DATE,
    IN p_costo DECIMAL(10,2)
)
BEGIN
    INSERT INTO equipo (
        nombre,
        estado,
        fecha_compra,
        costo
    )
    VALUES (
        p_nombre,
        p_estado,
        p_fecha_compra,
        p_costo
    );
END $$

DELIMITER ;

DELIMITER $$

CREATE PROCEDURE SP_EliminarEquipo(
    IN p_id_equipo INT
)
BEGIN
    DELETE FROM equipo
    WHERE id_equipo = p_id_equipo;
END $$

DELIMITER ;