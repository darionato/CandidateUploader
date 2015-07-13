using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CandidateUploader.Models;

namespace CandidateUploader.Controllers
{
    public class ApproveController : Controller
    {

        private readonly CuDataContext _cuDataContext = new CuDataContext();


        // GET: Approve
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Files()
        {

            var files = _cuDataContext.Files.Where(f => f.State == FileStatus.None).OrderBy(f => f.Created);

            return Json(files.Select(f => new { f.Id, f.Title, f.Email, f.Author, f.FreeTags }));

        }


        [HttpPost]
        public async Task<JsonResult> Approve(int id)
        {

            var file = await _cuDataContext.Files.FindAsync(id);

            file.State = FileStatus.Approved;

            var result = await _cuDataContext.SaveChangesAsync();

            return Json(result);

        }

    }
}