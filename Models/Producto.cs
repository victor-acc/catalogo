using System;
using System.Collections.Generic;

namespace catalogo.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public string? Nombreproducto { get; set; }

    public byte[]? Imagen { get; set; }

    public int? Precio { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
