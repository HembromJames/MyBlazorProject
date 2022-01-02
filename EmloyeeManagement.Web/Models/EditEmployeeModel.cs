using EmployeeManagement.Models;
using EmployeeManagement.Models.CustomValidators;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models
{
    public class EditEmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "First name must be provided")]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name must be provided")]
        public string LastName { get; set; }
        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "pragimtech.com", ErrorMessage = "Only pragimtech.com is allowed as domain name")]
        public string Email { get; set; }
        [CompareProperty("Email",ErrorMessage ="Email and Confirm Email must be same")]
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }
        [ValidateComplexType]
        public Department Department { get; set; } = new Department();
    }
}
