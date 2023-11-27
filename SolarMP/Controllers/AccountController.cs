using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Account;
using SolarMP.DTOs.Product;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccount _service;
        public AccountController(IAccount Service)
        {
            this._service = Service;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "1, 2, 3, 4")]
        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.getAll();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Authorize(Roles = "1, 2, 3, 4")]
        [Route("get-name")]
        [HttpGet]
        public async Task<IActionResult> getName(string name)
        {
            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.getByName(name);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Authorize(Roles = "1, 2, 3, 4")]
        [Route("get-id")]
        [HttpGet]
        public async Task<IActionResult> getId(string id)
        {
            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.getById(id);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        /// <summary>
        /// role 1: admin
        /// role 2: owner
        /// role 3: staff
        /// role 4: customer
        /// role 5: consultant
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>
        /// </returns>
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> register(AccountRegisterDTO dto)
        {

            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.register(dto);
                responseAPI.Message = "success";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [AllowAnonymous]
        [Route("update-Account")]
        [HttpPut]
        public async Task<IActionResult> update(AccountUpdateDTO dto)
        {
            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.update(dto);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [AllowAnonymous]
        [Route("ChangePass")]
        [HttpPut]
        public async Task<IActionResult> ChangePass(ChangePassDTO dto)
        {
            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.changePass(dto);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Authorize(Roles = "1")]
        [Route("delete-Account")]
        [HttpDelete]
        public async Task<IActionResult> delete(string dto)
        {
            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.delete(dto);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("delete-Account-hard")]
        [HttpDelete]
        public async Task<IActionResult> deleteHard(string dto)
        {
            ResponseAPI<List<Account>> responseAPI = new ResponseAPI<List<Account>>();
            try
            {
                responseAPI.Data = await this._service.deleteHardCode(dto);
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
