using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGeneration.App
{
    public class FEViewGenerator : FEGenerator
    {
        private List<Type> Classes;
        public FEViewGenerator(List<Type> Classes)
        {
            this.Classes = Classes;
        }

        public void Build()
        {
            BuildRoute();
            BuildView();
        }

        public void BuildRoute()
        {
            string contents = string.Empty;
            contents += $@"
export const HOME_ROUTE: string = '/';";
            foreach (Type type in Classes)
            {
                if (type.Name.Contains("_"))
                    continue;
                string ClassName = GetClassName(type);
                contents += $@"
export const {UpperCase(ClassName)}_ROUTE: string = '/{KebabCase(ClassName)}';";
            }
            string folder = Path.Combine(rootPath, "config");
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, $"route-consts.ts");
            File.WriteAllText(path, contents);
        }
        public void BuildView()
        {
            foreach (Type type in Classes)
            {
                if (type.Name.Contains("_"))
                    continue;
                BuildPackageJson(type);
                BuildTest(type);
                BuildTSX(type);
                BuildCss(type);
            }
        }

        private void BuildTSX(Type type)
        {
            string contents = string.Empty;
            string ClassName = GetClassName(type);

            string folder = Path.Combine(rootPath, "views", ClassName);
            Directory.CreateDirectory(folder);

            contents += $@"
import {{{UpperCase(ClassName)}_ROUTE}} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {{Route,Switch, withRouter}} from 'react-router-dom';
import {ClassName}Detail from './{ClassName}Detail';
import {ClassName}Master from './{ClassName}Master';
import './{ClassName}View.scss';

function {ClassName}View() {{
  return (
    <Switch>
      <Route path={{path.join({UpperCase(ClassName)}_ROUTE)}} exact component={{{ClassName}Master}}/>
      <Route path={{path.join({UpperCase(ClassName)}_ROUTE, ':id')}} component={{{ClassName}Detail}}/>
    </Switch>
  );
}}

export default withRouter({ClassName}View);
";
            string path = Path.Combine(folder, $"{(ClassName)}View.tsx");
            File.WriteAllText(path, contents);
        }
        private void BuildPackageJson(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName);
            Directory.CreateDirectory(folder);
            string contents = $@"
{{
  ""main"": ""{ClassName}View.tsx""
}}";
            string path = Path.Combine(folder, $"package.json");
            File.WriteAllText(path, contents);
        }
        private void BuildTest(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName);
            Directory.CreateDirectory(folder);
            string contents = $@"
import React from 'react';
import ReactDOM from 'react-dom';
import {{MemoryRouter}} from 'react-router-dom';
import {ClassName}View from './{ClassName}View';

describe('{ClassName}Master', () => {{
    it('renders without crashing', () => {{
      const div = document.createElement('div');
      ReactDOM.render(
        <MemoryRouter>
          <{ClassName}View/>
        </MemoryRouter>,
        div,
      );
      ReactDOM.unmountComponentAtNode(div);
    }});
}});
";
            string path = Path.Combine(folder, $"{ClassName}View.test.tsx");
            File.WriteAllText(path, contents);
        }

        private void BuildCss(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName);
            Directory.CreateDirectory(folder);
            string contents = $@"";
            string path = Path.Combine(folder, $"{ClassName}View.scss");
            File.WriteAllText(path, contents);
        }
    }
}
