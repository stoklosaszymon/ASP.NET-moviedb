using aaaaaa.Classes;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace aaaaaa.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        DbQuerry db = new DbQuerry();
        public ActionResult Index()
        {
            ViewBag.Movies = db.GetAllMovies();
            return View();
        }

        [Route("Movies/Info/{title}")]
        public ActionResult Info(string title)
        {
            return View("Info", db.GetMovie(title));
        }

        [HttpPost]
        public ActionResult InfoPost(string title)
        {
            return RedirectToAction("Info", "Movies", new { title = title });
        }

        [Route("Movies/MovieById/{id}")]
        public ActionResult MovieById(int? id)
        {
            var a = db.context.Movies.Where(n => n.movieID == id).FirstOrDefault();
            if (a != null)
            {
                return RedirectToAction("Info", a.Title);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public JsonResult GetJson(string title)
        {
            var a = db.GetMovie(title);
            return Json(a, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<JsonResult> GetAllMoviesJson()
        {
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(a);
            var r = await Task.Run(() => db.GetAllMovies());
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetTopRatedMoviesJson(int count = 6)
        {
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(a);
            var r = await Task.Run(() => db.GetAllMovies().OrderByDescending(e => e.imdbRating).Take(count));

            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}