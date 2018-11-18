namespace ADondeIr.Frontend.Controllers
{
    using BusinessLogic;
    using Common.Attributes.ActionFilter;
    using Common.Extensions;
    using Common.Methods;
    using Common.Model;
    using Common.Session;
    using Model;
    using System.Web;
    using System.Web.Mvc;

    public class ProductoController : BaseController
    {
        private readonly ProductoBl _bl = new ProductoBl();
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        [JsonResultCustom]
        public JsonResult Listado()
        {
            return Json(new { data = _bl.GetAll() }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult PartialMantenimiento(int id = 0)
        {
            Producto model = null;
            ViewBag.listEmpresa = new EmpresaBl().GetAll();
            ViewBag.listTipoActividades = new TipoActividadBl().GetAll();
            ViewBag.listDistrito = new DistritoBl().GetAll();
            if (id != 0) model = _bl.Get(id);
            return PartialView("_Mantenimiento", model ?? new Producto());
        }

        [HttpPost]
        public JsonResult Mantenimiento(Producto model, HttpPostedFileBase foto)
        {
            Result result;
            if (foto == null && model.pkProducto == 0)
            {
                ModelState.AddModelError("ProductoImagen_Error", "Seleccione una Foto Principal!!!");
            }
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    model.FotoPrincipal = model.FotoPrincipal ?? new Foto();
                    model.FotoPrincipal.cFileName = foto.GenerateNameFile();
                    StorageAzureHelper.Save("producto", model.FotoPrincipal.cFileName, foto.InputStream);
                }
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

        public FileResult Foto(string id)
        {
            var stream = StorageAzureHelper.Get("producto", id, out var contentType);
            return File(stream, contentType);
        }

        public ActionResult Busqueda(string search, int? fkDistrito, int? fkTipoActividad)
        {
            ViewBag.totalProducts = _bl.Count();

            ViewBag.search = search;
            ViewBag.distrito = fkDistrito;
            ViewBag.tipoActividad = fkTipoActividad;

            ViewBag.listDistrito = new DistritoBl().GetAll();
            ViewBag.listTipoActividades = new TipoActividadBl().GetAll();
            return View();
        }
        

        [JsonResultCustom]
        public JsonResult ListadoLazy(ProductoFilter model)
        {
            var dataResult = _bl.GetAllLazy(model, out var filteredResultsCount, out var totalResultsCount);

            return Json(new
            {
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = dataResult
            }, JsonRequestBehavior.AllowGet);
        }
    }
}