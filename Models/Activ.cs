using System.ComponentModel.DataAnnotations;

namespace HelpDeskIgnatov.Models
{
    public class Activ
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Номер кабинета")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string CabNumber { get; set; }

        [Required]
        [Display(Name = "Отдел")]
        public int? DepartmentId { get; set; }
        
        public Department Department { get; set; }
    }
}
