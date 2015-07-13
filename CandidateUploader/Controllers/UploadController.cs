using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CandidateUploader.Infrastructure;
using CandidateUploader.Models;
using CandidateUploader.ViewModel;
using Newtonsoft.Json;

namespace CandidateUploader.Controllers
{
    public class UploadController : Controller
    {


        private enum ImageType
        {
            Thumbnail = 1,
            List,
            Detail
        }


        private readonly CuDataContext _cuDataContext = new CuDataContext();

        // GET: Upload
        public ActionResult Index()
        {

            var model = new UploadViewModel
            {
                MaxImageBytes = CuConfig.MaxImageBytes,
                Contests = _cuDataContext.Attributes.Where(a => a.Type.Equals("Concorso")).ToList()
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(CultureInfo.InvariantCulture),
                        Text = a.Text
                    })
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult VideoAllowed(string file, string filename)
        {

            var pieces = file.TrimEnd().Split(new[] {' '});

            var buffer = pieces.Select(p => Convert.ToByte(p)).ToArray();

            // save the file to a folder with an unique name
            var path = Path.Combine(Server.MapPath("~/Uploads"), Guid.NewGuid() + Path.GetExtension(filename));
            System.IO.File.WriteAllBytes(path, buffer);



            // check the video duration
            var result = IsVideoLengthAllowed(path);

            System.IO.File.Delete(path);

            return Json(new
                {
                    Result = result ? 1 : 0,
                    MaxSeconds = CuConfig.MaxVideoSeconds
                }
            );


        }


        private static bool IsVideoLengthAllowed(string path)
        {

            using (var video = TagLib.File.Create(path))
            {
                return video.Properties.Duration.TotalSeconds <= CuConfig.MaxVideoSeconds;
            }

        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string data)
        {

            // get the file name
            var filename = Path.GetFileName(file.FileName);

            if (filename == null) return new ContentResult();



            // get the extra data
            var extraData = JsonConvert.DeserializeObject<FileExtraData>(data);



            // check the max uploads limits
            if (UploadExceed() || UserUploadExceed(extraData.Email)) return new ContentResult();



            // check the dimension of the image files
            if (IsImage(extraData) && file.ContentLength > CuConfig.MaxImageBytes)

                return new ContentResult();




            // save the file to a folder with an unique name
            var path = Path.Combine(Server.MapPath("~/Uploads"), Guid.NewGuid() + Path.GetExtension(filename));
            file.SaveAs(path);



            // check the video duration
            if (IsVideo(extraData) && !IsVideoLengthAllowed(path)) return new ContentResult();



            // save the file into the database
            var result = SaveFile(path, filename, extraData);



            // remove the file
            try
            {
                System.IO.File.Delete(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return new ContentResult
            {
                ContentType = "text/plain",
                Content = result ? filename : string.Empty,
                ContentEncoding = Encoding.UTF8
            };

        }


        private bool SaveFile(string filePath, string filename, FileExtraData data)
        {

            var file = new FileModel
            {
                Title = data.Title,
                Author = data.Author,
                Email = data.Email,
                Location = data.Location,
                FreeTags = data.FreeTags,
                Created = DateTime.Now,
                File = System.IO.File.ReadAllBytes(filePath),
                FileName = filename,
                FileType = data.FileType,
                Contest = _cuDataContext.Attributes.FirstOrDefault(a => a.Id == data.ContestId)
            };



            // check to resize the images
            if (IsImage(data))
            {
                file.ImageThumbnail = ResizeImage(filePath, ImageType.Thumbnail);
                file.ImageList = ResizeImage(filePath, ImageType.List);
                file.ImageDetail = ResizeImage(filePath, ImageType.Detail);
            }



            // add the new file
            _cuDataContext.Files.Add(file);


            // save
            return _cuDataContext.SaveChanges() != 0;

        }


        private static bool IsImage(FileExtraData extraData)
        {
            // check if the file is an image
            return extraData.FileType.StartsWith("image");
        }


        private static bool IsVideo(FileExtraData extraData)
        {
            // check if the file is a video
            return extraData.FileType.StartsWith("video");
        }


        private static byte[] ResizeImage(string imgPath, ImageType imageType)
        {


            // get the image to resize
            var imgToResize = Image.FromFile(imgPath);


            // resize the image
            var img = new Bitmap(imgToResize, GetSizeByImageType(imageType));


            // create a memory stream
            using (var stream = new MemoryStream())
            {


                // save the new image in memory
                img.Save(stream, ImageFormatFromExtension(imgPath));


                // return the byte array
                return stream.ToArray();


            }


        }

        private static ImageFormat ImageFormatFromExtension(string imgPath)
        {

            var ext = Path.GetExtension(imgPath) ?? string.Empty;

            switch (ext.ToLower())
            {
                case "bmp":
                    return ImageFormat.Bmp;
                case "emf":
                    return ImageFormat.Emf;
                case "exif":
                    return ImageFormat.Exif;
                case "gif":
                    return ImageFormat.Gif;
                case "ico":
                    return ImageFormat.Icon;
                case "png":
                    return ImageFormat.Png;
                case "tiff":
                    return ImageFormat.Tiff;
                case "wmf":
                    return ImageFormat.Wmf;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        private static Size GetSizeByImageType(ImageType imageType)
        {

            switch (imageType)
            {
                case ImageType.Thumbnail:
                    return CuConfig.ImageThumbnailSize;

                case ImageType.List:
                    return CuConfig.ImageListSize;

                default:
                    return CuConfig.ImageDetailsSize;
            }
        }


        private bool UploadExceed()
        {

            var count = _cuDataContext.Files.Count();

            return count >= CuConfig.MaxUploads;

        }


        private bool UserUploadExceed(string email)
        {

            var count = _cuDataContext.Files.Count(f => f.Email.Equals(email));

            return count >= CuConfig.MaxUploadsPerUser;

        }

}
}