
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.district.district_detail
{
    public class DistrictDetail_ProvinceDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public DistrictDetail_ProvinceDTO() {}
        public DistrictDetail_ProvinceDTO(Province Province)
        {
            
            this.Id = Province.Id;
            this.Name = Province.Name;
            this.OrderNumber = Province.OrderNumber;
        }
    }

    public class DistrictDetail_ProvinceFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public ProvinceOrder OrderBy { get; set; }
    }
}
