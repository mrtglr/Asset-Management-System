using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthLoginDemo_bnd.Models
{
    public class District
    {
        [Key]
        public int district_id {get; set;}
        public string district_name {get; set;}

        [ForeignKey("Province")]
        public int province_id {get; set;}       
        
        public virtual Province Province {get; set;}
    }
}