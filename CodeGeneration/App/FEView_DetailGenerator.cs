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

            string ImportReference = string.Empty;
            string ConstReference = string.Empty;
            string PrimitiveItem = string.Empty;
            string ReferenceItem = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name.Contains("_") || PropertyInfo.Name.Equals("Id"))
                    continue;
                string prmitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(prmitiveType))
                {
                    if (prmitiveType.Contains("Date"))
                    {
                        PrimitiveItem += $@"
        <Form.Item label={{translate('{CamelCase(ClassName)}Detail.{CamelCase(PropertyInfo.Name)}')}}>
          {{
            form.getFieldDecorator(
                'name', 
                {{
                    initialValue: {CamelCase(ClassName)}.{CamelCase(PropertyInfo.Name)},
                    rules: [
                    ],
                }}
            )
            (<DatePicker/>)
          }}
        </Form.Item>
";
                    }
                    else
                    {
                        PrimitiveItem += $@"
        <Form.Item label={{translate('{CamelCase(ClassName)}Detail.{CamelCase(PropertyInfo.Name)}')}}>
          {{form.getFieldDecorator('code', {{
            initialValue: {CamelCase(ClassName)}.{CamelCase(PropertyInfo.Name)},
            rules: [
              {{
                required: true,
                message: translate('{CamelCase(ClassName)}Detail.errors.{CamelCase(PropertyInfo.Name)}.required'),
              }},
            ],
          }})(
            <Input type=""text""/>,
          )}}
        </Form.Item>
";
                    }
                }
                if (!string.IsNullOrEmpty(referenceType))
                {
                    ImportReference += $@"
import {{{referenceType}Search}} from 'models/{referenceType}Search';";

                    ConstReference += $@"
  const [{CamelCase(referenceType)}Search, set{referenceType}Search] = useState<{referenceType}Search>(new {referenceType}Search());";

                    ReferenceItem += $@"
        <Form.Item label={{translate('{CamelCase(ClassName)}Detail.{CamelCase(PropertyInfo.Name)}')}}>
            {{
                form.getFieldDecorator(
                    '{CamelCase(PropertyInfo.Name)}Id', 
                    {{
                        initialValue: {CamelCase(ClassName)}.{CamelCase(PropertyInfo.Name)} 
                            ? {CamelCase(ClassName)}.{CamelCase(PropertyInfo.Name)}.id 
                            : null,
                    }}
                )
                (
                    <SingleSelect getList={{{CamelCase(ClassName)}DetailRepository.singleList{referenceType}}}
                                  search={{{CamelCase(referenceType)}Search}}
                                  searchField=""name""
                                  showSearch
                                  setSearch={{set{referenceType}Search}}>
                      {{{CamelCase(ClassName)}.{CamelCase(PropertyInfo.Name)} && (
                        <Option value={{{CamelCase(ClassName)}.{CamelCase(PropertyInfo.Name)}.id}}>
                          {{{CamelCase(ClassName)}.{CamelCase(PropertyInfo.Name)}.name}}
                        </Option>
                      )}}
                    </SingleSelect>,
                )
            }}
        </Form.Item>";
                }
            }

            string contents = $@"
import Card from 'antd/lib/card';
import Form from 'antd/lib/form';
import Input from 'antd/lib/input';
import DatePicker from 'antd/lib/date-picker';
import Spin from 'antd/lib/spin';
import Table from 'antd/lib/table';
import CardTitle from 'components/CardTitle';
import SingleSelect, {{Option}} from 'components/SingleSelect';
import {{useDetail}} from 'core/hooks/useDetail';
import {{usePagination}} from 'core/hooks/usePagination';
import {{notification}} from 'helpers';
import path from 'path';
import React, {{useState}} from 'react';
import {{useTranslation}} from 'react-i18next';
import {{withRouter}} from 'react-router-dom';

import {{{UpperCase(ClassName)}_ROUTE}} from 'config/route-consts';
import {{{ClassName}}} from 'models/{ClassName}';
import './{ClassName}Detail.scss';
import {CamelCase(ClassName)}DetailRepository from './{ClassName}DetailRepository';
{ImportReference}

function {ClassName}Detail(props) {{
  const {{
    form,
    match: {{
      params: {{
        id,
      }},
    }},
  }} = props;

  const [translate] = useTranslation();
  const [pageSpinning, setPageSpinning] = useState<boolean>(false);
  const [{CamelCase(ClassName)}, loading] = useDetail<{ClassName}>(id, {CamelCase(ClassName)}DetailRepository.get, new {ClassName}());
  {ConstReference}

  function handleSubmit() {{
    form.validateFields((validationError: Error, {CamelCase(ClassName)}: {ClassName}) => {{
      if (validationError) {{
        return;
      }}
      setPageSpinning(true);
      {CamelCase(ClassName)}DetailRepository.save({CamelCase(ClassName)})
        .subscribe(
          () => {{
            notification.success({{
              message: translate('{CamelCase(ClassName)}Detail.update.success'),
            }});
            props.history.push(path.join({UpperCase(ClassName)}_ROUTE));
          }},
          (error: Error) => {{
            setPageSpinning(false);
            notification.error({{
              message: translate('{CamelCase(ClassName)}Detail.update.error'),
              description: error.message,
            }});
          }},
        );
    }});
  }}

  function backToList() {{
    props.history.push(path.join({UpperCase(ClassName)}_ROUTE));
  }}

  return (
    <Spin spinning={{pageSpinning}}>
      <Card
        loading={{loading}}
        title={{
          <CardTitle
            title={{translate('{CamelCase(ClassName)}Detail.detail.title', {{
            }})}}
            allowSave
            onSave={{handleSubmit}}
            allowCancel
            onCancel={{backToList}}
          />
        }}>
        {{form.getFieldDecorator('id', {{
          initialValue: {CamelCase(ClassName)}.id,
        }})(
          <Input type=""hidden""/>,
        )}}
        {PrimitiveItem}
        {ReferenceItem}
      </Card>
    </Spin>
  );
}}

export default Form.create()(withRouter({ClassName}Detail));";
            string path = Path.Combine(folder, $"{ClassName}Detail.tsx");
            File.WriteAllText(path, contents);
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

describe('{ClassName}Detail', () => {{
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
}});
";
            string path = Path.Combine(folder, $"{ClassName}Detail.test.tsx");
            File.WriteAllText(path, contents);
        }

        private string BuildSingleListImport(Type type)
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
                string listType = GetListType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(listType))
                {
                    contents += $@"
  public singleList{listType} = ({CamelCase(listType)}Search: {listType}Search): Observable<{listType}[]> => {{
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
