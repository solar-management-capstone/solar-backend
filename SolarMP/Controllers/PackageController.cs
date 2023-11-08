using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Package;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private IPackage service;
        public PackageController(IPackage Service)
        {
            this.service = Service;
        }

        [AllowAnonymous]
        [Route("get-Package")]
        [HttpGet]
        public async Task<IActionResult> getall()
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
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

        [Authorize(Roles = "1, 2")]
        [Route("get-Package-admin")]
        [HttpGet]
        public async Task<IActionResult> getallAdmin()
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
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

        /// <summary>
        /// truyền vào số nhiêu thì nó trả ra bây nhiêu data
        /// 
        /// các gói đặc biệt này hiển thị theo gói nào dc tạo hợp đồng nhiều
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [Route("get-Package-mobileSpecial")]
        [HttpGet]
        public async Task<IActionResult> getallSpecial(int count)
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
            try
            {
                responseAPI.Data = await this.service.getAllForMobile(count);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("get-name")]
        [HttpGet]
        public async Task<IActionResult> getName(string name)
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
            try
            {
                responseAPI.Data = await this.service.getByName(name);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [Route("sort")]
        [HttpGet]
        public async Task<IActionResult> sorf([FromQuery] int? roofArea, decimal? electricBill)
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
            try
            {
                responseAPI.Data = await this.service.SortPck(roofArea, electricBill);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        //[Authorize(Roles = "1, 2")]
        [Route("get-id")]
        [HttpGet]
        public async Task<IActionResult> getId(string id)
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
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

        [Authorize(Roles = "1, 2")]
        [Route("Insert-Package")]
        [HttpPost]
        public async Task<IActionResult> Insert(PackageCreateDTO dto)
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
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

        [Authorize(Roles = "1, 2")]
        [Route("Insert-product-package")]
        [HttpPost]
        public async Task<IActionResult> InsertProduct(PackageProductCreateDTO dto)
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
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

        [Authorize(Roles = "1, 2")]
        [Route("update-Package")]
        [HttpPut]
        public async Task<IActionResult> delete(PackageUpdateDTO dto)
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
            try
            {
                responseAPI.Data = await this.service.update(dto);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Authorize(Roles = "1, 2")]
        [Route("delete-Package")]
        [HttpDelete]
        public async Task<IActionResult> delete(string dto)
        {
            ResponseAPI<List<Package>> responseAPI = new ResponseAPI<List<Package>>();
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
