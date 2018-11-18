namespace ADondeIr.Frontend.Controllers
{
    using System.Web.Mvc;
    using BusinessLogic;

    [Autenticado(Optional = true)]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.listDistrito = new DistritoBl().GetAll();
            ViewBag.listTipoActividades = new TipoActividadBl().GetAll();
            return View();
        }
    }
}