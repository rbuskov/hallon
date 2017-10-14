using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Hallon.Demo.Models;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api/pocos")]
    public class PocosController : ApiController
    {
        private static readonly List<Poco> Pocos = new List<Poco>();

        static PocosController()
        {
            Pocos.Add(new Poco() { Id = 1, Date = new DateTime(2017, 10, 1) });
            Pocos.Add(new Poco() { Id = 2, Date = new DateTime(2017, 10, 3) });
            Pocos.Add(new Poco() { Id = 3, Date = new DateTime(2017, 10, 5) });
        }

        [HttpGet, Route("{id}")]
        public Poco Get(int id)
        {
            return Pocos.FirstOrDefault(o => o.Id == id);
        }

        [HttpGet, Route("")]
        public IEnumerable<Poco> Get()
        {
            return Pocos;
        }
    }
}
