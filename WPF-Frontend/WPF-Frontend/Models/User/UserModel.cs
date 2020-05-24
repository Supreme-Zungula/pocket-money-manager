﻿using JetBrains.Annotations;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace WPF_Frontend.Models.User
{
    public class UserModel : INotifyPropertyChanged
    {
        private readonly ObjectId _id;
        private readonly int _familyId;
        [Required]
        public string _firstName;
        [Required]
        public string _lastName;
        [Required]
        public string _role;
        [Required]
        [MinLength(10)]
        public string _phone;
        [Required]
        public string _password;


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

        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                RaisePropertyChanged("Role");
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

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
