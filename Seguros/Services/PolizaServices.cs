using System.Text;
using Newtonsoft.Json;
using Seguros.IServices;
using SegurosAPI.DTOs;

namespace Seguros.Services
{
    public class PolizaServices : IPolizaServices
    {

        private static string _baseurl;

        public PolizaServices()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        #region "Obtener"
        public async Task<List<PolizaDTO>> GetAllPoliza()
        {
            if (string.IsNullOrEmpty(_baseurl))
            {
                throw new ArgumentException("La URL base no puede ser nula o vacía");
            }

            List<PolizaDTO> lista = new List<PolizaDTO>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseurl);
                var response = await httpClient.GetAsync($"/api/Poliza");

                if (response.IsSuccessStatusCode)
                {
                    var jsonRespuesta = await response.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<PolizaDTO>>(jsonRespuesta);
                }
                else
                {
                    // Manejar el caso cuando la respuesta no es exitosa
                    // Puedes lanzar una excepción, registrar el error, etc.
                    throw new Exception($"La solicitud GET no fue exitosa. Código de estado: {response.StatusCode}");
                }
            }

            return lista;
        }

        #endregion

        #region "Obtener Id"
        public async Task<PolizaDTO> GetPolizaById(int id)
        {

            PolizaDTO poliza = new PolizaDTO();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/Poliza/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado1 = JsonConvert.DeserializeObject<PolizaDTO>(json_respuesta);
                poliza = resultado1;
            }

            return poliza;

        }


        #endregion

        #region "Insertar"

        public async Task<bool> CreatePoliza(PolizaDTOCreate poliza)
        {
            bool respuesta1 = false;

            var poliza1 = new HttpClient();
            poliza1.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(poliza), Encoding.UTF8, "application/json");
            var response = await poliza1.PostAsync($"api/Poliza/Insertar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta1 = true;
            }

            return respuesta1;
        }

        #endregion

        #region "Actualizar"
        public async Task<bool> UpdatePoliza(PolizaDTO poliza)
        {
            bool respuesta = false;

            var cata = new HttpClient();
            cata.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(poliza), Encoding.UTF8, "application/json");
            var response = await cata.PutAsync($"api/Poliza/Actualizar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        #endregion

        #region "Eliminar Catalogo"

        public async Task<bool> DeletePoliza(int id)
        {
            bool respuesta = false;

            var poliza = new HttpClient();
            poliza.BaseAddress = new Uri(_baseurl);

            var response = await poliza.DeleteAsync($"api/Poliza/Eliminar/{id}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        #endregion



    } // fin clase
} // fin namespace
