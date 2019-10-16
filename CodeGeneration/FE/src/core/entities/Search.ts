export class Search {
  public skip?: number = 0;

  public take?: number = 10;

  public orderBy?: string;

  public orderType?: 'ASC' | 'DESC';

  constructor(search?: Search) {
    Object.assign(this, search);
  }
}
