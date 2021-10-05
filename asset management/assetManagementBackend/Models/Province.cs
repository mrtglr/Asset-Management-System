using System.ComponentModel.DataAnnotations;

namespace assetManagementBackend.Models
{
    public class Province
    {
        [Key]
        public int province_id {get; set;}
        public string province_name {get; set;}
    }
}