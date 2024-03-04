using Microsoft.EntityFrameworkCore;
using MvcCoreCryptography.Data;
using MvcCoreCryptography.Helpers;
using MvcCoreCryptography.Models;

namespace MvcCoreCryptography.Repositories
{
    public class RepositoryUsuarios
    {
        private UsuariosContext context;

        public RepositoryUsuarios(UsuariosContext context)
        {
            this.context = context;
        }

        private async Task<int> GetMaxIdUsuarioAsync()
        {
            if (this.context.Usuarios.Count() == 0) return 1;
            return await this.context.Usuarios.MaxAsync(x => x.IdUsuario) + 1;
        }

        public async Task RegisterUsuarioAsync
            (string nombre, string email, string password, string imagen)
        {
            Usuario user = new Usuario();
            user.IdUsuario = await GetMaxIdUsuarioAsync();
            user.Nombre = nombre;
            user.Email = email;
            user.Imagen = imagen;
            // Cada usuario tendrá un SALT distinto
            user.Salt = HelperCryptography.GenerateSalt();
            // Guardamos el password en byte[]
            user.Password = HelperCryptography.EncryptPassword
                (password, user.Salt);
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        // Necesitamos un método para validar al usuario
        // Dicho método devolverá el propio usuario
        // Como comparamos, con email
        // password (12345)
        // 1) Recuperar el user por su email
        // 2) Recuperamos el SALT
        // 3) Convertimos de nuevo el password con el SALT
        // 4) Recuperamos el byte[] de password de la BBDD
        // 5) Comparamos los dos arrays (BBDD y el generado)
        public async Task<Usuario> LoginUserAsync(string email, string password)
        {
            Usuario user = await this.context.Usuarios
                .FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;
            else
            {
                string salt = user.Salt;
                byte[] temp =
                    HelperCryptography.EncryptPassword(password, salt);
                byte[] passUser = user.Password;
                bool response = HelperCryptography.CompareArrays(temp, passUser);
                if (response == true)
                    return user;
                else
                    return null;
            }
        }
    }
}
