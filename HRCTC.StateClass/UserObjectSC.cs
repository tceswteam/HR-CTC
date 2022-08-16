
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using EMPCTC.StateClass;

namespace HRCTC.StateClass
{
    [Serializable()]
    public class UserObjectSC
    {

        #region " Global Variables "

        private String _OTPPassword = String.Empty;
        public String OTPPassword
        {
            get { return _OTPPassword; }
            set { _OTPPassword = value; }
        }

        private String _RoleID = String.Empty;
        private String _Roles = String.Empty;
        private String _CreatedBy = String.Empty;
        private String _CreatedOn = String.Empty;
        private String _LModBy = String.Empty;
        private String _LModOn = String.Empty;

        private String _EmpCode = String.Empty;
        private Int64 _EmpID = 0;
        private String _EmpName = String.Empty;
        private String _EmailAdd = String.Empty;
        private UserPropertiesSC _UserProperties = null;

        public List<UserRoleSC> UserRoles = new List<UserRoleSC>();
        //public List<AdminRoleSC> AdminRoleList = new List<AdminRoleSC>();
        private String _Department = String.Empty;

        public String Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        #endregion

        #region " Properties "
        public String RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        public String Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }

        public String CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public String CreatedOn
        {
            get { return _CreatedOn; }
            set { _CreatedOn = value; }
        }

        public String LModBy
        {
            get { return _LModBy; }
            set { _LModBy = value; }
        }

        public String LModOn
        {
            get { return _LModOn; }
            set { _LModOn = value; }
        }



        public String EmpCode
        {
            get
            {
                return _EmpCode;
            }
            set
            {
                _EmpCode = value;

            }
        }

        public Int64 EmpID
        {
            get
            {
                return _EmpID;
            }
            set
            {
                _EmpID = value;

            }
        }

        public String EmpName
        {
            get
            {
                return _EmpName;
            }
            set
            {
                _EmpName = value;

            }
        }

        public String EmailAdd
        {
            get
            {
                return _EmailAdd;
            }
            set
            {
                _EmailAdd = value;

            }
        }



        public UserPropertiesSC UserProperties
        {
            get
            {
                return _UserProperties;
            }
            set
            {
                _UserProperties = value;
            }

        }
        private String _RoleDesc = String.Empty;
        public String RoleDesc
        {
            get { return _RoleDesc; }
            set { _RoleDesc = value; }
        }
        private String _CurrRoleDesc = String.Empty;
        public String CurrRoleDesc
        {
            get { return _CurrRoleDesc; }
            set { _CurrRoleDesc = value; }
        }
        private String _CurrRoleCode = String.Empty;
        public String CurrRoleCode
        {
            get { return _CurrRoleCode; }
            set { _CurrRoleCode = value; }
        }
        #endregion

        public bool HasRole(string vRoleCode)
        {
            if (this.UserRoles == null || this.UserRoles.Count == 0)
                return false;

            foreach (UserRoleSC mItem in this.UserRoles)
            {
                if (mItem.RoleID == vRoleCode)
                    return true;
            }

            return false;
        }

    } //End Class
}
