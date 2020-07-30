using System;
using System.ComponentModel.DataAnnotations;
using MSOFT.Entities.Properties;

namespace MSOFT.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name ="Tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", 
            ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password,
            ErrorMessageResourceName = "PasswordNotRoleValid", 
            ErrorMessageResourceType =typeof(Resources))]
        private string _password { get; set; }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _passwordHash;
        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }

        [EmailAddress(ErrorMessageResourceName ="EmailNotValid",ErrorMessageResourceType =typeof(Resources))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? EmailConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
        public Guid? SecurityStamp { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool? LockoutEnabled { get; set; }

        public string GetPassword()
        {
            return this._password;
        }
        public string GetPasswordHash()
        {
            return this._passwordHash;
        }

    }
}