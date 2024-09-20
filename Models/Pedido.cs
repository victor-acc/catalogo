using System;
using System.Collections.Generic;

namespace catalogo.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int Idproducto { get; set; }

    public int Idcliente { get; set; }

    public int? Cantidad { get; set; }

    public virtual Cliente? IdclienteNavigation { get; set; } 

    public virtual Producto? IdproductoNavigation { get; set; } 
}
