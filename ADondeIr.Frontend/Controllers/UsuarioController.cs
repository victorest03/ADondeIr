namespace ADondeIr.Frontend.Controllers
{
    using System.Web.Mvc;
    using BusinessLogic;
    using Common.Attributes.ActionFilter;
    using Common.Extensions;
    using Common.Model;
    using Common.Session;
    using Model;

    public class UsuarioController : BaseController
    {
        private readonly UsuarioBl _bl = new UsuarioBl();

        [Autenticado(IsAdmin = true)]
        public ActionResult Index()
        {
            return View();
        }

        [Autenticado]
        public ActionResult Profile()
        {
            return View();
        }

        [Autenticado(IsAdmin = true)]
        [JsonResultCustom]
        public JsonResult Listado()
        {
            return Json(new { data = _bl.GetAll() }, JsonRequestBehavior.AllowGet);
        }

        [Autenticado(IsAdmin = true)]
        public PartialViewResult PartialMantenimiento(int id = 0)
        {
            Usuario model = null;
            if (id != 0) model = _bl.Get(id);
            return PartialView("_Mantenimiento", model ?? new Usuario());
        }

        [Autenticado(IsAdmin = true)]
        [HttpPost]
        public JsonResult Mantenimiento(Usuario model)
        {
            Result result;
            if (ModelState.IsValid)
            {
                if (model.pkUsuario != 0)
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

        [Autenticado(IsAdmin = true)]
        public JsonResult Delete(int id)
        {
            return Json(_bl.Delete(id, GetUser<Usuario>().pkUsuario), JsonRequestBehavior.AllowGet);
        }
    }
}