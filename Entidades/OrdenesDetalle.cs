using RegistroPedidos.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistroPedidos.Entidades
{
    public class OrdenesDetalle
    {
        [Key]
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public float Cantidad { get; set; }
        public float Costo { get; set; }

        public OrdenesDetalle(int ordenId, int productoId, float cantidad, float costo)
        {
            Id = 0;
            OrdenId = ordenId;
            ProductoId = productoId;
            Cantidad = cantidad;
            Costo = costo;
        }

    }
}
