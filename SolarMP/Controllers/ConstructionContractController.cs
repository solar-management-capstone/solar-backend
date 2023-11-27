using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SolarMP.DTOs;
using SolarMP.DTOs.Acceptances;
using SolarMP.DTOs.ConstructionContract;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionContractController : ControllerBase
    {
        private IConstructionContract service;
        public ConstructionContractController(IConstructionContract service)
        {
            this.service = service;
        }
        [Route("get-all-Construction-Contract")]
        [HttpGet]
        public async Task<IActionResult> GetAllConstructionContracts()
        {
            ResponseAPI<List<ConstructionContract>> responseAPI = new ResponseAPI<List<ConstructionContract>>();
            try
            {
                responseAPI.Data = await this.service.GetAllConstructionContracts();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-Construction-Contract-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetConstructionContractById(string? constructionContractId)
        {
            ResponseAPI<List<ConstructionContract>> responseAPI = new ResponseAPI<List<ConstructionContract>>();
            try
            {
                responseAPI.Data = await this.service.GetConstructionContractById(constructionContractId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-Construction-Contract-by-staffid")]
        [HttpGet]
        public async Task<IActionResult> GetConstructionContractStaff(string staffId)
        {
            ResponseAPI<List<ConstructionContract>> responseAPI = new ResponseAPI<List<ConstructionContract>>();
            try
            {
                responseAPI.Data = await this.service.GetConstructionContractByStaffId(staffId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-Construction-Contract-by-Customerid")]
        [HttpGet]
        public async Task<IActionResult> GetConstructionContractByCus([FromQuery] string customerId , string? status)
        {
            ResponseAPI<List<ConstructionContract>> responseAPI = new ResponseAPI<List<ConstructionContract>>();
            try
            {
                responseAPI.Data = await this.service.GetConstructionContractByCusId(customerId,status);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-Construction-Contract-by-Customeridv2")]
        [HttpPost]
        public async Task<IActionResult> GetConstructionContractByCusv2(CusManyStatusDTO dto)
        {
            ResponseAPI<List<ConstructionContract>> responseAPI = new ResponseAPI<List<ConstructionContract>>();
            try
            {
                responseAPI.Data = this.service.GetAllConstructionContractsByCusIDv2(dto);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// 0: Đã hủy
        /// 1: Chờ duyệt 
        /// 2: Hoạt động
        /// 3: Nghiệm thu
        /// 4: Hoàn thành
        /// </summary>
        /// <param name="upConstructionContract"></param>
        /// <returns></returns>
        [Route("Update-construction-contract-with-id")]
        [HttpPut]
        public async Task<IActionResult> UpdateConstructionContract(ConstructionContractUpdateDTO upConstructionContract)
        {
            ResponseAPI<List<ConstructionContract>> responseAPI = new ResponseAPI<List<ConstructionContract>>();
            try
            {
                responseAPI.Data = await this.service.UpdateConstructionContract(upConstructionContract);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// 0: Đã hủy
        /// 1: Chờ duyệt 
        /// 2: Hoạt động
        /// 3: Nghiệm thu
        /// 4: Hoàn thành
        /// </summary>
        /// <param name="constructionContract"></param>
        /// <returns></returns>
        [Route("Insert-Construction-contract")]
        [HttpPost]
        public async Task<IActionResult> InsertConstructionContract(ConstructionContractDTO constructionContract)
        {
            ResponseAPI<List<ConstructionContract>> responseAPI = new ResponseAPI<List<ConstructionContract>>();
            try
            {
                responseAPI.Data = await this.service.InsertConstructionContract(constructionContract);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("Delete-Construction-contract")]
        [HttpDelete]
        public async Task<IActionResult> DeleteConstructionContract(string constructionContractId)
        {
            ResponseAPI<List<ConstructionContract>> responseAPI = new ResponseAPI<List<ConstructionContract>>();
            try
            {
                responseAPI.Data = await this.service.DeleteConstructionContract(constructionContractId);
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
