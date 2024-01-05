using Jabil.Pico.Web.BLL.Services;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Jabil.Pico.Web.Common
{
    public class ApiAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public string ApiKeyName { get; set; } = "api-key";

        public override async void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Any(x => ApiKeyName.Equals(x.Key)))
            {
                var settingService = DependencyResolver.Current.GetService<ISettingService>();
                var actualApiKey = actionContext.Request.Headers.GetValues(ApiKeyName).FirstOrDefault();
                var expectedApiKey = await settingService.GetByNameAsync(ApiKeyName);
                if (string.IsNullOrEmpty(actualApiKey) || !actualApiKey.Equals(expectedApiKey.Value))
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}