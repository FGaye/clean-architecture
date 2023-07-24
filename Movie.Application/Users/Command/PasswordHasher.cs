using System.Security.Cryptography;
using System.Text;
using Movie.Application.Common.Dtos;

namespace Movie.Application.Users.Command;
// hash password before saving to db
public class PasswordHasher
{
    public static string HashPassword(RegisterDto registerDto)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            var password = registerDto.Password;
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
