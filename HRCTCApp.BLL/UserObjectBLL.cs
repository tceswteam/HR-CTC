
#region " Using "

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Net.Mail;
using HRCTCApp.DAL;
using HRCTC.StateClass;
using System.Transactions;
using System.Web.UI;
using System.Web.SessionState;
using NosActiveDirectoryIntegration;
using System.Configuration;

#endregion

namespace HRCTCApp.BLL
{
    public class UserObjectBLL
    {
        #region " Functions "

        //public String ValidateUserACL(Int32 ACLID, String ACLUser, String RoleCode)
        //{
        //    UserObjectDAL mUserObjectDAL = null;
        //    string mReturn = string.Empty;

        //    mUserObjectDAL = new UserObjectDAL();
        //    mReturn = mUserObjectDAL.ValidateUserACL(ACLID, ACLUser, RoleCode);
        //    return mReturn;

        //}


        public DataSet AccessApplicationConfig()
        {
            UserObjectDAL mUserDAL = new UserObjectDAL();
            DataSet ds= mUserDAL.AccessApplicationConfig();
            return ds;


        }
        public string GetLDAPAuthenticationOnly(string vEmpCode, string vPass, string mLDP_Ignore)
        {
            try
            {               
                CwidAuthenticator mLDAPAuth = null;
                bool mIsAuthenticated = false;
                if (mLDP_Ignore == "")
                {
                    mLDP_Ignore = ConfigurationManager.AppSettings["LDAP_Ignore"].ToString(); 
                }
                writetolog("1");
                if (mLDP_Ignore == "Y")
                {
                    if (vPass == "")
                    {
                        mIsAuthenticated = true;
                    }
                }
                writetolog("2");
                if (!mIsAuthenticated)
                {
                    writetolog("3");
                    mLDAPAuth = new CwidAuthenticator();
                    mIsAuthenticated = mLDAPAuth.AuthenticateUser(vEmpCode, vPass, ConfigurationManager.AppSettings["LDAP_Domain"].ToString());
                    writetolog("4" + " mIsAuthenticated : - " + mIsAuthenticated.ToString());

                    if (!mIsAuthenticated)
                        return "NotAuthenticated";
                }
                return "Authenticated";
            }
            catch (Exception ex)
            {
                writetolog(ex.Message + "UserObjectBLL GetAuthenticatedUserDetail");
                return "NotAuthenticated";
            }
 
        }
        public UserObjectSC GetAuthenticatedUserDetails(string vEmpCode, string vPass, string mLDP_Ignore)
        {
            try
            {
                UserObjectSC mUserObjectSC = null;
                UserObjectDAL mUserDAL = null;
              

                mUserObjectSC = new UserObjectSC();
                mUserDAL = new UserObjectDAL();
               
                writetolog("5");
                mUserObjectSC = mUserDAL.GetAuthenticatedUserDetails(vEmpCode);
                writetolog("6");
                return mUserObjectSC;
            }
            catch (Exception ex)
            {
                writetolog(ex.Message + "UserObjectBLL GetAuthenticatedUserDetail");
                return null;                
            }
            
            
        }
        public UserObjectSC GetAuthenticatedUserDetailsFromAdmin(string vEmpCode, string vPassword)
        {
            try
            {
                UserObjectSC mUserObjectSC = null;
                UserObjectDAL mUserDAL = null;


                mUserObjectSC = new UserObjectSC();
                mUserDAL = new UserObjectDAL();

                writetolog("5");
                mUserObjectSC = mUserDAL.GetAuthenticatedUserDetailsFromAdmin(vEmpCode, vPassword);
                writetolog("6");
                return mUserObjectSC;
            }
            catch (Exception ex)
            {
                writetolog(ex.Message + "UserObjectBLL GetAuthenticatedUserDetail");
                return null;
            }
        }
        public UserObjectSC GetUserSession()
        {
            HttpSessionState mPageSession = default(HttpSessionState);

            UserObjectSC mUserObjectSC = null;
            String mURL = String.Empty;

            Page mPage = default(Page);
            try
            {
                mPageSession = HttpContext.Current.Session;
                mUserObjectSC = (UserObjectSC)mPageSession["UserObj"];
                mPage = (Page)HttpContext.Current.Handler;
                mPage.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                mURL = mPage.Request.Path;

                if (mUserObjectSC == null)
                {
                    //Call authentication by LDAP here
                    mPage.Response.Redirect("~/SessionExpired.aspx?ReturnURL=" + mPage.Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                //ErrorBLL.GetErrorInfo(ex.Message, ex.ToString(), "", null);
                throw ex;
            }

            return mUserObjectSC;
        }

        public DataTable GetUserRoles(String vEmpCode)
        {
            UserObjectDAL mUserObjectDAL = null;
            DataTable mDTable = null;

            mUserObjectDAL = new UserObjectDAL();
            mDTable = new DataTable();
            mDTable = mUserObjectDAL.GetUserRoles(vEmpCode);
            return mDTable;
        }

        public void LogoutUser()
        {
            System.Web.SessionState.HttpSessionState mPageSession = default(System.Web.SessionState.HttpSessionState);
            mPageSession = System.Web.HttpContext.Current.Session;
            mPageSession["UserObj"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect("Logout.aspx");
        }

        public void writetolog(string vStr)
        {
            string mLogFileFlag = "", mLogFileName ="";
            mLogFileFlag = ConfigurationManager.AppSettings["LogFileFlag"].ToString();

            mLogFileName = ConfigurationManager.AppSettings["Debug_Path_Text"].ToString();  
            if (mLogFileFlag == "Y")
            {
                System.IO.File.AppendAllText(mLogFileName, Environment.NewLine + vStr + " " + System.DateTime.Now);
            }
        }
        public string IsTCAPEmployee(string vEmpCode)
        {
            UserObjectDAL mUserObjectDAL = null;
            string IsTCAPEmployee_flg = null;

            mUserObjectDAL = new UserObjectDAL();

            IsTCAPEmployee_flg = mUserObjectDAL.IsTCAPEmployee(vEmpCode);
            return IsTCAPEmployee_flg;
        }

        public string IsTCAPEmployee_Auth(string vEmpCode, string vPassword)
        {
            UserObjectDAL mUserObjectDAL = null;
            string IsTCAPEmployee_flg = null;

            mUserObjectDAL = new UserObjectDAL();

            IsTCAPEmployee_flg = mUserObjectDAL.IsTCAPEmployee_Auth(vEmpCode, vPassword);
            return IsTCAPEmployee_flg;

        }


        public UserObjectSC GetAuthenticatedUserDetails(string vEmpCode, string vPass, bool TCAP)
        {

            UserObjectSC mUserObjectSC = null;
            UserObjectDAL mUserDAL = null;
            mUserObjectSC = new UserObjectSC();
            mUserDAL = new UserObjectDAL();
            if (TCAP == true)
            {
                mUserObjectSC = mUserDAL.GetAuthenticatedUserDetails(vEmpCode);

            }
            else
            {
                return null;
            }

            return mUserObjectSC;
        }

        //public DataTable GetUserACL(string vColumnName, string vColumnValue, string vIsActive)
        //{
        //    UserObjectDAL mUserObjectDAL = null;
        //    DataTable mDTable = null;

        //    mUserObjectDAL = new UserObjectDAL();
        //    mDTable = new DataTable();
        //    mDTable = mUserObjectDAL.GetUserACL(vColumnName, vColumnValue, vIsActive);
        //    return mDTable;

        //}

        //public UserObjectSC GetUserACL(int ACLID)
        //{
        //    UserObjectSC mUserObjectSC = null;
        //    UserObjectDAL mUserObjectDAL = null;

        //    mUserObjectSC = new UserObjectSC();
        //    mUserObjectDAL = new UserObjectDAL();
        //    mUserObjectSC = mUserObjectDAL.GetUserACL(ACLID);
        //    return mUserObjectSC;


        //}

        #endregion
    }
}
