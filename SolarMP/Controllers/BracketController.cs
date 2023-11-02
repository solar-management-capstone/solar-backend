using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Bracket;
using SolarMP.DTOs.Survey;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BracketController : ControllerBase
    {
        private IBracket service;
        public BracketController(IBracket service)
        {
            this.service = service;
        }
        [Route("get-all-bracket")]
        [HttpGet]
        public async Task<IActionResult> GetAllBrackets()
        {
            ResponseAPI<List<Bracket>> responseAPI = new ResponseAPI<List<Bracket>>();
            try
            {
                responseAPI.Data = await this.service.GetAllBrackets();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-all-bracket-admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBracketsAdmin()
        {
            ResponseAPI<List<Bracket>> responseAPI = new ResponseAPI<List<Bracket>>();
            try
            {
                responseAPI.Data = await this.service.GetAllBracketsAdmin();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// get Bracket with id 
        /// </summary>
        /// <param name="bracketId"></param>
        /// <returns></returns>
        [Route("get-bracket-id")]
        [HttpGet]
        public async Task<IActionResult> GetBracketById(string? bracketId)
        {
            ResponseAPI<List<Bracket>> responseAPI = new ResponseAPI<List<Bracket>>();
            try
            {
                responseAPI.Data = await this.service.GetBracketById(bracketId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Update bracket with id
        /// </summary>
        /// <param name="upSurvey"></param>
        /// <returns></returns>
        [Route("Update-bracket-by-id")]
        [HttpPut]
        public async Task<IActionResult> UpdateBracket(BracketUpdateDTO upBracket)
        {
            ResponseAPI<List<Bracket>> responseAPI = new ResponseAPI<List<Bracket>>();
            try
            {
                responseAPI.Data = await this.service.UpdateBracket(upBracket);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// auto generate id with 10 character
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        [Route("Insert-bracket")]
        [HttpPost]
        public async Task<IActionResult> InsertBracket(BracketDTO bracket)
        {
            ResponseAPI<List<Bracket>> responseAPI = new ResponseAPI<List<Bracket>>();
            try
            {
                responseAPI.Data = await this.service.InsertBracket(bracket);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Delete Bracket with id
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        [Route("Delete-bracket")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBracket(string bracketId)
        {
            ResponseAPI<List<Bracket>> responseAPI = new ResponseAPI<List<Bracket>>();
            try
            {
                responseAPI.Data = await this.service.DeleteBracket(bracketId);
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
