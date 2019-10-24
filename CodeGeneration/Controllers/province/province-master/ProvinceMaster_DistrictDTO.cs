
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.province.province_master
{
    public class ProvinceMaster_DistrictDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long ProvinceId { get; set; }
        public ProvinceMaster_DistrictDTO() {}
        public ProvinceMaster_DistrictDTO(District District)
        {
            
            this.Id = District.Id;
            this.Name = District.Name;
            this.OrderNumber = District.OrderNumber;
            this.ProvinceId = District.ProvinceId;
        }
    }

    public class ProvinceMaster_DistrictFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public long? ProvinceId { get; set; }
        public DistrictOrder OrderBy { get; set; }
    }
}
