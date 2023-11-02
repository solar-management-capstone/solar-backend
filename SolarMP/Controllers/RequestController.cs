using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Product;
using SolarMP.DTOs.Request;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private IRequest service;
        public RequestController(IRequest Service)
        {
            this.service = Service;
        }

        [Route("get-request")]
        [HttpGet]
        public async Task<IActionResult> getall()
        {
            ResponseAPI<List<Request>> responseAPI = new ResponseAPI<List<Request>>();
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
        [Route("get-request-account")]
        [HttpGet]
        public async Task<IActionResult> getAcc(string CusId)
        {
            ResponseAPI<List<Request>> responseAPI = new ResponseAPI<List<Request>>();
            try
            {
                responseAPI.Data = await this.service.getAllForUser(CusId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-request-staff")]
        [HttpGet]
        public async Task<IActionResult> getRequestforStaff(string StaffId)
        {
            ResponseAPI<List<Request>> responseAPI = new ResponseAPI<List<Request>>();
            try
            {
                responseAPI.Data = await this.service.getAllForStaff(StaffId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-request-package")]
        [HttpGet]
        public async Task<IActionResult> getPCK(string pckId)
        {
            ResponseAPI<List<Request>> responseAPI = new ResponseAPI<List<Request>>();
            try
            {
                responseAPI.Data = await this.service.getAllForPackage(pckId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("Insert-request")]
        [HttpPost]
        public async Task<IActionResult> Insert(RequestCreateDTO dto)
        {
            ResponseAPI<List<Request>> responseAPI = new ResponseAPI<List<Request>>();
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

        [Route("assign-staff")]
        [HttpPut]
        public async Task<IActionResult> update(RequestUpdateDTO dto)
        {
            ResponseAPI<List<Request>> responseAPI = new ResponseAPI<List<Request>>();
            try
            {
                responseAPI.Data = await this.service.assignStaff(dto);
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
