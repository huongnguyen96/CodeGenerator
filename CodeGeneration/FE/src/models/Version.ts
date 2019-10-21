import {Model} from 'core';

import {VersionGrouping} from 'models/VersionGrouping';
import {Unit} from 'models/Unit';
import {Unit} from 'models/Unit';
import {Unit} from 'models/Unit';

export class Version extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public versionGroupingId?: number;

  public versionGrouping?: VersionGrouping;
  
  public unitFirstVersions?: Unit[];
  
  public unitSecondVersions?: Unit[];
  
  public unitThirdVersions?: Unit[];

  public constructor(version?: Version) {
    super(version);
  }
}
