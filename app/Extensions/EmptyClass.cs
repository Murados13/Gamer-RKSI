using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using Microsoft.AspNetCore.Http;

namespace app.Extensions
{
    public static class ControllerExtensions
    {
        public static String ClaimSid(this Controller controller)
        {
            try
            {
                var claim = controller.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
                return claim?.Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
