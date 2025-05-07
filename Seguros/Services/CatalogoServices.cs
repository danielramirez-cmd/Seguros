using System.Text;
using Newtonsoft.Json;
using Seguros.IServices;
using Seguros.Models;
using SegurosAPI.DTOs;


namespace Seguros.Services
{
    public class CatalogoServices : ICatalogosServices
    {
        private static string _baseurl;

        public CatalogoServices()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseurl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        #region "Obtener"
        public async Task<List<CatalogoDTO>> GetAllCatalogo()
        {
            if (string.IsNullOrEmpty(_baseurl))
            {
                throw new ArgumentException("La URL base no puede ser nula o vacía");
            }

            List<CatalogoDTO> lista = new List<CatalogoDTO>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseurl);
                var response = await httpClient.GetAsync($"/api/Catalogo");

                if (response.IsSuccessStatusCode)
                {
                    var jsonRespuesta = await response.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<CatalogoDTO>>(jsonRespuesta);
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
        public async Task<CatalogoDTO> GetCatalogoById(int id)
        {

            CatalogoDTO catalogo = new CatalogoDTO();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/Catalogo/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado1 = JsonConvert.DeserializeObject<CatalogoDTO>(json_respuesta);
                catalogo = resultado1;
            }

            return catalogo;

        }


        #endregion

        #region "Insertar"

        public async Task<bool> CreateCatalogo(CatalogoDTOCreate catalogo)
        {
            bool respuesta1 = false;

            var catalogo1 = new HttpClient();
            catalogo1.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(catalogo), Encoding.UTF8, "application/json");
            var response = await catalogo1.PostAsync($"api/Catalogo/Insertar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta1 = true;
            }
            
            return  respuesta1;
        }

        #endregion

        #region "Actualizar"
        public async Task<bool> UpdateCatalogo(CatalogoDTO catalogo)
        {
            bool respuesta = false;

            var cata = new HttpClient();
            cata.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(catalogo), Encoding.UTF8, "application/json");
            var response = await cata.PutAsync($"api/Catalogo/Actualizar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        #endregion

        #region "Eliminar Catalogo"

        public async Task<bool> DeleteCatalogo(int id)
        {
            bool respuesta = false;

            var catalogo = new HttpClient();
            catalogo.BaseAddress = new Uri(_baseurl);

            var response = await catalogo.DeleteAsync($"api/Catalogo/Eliminar/{id}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        #endregion








    } // fin CatalogoServices
} // fin namespace
