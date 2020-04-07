using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalloWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EisApiController : ControllerBase
    {
        DataManager data = new DataManager();


        // GET: api/EisApi
        [HttpGet]
        public IEnumerable<Eis> Get()
        {
            return data.GetAll();
        }

        // GET: api/EisApi/5
        [HttpGet("{id}", Name = "Get")]
        public Eis Get(int id)
        {
            return data.GetById(id);
        }

        // POST: api/EisApi
        [HttpPost]
        public void Post([FromBody] Eis value)
        {
            data.Add(value);
        }

        // PUT: api/EisApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Eis value)
        {
            data.Update(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            data.Delete(data.GetById(id));
        }
    }
}
