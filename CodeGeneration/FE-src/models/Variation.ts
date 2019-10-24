import {Model} from 'core';

import {VariationGrouping} from 'models/VariationGrouping';
import {Unit} from 'models/Unit';

export class Variation extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public variationGroupingId?: number;

  public variationGrouping?: VariationGrouping;
  
  public unitFirstVariations?: Unit[];
  
  public unitSecondVariations?: Unit[];
  
  public unitThirdVariations?: Unit[];

  public constructor(variation?: Variation) {
    super(variation);
  }
}
