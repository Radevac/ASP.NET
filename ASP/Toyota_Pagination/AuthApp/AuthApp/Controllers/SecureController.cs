using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class SecureController : ControllerBase
    {
        [HttpGet("data")]
        public IActionResult GetSecureData()
        {
            return Ok(new { Message = "Це захищений ресурс, доступний лише авторизованим користувачам!" });
        }

        [HttpGet("admin-data")]
        [Authorize(Roles = "Admin")] 
        public IActionResult GetAdminData()
        {
            return Ok(new { Message = "Це ресурс доступний лише для адміністраторів!" });
        }
    }
}