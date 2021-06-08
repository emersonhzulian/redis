using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApresentacaoRedis.API.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;

namespace ApresentacaoRedis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteCacheStaticController : ControllerBase
    {
        public static int? staticVariable = null;

        [HttpGet]
        public async Task<int> Get()
        {
            if(staticVariable is null)
            {
                staticVariable = RealizaMuitoProcessamento();
            }

            return staticVariable.Value;
        }

        private int? RealizaMuitoProcessamento()
        {
            Thread.Sleep(5000);
            var rng = new Random();

            return rng.Next(1, 100); 
        }
    }
}
