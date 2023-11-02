using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Promotions;
using SolarMP.DTOs.Survey;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private ISurvey service;
        public SurveyController(ISurvey service)
        {
            this.service = service;
        }
        [Route("getall-survey")]
        [HttpGet]
        public async Task<IActionResult> GetAllSurveys()
        {
            ResponseAPI<List<Survey>> responseAPI = new ResponseAPI<List<Survey>>();
            try
            {
                responseAPI.Data = await this.service.GetAllSurveys();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Get surveyID
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        [Route("get-servey-id")]
        [HttpGet]
        public async Task<IActionResult> GetSurveyById(string? surveyId)
        {
            ResponseAPI<List<Survey>> responseAPI = new ResponseAPI<List<Survey>>();
            try
            {
                responseAPI.Data = await this.service.GetSurveyById(surveyId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-servey-staffid")]
        [HttpGet]
        public async Task<IActionResult> GetSurveyBystaffId(string staffId)
        {
            ResponseAPI<List<Survey>> responseAPI = new ResponseAPI<List<Survey>>();
            try
            {
                responseAPI.Data = await this.service.GetSurveyByIdstaff(staffId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Update surveyid
        /// </summary>
        /// <param name="upSurvey"></param>
        /// <returns></returns>
        [Route("Update-survey-by-id")]
        [HttpPut]
        public async Task<IActionResult> UpdateSurvey(SurveyUpdateDTO upSurvey)
        {
            ResponseAPI<List<Survey>> responseAPI = new ResponseAPI<List<Survey>>();
            try
            {
                responseAPI.Data = await this.service.UpdateSurvey(upSurvey);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// auto generate id
        /// </summary>
        /// <param name="promotion"></param>
        /// <returns></returns>
        [Route("Insert-survey")]
        [HttpPost]
        public async Task<IActionResult> InsertSurvey(SurveyDTO survey)
        {
            ResponseAPI<List<Survey>> responseAPI = new ResponseAPI<List<Survey>>();
            try
            {
                responseAPI.Data = await this.service.InsertSurvey(survey);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Delete survey with id
        /// </summary>
        /// <param name="promotionId"></param>
        /// <returns></returns>
        [Route("Delete-promotion")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSurvey(string surveyId)
        {
            ResponseAPI<List<Survey>> responseAPI = new ResponseAPI<List<Survey>>();
            try
            {
                responseAPI.Data = await this.service.DeleteSurvey(surveyId);
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
