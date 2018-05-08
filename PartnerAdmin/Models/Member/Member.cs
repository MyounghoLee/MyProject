using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerAdmin.Models.Member
{
    public class Member
    {
        [Required(ErrorMessage = "email을 입력 해 주세요.")]
        [EmailAddress(ErrorMessage = "email이 유효하지 않습니다.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "패스워드를 입력 해 주세요.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "패스워드는 10~50자까지 가능합니다.")]
        public string Password { get; set; }

        [Required(ErrorMessage ="재확인 패스워드를 입력 해 주세요.")]
        [Compare(nameof(Password), ErrorMessage = "패스워드가 일치하지 않습니다.")]
        public string ConfirmPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewConfirmPassword { get; set; }

        public string EncPassword { get; set; }

        public string NewEncPassword { get; set; }

        [Required(ErrorMessage = "이름을 입력 해 주세요.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage ="이름은 1~10자까지 입력 해 주세요.")]
        public string Name { get; set; }

        public string Grade { get; set; }

        public DateTime UptDate { get; set; }

        public DateTime InsDate { get; set; }

        public bool IsUse { get; set; }

        public bool IsActivation { get; set; }
    }

}
