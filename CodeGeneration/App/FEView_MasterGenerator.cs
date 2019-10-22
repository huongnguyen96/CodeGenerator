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
                BuildPackageJson(type);
                BuildTest(type);
            }
        }

        private void BuildView(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Master");
            Directory.CreateDirectory(folder);
            string contents = $@"
import Button from 'antd/lib/button';
import Card from 'antd/lib/card';
import Table from 'antd/lib/table';
import CardTitle from 'components/CardTitle/CardTitle';
import {{useList}} from 'core/hooks/useList';
import {{confirm, getColumnSortOrder, notification, renderIndex }} from 'helpers';
import path from 'path';
import React, {{useState}} from 'react';
import {{useTranslation}} from 'react-i18next';
import {{ Link,RouteComponentProps, withRouter }} from 'react-router-dom';

import './{ClassName}Master.scss';
import {CamelCase(ClassName)}MasterRepository from './{ClassName}MasterRepository';
import {{ {UpperCase(ClassName)}_ROUTE }} from 'config/route-consts';
import {{ {ClassName} }} from 'models/{ClassName}';
import {{ {ClassName}Search }} from 'models/{ClassName}Search';
{BuildSingleListImport(type)}

const {{Column}} = Table;

function {ClassName}Master(props: RouteComponentProps) {{
  function handleAdd() {{
    props.history.push(path.join({UpperCase(ClassName)}_ROUTE, 'add'));
  }}

  function handleClear() {{
    clearFiltersAndSorters();
    setSearch(new {ClassName}Search());
  }}

  function reloadList() {{
    setSearch(new {ClassName}Search(search));
  }}

  function handleDelete({CamelCase(ClassName)}: {ClassName}) {{
    return () => {{
      confirm({{
        title: translate('{CamelCase(ClassName)}Master.deletion.title'),
        content: translate('{CamelCase(ClassName)}Master.deletion.content'),
        okType: 'danger',
        onOk: () => {{
          {CamelCase(ClassName)}MasterRepository.delete({CamelCase(ClassName)})
            .subscribe(
              () => {{
                notification.success({{
                  message: translate('{CamelCase(ClassName)}Master.deletion.success'),
                }});
                reloadList();
              }},
              (error: Error) => {{
                notification.error({{
                  message: translate('{CamelCase(ClassName)}Master.deletion.error'),
                  description: error.message,
                }});
              }},
            );
        }},
      }});
    }};
  }}

  const [translate] = useTranslation();
  const [search, setSearch] = useState<{ClassName}Search>(new {ClassName}Search());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<{ClassName}, {ClassName}Search>(
    search,
    setSearch,
    {CamelCase(ClassName)}MasterRepository.list,
    {CamelCase(ClassName)}MasterRepository.count,
  );

  return (
    <Card title={{
      <CardTitle title={{translate('{CamelCase(ClassName)}Master.title')}}
                 allowAdd
                 onAdd={{handleAdd}}
                 allowClear
                 onClear={{handleClear}}
      />
    }}>
      <Table dataSource={{list}}
             rowKey=""id""
             loading={{loading}}
             onChange={{handleChange}}
             pagination={{{{
               total,
             }}}}
      >
        <Column key=""index""
                title={{translate('{CamelCase(ClassName)}Master.index')}}
                render={{renderIndex<{ClassName}, {ClassName}Search>(search)}}
        />
        {BuildColumn(type)}
        <Column key=""actions""
                dataIndex=""id""
                render={{(id: string, {CamelCase(ClassName)}: {ClassName}) => {{
                  return (
                    <>
                      <Link to={{path.join({UpperCase(ClassName)}_ROUTE, id.toString())}}>
                        {{translate('general.actions.edit')}}
                      </Link>
                      <Button htmlType=""button"" type=""link"" onClick={{handleDelete({CamelCase(ClassName)})}}>
                        {{translate('general.actions.delete')}}
                      </Button>
                    </>
                  );
                }}}}
        />
      </Table>
    </Card>
  );
}}

export default withRouter({ClassName}Master);
";
            string path = Path.Combine(folder, $"{ClassName}Master.tsx");
            File.WriteAllText(path, contents);
        }

        private string BuildColumn(Type type)
        {
            string contents = string.Empty;
            string ClassName = GetClassName(type);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(primitiveType) && !PropertyInfo.Name.EndsWith("Id"))
                {
                    contents += $@"
         <Column key=""{CamelCase(PropertyInfo.Name)}""
                dataIndex=""{CamelCase(PropertyInfo.Name)}""
                title={{translate('{CamelCase(ClassName)}Master.{CamelCase(PropertyInfo.Name)}')}}
                sorter
                sortOrder={{getColumnSortOrder<{ClassName}>('{CamelCase(PropertyInfo.Name)}', sorter)}}
        />";
                }
                if (!string.IsNullOrEmpty(referenceType))
                {
                    contents += $@"
         <Column key=""{CamelCase(PropertyInfo.Name)}""
                dataIndex=""{CamelCase(PropertyInfo.Name)}""
                title={{translate('{CamelCase(ClassName)}Master.{CamelCase(PropertyInfo.Name)}')}}
                sorter
                sortOrder={{getColumnSortOrder<{ClassName}>('{CamelCase(PropertyInfo.Name)}', sorter)}}
                render={{({CamelCase(PropertyInfo.Name)}: {referenceType}) => {{
                       return (
                         <>
                           {{{CamelCase(PropertyInfo.Name)} && {CamelCase(PropertyInfo.Name)}.id}}
                         </>
                       );
                     }}}}
        />";
                }
            }
            return contents;
        }

        private void BuildRepository(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Master");
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
    
  public delete = ({CamelCase(ClassName)}: {ClassName}): Observable<{ClassName}> => {{
    return this.httpService.post<{ClassName}>(`/delete`, {CamelCase(ClassName)})
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
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Master");
            string contents = $@"";
            string path = Path.Combine(folder, $"{ClassName}Master.scss");
            File.WriteAllText(path, contents);
        }

        private void BuildPackageJson(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Master");
            string contents = $@"
{{
  ""main"": ""{ClassName}Master.tsx""
}}";
            string path = Path.Combine(folder, $"package.json");
            File.WriteAllText(path, contents);
        }
        private void BuildTest(Type type)
        {
            string ClassName = GetClassName(type);
            string folder = Path.Combine(rootPath, "views", ClassName, ClassName + "Master");
            string contents = $@"
import React from 'react';
import ReactDOM from 'react-dom';
import {{MemoryRouter}} from 'react-router-dom';
import {ClassName}Master from './{ClassName}Master';

describe('{ClassName}Master', () => {{
    it('renders without crashing', () => {{
      const div = document.createElement('div');
      ReactDOM.render(
        <MemoryRouter>
          <{ClassName}Master/>
        </MemoryRouter>,
        div,
      );
      ReactDOM.unmountComponentAtNode(div);
    }});
}});
";
            string path = Path.Combine(folder, $"{ClassName}Master.test.tsx");
            File.WriteAllText(path, contents);
        }

        private string BuildSingleListImport(Type type)
        {
            string contents = string.Empty;
            string ClassName = GetClassName(type);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            List<string> referenceTypes = new List<string>();
            referenceTypes.Add(ClassName);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType) && !referenceTypes.Contains(referenceType))
                {
                    referenceTypes.Add(referenceType);
                    contents += $@"
import {{{referenceType}}} from 'models/{referenceType}';
import {{{referenceType}Search}} from 'models/{referenceType}Search';";
                }
            }
            return contents;
        }

        private string BuildSingleListMethod(Type type)
        {
            string contents = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            List<string> referenceTypes = new List<string>();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType) && !referenceTypes.Contains(referenceType))
                {
                    referenceTypes.Add(referenceType);
                    contents += $@"
  public singleList{referenceType} = ({CamelCase(referenceType)}Search: {referenceType}Search): Observable<{referenceType}[]> => {{
    return this.httpService.post('/single-list-{KebabCase(referenceType)}',{CamelCase(referenceType)}Search)
      .pipe(
        map((response: AxiosResponse<{referenceType}[]>) => response.data),
      );
  }};";
                }
            }
            return contents;
        }
    }
}