using ei_back.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ei_back.Application.Api.FunTranslate
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunTranslateController : ControllerBase
    {
        private readonly IFunTranslateApiHttpService _externalApiHttpService;

        public FunTranslateController(IFunTranslateApiHttpService externalApiHttpService)
        {
            _externalApiHttpService = externalApiHttpService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, CommonUser")]
        public async Task<IActionResult> Get([FromBody] string textRequest, CancellationToken cancellationToken = default)
        {
            if (textRequest.IsNullOrEmpty()) return BadRequest();
            return Ok(await _externalApiHttpService.GetValyrianTranslate(textRequest));
        }
    }
}
