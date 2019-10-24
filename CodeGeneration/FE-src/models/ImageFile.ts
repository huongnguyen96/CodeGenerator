import {Model} from 'core';


export class ImageFile extends Model {
   
  public id?: number;
 
  public path?: string;
 
  public name?: string;

  public constructor(imageFile?: ImageFile) {
    super(imageFile);
  }
}
