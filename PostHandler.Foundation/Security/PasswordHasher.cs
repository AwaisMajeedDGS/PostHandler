using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostHandler.Foundation.Security
{
    public static class CustomPasswordHasher
    {
         public static string ToPassHash(this string password){
             return (new PasswordHasher()).HashPassword(password);
         }

         public static bool VerifyHash(this string password,string hashedPassword)
         {
             var result = (new PasswordHasher()).VerifyHashedPassword(hashedPassword, password);
             switch (result)
             {
                 case PasswordVerificationResult.Success:
                 case PasswordVerificationResult.SuccessRehashNeeded: return true;
                 case PasswordVerificationResult.Failed:
                 default:
                     return false;
             }
         }

    }
}
