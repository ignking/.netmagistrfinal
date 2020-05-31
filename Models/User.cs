using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskIgnatov.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Фамилия Имя Отчество")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Логин")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Password { get; set; }

        [Display(Name = "Должность")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Position { get; set; }

        [Display(Name = "Отдел")]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public string PasswordHash { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
