using ProyectoService.LogicaNegocio.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.AccesoDatos.Servicios
{
    public class WspService : IWspService
    {
        private readonly HttpClient _httpClient;

        public WspService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<string> EnviarWsp(string numero, string mensaje)
        {
            //EJEMPLO NO FUNCIONAL, ES PARA VER ESTRUCTURA

            //var response = await _httpClient.GetAsync(url);

            //if (response.IsSuccessStatusCode)
            //{
            //    return await response.Content.ReadAsStringAsync();
            //}

            //throw new Exception($"Error al consumir API. Código: {response.StatusCode}");
            throw new NotImplementedException();
        }
    }
}
