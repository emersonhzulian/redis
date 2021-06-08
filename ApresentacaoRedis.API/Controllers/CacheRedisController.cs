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
    public class CacheRedisController : ControllerBase
    {
        [HttpGet]
        public async Task<int> Get([FromServices] IDistributedCache cache)
        {
            int result = 0;

            if(cache.TryGetRecord<int>("testeEmerson", out result))
            {
                return result;
            }
            else
            {
                result = RealizaMuitoProcessamento();
                
                await cache.SetRecordAsync<int>("testeEmerson", result);
            }

            return result;
        }

        private int RealizaMuitoProcessamento()
        {
            Thread.Sleep(5000);
            var rng = new Random();

            return rng.Next(1, 100); 
        }
    }
}
