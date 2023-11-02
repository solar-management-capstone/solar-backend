using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Feedback;
using SolarMP.DTOs.Package;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private IFeedback service;
        public FeedbackController(IFeedback Service)
        {
            this.service = Service;
        }

        [AllowAnonymous]
        [Route("get-feedback")]
        [HttpGet]
        public async Task<IActionResult> getall()
        {
            ResponseAPI<List<Feedback>> responseAPI = new ResponseAPI<List<Feedback>>();
            try
            {
                responseAPI.Data = await this.service.getAll();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [AllowAnonymous]
        [Route("get-feedback-package")]
        [HttpGet]
        public async Task<IActionResult> getpck(string packageId)
        {
            ResponseAPI<List<Feedback>> responseAPI = new ResponseAPI<List<Feedback>>();
            try
            {
                responseAPI.Data = await this.service.getAllPack(packageId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("Insert-feedback")]
        [HttpPost]
        public async Task<IActionResult> Insert(FeedbackCreateDTO dto)
        {
            ResponseAPI<List<Feedback>> responseAPI = new ResponseAPI<List<Feedback>>();
            try
            {
                responseAPI.Data = await this.service.insert(dto);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("delete-feedback")]
        [HttpDelete]
        public async Task<IActionResult> delete(string dto)
        {
            ResponseAPI<List<Feedback>> responseAPI = new ResponseAPI<List<Feedback>>();
            try
            {
                responseAPI.Data = await this.service.delete(dto);
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
