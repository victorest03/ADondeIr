namespace ADondeIr.Frontend.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using BusinessLogic;
    using Common.Extensions;
    using Common.Model;
    using Common.Session;
    using Model;

    public class ProductoController : BaseController
    {
        private readonly ProductoBl _bl = new ProductoBl();
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listado()
        {
            return Json(new { data = _bl.GetAll() }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult PartialMantenimiento(int id = 0)
        {
            Producto model = null;
            if (id != 0) model = _bl.Get(id);
            return PartialView("_Mantenimiento", model ?? new Producto());
        }
        [HttpPost]
        public JsonResult Mantenimiento(Producto model, HttpPostedFileBase fotoPrincipal)
        {
            Result result;
            if (fotoPrincipal == null && model.pkProducto == 0)
            {
                ModelState.AddModelError("ProductoImagen_Error", "Seleccione una Foto Principal!!!");
            }
            if (ModelState.IsValid)
            {
                //if (fotoPrincipal != null)
                //    model.cFileName = fotoPrincipal.SaveFileInAzure("mpol", "Producto", true);
                if (model.pkProducto != 0)
                {
                    model.fkUsuarioEdita = GetUser<Usuario>().pkUsuario;
                }
                else
                {
                    model.fkUsuarioCrea = GetUser<Usuario>().pkUsuario;
                }
                result = _bl.Save(model);

            }
            else
            {
                result = new Result { Errors = ModelState.AllErrors() };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            return Json(_bl.Delete(id, GetUser<Usuario>().pkUsuario), JsonRequestBehavior.AllowGet);
        }
    }
}