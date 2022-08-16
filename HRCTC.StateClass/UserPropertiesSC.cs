using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace HRCTC.StateClass
{
    [Serializable()]
    public class UserPropertiesSC
    {

        #region " Global Variables "

        private Boolean _IsAdmin = false;
        private String _Roles = String.Empty;
        private String[] _RoleList = null;
        string _PurchasingGroup = string.Empty;
        string _PGCode = string.Empty;

        #endregion

        #region " Properties "

        public Boolean IsAdmin
        {

            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

        public String Roles
        {

            get { return _Roles; }
            set
            {
                _RoleList = _Roles.ToString().Split(new Char[] { ',' });
                _Roles = value;
            }
        }

        public String[] RoleList
        {
            get
            {
                return _RoleList;
            }
        }

        public string PurchasingGroup
        {
            get { return _PurchasingGroup; }
            set { _PurchasingGroup = value; }
        }

        public string PGCode
        {
            get { return _PGCode; }
            set { _PGCode = value; }
        }


        #endregion

    }
}
