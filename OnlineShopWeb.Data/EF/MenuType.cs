namespace OnlineShopWeb.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuType")]
    public partial class MenuType
    {
        public int MenuTypeID { get; set; }

        [StringLength(50)]
        public string MenuTypeName { get; set; }
    }
}
