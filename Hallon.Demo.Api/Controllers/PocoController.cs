using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Hallon.Demo.Data;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class PocoController : ApiController
    {
        private static readonly List<Poco> Pocos = new List<Poco>();

        static PocoController()
        {
            Pocos.Add(new Poco() { Id = 1, Date = new DateTime(2017, 10, 1) });
            Pocos.Add(new Poco() { Id = 2, Date = new DateTime(2017, 10, 3) });
            Pocos.Add(new Poco() { Id = 3, Date = new DateTime(2017, 10, 5) });
        }

        [HttpGet, Route("pocos/{id}")]
        public Poco Get(int id)
        {
            return Pocos.FirstOrDefault(poco => poco.Id == id);
        }

        [HttpGet, Route("pocos")]
        public IEnumerable<Poco> Get()
        {
            return Pocos;
        }
    }
}
