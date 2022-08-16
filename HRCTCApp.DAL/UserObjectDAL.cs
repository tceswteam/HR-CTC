using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using HRCTC.StateClass;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;

namespace HRCTCApp.DAL
{
    public class UserObjectDAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region " Constructor "

        public UserObjectDAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        #region " Sub-routines "

        //public String ValidateUserACL(Int32 ACLID, String ACLUser, String RoleCode)
        //{
        //    string mStoredProcName = string.Empty;
        //    string mReturnMsg = string.Empty;
        //    DbCommand mDBCommand = null;
        //    DbDataReader mDbReader = null;
        //    try
        //    {
        //        mStoredProcName = StoredProcedures.spr_ValidateUserACLMstRec;
        //        mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

        //        currentDatabase.AddInParameter(mDBCommand, "@vACLID", DbType.Int32, ACLID);
        //        currentDatabase.AddInParameter(mDBCommand, "@vACLUser", DbType.String, ACLUser);
        //        currentDatabase.AddInParameter(mDBCommand, "@vRoleCode", DbType.String, RoleCode);

        //        mDbReader = (DbDataReader)currentDatabase.ExecuteReader(mDBCommand);
        //        if (mDbReader.Read())
        //        {
        //            mReturnMsg = mDbReader["ReturnMsg"].ToString();
        //        }
        //        return mReturnMsg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (mDbReader != null)
        //        {
        //            mDbReader.Close();
        //            mDbReader = null;
        //        }
        //    }
        //}

        public DataSet AccessApplicationConfig()
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;

            mDSet = new DataSet();
            mStoredProcName = "sp_GetAppConfig";
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

          
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDSet;

        }
        public void LogError(string Err_msg, string Err_Place)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;

            mDSet = new DataSet();
            mStoredProcName = "CreateErrorLog";
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@Err_msg", DbType.String, Err_msg);
            currentDatabase.AddInParameter(mDBCommand, "@Err_Place", DbType.String, Err_Place);
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
           
        }
        public UserObjectSC GetAuthenticatedUserDetails(string vEmpCode)
        {
            String mStoredProcName = String.Empty;
            UserObjectSC mUserObjectCS = null;
            IDataReader mQueryResult = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;
            UserRoleSC mUserRoleSC = null;
            try
            {
                writetolog("Inside DAL");

                mDTable = new DataTable();
                mStoredProcName = StoredProcedure.spr_Authenticate;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vEmpCode", DbType.String, vEmpCode);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mDTable = mDSet.Tables[0];
                    mUserObjectCS = new UserObjectSC();
                    mUserObjectCS.EmpCode = mDTable.Rows[0]["EmpCode"].ToString();
                    mUserObjectCS.EmpName = mDTable.Rows[0]["EmpName"].ToString();
                    mUserObjectCS.EmailAdd = mDTable.Rows[0]["EmailId"].ToString();

                    if (mDSet.Tables[1].Rows.Count > 0)
                    {

                        mDTable = new DataTable();
                        mDTable = mDSet.Tables[1];
                        foreach (DataRow mRow in mDTable.Rows)
                        {
                            mUserRoleSC = new UserRoleSC();
                            mUserRoleSC.RoleID = mRow["RoleCode"].ToString();
                            mUserRoleSC.RoleDesc = mRow["RoleDesc"].ToString();

                            mUserObjectCS.UserRoles.Add(mUserRoleSC);

                            if (mUserObjectCS.Roles == "" || mUserObjectCS.Roles == null)
                            {
                                mUserObjectCS.Roles = mRow["RoleCode"].ToString();
                                mUserObjectCS.Roles = mRow["RoleDesc"].ToString();
                            }
                            else
                            {
                                mUserObjectCS.Roles += "," + mRow["RoleCode"].ToString();
                                mUserObjectCS.RoleDesc += "," + mRow["RoleDesc"].ToString();
                            }
                        }
                        mUserObjectCS.CurrRoleCode = mDTable.Rows[0]["RoleCode"].ToString();
                        mUserObjectCS.CurrRoleDesc = mDTable.Rows[0]["RoleDesc"].ToString();
                    }

                    mUserObjectCS.UserProperties = new UserPropertiesSC();
                    mUserObjectCS.UserProperties.PurchasingGroup = "Project";
                    mUserObjectCS.UserProperties.PGCode = "00000";
                }
            }
            catch (Exception ex)
            {
                writetolog(ex.Message + " " + ex.StackTrace);
                LogError(ex.Message, ex.StackTrace);
                //throw new ApplicationException("", ex);
                throw ex;
            }
            finally
            {
                if (mQueryResult != null)
                {
                    mQueryResult.Close();
                    mQueryResult = null;
                }
            }
            return mUserObjectCS;
        }

        public UserObjectSC GetAuthenticatedUserDetailsFromAdmin(string vEmpCode,string vPassword)
        {
            String mStoredProcName = String.Empty;
            UserObjectSC mUserObjectCS = null;
            IDataReader mQueryResult = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;
            UserRoleSC mUserRoleSC = null;
            try
            {
                writetolog("Inside DAL");

                mDTable = new DataTable();
                mStoredProcName = "spr_User_AuthUser_Admin";
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vEmpCode", DbType.String, vEmpCode);
                currentDatabase.AddInParameter(mDBCommand, "@vPassword", DbType.String, vPassword);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mDTable = mDSet.Tables[0];
                    mUserObjectCS = new UserObjectSC();
                    mUserObjectCS.EmpCode = mDTable.Rows[0]["EmpCode"].ToString();
                    mUserObjectCS.EmpName = mDTable.Rows[0]["EmpName"].ToString();
                    mUserObjectCS.EmailAdd = mDTable.Rows[0]["EmailId"].ToString();

                    if (mDSet.Tables[1].Rows.Count > 0)
                    {

                        mDTable = new DataTable();
                        mDTable = mDSet.Tables[1];
                        foreach (DataRow mRow in mDTable.Rows)
                        {
                            mUserRoleSC = new UserRoleSC();
                            mUserRoleSC.RoleID = mRow["RoleCode"].ToString();
                            mUserRoleSC.RoleDesc = mRow["RoleDesc"].ToString();

                            mUserObjectCS.UserRoles.Add(mUserRoleSC);

                            if (mUserObjectCS.Roles == "" || mUserObjectCS.Roles == null)
                            {
                                mUserObjectCS.Roles = mRow["RoleCode"].ToString();
                                mUserObjectCS.Roles = mRow["RoleDesc"].ToString();
                            }
                            else
                            {
                                mUserObjectCS.Roles += "," + mRow["RoleCode"].ToString();
                                mUserObjectCS.RoleDesc += "," + mRow["RoleDesc"].ToString();
                            }
                        }
                        mUserObjectCS.CurrRoleCode = mDTable.Rows[0]["RoleCode"].ToString();
                        mUserObjectCS.CurrRoleDesc = mDTable.Rows[0]["RoleDesc"].ToString();
                    }

                    mUserObjectCS.UserProperties = new UserPropertiesSC();
                    mUserObjectCS.UserProperties.PurchasingGroup = "Project";
                    mUserObjectCS.UserProperties.PGCode = "00000";
                }
            }
            catch (Exception ex)
            {
                writetolog(ex.Message + " " + ex.StackTrace);
                LogError(ex.Message, ex.StackTrace);
                //throw new ApplicationException("", ex);
                throw ex;
            }
            finally
            {
                if (mQueryResult != null)
                {
                    mQueryResult.Close();
                    mQueryResult = null;
                }
            }
            return mUserObjectCS;
        }

        public void writetolog(string vStr)
        {
            string mLogFileFlag = "", mLogFileName="";
            mLogFileFlag = ConfigurationManager.AppSettings["LogFileFlag"].ToString();
            mLogFileName = ConfigurationManager.AppSettings["Debug_Path_Text"].ToString();
            if (mLogFileFlag == "Y")
            {
                System.IO.File.AppendAllText(mLogFileName, Environment.NewLine + vStr + " " + System.DateTime.Now);
            }
            //if (mLogFileFlag == "Y")
            //{
            //    System.IO.File.AppendAllText("C:\\HRApp\\EmpSal.txt", Environment.NewLine + vStr);
            //}
        }

        public DataTable GetUserRoles(string vEmpCode)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;

            mDSet = new DataSet();
            mStoredProcName = StoredProcedure.spr_GetUserRoles;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vEmpCode", DbType.String, vEmpCode);
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDSet.Tables[0];
        }
        public string IsTCAPEmployee(string vEmpCode)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;

            mDSet = new DataSet();
            mStoredProcName = "sp_IsTCAPEmp";
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vEmpCode", DbType.String, vEmpCode);
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDSet.Tables[0].Rows[0][0].ToString();

        }

        public string IsTCAPEmployee_Auth(string vEmpCode, string vPassword)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;

            mDSet = new DataSet();
            mStoredProcName = "[dbo].[spr_User_AuthUser_TCAP]";
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vEmpCode", DbType.String, vEmpCode);
            currentDatabase.AddInParameter(mDBCommand, "@vPassword", DbType.String, vPassword);
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDSet.Tables[0].Rows[0][0].ToString();

        }
        #endregion
    }
}
