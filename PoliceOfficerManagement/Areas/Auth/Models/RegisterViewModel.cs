using Microsoft.AspNetCore.Identity;
using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Data.Auth;
using PoliceOfficerManagement.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace PoliceOfficerManagement.Areas.Auth.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? NameBn { get; set; }
        public string? bpNumber { get; set; }
        public string? Email { get; set; }
        public string? Genders { get; set; }
        public string? UserRole { get; set; }
        public string? Citizenship { get; set; }
        public int? rankId { get; set; }
        public int? range { get; set; }
        public int? district { get; set; }
        public int? zone { get; set; }
        public int? thana { get; set; }
        public int? NationalIdentityType { get; set; }
        public string? NationalIdentityNo { get; set; }
        public int? AddressType { get; set; }
        public IFormFile? formFile { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserOTPCode { get; set; }
        public string? PassportNo { get; set; }
        public int? UserTypeId { get; set; }
        public int? UserId { get; set; }
        public string? userRole { get; set; }
        public string? verifyMessage { get; set; }
        public DateTime? dob { get; set; }
        public string? UserName { get; set; }
        public int? secuirityQuestionId { get; set; }
        public string? secuirityQuestionAns { get; set; }
        public int? secuirityQuestionId2 { get; set; }
        public string? secuirityQuestionAns2 { get; set; }
        public string? OldPassword { get; set; }
        public string[]? assignRoles { get; set; }
        public string[]? removeRoles { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        public string? userFrom { get; set; }
        public string? imagePath { get; set; }
        public string? userId { get; set; }

        public IEnumerable<UserType>? userTypes { get; set; }
        public IEnumerable<IdentityRole>? identityRoles { get; set; }
        public IEnumerable<Rank>? ranks { get; set; }
        public IEnumerable<AspNetUsersViewModel>? aspNetUsersViewModels { get; set; }
        public IEnumerable<RangeMetro>? rangeMetros { get; set; }
        public IEnumerable<DivisionDistrict>? divisionDistricts { get; set; }
        public IEnumerable<ZoneCircle>? zoneCircles { get; set; }
        public ApplicationUser? users { get; set; }
        //public EmployeeInfo? employeeInfo { get; set; }
        //public RegisterLn? rLang { get; set; }
        #region For Device Log

        public string? IMENumber { get; set; }
        public string? SIMSerialNumber { get; set; }
        public string? deviceLocation { get; set; }
        public string? SubscribNumber { get; set; }
        public string? DNSAddress { get; set; }
        public string? CountryCode { get; set; }
        public string? MotherBoard { get; set; }
        public string? BrowserName { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public string? OperatorName { get; set; }
        public string? IPAddress { get; set; }
        public string? WIFIAddress { get; set; }
        public string? Processor { get; set; }
        public string? MacAddress { get; set; }
        public string? deviceBrand { get; set; }
        public string? deviceModel { get; set; }
        public string? deviceAndroidId { get; set; }
        public string? deviceInfo { get; set; }
        public string? deviceProduct { get; set; }
        public string? infoType { get; set; } //Log In, Registration, GD Apply
        //public EmployeeInfo? employee { get; set; }
        //public EmployeeViewModel? employeeInformation { get; set; }
        #endregion
    }
}
