
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.ward.ward_master
{
    public class WardMaster_DistrictDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long ProvinceId { get; set; }
        public WardMaster_DistrictDTO() {}
        public WardMaster_DistrictDTO(District District)
        {
            
            this.Id = District.Id;
            this.Name = District.Name;
            this.OrderNumber = District.OrderNumber;
            this.ProvinceId = District.ProvinceId;
        }
    }

    public class WardMaster_DistrictFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public long? ProvinceId { get; set; }
        public DistrictOrder OrderBy { get; set; }
    }
}
