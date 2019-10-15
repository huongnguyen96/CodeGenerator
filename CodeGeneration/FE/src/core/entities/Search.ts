export class Search {
  public skip: number = 0;

  public take: number = 10;

  [key: string]: any;

  constructor(search?: Search) {
    Object.assign(this, search);
  }
}
