namespace OnlineShopWeb.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
       
        public long UserID { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage = "Vui l�ng nh?p UserName")]
        public string UserName { get; set; }

        [StringLength(50)]
       // [Required(ErrorMessage = "Vui l�ng nh?p Password")]
        public string Password { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        
        public bool Status { get; set; }
    }
}
