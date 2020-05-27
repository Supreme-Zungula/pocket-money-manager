using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.Models.Family;

namespace WPF_Frontend.Event_Helper
{
    public class CreateData
    {
        private void Create(UserModel userObject)
        {
            #region set all datastore values
            
            DataStore.ID = userObject.Id;
            DataStore.FamilyId = userObject.FamilyId;
            DataStore.FirstName = userObject.FirstName;
            DataStore.LastName = userObject.LastName;
            DataStore.Role = userObject.Role;
            DataStore.Phone = userObject.Phone;

            #endregion
        }

        public CreateData(UserModel userModel)
        {
            Create(userModel);
        }
    }
}
