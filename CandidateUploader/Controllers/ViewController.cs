using System.Linq;
using System.Web.Mvc;
using CandidateUploader.Models;

namespace CandidateUploader.Controllers
{
    public class ViewController : Controller
    {

        private readonly CuDataContext _cuDataContext = new CuDataContext();

        // GET: View
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Files()
        {

            var files = _cuDataContext.Files.Where(f => f.State == FileStatus.Approved)
                .OrderByDescending(f => f.Created).ToList();

            return Json(files.Select(f => new
            {
                f.Id,
                f.Author,
                f.Email,
                f.Location,
                f.Title,
                Created = f.Created.ToString("yy/MM/yyyy hh:mm:ss")
            }));

        }

        public PartialViewResult TemplateItem()
        {
            return PartialView();
        }

    }
}