USE DB_Orion_Fit;
DROP PROCEDURE IF EXISTS sp_ObtenerClientesResumen;
DROP PROCEDURE IF EXISTS sp_AgregarCliente;

DELIMITER $$
CREATE PROCEDURE sp_ObtenerClientesResumen()
BEGIN
    SELECT 
        c.*,
        cm.estado AS EstadoMembresia,
        cm.fecha_fin AS Vencimiento
    FROM Cliente c
    LEFT JOIN Cliente_Membresia cm ON c.id_cliente = cm.id_cliente
    LEFT JOIN Membresia m ON cm.id_membresia = m.id_membresia;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE sp_AgregarCliente(
    IN pNombre VARCHAR(100),
    IN pCedula VARCHAR(20),
    IN pTelefono VARCHAR(20),
    IN pCorreo VARCHAR(100),
    IN pFechaNacimiento DATE,
    IN pEstado VARCHAR(20)
)
BEGIN
	IF EXISTS (SELECT 1 FROM Usuario WHERE username = pCedula) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Revisa el Cliente, ya que se encuentra duplicado';
    ELSE
		INSERT INTO Usuario (username, contrasena, rol)
		VALUES (pCedula, SHA2(pCedula, 256), 'USER');
		SET @nuevoUsuarioId = LAST_INSERT_ID();
		INSERT INTO cliente (id_usuario, nombre, cedula, telefono, correo, fecha_nacimiento, estado)
		VALUES (@nuevoUsuarioId, pNombre, pCedula, pTelefono, pCorreo, pFechaNacimiento, pEstado);
	END IF;
END $$
DELIMITER ;

