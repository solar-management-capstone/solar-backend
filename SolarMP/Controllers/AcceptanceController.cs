using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Acceptances;
using SolarMP.DTOs.Promotions;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcceptanceController : ControllerBase
    {
        private IAcceptance service;
        public AcceptanceController(IAcceptance service)
        {
            this.service = service;
        }
        [Route("get-all-Acceptance")]
        [HttpGet]
        public async Task<IActionResult> GetAllAcceptances()
        {
            ResponseAPI<List<Acceptance>> responseAPI = new ResponseAPI<List<Acceptance>>();
            try
            {
                responseAPI.Data = await this.service.GetAllAcceptances();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-all-Acceptance-admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAcceptancesad()
        {
            ResponseAPI<List<Acceptance>> responseAPI = new ResponseAPI<List<Acceptance>>();
            try
            {
                responseAPI.Data = await this.service.GetAllAcceptancesAD();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Get acceptance by id
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        [Route("get-acceptance-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetAcceptanceById(string? acceptanceId)
        {
            ResponseAPI<List<Acceptance>> responseAPI = new ResponseAPI<List<Acceptance>>();
            try
            {
                responseAPI.Data = await this.service.GetAcceptanceById(acceptanceId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// update acceptance by id
        /// </summary>
        /// <param name="upAcceptance"></param>
        /// <returns></returns>
        [Route("Update-acceptance-by-id")]
        [HttpPut]
        public async Task<IActionResult> UpdateAcceptance(AcceptanceUpdateDTO upAcceptance)
        {
            ResponseAPI<List<Acceptance>> responseAPI = new ResponseAPI<List<Acceptance>>();
            try
            {
                responseAPI.Data = await this.service.UpdateAcceptance(upAcceptance);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// ID auto generate with 10 character
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        [Route("Insert-acceptance")]
        [HttpPost]
        public async Task<IActionResult> InsertAcceptance(AcceptanceDTO acceptance)
        {
            ResponseAPI<List<Acceptance>> responseAPI = new ResponseAPI<List<Acceptance>>();
            try
            {
                responseAPI.Data = await this.service.InsertAcceptance(acceptance);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Delete acceptance by id
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        [Route("Delete-accetance")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAcceptance(string acceptanceId)
        {
            ResponseAPI<List<Acceptance>> responseAPI = new ResponseAPI<List<Acceptance>>();
            try
            {
                responseAPI.Data = await this.service.DeleteAcceptance(acceptanceId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

    }
}
