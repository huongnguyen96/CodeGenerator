export abstract class Model {

    [key: string]: any;
    errors: any;
    protected constructor(model?: Model) {

        if (model) {
            Object.assign(this, model);
        }
    }
}
