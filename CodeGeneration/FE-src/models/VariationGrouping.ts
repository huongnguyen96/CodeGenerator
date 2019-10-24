import {Model} from 'core';

import {Item} from 'models/Item';
import {Variation} from 'models/Variation';

export class VariationGrouping extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public itemId?: number;

  public item?: Item;
  
  public variations?: Variation[];

  public constructor(variationGrouping?: VariationGrouping) {
    super(variationGrouping);
  }
}
