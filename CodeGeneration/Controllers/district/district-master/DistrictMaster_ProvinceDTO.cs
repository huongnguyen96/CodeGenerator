
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.district.district_master
{
    public class DistrictMaster_ProvinceDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public DistrictMaster_ProvinceDTO() {}
        public DistrictMaster_ProvinceDTO(Province Province)
        {
            
            this.Id = Province.Id;
            this.Name = Province.Name;
            this.OrderNumber = Province.OrderNumber;
        }
    }

    public class DistrictMaster_ProvinceFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public ProvinceOrder OrderBy { get; set; }
    }
}
