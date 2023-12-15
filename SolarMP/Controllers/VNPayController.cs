using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarMP.Models;
using SolarMP.Services;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        protected readonly solarMPContext context;
        public string VNPAY_TMNCODE = "B406HZGA";
        public string VNPAY_HASH_SECRECT = "GQUAWUAEWSHKGQSBPCTSWMUWIAEFSUYV";
        public string VNPAY_VERSION = "2.0.0";

        public VNPayController(IConfiguration configuration , solarMPContext context)
        {
            _configuration = configuration;
            this.context = context;
        }

        /// <summary>
        /// [Guest] Endpoint for company create url payment with condition
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string paymentId)
        {
            try
            {
                var check = await this.context.PaymentProcess.Where(x => x.PaymentId.Equals(paymentId)).FirstOrDefaultAsync();
                if (check != null)
                {
                    string ip = "256.256.256.1";
                    string url = _configuration["VnPay:Url"];
                    string returnUrl = _configuration["VnPay:ReturnAdminPath"];
                    string tmnCode = _configuration["VnPay:TmnCode"];
                    string hashSecret = _configuration["VnPay:HashSecret"];
                    VnPayLibrary pay = new VnPayLibrary();

                    pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
                    pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
                    pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
                    pay.AddRequestData("vnp_Amount", check.Amount.ToString("F").TrimEnd('0').TrimEnd('.').TrimEnd(',') + "00"); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
                    pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
                    pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
                    pay.AddRequestData("vnp_IpAddr", ip); //Địa chỉ IP của khách hàng thực hiện giao dịch
                    pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
                    //pay.AddRequestData("vnp_OrderInfo", "ĐASADASOOPAO23SDSD"); //Thông tin mô tả nội dung thanh toán
                    pay.AddRequestData("vnp_OrderInfo", "Thanh toán qua solarMP");
                    pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
                    pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
                    // tách hóa đơn ra để thêm vào db
                    string taxVNPay = DateTime.Now.Ticks.ToString();
                    pay.AddRequestData("vnp_TxnRef", taxVNPay); //mã hóa đơn
                    pay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss")); //Thời gian kết thúc thanh toán
                    string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

                    // update db
                    check.TaxVnpay = taxVNPay;
                    this.context.PaymentProcess.Update(check);
                    if (await this.context.SaveChangesAsync() > 0)
                    {
                        return Ok(paymentUrl);
                    }
                    else
                    {
                        throw new Exception("Lỗi trong quá trình lưu vào cơ sở dữ liệu");
                    }

                }
                else
                {
                    throw new Exception("không tồn tại donate id");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// [Guest] Endpoint for company confirm payment with condition
        /// </summary>
        /// <returns></returns>
        [HttpGet("PaymentConfirm")]
        public async Task<IActionResult> Confirm()
        {
            string returnUrl = _configuration["VnPay:ReturnPath"];
            float amount = 0;
            string status = "failed";
            if (Request.Query.Count > 0)
            {
                string vnp_HashSecret = _configuration["VnPay:HashSecret"]; //Secret key
                var vnpayData = Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();
                foreach (string s in vnpayData.Keys)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //Lay danh sach tham so tra ve tu VNPAY
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                float vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                amount = vnp_Amount;
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.Query["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
                var vnp_TransDate = vnpay.GetResponseData("vnp_PayDate");
                //Guid companyId = Guid.Parse(vnp_OrderInfo);
                if(vnp_ResponseCode == "00")
                {
                    status = "success";
                }
                else
                {
                    return Redirect(returnUrl +"?status=" + status);
                }
                

                string taxVNPay = orderId.ToString();
                var check = await this.context.PaymentProcess.Where(x => x.TaxVnpay.Equals(taxVNPay)).FirstOrDefaultAsync();
                check.Status = status;
                check.PayDate = DateTime.Now;
                check.PayDateVnpay = vnp_TransDate;
                this.context.PaymentProcess.Update(check);
                await this.context.SaveChangesAsync();

                if(check.IsDeposit == false)
                {
                    var contract = await this.context.ConstructionContract
                    .Where(x => x.ConstructioncontractId.Equals(check.ConstructionContractId)).FirstOrDefaultAsync();
                    if (contract != null && contract.SurveyId != null)
                    {
                        var survey = await this.context.Survey.Where(x => x.SurveyId.Equals(contract.SurveyId)).FirstOrDefaultAsync();
                        if (survey.RequestId != null)
                        {
                            var request = await this.context.Request.Where(x => x.RequestId.Equals(survey.RequestId)).FirstOrDefaultAsync();
                            if (request != null)
                            {
                                request.Status = false;
                                this.context.Request.Update(request);
                                await this.context.SaveChangesAsync();
                            }
                        }
                        survey.Status = false;
                        this.context.Survey.Update(survey);
                        await this.context.SaveChangesAsync();
                    }
                }           
            }

            return Redirect(returnUrl + "?amount=" + amount + "&status=" + status);
        }
    }
}
