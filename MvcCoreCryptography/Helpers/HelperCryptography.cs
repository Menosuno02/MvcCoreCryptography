using System.Security.Cryptography;
using System.Text;

namespace MvcCoreCryptography.Helpers
{
    public class HelperCryptography
    {
        // Vamos a tener un par de métodos que no
        // tienen nada que ver con criptografía
        public static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int aleatorio = random.Next(1, 255);
                char letra = Convert.ToChar(aleatorio);
                salt += letra;
            }
            return salt;
        }

        // Necesitamos un método para comparar si los password
        // son iguales. Debemos comparar a nivel de byte
        public static bool CompareArrays(byte[] a, byte[] b)
        {
            bool iguales = true;
            if (a.Length != b.Length)
            {
                iguales = false;
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    // Preguntamos si el contenido de cada byte
                    // es distinto
                    if (!a[i].Equals(b[i]))
                    {
                        iguales = false;
                        break;
                    }
                }
            }
            return iguales;
        }

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
