using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Package;
using SolarMP.DTOs.WarrantyReport;
using SolarMP.Interfaces;
using SolarMP.Models;
using System.Data;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyReportController : ControllerBase
    {
        private IWarrantyReport service;
        public WarrantyReportController(IWarrantyReport service)
        {
            this.service = service;
        }

        [Route("get-warranty")]
        [HttpGet]
        public async Task<IActionResult> getall()
        {
            ResponseAPI<List<WarrantyReport>> responseAPI = new ResponseAPI<List<WarrantyReport>>();
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
        [Route("get-warranty-admin")]
        [HttpGet]
        public async Task<IActionResult> getalladmin()
        {
            ResponseAPI<List<WarrantyReport>> responseAPI = new ResponseAPI<List<WarrantyReport>>();
            try
            {
                responseAPI.Data = await this.service.getAllForAdmin();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-warranty-customer")]
        [HttpGet]
        public async Task<IActionResult> getallcus(string cusId)
        {
            ResponseAPI<List<WarrantyReport>> responseAPI = new ResponseAPI<List<WarrantyReport>>();
            try
            {
                responseAPI.Data = await this.service.getAllForCus(cusId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-warranty-contract")]
        [HttpGet]
        public async Task<IActionResult> getallcontract(string contractId)
        {
            ResponseAPI<List<WarrantyReport>> responseAPI = new ResponseAPI<List<WarrantyReport>>();
            try
            {
                responseAPI.Data = await this.service.getAllForContract(contractId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("Insert-warranty")]
        [HttpPost]
        public async Task<IActionResult> Insert(WarrantyReportDTO dto)
        {
            ResponseAPI<List<WarrantyReport>> responseAPI = new ResponseAPI<List<WarrantyReport>>();
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

        [Route("Insert-product-warranty")]
        [HttpPost]
        public async Task<IActionResult> InsertProduct(ProductWarrantyDTO dto)
        {
            ResponseAPI<List<WarrantyReport>> responseAPI = new ResponseAPI<List<WarrantyReport>>();
            try
            {
                responseAPI.Data = await this.service.insertProduct(dto);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("delete-warranty")]
        [HttpDelete]
        public async Task<IActionResult> delete(string dto)
        {
            ResponseAPI<List<WarrantyReport>> responseAPI = new ResponseAPI<List<WarrantyReport>>();
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
