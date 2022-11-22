using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestJannusAutomation.Models;
using TestJannusAutomation.Services;

namespace TestJannusAutomation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("Policy")]
    public class TipoProductoController : ControllerBase
    {
        private readonly TestContext db;
        public TipoProductoController(TestContext _db)
        {
            this.db = _db;
        }
        [HttpGet]
        public IActionResult GetTipoProducto()
        {
            try
            {
                var bsq = db.TipoProductos.Where(x=>x.Deleted == false).ToList();

                return Ok(new { Err = false, msg = bsq });
            }
            catch (Exception e)
            {
                LogService.Log("Ha ocurrido un error en TipoProductoController-GETTIPOPRODUCTO " + e.Message);
                return BadRequest(new { Err = true, msg = "Error al traer el listado!" });
            }
        }
        [HttpPost]
        public IActionResult PostTipoProducto(TipoProducto model)
        {
            try
            {
                TipoProducto descripcion = new TipoProducto();
                descripcion.Descripcion= model.Descripcion;
                descripcion.Deleted = false;
                descripcion.CreatedAt = DateTime.Now;
                db.TipoProductos.Add(descripcion);
                db.SaveChanges();
                return Ok(new { Err = false , msg = "Tipo de producto creado exitosamente!"});
            }
            catch (Exception e)
            {
                LogService.Log("Ha ocurrido un error en TipoProductoController-POSTTIPOPRODUCTO " + e.Message);
                return BadRequest(new { Err = true, msg = "Error al crear el tipo de producto!" });
            }
        }
        [HttpPut]
        public IActionResult PutTipoProducto(TipoProducto model)
        {
            try
            {
                var bsq = db.TipoProductos.Find(model.Id);
                bsq.Descripcion = model.Descripcion;
                bsq.Deleted= false;
                db.SaveChanges();
                return Ok(new { Err = false, msg = "Producto editado correctamente" });
            }
            catch (Exception e)
            {
                LogService.Log("Ha ocurrido un error en TipoProductoController-PUTTIPOPRODUCTO " + e.Message);
                return BadRequest(new { Err = true, msg = "Error al editar el tipo de producto!" });
            }
        }
        [HttpDelete]
        public IActionResult DeleteTipoProducto(TipoProducto model)
        {
            try
            {
                var bsq = db.TipoProductos.Find(model.Id);
                bsq.Deleted = true;
                db.SaveChanges();
                return Ok(new { Err = false, msg = "Tipo de producto eliminado correctamente" });
            }
            catch (Exception e)
            {
                LogService.Log("Ha ocurrido un error en TipoProductoController-DELETETIPOPRODUCTO " + e.Message);
                return BadRequest(new { Err = true, msg = "Error al eliminar el tipo de producto!" });
            }
        }

    }
}
