using System;
using System.Configuration;
using System.Linq;

namespace ASPX_App
{
    public partial class Default : System.Web.UI.Page
    {
        private T GetConfigSettings<T>(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(value)
                ? default(T)
                : (T)Convert.ChangeType(value, typeof(T));
        }

        private void ShowMessage(string message)
        {
            Message.Text = message;
        }

        protected void UploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMessage("");

                if (!FileUploader.HasFile)
                {
                    throw new Exception("Seleccione un archivo del disco duro.");
                }

                string fileName = FileUploader.PostedFile.FileName;

                if (!ImageManager.ImageService.IsImage(fileName))
                {
                    throw new Exception("Formato de imagen inválido.");
                }

                byte[] image = ImageManager.ImageService.Resize(
                    FileUploader.PostedFile.InputStream,
                    GetConfigSettings<int>("MaxSize"));

                if (SaveDisk.Checked)
                {
                    string folder = ConfigurationManager.AppSettings["ImagesFolder"];
                    FileManager.FileService.SaveImage(
                        image,
                        fileName,
                        Server.MapPath(folder));
                }
                else if (SaveDb.Checked)
                {
                    DbManager.DbService.InsertImage(
                        image,
                        fileName);
                }
                ShowMessage($"Imagen guardada como '{fileName}'");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }
    }
}