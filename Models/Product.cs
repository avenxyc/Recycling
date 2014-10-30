using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Recycling.Models
{
    //Validaton Method
    //public class MaxWordsAttribute : ValidationAttribute
    //{
    //    public MaxWordsAttribute(int maxWord)
    //        : base("{0} has too many words.")
    //    {
    //        _maxWords = maxWord;
    //    }

    //    protected override ValidationResult IsValid(
    //        object value, ValidationContext validationContext)
    //    {
    //        if (value != null)
    //        {
    //            var valueAsString = value.ToString();
    //            if (valueAsString.Split(' ').Length > _maxWords)
    //            {
    //                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
    //                return new ValidationResult(errorMessage);
    //            }

    //            return ValidationResult.Success;
    //        }
    //    }

    //    private readonly int _maxWords;
    //}

    public class Product //: IValidatableObject
    {
        [Key]
        public virtual string UPC { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string ParentCompany { get; set; }
        public double Weight { get; set; }
        public double TotalWeight { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        // Virtual used to prevent problems
        public virtual ICollection<Consituent> Constituents { get; set; }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if()
        //}
    }
}