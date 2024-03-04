namespace MvcCoreCryptography.Helpers
{
    public class HelperTools
    {
        public static string GenerateTokenMail()
        {
            Random random = new Random();
            string token = "";
            for (int i = 1; i <= 20; i++)
            {
                int aleatorio = random.Next(65, 122);
                token += Convert.ToChar(aleatorio); // letra
            }
            return token;
        }

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
    }
}
