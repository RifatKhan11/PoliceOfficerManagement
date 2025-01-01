
namespace PoliceOfficerManagement.Helpers
{
    public static class HideFilePath
    {

        public static (Stream fileStream, string contentType, string fileName) GetFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Invalid file name.");

            // Define the folder paths for each file type
            var folders = new Dictionary<string, string>
        {
            { "Videos", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Videos") },
            { "Images", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images") },
            { "PDF", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "PDF") },
            { "Word_Text", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Word_Text") },
            { "Excel", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Excel") },
            { "Audios", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Audios") }
        };

            // Locate the file in one of the folders
            string filePath = folders.Values
                .Select(folder => Path.Combine(folder, fileName))
                .FirstOrDefault(System.IO.File.Exists);

            if (filePath == null)
                throw new FileNotFoundException("File not found.");

            // Determine the content type
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            // Return the file stream, content type, and file name
            return (System.IO.File.OpenRead(filePath), contentType, fileName);
        }

    }
}
