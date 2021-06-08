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
    public class RedisSimplesController : ControllerBase
    {
        [HttpGet]
        public async Task<int> Get([FromServices] IDistributedCache cache)
        {
            int result = 0;

            var cachedValue = cache.GetString("testeEmerson");

            if(cachedValue is null)
            {
                 result = RealizaMuitoProcessamento();
                 cache.SetString("testeEmerson", result.ToString(), 
                    new DistributedCacheEntryOptions()
                    {
                        //Quanto tempo o cache deve ficar armazenado (independente do tempo que se passou desde a última utilização)
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),

                        //Quanto tempo o cache deve ficar armazenado (independente do tempo que se passou desde a última utilização)
                        SlidingExpiration =  TimeSpan.FromSeconds(20)
                    }        
                );
            }
            else
            {
                result = int.Parse(cachedValue);
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
