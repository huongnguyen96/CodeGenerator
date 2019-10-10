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

            string Namespace = "WeGift";
            EntityGeneration EntityGeneration = new EntityGeneration(Namespace, types);
            EntityGeneration.Build();
            RepositoryGeneration RepositoryGeneration = new RepositoryGeneration(Namespace, "WGContext", types);
            RepositoryGeneration.Build();
            ServiceGenerator ServiceGenerator = new ServiceGenerator(Namespace, types);
            ServiceGenerator.Build();
            ControllerGenerator_Master ControllerGenerator_Master = new ControllerGenerator_Master(Namespace, "", types);
            ControllerGenerator_Master.Build();
            ControllerGenerator_Detail ControllerGenerator_Detail = new ControllerGenerator_Detail(Namespace, "", types);
            ControllerGenerator_Detail.Build();
            return;
        }
    }
}
