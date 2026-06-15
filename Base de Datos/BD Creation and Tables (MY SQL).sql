/* =========================================================
   CREACIÓN DE BASE DE DATOS
========================================================= */
DROP DATABASE IF EXISTS DB_Orion_Fit;
CREATE DATABASE DB_Orion_Fit;
USE DB_Orion_Fit;

/* =========================================================
   TABLA USUARIO
========================================================= */
CREATE TABLE Usuario (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    rol VARCHAR(30) NOT NULL
);

/* =========================================================
   TABLA ADMINISTRADOR
========================================================= */
CREATE TABLE Administrador (
    id_administrador INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(100),
    CONSTRAINT FK_Administrador_Usuario FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);

/* =========================================================
   TABLA MEMBRESIA
========================================================= */
CREATE TABLE Membresia (
    id_membresia INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    duracion_dias INT NOT NULL
);

/* =========================================================
   TABLA CLIENTE
========================================================= */
CREATE TABLE Cliente (
    id_cliente INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(100) UNIQUE,
    fecha_nacimiento DATE,
    estado VARCHAR(20) NOT NULL,
    CONSTRAINT FK_Cliente_Usuario FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);
/* =========================================================
   TABLA CLIENTE_MEMBRESIA
========================================================= */
CREATE TABLE Cliente_Membresia (
    id_cliente_membresia INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_membresia INT NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    estado VARCHAR(20) NOT NULL,
    CONSTRAINT FK_ClienteMembresia_Cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT FK_ClienteMembresia_Membresia FOREIGN KEY (id_membresia) REFERENCES Membresia(id_membresia)
);

/* =========================================================
   TABLA PAGO
========================================================= */
CREATE TABLE Pago (
    id_pago INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente_membresia INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fecha_pago DATE NOT NULL,
    metodo_pago VARCHAR(50),
    descripcion VARCHAR(255),
    CONSTRAINT FK_Pago_ClienteMembresia FOREIGN KEY (id_cliente_membresia) REFERENCES Cliente_Membresia(id_cliente_membresia)
);

/* =========================================================
   TABLA ENTRENADOR
========================================================= */
CREATE TABLE Entrenador (
    id_entrenador INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(100),
    CONSTRAINT FK_Entrenador_Usuario FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);

/* =========================================================
   TABLA CLASE
========================================================= */
CREATE TABLE Clase (
    id_clase INT AUTO_INCREMENT PRIMARY KEY,
    id_entrenador INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    horario DATETIME NOT NULL,
    cupo INT NOT NULL,
    CONSTRAINT FK_Clase_Entrenador FOREIGN KEY (id_entrenador) REFERENCES Entrenador(id_entrenador)
);

/* =========================================================
   TABLA RESERVA
========================================================= */
CREATE TABLE Reserva (
    id_reserva INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_clase INT NOT NULL,
    fecha_reserva DATE NOT NULL,
    estado VARCHAR(20) NOT NULL,
    CONSTRAINT FK_Reserva_Cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT FK_Reserva_Clase FOREIGN KEY (id_clase) REFERENCES Clase(id_clase)
);

/* =========================================================
   TABLA RUTINA
========================================================= */
CREATE TABLE Rutina (
    id_rutina INT AUTO_INCREMENT PRIMARY KEY,
    id_entrenador INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    objetivo VARCHAR(255),
    CONSTRAINT FK_Rutina_Entrenador FOREIGN KEY (id_entrenador) REFERENCES Entrenador(id_entrenador)
);

/* =========================================================
   TABLA EQUIPO
========================================================= */
CREATE TABLE Equipo (
    id_equipo INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    estado VARCHAR(30) NOT NULL,
    fecha_compra DATE,
    costo DECIMAL(10,2)
);

/* =========================================================
   TABLA EJERCICIO
========================================================= */
CREATE TABLE Ejercicio (
    id_ejercicio INT AUTO_INCREMENT PRIMARY KEY,
    id_equipo INT,
    nombre VARCHAR(100) NOT NULL,
    grupo_muscular VARCHAR(100),
    descripcion VARCHAR(255),
    CONSTRAINT FK_Equipo_Ejercicio FOREIGN KEY (id_equipo) REFERENCES Equipo(id_equipo)
);

/* =========================================================
   TABLA RUTINA_EJERCICIO
========================================================= */
CREATE TABLE Rutina_Ejercicio (
    id_rutina_ejercicio INT AUTO_INCREMENT PRIMARY KEY,
    id_rutina INT NOT NULL,
    id_reserva INT NOT NULL,
    id_ejercicio INT NOT NULL,
    series INT NOT NULL,
    repeticiones INT NOT NULL,
    descanso INT,
    CONSTRAINT FK_RutinaEjercicio_Rutina FOREIGN KEY (id_rutina) REFERENCES Rutina(id_rutina),
    CONSTRAINT FK_RutinaEjercicio_Ejercicio FOREIGN KEY (id_ejercicio) REFERENCES Ejercicio(id_ejercicio),
    CONSTRAINT FK_Reserva_Ejercicio FOREIGN KEY (id_reserva) REFERENCES Reserva(id_reserva)
);

/* =========================================================
   TABLA CLIENTE_RUTINA
========================================================= */
CREATE TABLE Cliente_Rutina (
    id_cliente_rutina INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_rutina INT NOT NULL,
    fecha_asignacion DATE NOT NULL,
    CONSTRAINT FK_ClienteRutina_Cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT FK_ClienteRutina_Rutina FOREIGN KEY (id_rutina) REFERENCES Rutina(id_rutina)
);

/* =========================================================
   TABLA ASISTENCIA
========================================================= */
CREATE TABLE Asistencia (
    id_asistencia INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    fecha DATE NOT NULL,
    hora_entrada TIME NOT NULL,
    hora_salida TIME,
    CONSTRAINT FK_Asistencia_Cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente)
);

/* =========================================================
   TABLA MANTENIMIENTO
========================================================= */
CREATE TABLE Mantenimiento (
    id_mantenimiento INT AUTO_INCREMENT PRIMARY KEY,
    id_equipo INT NOT NULL,
    fecha DATE NOT NULL,
    descripcion VARCHAR(255),
    costo DECIMAL(10,2),
    estado VARCHAR(30),
    CONSTRAINT FK_Mantenimiento_Equipo FOREIGN KEY (id_equipo) REFERENCES Equipo(id_equipo)
);

/* =========================================================
   TABLA INVENTARIO
========================================================= */
CREATE TABLE Inventario (
    id_producto INT AUTO_INCREMENT PRIMARY KEY,
    nombre_producto VARCHAR(100) NOT NULL,
    categoria VARCHAR(50),
    descripcion VARCHAR(255),
    cantidad INT NOT NULL,
    stock_minimo INT NOT NULL,
    costo DECIMAL(10,2),
    fecha_ingreso DATE NOT NULL
);

/* =========================================================
   TABLA VENTA
========================================================= */
CREATE TABLE Venta (
    id_venta INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    fecha DATE NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente)
);

/* =========================================================
   TABLA DETALLE_VENTA
========================================================= */
CREATE TABLE Detalle_Venta (
    id_detalle_venta INT AUTO_INCREMENT PRIMARY KEY,
    id_venta INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleVenta_Venta FOREIGN KEY (id_venta) REFERENCES Venta(id_venta),
    CONSTRAINT FK_DetalleVenta_Inventario FOREIGN KEY (id_producto) REFERENCES Inventario(id_producto)
);

