using System.Web.Mvc;

namespace ADondeIr.Frontend.Controllers
{
    using BusinessLogic;
    using Common.Session;
    using Model;

    [Autenticado]
    public class RutaController : BaseController
    {
        private readonly RutaBl _bl = new RutaBl();
        // GET: Ruta
        public ActionResult Index()
        {
            return View(_bl.GetAll(GetUser<Usuario>().pkUsuario));
        }

        public ActionResult Active()
        {
            return View(_bl.GetActiva(GetUser<Usuario>().pkUsuario));
        }

        public JsonResult GetActiveRuta()
        {
            return Json(_bl.GetActiva(GetUser<Usuario>().pkUsuario), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(int id)
        {
            var rutaActual = _bl.GetActiva(GetUser<Usuario>().pkUsuario);

            if (rutaActual == null)
            {
                var i = _bl.GetAll(GetUser<Usuario>().pkUsuario).Count;
                rutaActual = new Ruta
                {
                    fkUsuario = GetUser<Usuario>().pkUsuario,
                    cRuta = "Ruta #" + (i + 1),
                    bActivo = true,
                    fkUsuarioCrea = GetUser<Usuario>().pkUsuario
                };

                _bl.CreateRuta(rutaActual);

                rutaActual = _bl.GetActiva(GetUser<Usuario>().pkUsuario);
            }

            return Json(_bl.AddProduct(new RutaProducto
            {
                fkProducto = id,
                fkRuta = rutaActual.pkRuta
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProduct(RutaProducto producto)
        {
            return Json(_bl.UpdateProduct(producto), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProduct(int id)
        {
            return Json(_bl.DeleteProducto(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarRuta()
        {
            var ruta = _bl.GetActiva(GetUser<Usuario>().pkUsuario);
            var result = _bl.GuardarRuta(ruta.pkRuta);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}