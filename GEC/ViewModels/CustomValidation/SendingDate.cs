using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GEC.ViewModels.Mails;

namespace GEC.ViewModels.CustomValidation
{
    public class SendingDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }
            if(validationContext.ObjectInstance is NewMailViewModel)
            {
                var mail = (NewMailViewModel)validationContext.ObjectInstance;
                return mail.RegistrationDate >= Convert.ToDateTime(value) ? ValidationResult.Success : new ValidationResult("La date d'envoi doit précéder la date d'enregistrement");
            }
            else 
            {
                var mail = (EditMailViewModel)validationContext.ObjectInstance;
                return mail.RegistrationDate >= Convert.ToDateTime(value) ? ValidationResult.Success : new ValidationResult("La date d'envoi doit précéder la date d'enregistrement");
            }
        }        
    }
}
