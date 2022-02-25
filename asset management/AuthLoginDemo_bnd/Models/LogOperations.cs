using System;
using System.ComponentModel.DataAnnotations;

namespace AuthLoginDemo_bnd.Models
{
    public class LogOperations
    {
        [Key]
        public int log_id {get; set;}
        public bool log_situation {get; set;}
        public string user_id {get; set;}
        public string process {get; set;}
        public DateTime date_time {get; set;}
        public string user_ip {get; set;}
        public string statement {get; set;}
    }
}