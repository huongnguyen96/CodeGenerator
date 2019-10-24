import {Model} from 'core';

import {VariationGrouping} from 'models/VariationGrouping';
import {Item} from 'models/Item';

export class Variation extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public variationGroupingId?: number;

  public variationGrouping?: VariationGrouping;
  
  public itemFirstVariations?: Item[];
  
  public itemSecondVariations?: Item[];

  public constructor(variation?: Variation) {
    super(variation);
  }
}
