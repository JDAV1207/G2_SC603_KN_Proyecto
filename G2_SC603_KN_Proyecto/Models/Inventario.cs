using System;
using System.Collections.Generic;

namespace G2_SC603_KN_Proyecto.Models;

public partial class Inventario
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? Categoria { get; set; }

    public string? Descripcion { get; set; }

    public int Cantidad { get; set; }

    public int StockMinimo { get; set; }

    public decimal? Costo { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();
}
