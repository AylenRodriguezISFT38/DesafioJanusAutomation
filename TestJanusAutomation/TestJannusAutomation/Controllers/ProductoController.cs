using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using TestJannusAutomation.Models;
using TestJannusAutomation.Models.Metadata;
using TestJannusAutomation.Services;

namespace TestJannusAutomation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("Policy")]
    public class ProductoController : ControllerBase
    {
        private readonly TestContext db;
        public ProductoController(TestContext _db) {
            this.db = _db;
        }

        [HttpGet]
        public IActionResult GetProductos()
        {
            try
            {
                var lst = db.VwStockProductoTipos.ToList();
             
                return Ok(new {Err = false, data = lst});
            }
            catch (Exception e)
            {
                LogService.Log("Ha ocurrido un error en ProductoController-GETPRODUCTO "+e.Message);
                return BadRequest("Error al obtener el listado de productos "+e.Message);
            }
        }
        [HttpPost]
        public IActionResult PostProductos(ProductoStock model)
        {
            try
            {
                Producto producto = new();
                producto = model.Producto;
                producto.CreatedAt = DateTime.Now;
                db.Productos.Add(producto);
                db.SaveChanges();

                Stock stock = new();
                stock = model.Stock;
                stock.IdProducto = model.Producto.Id;
                stock.CreatedAt= DateTime.Now;
                db.Stocks.Add(stock);
                db.SaveChanges();

                return Ok(new { Err = false, msg = "Producto creado correctamente!" });
            }
            catch (Exception e)
            {
                LogService.Log("Ha ocurrido un error en ProductoController-POSTPRODUCTO " + e.Message);
                return BadRequest(new { Err = true, msg = "Error al crear el producto!" });
            }
        }
        [HttpPut]
        public IActionResult PutProductos(ProductoStock model)
        {
            try
            {
                var bsqProducto = db.Productos.Find(model.Producto.Id);
                bsqProducto.Nombre = model.Producto.Nombre;
                bsqProducto.Precio = model.Producto.Precio;
                bsqProducto.IdTipoProducto = model.Producto.IdTipoProducto;
                bsqProducto.Deleted = false;
                db.SaveChanges();

                var bsqStock = db.Stocks.Where(x=>x.IdProducto == model.Producto.Id).FirstOrDefault();
                bsqStock.IdProducto = model.Producto.Id;
                bsqStock.Cantidad = model.Stock.Cantidad;

                db.SaveChanges();
                return Ok(new { Err = false, msg = "Producto actualizado!" });
            }
            catch (Exception e)
            {
                LogService.Log("Ha ocurrido un error en ProductoController-PUTPRODUCTO " + e.Message);
                return BadRequest(new { Err = true, msg = "Error al editar el producto!" });
            }
        }
        [HttpDelete]
        public IActionResult DeleteProductos(Producto model)
        {
            try
            {
                var bsq = db.Productos.Find(model.Id);
                bsq.Deleted = true;
                db.SaveChanges();

                return Ok(new { Err = false, msg = "Eliminado correctamente!" });
            }
            catch (Exception e)
            {
                LogService.Log("Ha ocurrido un error en ProductoController-DELETEPRODUCTO " + e.Message);
                return BadRequest(new { Err = true, msg = "Error al eliminar el producto!" });
            }
        }

    }
}
