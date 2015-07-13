using System.Collections.Generic;
using System.Web.Mvc;

namespace CandidateUploader.ViewModel
{
    public class UploadViewModel
    {
        public long MaxImageBytes { get; set; }

        public int SelectedContest { get; set; }

        public IEnumerable<SelectListItem> Contests { get; set; }

    }
}