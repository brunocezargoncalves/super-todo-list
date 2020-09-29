using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Application.Models
{
    public static class Common
    {
        public static string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        }

        public static Guid? GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim sId = claimsIdentity?.FindFirst(ClaimTypes.Sid);

            if(sId != null)
                return Guid.Parse(sId?.Value);
            
            return null;
        }

        public static string GetUserName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim name = claimsIdentity?.FindFirst(ClaimTypes.Name);

            return name?.Value ?? string.Empty;
        }
    }       
}