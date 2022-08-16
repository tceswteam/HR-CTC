using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRCTC.StateClass
{
    [Serializable()]
    public class UserRoleSC
    {
        #region "Variables"

        private String _RoleID = String.Empty;
        private String _RoleDesc = String.Empty;




        #endregion

        #region "Properties"

        public String RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        public String RoleDesc
        {
            get { return _RoleDesc; }
            set { _RoleDesc = value; }
        }

        #endregion
    }
}
