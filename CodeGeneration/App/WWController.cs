using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.App
{
    [Route("api/WWGeneration")]
    [ApiController]
    public class WWController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            List<Type> types = typeof(WWController)
            .Assembly.GetTypes()
            .Where(t => t.Name.EndsWith("DAO") && !t.IsAbstract).ToList();

            string Namespace = "WG";
            BEEntityGeneration EntityGeneration = new BEEntityGeneration(Namespace, types);
            EntityGeneration.Build();
            BERepositoryGenerator RepositoryGeneration = new BERepositoryGenerator(Namespace, "DataContext", types);
            RepositoryGeneration.Build();
            BEServiceGenerator ServiceGenerator = new BEServiceGenerator(Namespace, types);
            ServiceGenerator.Build();
            BEControllerGenerator_Master ControllerGenerator_Master = new BEControllerGenerator_Master(Namespace, "", types);
            ControllerGenerator_Master.Build();
            BEControllerGenerator_Detail ControllerGenerator_Detail = new BEControllerGenerator_Detail(Namespace, "", types);
            ControllerGenerator_Detail.Build();

            FEEntityGenerator FEEntityGenerator = new FEEntityGenerator(types);
            FEEntityGenerator.Build();
            FEView_MasterGenerator FEView_MasterGenerator = new FEView_MasterGenerator(types);
            FEView_MasterGenerator.Build();
            FEView_DetailGenerator FEView_DetailGenerator = new FEView_DetailGenerator(types);
            FEView_DetailGenerator.Build();
            return;
        }
    }
}
