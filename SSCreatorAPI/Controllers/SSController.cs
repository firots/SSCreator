using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCreator;


namespace SSCreatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SSController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "SSCreator Working" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task <ActionResult> Post([FromBody] SSBody body)
        {
           try {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var task = Task.Run(() => {
                    using (SSGenerator SSG = new SSGenerator(body.model)) {
                        SSG.generate();
                    }
                });
                await task;
                stopwatch.Stop();
                Console.WriteLine("whole process took " + (Convert.ToDecimal(stopwatch.ElapsedMilliseconds) / 1000) + " seconds.");
                return Ok();
            } catch {
                Console.WriteLine("Cannot generate ss from given json.");
                return StatusCode(500, "check json file.");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
