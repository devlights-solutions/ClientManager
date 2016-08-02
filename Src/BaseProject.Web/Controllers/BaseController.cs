using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Framework.Common.Web.ActionResults;

namespace BaseProject.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        public const int DefaultPageSize = 50;
        //protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
        //    where TController : Controller
        //{
        //    return ControllerExtensions.RedirectToAction(this, action);
        //}

        protected ImageResult Image(byte[] fileBytes, string contentType, int? width = null, int? height = null)
        {
            return new ImageResult(fileBytes, contentType, width, height);
        }
    }
}