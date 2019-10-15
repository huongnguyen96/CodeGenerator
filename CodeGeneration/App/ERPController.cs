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
            BEEntityGeneration EntityGeneration = new BEEntityGeneration(Namespace, types);
            EntityGeneration.Build();
            BERepositoryGenerator RepositoryGeneration = new BERepositoryGenerator(Namespace, "WGContext", types);
            RepositoryGeneration.Build();
            BEServiceGenerator ServiceGenerator = new BEServiceGenerator(Namespace, types);
            ServiceGenerator.Build();
            BEControllerGenerator_Master ControllerGenerator_Master = new BEControllerGenerator_Master(Namespace, "", types);
            ControllerGenerator_Master.Build();
            BEControllerGenerator_Detail ControllerGenerator_Detail = new BEControllerGenerator_Detail(Namespace, "", types);
            ControllerGenerator_Detail.Build();

            FEEntityGenerator FEEntityGenerator = new FEEntityGenerator(types);
            FEEntityGenerator.Build();
            return;
        }
    }
}
