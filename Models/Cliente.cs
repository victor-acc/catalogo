using System;
using System.Collections.Generic;

namespace catalogo.Models;

public partial class Cliente
{
    public int Idcliente { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
