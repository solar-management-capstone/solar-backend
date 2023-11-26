using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Account;
using SolarMP.DTOs.Account.Team;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private IAccount _service;
        public TeamController(IAccount Service)
        {
            this._service = Service;
        }

        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            ResponseAPI<List<Team>> responseAPI = new ResponseAPI<List<Team>>();
            try
            {
                responseAPI.Data = await this._service.getAllMember();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-staff-not-have-team")]
        [HttpGet]
        public async Task<IActionResult> GetStaff()
        {
            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.staffLeadNotTeam();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("get-all-member-by-staff")]
        [HttpGet]
        public async Task<IActionResult> GetAllMem(string StaffId)
        {

            ResponseAPI<List<Team>> responseAPI = new ResponseAPI<List<Team>>();
            try
            {
                responseAPI.Data = await this._service.getMemberStaff(StaffId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> search([FromQuery]string? name, string? phone, string? email)
        {

            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.search(name, phone, email);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("add-team")]
        [HttpPost]
        public async Task<IActionResult> register(TeamDTO dto)
        {

            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.addTeam(dto);
                responseAPI.Message = "success";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> delete([FromQuery] string leadId, string memberId)
        {

            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.deleteMember(leadId,memberId);
                responseAPI.Message = "success";
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
