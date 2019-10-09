using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodeGeneration.Controllers
{
    [Route("api/ERPGeneration")]
    [ApiController]
    public class ERPController : ControllerBase
    {
        public const string Entities = "Entities";
        // GET api/values
        [HttpGet]
        public void Get()
        {
            List<Type> types = typeof(ERPController)
            .Assembly.GetTypes()
            .Where(t => t.Name.EndsWith("DAO") && !t.IsAbstract).ToList();

            if (!Directory.Exists(Entities))
                Directory.CreateDirectory(Entities);

            EntityGeneration EntityGeneration = new EntityGeneration("ERP", types);
            EntityGeneration.Build();
            RepositoryGeneration RepositoryGeneration = new RepositoryGeneration("ERP", "ERPContext", types);
            RepositoryGeneration.Build();
            return;
        }
    }
}
