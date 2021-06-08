using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApresentacaoRedis.API.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace ApresentacaoRedis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteCacheMemoryCacheController : ControllerBase
    {

        //Marcoratti    
        [HttpGet]
        public async Task<int> Get([FromServices] IMemoryCache cache)
	    {
            var valorCache = cache.GetOrCreate("MeuCacheKey",      
                entry =>
                {
                    //Quanto tempo o cache deve ficar armazenado (independente do tempo que se passou desde a última utilização)
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

                    //Quanto tempo o cache deve ficar armazenado (independente do tempo que se passou desde a última utilização)
                    entry.SlidingExpiration = TimeSpan.FromSeconds(20);

                    //Valor a ser armazenado no cache
                    return RealizaMuitoProcessamento();
	            }
            );
            
            return valorCache;
        }

        private int RealizaMuitoProcessamento()
        {
            Thread.Sleep(5000);
            var rng = new Random();

            return rng.Next(1, 100); 
        }
    }
}
