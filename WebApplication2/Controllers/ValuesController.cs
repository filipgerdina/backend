using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        private static List<Values> values = new List<Values>
        {
            new Values { Id = 1, Name = "Value1", Value = "Value1" },
            new Values { Id = 2, Name = "Value2", Value = "Value2" },
            new Values { Id = 3, Name = "Value3", Value = "Value3" },
            new Values { Id = 4, Name = "Value4", Value = "Value4" },
            new Values { Id = 5, Name = "Value5", Value = "Value5" }
        };

        [HttpGet(Name = "GetValues")]
        public IEnumerable<Values> Get()
        {
            return values; 
        }

        [HttpPost(Name = "PostValues")]
        public IActionResult Post([FromBody] ValuesPost value)
        {
            var rand = new Random();
            values.Add(new Values { Id = rand.Next(), Name = value.Name, Value = value.Value });
            return Ok();
        }

        [HttpDelete(Name = "DeleteValue")]
        public IActionResult Delete(int id)
        {
            values.RemoveAt(values.FindIndex(el => el.Id.Equals(id)));
            return Ok();
        }

    }
}
