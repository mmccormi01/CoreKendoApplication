using Microsoft.AspNetCore.Mvc;
using CoreKendoApplicationService;

namespace CoreKendoApplicationWeb
{
    [Route("api/Default")]
    [ApiController]
    public class DefaultAPIController : ControllerBase
    {
        public DefaultAPIController()
        {
            LogHelper.Debug("Instantiating DefaultAPIController");
        }

        [HttpGet]
        [Route("GetHelloWorld")]
        public string GetHelloWorld(string name)
        {
            LogHelper.Debug("Name is " + name);

            DefaultService defaultService = new DefaultService();
            return defaultService.GetHelloWorld(name);
        }
    }
}
