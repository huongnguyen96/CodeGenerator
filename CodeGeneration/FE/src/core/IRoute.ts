export interface IRoute {
  path: string;
  component?: any;
  render?: any;
  exact?: boolean;
  redirectTo?: string;
  children?: IRoute[];
  title?: string;

  [key: string]: any;
}
