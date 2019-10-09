
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class ProjectOrganization : DataEntity
    {
        public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid DivisionId { get; set; }
		public bool Disabled { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid ManagerId { get; set; }
		public Guid? StartDate { get; set; }
		public Guid? EndDate { get; set; }
		
    }

    public class ProjectOrganizationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter DivisionId { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter ManagerId { get; set; }
		public GuidFilter StartDate { get; set; }
		public GuidFilter EndDate { get; set; }
		
        public ProjectOrganizationOrder OrderBy {get; set;}
        public ProjectOrganizationSelect Selects {get; set;}
    }

    public enum ProjectOrganizationOrder
    {
        
        Code,
        Name,
        Description,
        Disabled,
        StartDate,
        EndDate,
    }

    public enum ProjectOrganizationSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        Description = E._4,
        Division = E._5,
        Disabled = E._6,
        BusinessGroup = E._7,
        Manager = E._8,
        StartDate = E._9,
        EndDate = E._10,
    }
}
