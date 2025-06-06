﻿using Seguros.DTOs;
using SegurosAPI.DTOs;

namespace Seguros.IServices
{
    public interface IPolizaServices
    {
        Task<List<PolizaDTO>> GetAllPoliza();
        Task<PolizaDTO> GetPolizaById(int id);
        Task<bool> CreatePoliza(PolizaDTOCreate poliza);
        Task<bool> UpdatePoliza(PolizaUpdateDTO poliza);
        Task<bool> DeletePoliza(int id);





    } // fin Interfaz 
} // fin namespace
