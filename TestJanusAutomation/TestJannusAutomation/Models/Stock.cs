using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestJannusAutomation.Models
{
    public partial class Stock
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public float Cantidad { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
    }
}
