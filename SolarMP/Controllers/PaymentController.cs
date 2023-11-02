using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Payment;
using SolarMP.DTOs.Product;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPayment service;
        public PaymentController(IPayment Service)
        {
            this.service = Service;
        }

        [Route("get-payment")]
        [HttpGet]
        public async Task<IActionResult> getall()
        {
            ResponseAPI<List<PaymentProcess>> responseAPI = new ResponseAPI<List<PaymentProcess>>();
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

        [Route("get-payment-contract")]
        [HttpGet]
        public async Task<IActionResult> getallcontract(string contractId)
        {
            ResponseAPI<List<PaymentProcess>> responseAPI = new ResponseAPI<List<PaymentProcess>>();
            try
            {
                responseAPI.Data = await this.service.getAllContract(contractId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("get-payment-user")]
        [HttpGet]
        public async Task<IActionResult> getalluser(string cusId)
        {
            ResponseAPI<List<PaymentProcess>> responseAPI = new ResponseAPI<List<PaymentProcess>>();
            try
            {
                responseAPI.Data = await this.service.getAllUser(cusId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("Insert-payment")]
        [HttpPost]
        public async Task<IActionResult> Insert(PaymentDTO dto)
        {
            ResponseAPI<List<PaymentProcess>> responseAPI = new ResponseAPI<List<PaymentProcess>>();
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
    }
}
