
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.district.district_detail
{
    public class DistrictDetail_DistrictDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long ProvinceId { get; set; }
        public DistrictDetail_DistrictDTO() {}
        public DistrictDetail_DistrictDTO(District District)
        {
            
            this.Id = District.Id;
            this.Name = District.Name;
            this.OrderNumber = District.OrderNumber;
            this.ProvinceId = District.ProvinceId;
        }
    }

    public class DistrictDetail_DistrictFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public long? ProvinceId { get; set; }
    }
}
