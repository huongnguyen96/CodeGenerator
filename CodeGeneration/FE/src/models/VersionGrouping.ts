import {Model} from 'core';

import {Item} from 'models/Item';
import {Version} from 'models/Version';

export class VersionGrouping extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public itemId?: number;

  public item?: Item;
  
  public versions?: Version[];

  public constructor(versionGrouping?: VersionGrouping) {
    super(versionGrouping);
  }
}
