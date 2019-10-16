using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeGeneration.App
{
    public class FEView_MasterGenerator : FEGenerator
    {
        private List<Type> Classes;
        public FEView_MasterGenerator(List<Type> Classes)
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
            }
        }

        private void BuildView(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", KebabCase(ClassName), KebabCase(ClassName) + "-master");
            Directory.CreateDirectory(folder);
            string contents = $@"
";
        }

        private void BuildRepository(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", KebabCase(ClassName), KebabCase(ClassName) + "-master");
            Directory.CreateDirectory(folder);
            string contents = $@"
import {{AxiosResponse}} from 'axios';
import {{Repository}} from 'core';
import {{Observable}} from 'rxjs';
import {{map}} from 'rxjs/operators';
import {{{ClassName}}} from 'models/{ClassName}';
import {{{ClassName}Search}} from 'models/{ClassName}Search';
{BuildSingleListImport(type)}

export class {ClassName}MasterRepository extends Repository {{
  public constructor() {{
    super();
    this.httpService.setBasePath('/api/{KebabCase(ClassName)}/{KebabCase(ClassName)}-master');
  }}

  public count = ({CamelCase(ClassName)}Search: {ClassName}Search): Observable<number> => {{
    return this.httpService.post('/count',{CamelCase(ClassName)}Search)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  }};

  public list = ({CamelCase(ClassName)}Search: {ClassName}Search): Observable<{ClassName}[]> => {{
    return this.httpService.post('/list',{CamelCase(ClassName)}Search)
      .pipe(
        map((response: AxiosResponse<{ClassName}[]>) => response.data),
      );
  }};

  public get = (id: number): Observable<{ClassName}> => {{
    return this.httpService.post<{ClassName}>('/get', {{ id }})
      .pipe(
        map((response: AxiosResponse<{ClassName}>) => response.data),
      );
  }};
  {BuildSingleListMethod(type)}
}}

export default new {ClassName}MasterRepository();";
            string path = Path.Combine(folder, $"{ClassName}MasterRepository.ts");
            File.WriteAllText(path, contents);
        }

        private void BuildCss(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", KebabCase(ClassName), KebabCase(ClassName) + "-master");
            string contents = $@"";
            string path = Path.Combine(folder, $"{ClassName}Master.scss");
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
