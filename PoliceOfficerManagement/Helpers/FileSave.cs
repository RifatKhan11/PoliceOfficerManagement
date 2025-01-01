namespace PoliceOfficerManagement.Helpers
{
    public static class FileSave
    {
        public static string SaveFile(out string fileName, IFormFile file, string localPath,string oldName,string newName, IWebHostEnvironment env)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".xlsx", ".csv", ".docx" ,".txt"};
            string message = "success";
            var extention = Path.GetExtension(file.FileName);
            
            fileName = Path.Combine(localPath,"OldName_"+ oldName+ "_newName_" + newName+"_"+ DateTime.Now.Ticks + extention);

            if (file.Length > 2000000)
                return "Select jpg or jpeg or png or pdf less than 2Μ";
            else if (!allowedExtensions.Contains(extention.ToLower()))
                return "Must be jpg or jpeg or png or pdf or xlsx or csv or docx or txt";


            //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            var path = env.WebRootPath+ fileName;
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            catch
            {
                return "can not upload image";
            }
            return message;
        }

        public static string SaveFoodItemImage(out string fileName, IFormFile img, IWebHostEnvironment env)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png",".gif" };
            string message = "success";

            var extention = Path.GetExtension(img.FileName);
            if (img.Length > 20000000)
                message = "Select jpg or jpeg or png less than 2Μ";
            else if (!allowedExtensions.Contains(extention.ToLower()))
                message = "Must be jpeg or png";

            fileName = Path.Combine("MenuItem","PHQFoodItem_"+ DateTime.Now.Ticks + extention);
            //var rootDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            //var path = Path.Combine(rootDirectory, "wwwroot", fileName);
            var path = env.WebRootPath+"\\" + fileName;
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
            }
            catch
            {
                fileName = null;
                message = "can not upload image";
            }
            return message;
        }

        public static string UploadUserImage(out string fileName, IFormFile img)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            string message = "success";

            var extention = Path.GetExtension(img.FileName);
            if (img.Length > 2000000)
                message = "Select jpg or jpeg or png less than 2Μ";
            else if (!allowedExtensions.Contains(extention.ToLower()))
                message = "Must be jpeg or png";

            fileName = Path.Combine("Upload/GenarelUser", DateTime.Now.Ticks + extention);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
            }
            catch
            {
                message = "can not upload image";
            }
            return message;
        }

        public static string SaveImage(out string fileName, string filePath, IFormFile img, IWebHostEnvironment env)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            string message = "success";
            var extention = Path.GetExtension(img.FileName);
            fileName = Path.Combine(filePath, DateTime.Now.Ticks + extention);

            if (img.Length > 2000000)
                return "Select jpg or jpeg or png less than 2Μ";
            else if (!allowedExtensions.Contains(extention.ToLower()))
                return "Must be jpg or jpeg or png";


            //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            var path = env.WebRootPath + "\\" + fileName;
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
            }
            catch
            {
                return "can not upload image";
            }
            return message;
        }

        public static string SaveImage(out string fileName, string filePath, byte[] imge, string ext)
        {
            string message = "success";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            //Check if directory exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }
            fileName = filePath + "/" + DateTime.Now.Ticks + ext;
            string currentPath = Path.Combine(path, fileName);
            try
            {
                File.WriteAllBytes(currentPath, imge);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }
        public static string SaveNIDImage(out string fileName, string filePath, byte[] imge)
        {
            string message = "success";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            //Check if directory exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }
            fileName = filePath + "/" + DateTime.Now.Ticks + ".jpeg";
            string currentPath = Path.Combine(path, fileName);
            try
            {
                File.WriteAllBytes(currentPath, imge);
            }
            catch
            {
                return "can not upload image";
            }

            return message;
        }

        public static void DeleteImage(string filePath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
