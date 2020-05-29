using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WPF_Frontend.Models.Family
{
    public class UserModel : INotifyPropertyChanged
    {
        private string _id;
        private int _familyId;
        [Required]
        private string _firstName;
        [Required]
        private string _lastName;
        [Required]
        private string _role;
        [Required]
        [MinLength(10)]
        private string _phone;
        [Required]
        private string _password;
        private decimal _balance;
       


        public string AccNo { get; set; }

        public string Id 
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
            }
        }

        public decimal Balance
        {
            get => _balance;
            set
            {
                if (value == _balance) return;
                _balance = value;
                RaisePropertyChanged("Balance");
            }
        }

        public int FamilyId
        {
            get => _familyId;
            set
            {
                if (value == _familyId) return;
                _familyId = value;
            }
        }
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
