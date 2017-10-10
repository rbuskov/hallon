using System.Web.Http;

namespace Hallon.Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.Add(new HalMediaTypeFormatter(new HalConfig()));
        }
    }
}