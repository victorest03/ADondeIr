namespace ADondeIr.Frontend.Controllers
{
    using BusinessLogic;
    using Common.Extensions;
    using Common.Model;
    using Common.Session;
    using Model;
    using System.Web.Mvc;

    [Autenticado(IsAdmin = true)]
    public class EmpresaController : BaseController
    {
        private readonly EmpresaBl _bl = new EmpresaBl();
        // GET: Empresa
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
            Empresa model = null;
            if (id != 0) model = _bl.Get(id);
            return PartialView("_Mantenimiento", model ?? new Empresa());
        }
        [HttpPost]
        public JsonResult Mantenimiento(Empresa model)
        {
            Result result;
            if (ModelState.IsValid)
            {
                if (model.pkEmpresa != 0)
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