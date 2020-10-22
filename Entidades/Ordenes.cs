using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPedidos.Entidades
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int SuplidorId { get; set; }
        public float Monto { get; set; }

        [ForeignKey("OrdenId")]
        public List<OrdenesDetalle> Detalle { get; set; } = new List<OrdenesDetalle>();

    }
}
