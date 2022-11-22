using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestJannusAutomation.Models
{
    public partial class TipoProducto
    {
        public TipoProducto()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
