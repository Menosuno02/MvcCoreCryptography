using System.Security.Cryptography;
using System.Text;

namespace MvcCoreCryptography.Helpers
{
    public class HelperCryptography
    {
        // Tendremos un método para cifrar el password
        // Vamos a recibir el password (string) y el salt(string)
        // y devolveremos el array de bytes[] del resultado cifrado
        public static byte[] EncryptPassword
            (string password, string salt)
        {
            string contenido = password + salt;
            SHA512 sha512 = SHA512.Create();
            // Convertimos contenido a byte[]
            byte[] salida = Encoding.UTF8.GetBytes(contenido);
            for (int i = 1; i <= 114; i++)
            {
                salida = sha512.ComputeHash(salida);
            }
            sha512.Clear();
            return salida;
        }
    }
}
