using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Frontend.Event_Helper
{
    public static class DataStore
    {
        #region datastore private members

        private static string _id;
        private static int _familyId;
        private static string _firstName;
        private static string _lastName;
        private static string _role;
        private static string _phone;

        #endregion

        #region datastore public properties

        public static string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public static int FamilyId
        {
            get
            {
                return _familyId;
            }
            set
            {
                _familyId = value;
            }
        }
        public static string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
        public static string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }
        public static string Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }
        public static string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }

        #endregion
    }
}
