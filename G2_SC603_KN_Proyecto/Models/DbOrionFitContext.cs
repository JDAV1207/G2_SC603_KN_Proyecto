using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace G2_SC603_KN_Proyecto.Models;

public partial class DbOrionFitContext : DbContext
{
    public DbOrionFitContext()
    {
    }

    public DbOrionFitContext(DbContextOptions<DbOrionFitContext> options)
        : base(options)
    {
    }
    public virtual DbSet<MembresiaProximaVencer> MembresiasProximasVencer { get; set; }
    public virtual DbSet<HistorialMembresia> HistorialMembresias { get; set; }
    public virtual DbSet<Administrador> Administradors { get; set; }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteMembresium> ClienteMembresia { get; set; }

    public virtual DbSet<ClienteRutina> ClienteRutinas { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Ejercicio> Ejercicios { get; set; }

    public virtual DbSet<Entrenador> Entrenadors { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<Membresium> Membresia { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Rutina> Rutinas { get; set; }

    public virtual DbSet<RutinaEjercicio> RutinaEjercicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    public DbSet<ClienteResumen> ClientesResumen { get; set; }
    public DbSet<ClienteMembresiaResumen> ClienteMembresiaResumen { get; set; }

    public DbSet<UsuarioNombre> UsuarioNombre { get; set; }
    public DbSet<Equipo> Equipo { get; set; }
    public DbSet<EjercicioResumen> EjerciciosResumen { get; set; }
    public DbSet<WodResumen> WodsResumen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //       => optionsBuilder.UseMySql("server=localhost;database=DB_Orion_Fit;user=root;password=12345;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.46-mysql"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HistorialMembresia>(entity =>
        {
            entity.HasKey(e => e.IdHistorial);

            entity.ToTable("historial_membresias");

            entity.Property(e => e.IdHistorial).HasColumnName("id_historial");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdMembresia).HasColumnName("id_membresia");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");

            entity.HasOne(d => d.Cliente)
                .WithMany()
                .HasForeignKey(d => d.IdCliente);
        });
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");
        modelBuilder.Entity<ClienteResumen>().HasNoKey();
        modelBuilder.Entity<ClienteMembresiaResumen>().HasNoKey();
        modelBuilder.Entity<UsuarioNombre>().HasNoKey();
        modelBuilder.Entity<EjercicioResumen>().HasNoKey();
        modelBuilder.Entity<WodResumen>().HasNoKey();
        modelBuilder.Entity<MembresiaProximaVencer>().HasNoKey();
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.IdAdministrador).HasName("PRIMARY");

            entity.ToTable("administrador");

            entity.HasIndex(e => e.IdUsuario, "FK_Administrador_Usuario");

            entity.Property(e => e.IdAdministrador).HasColumnName("id_administrador");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Administradors)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administrador_Usuario");
        });

        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PRIMARY");

            entity.ToTable("asistencia");

            entity.HasIndex(e => e.IdCliente, "FK_Asistencia_Cliente");

            entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraEntrada)
                .HasColumnType("time")
                .HasColumnName("hora_entrada");
            entity.Property(e => e.HoraSalida)
                .HasColumnType("time")
                .HasColumnName("hora_salida");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asistencia_Cliente");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClase).HasName("PRIMARY");

            entity.ToTable("clase");

            entity.HasIndex(e => e.IdEntrenador, "FK_Clase_Entrenador");

            entity.Property(e => e.IdClase).HasColumnName("id_clase");
            entity.Property(e => e.Cupo).HasColumnName("cupo");
            entity.Property(e => e.Horario)
                .HasColumnType("datetime")
                .HasColumnName("horario");
            entity.Property(e => e.IdEntrenador).HasColumnName("id_entrenador");
            entity.Property(e => e.IdRutina).HasColumnName("id_rutina");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdEntrenadorNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdEntrenador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clase_Entrenador");

            entity.HasOne(d => d.IdRutinaNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdRutina)
                .HasConstraintName("FK_Clase_Rutina");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.IdUsuario, "FK_Cliente_Usuario");

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Usuario");
        });

        modelBuilder.Entity<ClienteMembresium>(entity =>
        {
            entity.HasKey(e => e.IdClienteMembresia).HasName("PRIMARY");

            entity.ToTable("cliente_membresia");

            entity.HasIndex(e => e.IdCliente, "FK_ClienteMembresia_Cliente");

            entity.HasIndex(e => e.IdMembresia, "FK_ClienteMembresia_Membresia");

            entity.Property(e => e.IdClienteMembresia).HasColumnName("id_cliente_membresia");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdMembresia).HasColumnName("id_membresia");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClienteMembresia)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteMembresia_Cliente");

            entity.HasOne(d => d.IdMembresiaNavigation).WithMany(p => p.ClienteMembresia)
                .HasForeignKey(d => d.IdMembresia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteMembresia_Membresia");
        });

        modelBuilder.Entity<ClienteRutina>(entity =>
        {
            entity.HasKey(e => e.IdClienteRutina).HasName("PRIMARY");

            entity.ToTable("cliente_rutina");

            entity.HasIndex(e => e.IdCliente, "FK_ClienteRutina_Cliente");

            entity.HasIndex(e => e.IdRutina, "FK_ClienteRutina_Rutina");

            entity.Property(e => e.IdClienteRutina).HasColumnName("id_cliente_rutina");
            entity.Property(e => e.FechaAsignacion).HasColumnName("fecha_asignacion");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdRutina).HasColumnName("id_rutina");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClienteRutinas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteRutina_Cliente");

            entity.HasOne(d => d.IdRutinaNavigation).WithMany(p => p.ClienteRutinas)
                .HasForeignKey(d => d.IdRutina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteRutina_Rutina");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PRIMARY");

            entity.ToTable("detalle_venta");

            entity.HasIndex(e => e.IdProducto, "FK_DetalleVenta_Inventario");

            entity.HasIndex(e => e.IdVenta, "FK_DetalleVenta_Venta");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("id_detalle_venta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVenta_Inventario");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVenta_Venta");
        });

        modelBuilder.Entity<Ejercicio>(entity =>
        {
            entity.HasKey(e => e.IdEjercicio).HasName("PRIMARY");

            entity.ToTable("ejercicio");

            entity.HasIndex(e => e.IdEquipo, "FK_Equipo_Ejercicio");

            entity.Property(e => e.IdEjercicio).HasColumnName("id_ejercicio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.GrupoMuscular)
                .HasMaxLength(100)
                .HasColumnName("grupo_muscular");
            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Ejercicios)
                .HasForeignKey(d => d.IdEquipo)
                .HasConstraintName("FK_Equipo_Ejercicio");
        });

        modelBuilder.Entity<Entrenador>(entity =>
        {
            entity.HasKey(e => e.IdEntrenador).HasName("PRIMARY");

            entity.ToTable("entrenador");

            entity.HasIndex(e => e.IdUsuario, "FK_Entrenador_Usuario");

            entity.Property(e => e.IdEntrenador).HasColumnName("id_entrenador");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Entrenadors)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entrenador_Usuario");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo).HasName("PRIMARY");

            entity.ToTable("equipo");

            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");
            entity.Property(e => e.Costo)
                .HasPrecision(10, 2)
                .HasColumnName("costo");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCompra).HasColumnName("fecha_compra");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.ToTable("inventario");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .HasColumnName("categoria");
            entity.Property(e => e.Costo)
                .HasPrecision(10, 2)
                .HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .HasColumnName("nombre_producto");
            entity.Property(e => e.StockMinimo).HasColumnName("stock_minimo");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdMantenimiento).HasName("PRIMARY");

            entity.ToTable("mantenimiento");

            entity.HasIndex(e => e.IdEquipo, "FK_Mantenimiento_Equipo");

            entity.Property(e => e.IdMantenimiento).HasColumnName("id_mantenimiento");
            entity.Property(e => e.Tipo)
                .HasColumnType("enum('Preventivo','Correctivo','Calibracion','Limpieza')")
                .HasColumnName("tipo");
            entity.Property(e => e.Costo)
                .HasPrecision(10, 2)
                .HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mantenimiento_Equipo");
        });

        modelBuilder.Entity<Membresium>(entity =>
        {
            entity.HasKey(e => e.IdMembresia).HasName("PRIMARY");

            entity.ToTable("membresia");

            entity.Property(e => e.IdMembresia).HasColumnName("id_membresia");
            entity.Property(e => e.DuracionDias).HasColumnName("duracion_dias");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.ToTable("pago");

            entity.HasIndex(e => e.IdClienteMembresia, "FK_Pago_ClienteMembresia");

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdClienteMembresia).HasColumnName("id_cliente_membresia");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdClienteMembresiaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdClienteMembresia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_ClienteMembresia");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PRIMARY");

            entity.ToTable("reserva");

            entity.HasIndex(e => e.IdClase, "FK_Reserva_Clase");

            entity.HasIndex(e => e.IdCliente, "FK_Reserva_Cliente");

            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaReserva).HasColumnName("fecha_reserva");
            entity.Property(e => e.IdClase).HasColumnName("id_clase");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdClase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Clase");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Cliente");
        });

        modelBuilder.Entity<Rutina>(entity =>
        {
            entity.HasKey(e => e.IdRutina).HasName("PRIMARY");

            entity.ToTable("rutina");

            entity.HasIndex(e => e.IdEntrenador, "FK_Rutina_Entrenador");

            entity.Property(e => e.IdRutina).HasColumnName("id_rutina");
            entity.Property(e => e.IdEntrenador).HasColumnName("id_entrenador");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Objetivo)
                .HasMaxLength(255)
                .HasColumnName("objetivo");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .HasColumnName("imagen");

            entity.HasOne(d => d.IdEntrenadorNavigation).WithMany(p => p.Rutinas)
                .HasForeignKey(d => d.IdEntrenador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rutina_Entrenador");
        });

        modelBuilder.Entity<RutinaEjercicio>(entity =>
        {
            entity.HasKey(e => e.IdRutinaEjercicio).HasName("PRIMARY");

            entity.ToTable("rutina_ejercicio");

            entity.HasIndex(e => e.IdReserva, "FK_Reserva_Ejercicio");

            entity.HasIndex(e => e.IdEjercicio, "FK_RutinaEjercicio_Ejercicio");

            entity.HasIndex(e => e.IdRutina, "FK_RutinaEjercicio_Rutina");

            entity.Property(e => e.IdRutinaEjercicio).HasColumnName("id_rutina_ejercicio");
            entity.Property(e => e.Descanso).HasColumnName("descanso");
            entity.Property(e => e.IdEjercicio).HasColumnName("id_ejercicio");
            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.IdRutina).HasColumnName("id_rutina");
            entity.Property(e => e.Repeticiones).HasColumnName("repeticiones");
            entity.Property(e => e.Series).HasColumnName("series");

            entity.HasOne(d => d.IdEjercicioNavigation).WithMany(p => p.RutinaEjercicios)
                .HasForeignKey(d => d.IdEjercicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RutinaEjercicio_Ejercicio");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.RutinaEjercicios)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Ejercicio");

            entity.HasOne(d => d.IdRutinaNavigation).WithMany(p => p.RutinaEjercicios)
                .HasForeignKey(d => d.IdRutina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RutinaEjercicio_Rutina");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Rol)
                .HasMaxLength(30)
                .HasColumnName("rol");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PRIMARY");

            entity.ToTable("venta");

            entity.HasIndex(e => e.IdCliente, "FK_Venta_Cliente");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Venta_Cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}