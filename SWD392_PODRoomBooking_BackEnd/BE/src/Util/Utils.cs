using BE.src.Domains.Models;
using BE.src.Shared.Constant;
using BE.src.Shared.Type;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace BE.src.Util
{
    public static class Utils
    {
        static Utils()
        {
            try
            {
                if (FirebaseApp.DefaultInstance == null)
                {
                    string credentialPath = "Config/firebase.json";
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential = GoogleCredential.FromFile(credentialPath)
                    });
                }
            }
            catch (System.Exception ex)
            {
                // Log the full exception details
                Console.WriteLine($"Error during Firebase initialization: {ex}");
                throw; // Optional: rethrow the exception to notify callers
            }
        }

        public static string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: JWT.Issuer,
                    audience: JWT.Audience,
                   claims: claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static Guid? GetUserIdByJWT(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                return null;
            }
            Console.WriteLine(jwtToken);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
            Console.WriteLine(userIdClaim);
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return null;
        }
        public static string HashObject<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }
        public static async Task<string?> UploadImgToFirebase(IFormFile file, string name, string type)
        {
            try
            {
                var storageClient = StorageClient.Create();

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                string bucketName = Firebase.BucketName;
                var objectUploadOptions = new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead
                };
                string fileExtension = Path.GetExtension(file.FileName);
                string objectName = $"{type}/{name}{fileExtension}";

                await storageClient.UploadObjectAsync(bucketName, objectName, null, memoryStream, objectUploadOptions);
                string objectPublicUrl = $"https://storage.googleapis.com/{bucketName}/{objectName}";

                return objectPublicUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while uploading the image: {ex.Message}");
                return null;
            }
        }
        public static string ConvertToUnderscore(string input)
        {
            return input.Replace(" ", "_");
        }

        public static string ConvertDateTimeTime(DateTime time)
        {
            return time.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
