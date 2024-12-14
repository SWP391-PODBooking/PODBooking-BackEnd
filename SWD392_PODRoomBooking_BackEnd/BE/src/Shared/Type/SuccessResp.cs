using BE.src.Shared.Constant;
using Microsoft.AspNetCore.Mvc;

namespace BE.src.Shared.Type
{
    public static class SuccessResp
    {
        public static IActionResult Ok(string? message)
        {
            return new JsonResult(new { Message = message ?? RespMsg.OK }) { StatusCode = RespCode.OK };
        }

        public static IActionResult Ok(object? data)
        {
            return new JsonResult(data) { StatusCode = RespCode.OK };
        }
        public static IActionResult Created(string? message)
        {
            return new JsonResult(new { Message = message ?? RespMsg.CREATED }) { StatusCode = RespCode.CREATED };
        }

        public static IActionResult Created(object? data)
        {
            return new JsonResult(data) { StatusCode = RespCode.CREATED };
        }

        public static IActionResult Redirect(string url)
        {
            return new RedirectResult(url, false);
        }
    }
}
