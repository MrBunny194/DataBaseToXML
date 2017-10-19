using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DbToXML.Controllers
{
    public class CultureController : Controller
    {
        public ActionResult ChangeCulture()
        {
            var returnUrl = Request.UrlReferrer.AbsolutePath; // Получаем текущим URL
            var lang = Request.Form["lang"];
            var cookie = Request.Cookies["lang"]; // Сохраняем выбранную культуру в куки

            if (cookie != null)
            {
                cookie.Value = lang;   // обновляем значение
            }
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}