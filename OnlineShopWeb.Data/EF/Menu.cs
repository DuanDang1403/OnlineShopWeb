namespace OnlineShopWeb.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int MenuID { get; set; }

        [StringLength(250)]
        public string Text { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        [StringLength(10)]
        public string Target { get; set; }

        public bool? Status { get; set; }

        public int? TypeID { get; set; }
    }
}
