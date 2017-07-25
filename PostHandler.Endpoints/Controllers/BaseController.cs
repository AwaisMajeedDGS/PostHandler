namespace PostHandler.Endpoints.Controllers
{
    using PostHandler.Foundation.Configurations;
    //using PostHandler.Logger;
    //using PostHandler.MessagingQueue;
    using Foundation.Security;
    //using PostHandler.Serialization;
    using System;
    using System.Web.Http;
    using Model;
    using Models;

    public class BaseController : ApiController
    {
        protected APISettings _apiSettings;
        protected QueueLogger _logger;
        //protected QueueLogger _logger;

        //protected readonly UrlSettings _urlSettings;       
        public BaseController()
        {
            _apiSettings = APIConfigurationManager.Current.APISettings;            
            //_urlSettings = UrlConfigurationManager.Current.UrlSettings;
        }

        protected bool ValidateModelObject(string val1, string paramFor = "obj")
        {
            if (!ParamGuard.ArgumentNotNullOrWhiteSpace(val1))
            {
                ModelState.AddModelError(paramFor, new Exception(paramFor + " is null or whitespace") { });
                return false;
            }
            else
            {
                return true;
            }
        }

        protected bool ValidateModelObject(int val1, string paramFor = "obj")
        {
            if (val1.Equals(0))
            {
                ModelState.AddModelError(paramFor, new Exception(paramFor + " is not provided") { });
                return false;
            }
            else
            {
                return true;
            }
        }

        protected bool ValidateModelObject(object val1, string paramFor = "obj")
        {
            if (val1.Equals(0))
            {
                ModelState.AddModelError(paramFor, new Exception(paramFor + " is not provided") { });
                return false;
            }
            else
            {
                return true;
            }
        }

        protected bool ValidateModelObjectNotNull(object obj, string parameterValue)
        {
            if (ParamGuard.ArgumentIsNull(obj, parameterValue))
            {
                ModelState.AddModelError(parameterValue, new Exception(parameterValue + "is null") { });
                return false;
            }
            else
            {
                return true;
            }
        }                
    }
}
