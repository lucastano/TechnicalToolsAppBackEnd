using System.Security.Cryptography;

namespace ProyectoService.ApiRest
{
    public static class Seguridad
    {

        public static void CrearPasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            //algoritmo de cifrado
            using (var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;//se usa para cifrar la contrasena
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));//computa el password en byte
            }
        }

        internal static bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac=new HMACSHA512(passwordSalt))
            {
                var passwordHashCalculado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHashCalculado.SequenceEqual(passwordHash);
                
            }
        }
    }
}
