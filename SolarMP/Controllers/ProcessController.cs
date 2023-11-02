using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Package;
using SolarMP.DTOs.Process;
using SolarMP.Interfaces;
using SolarMP.Models;
using System.Data;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private IProcess service;
        public ProcessController(IProcess service)
        {
            this.service = service;
        }

        [Route("get-process")]
        [HttpGet]
        public async Task<IActionResult> getall()
        {
            ResponseAPI<List<Process>> responseAPI = new ResponseAPI<List<Process>>();
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
        [Route("get-processid")]
        [HttpGet]
        public async Task<IActionResult> getid(string id)
        {
            ResponseAPI<List<Process>> responseAPI = new ResponseAPI<List<Process>>();
            try
            {
                responseAPI.Data = await this.service.getById(id);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-process-contract")]
        [HttpGet]
        public async Task<IActionResult> getallid(string id)
        {
            ResponseAPI<List<Process>> responseAPI = new ResponseAPI<List<Process>>();
            try
            {
                responseAPI.Data = await this.service.getAllContract(id);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("Insert-process")]
        [HttpPost]
        public async Task<IActionResult> Insert(ProcessCreateDTO dto)
        {
            ResponseAPI<List<Process>> responseAPI = new ResponseAPI<List<Process>>();
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

        [Route("delete-process")]
        [HttpDelete]
        public async Task<IActionResult> delete(string dto)
        {
            ResponseAPI<List<Process>> responseAPI = new ResponseAPI<List<Process>>();
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
