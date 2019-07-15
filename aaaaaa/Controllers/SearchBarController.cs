using aaaaaa.Classes;
using System.Web.Http;
using System.Web.Mvc;

namespace aaaaaa.Controllers
{
    public class SearchBarController : Controller
    {
        // GET: SeatchBar
        [System.Web.Http.HttpPost]
        public ActionResult SearchBarTile([FromBody] string title)
        {
            DbQuerry db = new DbQuerry();
            return PartialView(db.GetMovie(title));
        }
    }
}