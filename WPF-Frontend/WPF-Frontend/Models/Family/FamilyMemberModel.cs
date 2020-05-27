using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WPF_Frontend.Models.Family
{
    public class FamilyMemberModel : BindableBase
    {
        private string _id;
        private int _familyId;
        [Required]
        private string _firstName;
        [Required]
        private string _lastName;
        [Required]
        private string _relationship;
        [Required]
        [MinLength(10)]
        private string _phone;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public string Relationship
        {
            get => _relationship;
            set
            {
                _relationship = value;
                RaisePropertyChanged("Relationship");
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }
    }
}
