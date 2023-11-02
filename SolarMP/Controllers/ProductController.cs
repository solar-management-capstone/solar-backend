using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs.Package;
using SolarMP.DTOs;
using SolarMP.Interfaces;
using SolarMP.Models;
using SolarMP.DTOs.Product;
using System.Data;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProduct service;
        public ProductController(IProduct Service)
        {
            this.service = Service;
        }

        [AllowAnonymous]
        [Route("get-Product")]
        [HttpGet]
        public async Task<IActionResult> getall()
        {
            ResponseAPI<List<Product>> responseAPI = new ResponseAPI<List<Product>>();
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
        [Route("get-Product-admin")]
        [HttpGet]
        public async Task<IActionResult> getallAdmin()
        {
            ResponseAPI<List<Product>> responseAPI = new ResponseAPI<List<Product>>();
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



        [Route("get-name")]
        [HttpGet]
        public async Task<IActionResult> getName(string name)
        {
            ResponseAPI<List<Product>> responseAPI = new ResponseAPI<List<Product>>();
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

        [Authorize(Roles = "1, 2")]
        [Route("get-id")]
        [HttpGet]
        public async Task<IActionResult> getId(string id)
        {
            ResponseAPI<List<Product>> responseAPI = new ResponseAPI<List<Product>>();
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
        [Route("Insert-Product")]
        [HttpPost]
        public async Task<IActionResult> Insert(ProductCreateDTO dto)
        {
            ResponseAPI<List<Product>> responseAPI = new ResponseAPI<List<Product>>();
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

        [Route("update-Product")]
        [HttpPut]
        public async Task<IActionResult> update(ProductUpdateDTO dto)
        {
            ResponseAPI<List<Product>> responseAPI = new ResponseAPI<List<Product>>();
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
        [Route("delete-Product")]
        [HttpDelete]
        public async Task<IActionResult> delete(string dto)
        {
            ResponseAPI<List<Product>> responseAPI = new ResponseAPI<List<Product>>();
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
