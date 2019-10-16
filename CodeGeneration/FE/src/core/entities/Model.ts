export abstract class Model {

  public errors?: {
    [key: string]: any;
  };

  protected constructor(model?: Model) {
    if (model) {
      Object.assign(this, model);
    }
  }
}
