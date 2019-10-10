using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.App
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


            EntityGeneration EntityGeneration = new EntityGeneration("WeGift", types);
            EntityGeneration.Build();
            RepositoryGeneration RepositoryGeneration = new RepositoryGeneration("WeGift", "WGContext", types);
            RepositoryGeneration.Build();
            ServiceGenerator ServiceGenerator = new ServiceGenerator("WeGift", types);
            ServiceGenerator.Build();
            ControllerGenerator ControllerGenerator = new ControllerGenerator("WeGift", "", types);
            ControllerGenerator.Build();
            return;
        }
    }
}
