using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JobScheduling.Entity.SecurityModel
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = " * UserID is Required")]
        [StringLength(25, ErrorMessage = " * Must be less than 25 characters")]
        public string UserID { get; set; }

        [Required(ErrorMessage = " * Passowrd is Required")]
        [StringLength(25, MinimumLength = 10, ErrorMessage = " * The length of the password must between 10 and 25 characters with no blank spaces")]
        public string Password { get; set; }
    }


    [PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = " * The new password and confirm password do not match.")]
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = " * Required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = " * Required")]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 10, ErrorMessage = " * The length of the password must between 10 and 25 characters with no blank spaces")]
        [PasswordMatch(ErrorMessage = " * The password must contain at least three of the following,upper case letters,lower case letters,numbers,special characters(!@#$%^&*_)")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = " * Required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }


    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' and '{1}' do not match";
        private readonly object _typeId = new object();

        public string ConfirmProperty { get; private set; }
        public string OriginalProperty { get; private set; }


        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(_defaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(System.Globalization.CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Object.Equals(originalValue, confirmValue);
        }
    }


    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = " * The new password and confirm password do not match.")]
    public class UserM
    {
        public string UID { get; set; }

        [RegularExpression("[^<]*<?[^a-zA-Z]*", ErrorMessage = " * The format is not correct!")]
        [Required(ErrorMessage = " * Required")]
        [StringLength(25, ErrorMessage = " * Must be less than 25 characters")]
        public string UserID { get; set; }

        [Required(ErrorMessage = " * Required")]
        [RegularExpression("[^<]*<?[^a-zA-Z]*", ErrorMessage = " * The format is not correct!")]
        [StringLength(50, ErrorMessage = " * Must be less than 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " * Required")]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 10, ErrorMessage = " * The length of the password must between 10 and 25 characters with no blank spaces")]
        [PasswordMatch(ErrorMessage=" * The password must contain at least three of the following,upper case letters,lower case letters,numbers,special characters(!@#$%^&*_)")]
        public string Password { get; set; }

        public bool Active { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public byte IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedTime { get; set; }

        [Required(ErrorMessage = " * Required")]
        public string ConfirmPassword { get; set; }


    }

    /// <summary>
    /// validation of the password
    /// </summary>
    public sealed class PasswordMatchAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;
            int matchNum = 0;

            if (value == null)
                return false;

            string passowrd = value.ToString();
            if (passowrd.Contains(" "))
                result = false;
            //
            string regexUpperLetter = "[A-Z]+";
            if (Regex.IsMatch(passowrd, regexUpperLetter))
                matchNum += 1;
            //
            string regexLowerLetter = "[a-z]+";
            if (Regex.IsMatch(passowrd, regexLowerLetter))
                matchNum += 1;
            //
            string regexNumber = "[1-9]+";
            if (Regex.IsMatch(passowrd, regexNumber))
                matchNum += 1;
            //
            string regexSpecialLetter = "[!@#$%^&*_]+";
            if (Regex.IsMatch(passowrd, regexSpecialLetter))
                matchNum += 1;

            if (matchNum < 3)
                result = false;

            return result;
        }
    }




}
