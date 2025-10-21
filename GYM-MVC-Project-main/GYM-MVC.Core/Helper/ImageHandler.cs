using Microsoft.AspNetCore.Http;

namespace GYM_MVC.Core.Helper {

    public abstract class UploadImageStatus {
        public string Message { get; set; }
    }

    public class UploadImageError : UploadImageStatus {

        public UploadImageError(string message) {
            Message = message;
        }
    }

    public class UploadImageSuccess : UploadImageStatus {
        public string FileName { get; set; }

        public UploadImageSuccess(string FileName) {
            Message = "Image uploaded successfully";
            this.FileName = FileName;
        }
    }

    public class ImageHandler {

        public static async Task<UploadImageStatus> UploadImage(IFormFile image, string prefix = "") {
            try {
                var allowedSize = 5 * 1024 * 1024;

                if (image.Length > allowedSize)
                    return new UploadImageError("Image size must be less than 5MB");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" , ".jfif" , ".heic" };
                var extension = Path.GetExtension(image.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    return new UploadImageError("Only .jpg, .jpeg, .png, .gif are allowed");

                var safeFileName = $"{DateTime.Now:yyyyMMdd_HHmmssfff}{extension}";

                var filePath = Path.Combine($"wwwroot/images/{prefix}", safeFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await image.CopyToAsync(stream);
                }

                return new UploadImageSuccess($"/images/{prefix}/{safeFileName}");
            } catch (Exception) {
                return new UploadImageError("Image upload failed");
            }
        }
    }
}