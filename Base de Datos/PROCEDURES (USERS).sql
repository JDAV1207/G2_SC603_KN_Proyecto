USE DB_Orion_Fit;
DROP PROCEDURE IF EXISTS sp_ObtenerUsuariosConNombre;
DROP PROCEDURE IF EXISTS sp_AgregarUsuario;
DROP PROCEDURE IF EXISTS sp_EditarUsuario;
DROP PROCEDURE IF EXISTS sp_ObtenerUsuarioConNombre;
DROP PROCEDURE IF EXISTS sp_ActualizarContraseña;

DELIMITER $$

CREATE PROCEDURE sp_ObtenerUsuariosConNombre()
BEGIN
    SELECT u.username,
           u.rol,
           CASE 
               WHEN u.rol = 'ADMIN' or u.rol = 'RECEPTION' THEN a.nombre
               WHEN u.rol = 'USER' THEN c.nombre
               WHEN u.rol = 'TRAINER' THEN e.nombre
               ELSE ''
           END AS nombre,
           CASE 
               WHEN u.rol = 'ADMIN' or u.rol = 'RECEPTION' THEN a.telefono
               WHEN u.rol = 'USER' THEN c.telefono
               WHEN u.rol = 'TRAINER' THEN e.telefono
               ELSE ''
           END AS telefono,
           CASE 
               WHEN u.rol = 'ADMIN' or u.rol = 'RECEPTION' THEN a.correo
               WHEN u.rol = 'USER' THEN c.correo
               WHEN u.rol = 'TRAINER' THEN e.correo
               ELSE ''
           END AS correo
    FROM Usuario u
    LEFT JOIN Administrador a ON u.id_usuario = a.id_usuario
    LEFT JOIN Cliente c ON u.id_usuario = c.id_usuario
    LEFT JOIN Entrenador e ON u.id_usuario = e.id_usuario;
END $$

DELIMITER ;

DELIMITER $$
CREATE PROCEDURE sp_AgregarUsuario(
    IN pNombre VARCHAR(100),
    IN pTelefono VARCHAR(20),
    IN pCorreo VARCHAR(100),
    IN pRol VARCHAR(100),
    IN pUsername VARCHAR(100)
)
BEGIN
	IF EXISTS (SELECT 1 FROM Usuario WHERE username = pUsername) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Revisa el User, ya que se encuentra duplicado';
    ELSE
		INSERT INTO Usuario (username, contrasena, rol)
		VALUES (pUsername, SHA2(pUsername, 256), pRol);
		SET @nuevoUsuarioId = LAST_INSERT_ID();
        
        IF pRol = 'ADMIN' or pRol = 'RECEPTION' THEN
			INSERT INTO Administrador (id_usuario, nombre, telefono, correo)
			VALUES (@nuevoUsuarioId, pNombre, pTelefono, pCorreo);
        
        else
			INSERT INTO Entrenador (id_usuario, nombre, telefono, correo)
			VALUES (@nuevoUsuarioId, pNombre, pTelefono, pCorreo);
        
        END IF;
	END IF;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE sp_EditarUsuario(
    IN pNombre VARCHAR(100),
    IN pTelefono VARCHAR(20),
    IN pCorreo VARCHAR(100),
    IN pRol VARCHAR(100),
    IN pUsername VARCHAR(100)
)
BEGIN
    DECLARE nuevoUsuarioId INT;

    -- Verificar si existe el usuario
    SELECT id_usuario INTO nuevoUsuarioId
    FROM Usuario
    WHERE username = pUsername;

    IF nuevoUsuarioId IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Revisa el User, ya que no se encuentra';
    ELSE
        UPDATE Usuario
        SET rol = pRol
        WHERE id_usuario = nuevoUsuarioId;

        CASE 
        WHEN pRol = 'ADMIN' OR pRol = 'RECEPTION' THEN
            IF EXISTS (SELECT 1 FROM Administrador WHERE id_usuario = nuevoUsuarioId) THEN
                UPDATE Administrador
                SET nombre = pNombre, telefono = pTelefono, correo = pCorreo
                WHERE id_usuario = nuevoUsuarioId;
            ELSE
                INSERT INTO Administrador (id_usuario, nombre, telefono, correo)
                VALUES (nuevoUsuarioId, pNombre, pTelefono, pCorreo);
            END IF;
		WHEN pRol = 'USER' THEN
            IF EXISTS (SELECT 1 FROM cliente WHERE id_usuario = nuevoUsuarioId) THEN
                UPDATE cliente
                SET nombre = pNombre, telefono = pTelefono, correo = pCorreo
                WHERE id_usuario = nuevoUsuarioId;
            ELSE
                INSERT INTO cliente (id_usuario, nombre, telefono, correo)
                VALUES (nuevoUsuarioId, pNombre, pTelefono, pCorreo);
            END IF;
        WHEN pRol = 'TRAINER' tHEN
            IF EXISTS (SELECT 1 FROM Entrenador WHERE id_usuario = nuevoUsuarioId) THEN
                UPDATE Entrenador
                SET nombre = pNombre, telefono = pTelefono, correo = pCorreo
                WHERE id_usuario = nuevoUsuarioId;
            ELSE
                INSERT INTO Entrenador (id_usuario, nombre, telefono, correo)
                VALUES (nuevoUsuarioId, pNombre, pTelefono, pCorreo);
            END IF;
		END CASE;
    END IF;
END $$

DELIMITER ;

DELIMITER $$

CREATE PROCEDURE sp_ObtenerUsuarioConNombre(IN pId INT)
BEGIN
    SELECT u.username,
           u.rol,
           CASE 
               WHEN u.rol = 'ADMIN' OR u.rol = 'RECEPTION' THEN a.nombre
               WHEN u.rol = 'USER' THEN c.nombre
               WHEN u.rol = 'TRAINER' THEN e.nombre
               ELSE ''
           END AS nombre,
           CASE 
               WHEN u.rol = 'ADMIN' OR u.rol = 'RECEPTION' THEN a.telefono
               WHEN u.rol = 'USER' THEN c.telefono
               WHEN u.rol = 'TRAINER' THEN e.telefono
               ELSE ''
           END AS telefono,
           CASE 
               WHEN u.rol = 'ADMIN' OR u.rol = 'RECEPTION' THEN a.correo
               WHEN u.rol = 'USER' THEN c.correo
               WHEN u.rol = 'TRAINER' THEN e.correo
               ELSE ''
           END AS correo
    FROM Usuario u
    LEFT JOIN Administrador a ON u.id_usuario = a.id_usuario
    LEFT JOIN Cliente c ON u.id_usuario = c.id_usuario
    LEFT JOIN Entrenador e ON u.id_usuario = e.id_usuario
    WHERE u.id_usuario = pId;
END$$

DELIMITER ;

DELIMITER $$

CREATE PROCEDURE sp_ActualizarContraseña(
    IN pIdUsuario INT,
    IN pPasswordActual VARCHAR(255),
    IN pPasswordNueva VARCHAR(255)
)
BEGIN
    DECLARE vPassword VARCHAR(255);

    -- Obtener la contraseña actual (hash) del usuario
    SELECT contrasena INTO vPassword
    FROM Usuario
    WHERE id_usuario = pIdUsuario;

    -- Validar si coincide el hash
    IF vPassword = SHA2(pPasswordActual,256) THEN
        UPDATE Usuario
        SET contrasena = SHA2(pPasswordNueva, 256)
        WHERE id_usuario = pIdUsuario;
    ELSE
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La contraseña actual no es correcta';
    END IF;
END$$

DELIMITER ;




