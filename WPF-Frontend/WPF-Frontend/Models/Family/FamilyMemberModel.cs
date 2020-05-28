using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;

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

        public int FamilyId
        {
            get => _familyId;
            set
            {
                _familyId = value;
                RaisePropertyChanged("FamilyId");
            }
        }

        public string Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                RaisePropertyChanged("Id");
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

        public FamilyMemberModel ToFamilyMember(UserModel user, string relationship)
        {
            return new FamilyMemberModel
            {
                Id = user.Id,
                FamilyId = user.FamilyId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Relationship = relationship
            };
        }

        public FamilyMemberModel ToFamilyMember(FamilyMemberModel user)
        {
            return new FamilyMemberModel
            {
                Id = user.Id,
                FamilyId = user.FamilyId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Relationship = user.Relationship
            };
        }

        public UserModel FromFamily(FamilyMemberModel member)
        {
            return new UserModel
            {
                Id = this.Id,
                FamilyId = this.FamilyId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Phone = this.Phone
            };
        }
    }
}
