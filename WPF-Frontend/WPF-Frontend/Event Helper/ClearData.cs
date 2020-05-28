using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Frontend.Event_Helper
{
    public class ClearData
    {
        private void Clear()
        {
            #region set all datastore values

            DataStore.ID = null;
            DataStore.FamilyId = 0;
            DataStore.FirstName = null;
            DataStore.LastName = null;
            DataStore.Role = null;
            DataStore.Phone = null;

            #endregion

        }
        public ClearData()
        {
            Clear();
        }
    }
}
