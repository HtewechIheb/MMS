using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GEC.ViewModels.Contacts;
using GEC.ViewModels.Folders;
using GEC.ViewModels.Groups;
using GEC_DataLayer.Models.BLL;

namespace GEC.ViewModels.CustomValidation
{
    public class UniqueName : ValidationAttribute
    {       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance.GetType() == typeof(NewContactViewModel))
            {
                if (!BLL_Contact.CheckNameUnicity(value.ToString()))
                {
                    return new ValidationResult("Le nom du contact doit être unique.");
                }
            }
            else if(validationContext.ObjectInstance.GetType() == typeof(EditContactViewModel))
            {
                var contact = (EditContactViewModel)validationContext.ObjectInstance;
                if (BLL_Contact.SelectById(contact.Id) != null && BLL_Contact.SelectById(contact.Id).Name != value.ToString() && !BLL_Contact.CheckNameUnicity(value.ToString()))
                {
                    return new ValidationResult("Le nom du contact doit être unique.");
                }
            }
            else if (validationContext.ObjectInstance.GetType() == typeof(NewFolderViewModel))
            {
                if (!BLL_Folder.CheckNameUnicity(value.ToString()))
                {
                    return new ValidationResult("Le nom du dossier doit être unique.");
                }
            }
            else if (validationContext.ObjectInstance.GetType() == typeof(EditFolderViewModel))
            {
                var folder = (EditFolderViewModel)validationContext.ObjectInstance;
                if (BLL_Folder.SelectById(folder.Id) != null && BLL_Folder.SelectById(folder.Id).Name != value.ToString() && !BLL_Folder.CheckNameUnicity(value.ToString()))
                {
                    return new ValidationResult("Le nom du dossier doit être unique.");
                }
            }
            else if (validationContext.ObjectInstance.GetType() == typeof(NewGroupViewModel))
            {
                if (!BLL_Group.CheckNameUnicity(value.ToString()))
                {
                    return new ValidationResult("Le nom du groupe doit être unique.");
                }
            }
            else if (validationContext.ObjectInstance.GetType() == typeof(EditGroupViewModel))
            {
                var group = (EditGroupViewModel)validationContext.ObjectInstance;
                if (BLL_Group.SelectById(group.Id) != null && BLL_Group.SelectById(group.Id).Name != value.ToString() && !BLL_Group.CheckNameUnicity(value.ToString()))
                {
                    return new ValidationResult("Le nom du groupe doit être unique.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
