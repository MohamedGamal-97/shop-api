﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourStoreApi.Errors;

namespace YourStoreApi.Controllers
{

    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
