using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeGeneration.App
{
    public class FEView_DetailGenerator : FEGenerator
    {
        private List<Type> Classes;
        public FEView_DetailGenerator(List<Type> Classes)
        {
            this.Classes = Classes;
        }

        public void Build()
        {
            foreach (Type type in Classes)
            {
                if (type.Name.Contains("_"))
                    continue;
                BuildView(type);
                BuildRepository(type);
                BuildCss(type);
                BuildPackageJson(type);
                BuildTest(type);
            }
        }

        private void BuildView(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Detail");
            Directory.CreateDirectory(folder);
            string contents = $@"
";
        }

        private void BuildRepository(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Detail");
            Directory.CreateDirectory(folder);
            string contents = $@"
import {{AxiosResponse}} from 'axios';
import {{Repository}} from 'core';
import {{Observable}} from 'rxjs';
import {{map}} from 'rxjs/operators';
import {{{ClassName}}} from 'models/{ClassName}';
import {{{ClassName}Search}} from 'models/{ClassName}Search';
{BuildSingleListImport(type)}

export class {ClassName}DetailRepository extends Repository {{
  public constructor() {{
    super();
    this.httpService.setBasePath('/api/{KebabCase(ClassName)}/{KebabCase(ClassName)}-detail');
  }}

  public get = (id: number): Observable<{ClassName}> => {{
    return this.httpService.post<{ClassName}>('/get', {{ id }})
      .pipe(
        map((response: AxiosResponse<{ClassName}>) => response.data),
      );
  }};
  
  public create = ({CamelCase(ClassName)}: {ClassName}): Observable<{ClassName}> => {{
    return this.httpService.post<{ClassName}>(`/create`, {CamelCase(ClassName)})
      .pipe(
        map((response: AxiosResponse<{ClassName}>) => response.data),
      );
  }};
  public update = ({CamelCase(ClassName)}: {ClassName}): Observable<{ClassName}> => {{
    return this.httpService.post<{ClassName}>(`/update`, {CamelCase(ClassName)})
      .pipe(
        map((response: AxiosResponse<{ClassName}>) => response.data),
      );
  }};
  public delete = ({CamelCase(ClassName)}: {ClassName}): Observable<{ClassName}> => {{
    return this.httpService.post<{ClassName}>(`/delete`, {CamelCase(ClassName)})
      .pipe(
        map((response: AxiosResponse<{ClassName}>) => response.data),
      );
  }};
  
  public save = ({CamelCase(ClassName)}: {ClassName}): Observable<{ClassName}> => {{
    return {CamelCase(ClassName)}.id ? this.update({CamelCase(ClassName)}) : this.create({CamelCase(ClassName)});
  }};
  {BuildSingleListMethod(type)}
}}

export default new {ClassName}DetailRepository();";
            string path = Path.Combine(folder, $"{ClassName}DetailRepository.ts");
            File.WriteAllText(path, contents);
        }

        private void BuildCss(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Detail");
            Directory.CreateDirectory(folder);
            string contents = $@"";
            string path = Path.Combine(folder, $"{ClassName}Detail.scss");
            File.WriteAllText(path, contents);
        }
        private void BuildPackageJson(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Detail");
            string contents = $@"
{{
  ""main"": ""{ClassName}Detail.tsx""
}}";
            string path = Path.Combine(folder, $"package.json");
            File.WriteAllText(path, contents);
        }
        private void BuildTest(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Detail");
            string contents = $@"
import React from 'react';
import ReactDOM from 'react-dom';
import {{MemoryRouter}} from 'react-router-dom';
import {ClassName}Detail from './{ClassName}Detail';

it('renders without crashing', () => {{
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <{ClassName}Detail/>
    </MemoryRouter>,
    div,
  );
  ReactDOM.unmountComponentAtNode(div);
}});
";
            string path = Path.Combine(folder, $"{ClassName}Detail.test.tsx");
            File.WriteAllText(path, contents);
        }

        private string BuildSingleListImport(Type type)
        {
            string contents = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType))
                {
                    contents += $@"
import {{{referenceType}}} from 'models/{referenceType}';
import {{{referenceType}Search}} from 'models/{referenceType}Search';";
                }
                string listType = GetListType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(listType))
                {
                    contents += $@"
import {{{listType}}} from 'models/{listType}';
import {{{listType}Search}} from 'models/{listType}Search';";
                }
            }
            return contents;
        }

        private string BuildSingleListMethod(Type type)
        {
            string contents = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType))
                {
                    contents += $@"
  public singleList{referenceType} = ({CamelCase(referenceType)}Search: {referenceType}Search): Observable<{referenceType}[]> => {{
    return this.httpService.post('/single-list-{KebabCase(referenceType)}',{CamelCase(referenceType)}Search)
      .pipe(
        map((response: AxiosResponse<{referenceType}[]>) => response.data),
      );
  }};";
                }
                string listType = GetListType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(listType))
                {
                    contents += $@"
  public singleList{referenceType} = ({CamelCase(listType)}Search: {listType}Search): Observable<{listType}[]> => {{
    return this.httpService.post('/single-list-{KebabCase(listType)}',{CamelCase(listType)}Search)
      .pipe(
        map((response: AxiosResponse<{listType}[]>) => response.data),
      );
  }};";
                }
            }
            return contents;
        }
    }
}
