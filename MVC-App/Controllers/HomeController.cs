using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace MVC_App.Controllers
{
    public class HomeController : Controller
    {
        private T GetConfigSettings<T>(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(value)
                ? default(T)
                : (T)Convert.ChangeType(value, typeof(T));
        }

        private void UploadImage()
        {
            try
            {
                if (Request.Files.Count == 0)
                {
                    throw new Exception("Seleccione un archivo del disco duro.");
                }

                HttpPostedFileBase file = Request.Files[0];
                string fileName = file.FileName;

                if (!ImageManager.ImageService.IsImage(fileName))
                {
                    throw new Exception("Formato de imagen inválido.");
                }

                byte[] image = ImageManager.ImageService.Resize(
                    file.InputStream,
                    GetConfigSettings<int>("MaxSize"));

                switch (Request.Form["save"])
                {
                    case "disk":
                        string folder = GetConfigSettings<string>("ImagesFolder");
                        FileManager.FileService.SaveImage(
                            image,
                            fileName,
                            Server.MapPath(folder));
                        break;
                    case "db":
                        DbManager.DbService.InsertImage(
                            image,
                            fileName);
                        break;
                }
                ViewBag.Message = $"Imagen guardada como '{fileName}'";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
        }

        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request.Form["upload-image"]))
            {
                UploadImage();
            }

            return View();
        }
    }
}