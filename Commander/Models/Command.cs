using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{   
    public class Command
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength]
        public string HowTo { get; set; }
        [Required]
        public string CommandLine{get;set;}
        [Required]
        public string Platform {get; set;}
    }
    
}