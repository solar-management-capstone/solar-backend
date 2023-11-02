using Microsoft.AspNetCore.Mvc;
using SolarMP.DTOs;
using SolarMP.DTOs.Promotions;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private IPromotion service;
        public PromotionController(IPromotion service)
        {
            this.service = service;
        }
        [Route("getall-promotion")]
        [HttpGet]
        public async Task<IActionResult> getall()
        {
            ResponseAPI<List<Promotion>> responseAPI = new ResponseAPI<List<Promotion>>();
            try
            {
                responseAPI.Data = await this.service.GetAllPromotions();
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Get promotionID
        /// </summary>
        /// <param name="promotionId"></param>
        /// <returns></returns>
        [Route("get-promotion-id")]
        [HttpGet]

        public async Task<IActionResult> GetPromotionById(string? promotionId)
        {
            ResponseAPI<List<Promotion>> responseAPI = new ResponseAPI<List<Promotion>>();
            try
            {
                responseAPI.Data = await this.service.GetPromotionById(promotionId);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// update Promotion by id
        /// </summary>
        /// <param name="upPromotion"></param>
        /// <returns></returns>
        [Route("Update-promotion-by-id")]
        [HttpPut]
        public async Task<IActionResult> UpdatePromotion(PromotionUpdateDTO upPromotion)
        {
            ResponseAPI<List<Promotion>> responseAPI = new ResponseAPI<List<Promotion>>();
            try
            {
                responseAPI.Data = await this.service.UpdatePromotion(upPromotion);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Id auto generate
        /// </summary>
        /// <param name="promotion"></param>
        /// <returns></returns>
        [Route("Insert-promotion")]
        [HttpPost]
        public async Task<IActionResult> InsertPromotion(PromotionDTO promotion)
        {
            ResponseAPI<List<Promotion>> responseAPI = new ResponseAPI<List<Promotion>>();
            try
            {
                responseAPI.Data = await this.service.InsertPromotion(promotion);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        /// <summary>
        /// Delete promotion by id
        /// </summary>
        /// <param name="promotion"></param>
        /// <returns></returns>
        [Route("Delete-promotion")]
        [HttpDelete]
        public async Task<IActionResult> DeletePromotion(string promotionId)
        {
            ResponseAPI<List<Promotion>> responseAPI = new ResponseAPI<List<Promotion>>();
            try
            {
                responseAPI.Data = await this.service.DeletePromotion(promotionId);
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
