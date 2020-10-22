using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistroPedidos.Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public string  Descripcion { get; set; }
        public float Costo  { get; set; }
        public float Inventario { get; set; }

    }
}
