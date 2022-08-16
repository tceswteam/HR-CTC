using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Text;
using HRCTC.StateClass;
using HRCTCApp.DAL;
using System.Web.UI;
using System.Web;


namespace HRCTCApp.BLL
{
    public class CommonBLL
    {
        #region " Functions "

        //public bool AuthenticateUser(string ACLUser, string Password)
        //{
        //    UserObjectSC mUserSCObj = null;
        //    UserObjectBLL mUserBLLObj = null;
        //    bool mFlag = false;
        //    Page mPage = default(Page);
        //    System.Web.SessionState.HttpSessionState mPageSession = default(System.Web.SessionState.HttpSessionState);
        //    FE_Symmetric mObj = null;
        //    String mUserGrps = String.Empty;
        //    UserADSProperties mUserADSProps = null;
        //    String mLDAPLogin = String.Empty;

        //    try
        //    {
        //        mUserSCObj = new UserObjectSC();
        //        mUserBLLObj = new UserObjectBLL();
        //        mObj = new FE_Symmetric();

        //        mLDAPLogin = ConfigurationSettings.AppSettings["LDAPLogin"].ToString();

        //        mPageSession = System.Web.HttpContext.Current.Session;
        //        mPage = (Page)HttpContext.Current.Handler;



        //        if (ACLUser.Equals("Superadmin", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            Password = mObj.EncryptData("Password", Password);

        //            if (Password != ConfigurationSettings.AppSettings["Password"].ToString())
        //                return false;

        //            mUserSCObj = mUserBLLObj.GetAuthenticatedUserDetails(ACLUser, Password);
        //        }
        //        else
        //        {

        //            if (mLDAPLogin == "Y")
        //            {
        //                if (!LDAPAuthenticationFunc(ACLUser, Password, ref mUserGrps, ref mUserADSProps))
        //                {
        //                    return false;
        //                }
        //            }
        //            else if (mObj.EncryptData("Password", Password) != ConfigurationSettings.AppSettings["Password"].ToString())
        //            {
        //                return false;
        //            }

        //            mUserSCObj = mUserBLLObj.GetUsrDtlsByLDAPID(ACLUser);
        //        }


        //        if (mUserSCObj != null)
        //        {
        //            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(ACLUser, false);
        //            mFlag = true;
        //            mPageSession.Add("UserObj", mUserSCObj);
        //        }
        //        else
        //        {
        //            mFlag = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return mFlag;
        //}

        public static bool _IsMember(string vSrch, string vSource)
        {
            bool mFlag = false;
            int mLoop1 = 0;
            int mLoop2 = 0;
            string[] mSrc = null;
            string[] mSrch = null;

            if (vSource == null)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(vSource))
            {
                return false;
            }
            else
            {
                mSrc = vSource.Split(',');
            }

            mSrch = vSrch.Split(',');
            mFlag = false;
            for (mLoop1 = 0; mLoop1 <= mSrch.Length - 1; mLoop1++)
            {
                mFlag = false;
                for (mLoop2 = 0; mLoop2 <= mSrc.Length - 1; mLoop2++)
                {
                    if (mSrch[mLoop1].Trim() == mSrc[mLoop2].Trim() || mSrch[mLoop1].Trim() == "[" + mSrc[mLoop2].Trim() + "]")
                        mFlag = true;
                }
                if (mFlag == false)
                    break; // TODO: might not be correct. Was : Exit For

            }
            return mFlag;
        }

        public static UserObjectSC GetUserSession()
        {
            System.Web.SessionState.HttpSessionState mPageSession = default(System.Web.SessionState.HttpSessionState);

            UserObjectSC mUserObjectSC = null;
            String mURL = String.Empty;

            Page mPage = default(Page);
            try
            {
                mPageSession = System.Web.HttpContext.Current.Session;
                mUserObjectSC = (UserObjectSC)mPageSession["UserObj"];
                mPage = (Page)System.Web.HttpContext.Current.Handler;
                mPage.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                mURL = mPage.Request.Path;

                if (mUserObjectSC == null)
                {
                    mPage.Response.Redirect("../SessionExpired.aspx?ReturnURL=" + mPage.Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mUserObjectSC;
        }

        public static void LogoutUser()
        {
            System.Web.SessionState.HttpSessionState mPageSession = default(System.Web.SessionState.HttpSessionState);
            mPageSession = System.Web.HttpContext.Current.Session;
            mPageSession["UserObj"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect("Login.aspx");
        }

        public static string ConvertDTToDisplayFormat(DateTime vDT)
        {
            return CommonDAL.ConvertDTToDisplayFormat(vDT);
        }

        public static string GetDateDisplayFormat(System.DateTime vDate)
        {
            string mFormattedDate = string.Empty;
            mFormattedDate = CommonDAL.GetDateDisplayFormat();
            return mFormattedDate;
        }



        public static string GetDateInMMDDYYYY(string vDate, char vSeparator)
        {
            string mFormattedDate = string.Empty;
            mFormattedDate = CommonDAL.GetDateInMMDDYYYY(vDate, vSeparator);
            return mFormattedDate;
        }

        //public bool LDAPAuthenticationFunc(string vLoginID, string vPass, ref string vUserGroups, ref UserADSProperties vUserADSProps)
        //{

        //    string mADPath = string.Empty;
        //    LdapAuthentication mADAuth = null;
        //    string mDomain = string.Empty;



        //    try
        //    {
        //        mADPath = ConfigurationSettings.AppSettings["ADSPath"].ToString();
        //        mDomain = ConfigurationSettings.AppSettings["Domain"].ToString();

        //        mADAuth = new LdapAuthentication(mADPath);

        //        if (mADAuth.IsAuthenticated(mDomain, vLoginID, vPass) == true)
        //        {
        //            return true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //DisplayErrorMessage("ADS Connection Error: " + ex.Message);
        //        throw ex;
        //    }

        //    return false;

        //}

        public static bool CheckDefaultDate(string vDate)
        {
            if (vDate == "1900-01-01 00:00:00" | vDate == "1-1-1900" | vDate == "01-Jan-1900" | vDate == "01-01-1900" | string.IsNullOrEmpty(vDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

     

        #endregion
        public DataSet GetServerSystemDate()
        {
            CommonDAL objCommonDAL = new CommonDAL();
            DataSet Dset = objCommonDAL.GetServerSystemDate();
            return Dset;
        }
        public DataSet GetParameterList(string ParameterFor)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            DataSet Dset = objCommonDAL.GetParameterList(ParameterFor);
            return Dset;

        }
        public DataSet GetLocationAndLocationVerifier(string INTERNAL_CODE, string RoleName)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            DataSet Dset = objCommonDAL.GetLocationAndLocationVerifier(INTERNAL_CODE, RoleName);
            return Dset;

        }
        public DataSet GetParameterList(string ParameterFor, string IDFeild, string NameField)
        {

            CommonDAL objCommonDAL = new CommonDAL();
            DataSet Dset = objCommonDAL.GetParameterList(ParameterFor);
            Dset.Tables[0].Columns["ParameterID"].ColumnName = IDFeild;
            Dset.Tables[0].Columns["ParameterField"].ColumnName = NameField;
            return Dset;

        }

        public DataSet GetFinancialMonthYr()
        {
            CommonDAL objCommonDAL = new CommonDAL();
            DataSet Dset = objCommonDAL.GetFinancialMonthYr();
            return Dset;

        }
        public static T StringToEnum<T>(string name)
        {
            return CommonDAL.StringToEnum<T>(name);
        }

      

     
    }
}
