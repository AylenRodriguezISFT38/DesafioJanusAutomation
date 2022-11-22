using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestJannusAutomation.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public int IdTipoProducto { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }

        public virtual TipoProducto IdTipoProductoNavigation { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
