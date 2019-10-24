
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.province.province_detail
{
    public class ProvinceDetail_ProvinceDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public ProvinceDetail_ProvinceDTO() {}
        public ProvinceDetail_ProvinceDTO(Province Province)
        {
            
            this.Id = Province.Id;
            this.Name = Province.Name;
            this.OrderNumber = Province.OrderNumber;
        }
    }

    public class ProvinceDetail_ProvinceFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public ProvinceOrder OrderBy { get; set; }
    }
}
