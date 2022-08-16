using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRCTC.StateClass;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace HRCTCApp.DAL
{
    public class UserAclDAL
    {

        #region " Variable "

        private Database CurrentDatabase;

        #endregion

        #region " Constructor "

        public UserAclDAL()
        {
            CurrentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        public String Save(UserAclSC vUserAclSC)
        {
            String mUserID = String.Empty;
            DataSet mDSet = null;
            string mStoredProcName = string.Empty;
            DbCommand mDBCommand = null;

            mStoredProcName = StoredProcedure.spr_UserACL_Save;
            mDBCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);

            CurrentDatabase.AddInParameter(mDBCommand, "@vUserID", DbType.String, vUserAclSC.UserID);
            CurrentDatabase.AddInParameter(mDBCommand, "@vUserACLId", DbType.String, vUserAclSC.UserACLId);     
            CurrentDatabase.AddInParameter(mDBCommand, "@vRoleCode", DbType.String, vUserAclSC.RoleCode);
            CurrentDatabase.AddInParameter(mDBCommand, "@vCreatedBy", DbType.String, vUserAclSC.CreatedBy);
            CurrentDatabase.AddInParameter(mDBCommand, "@vModifiedBy", DbType.String, vUserAclSC.ModifiedBy);
            CurrentDatabase.AddInParameter(mDBCommand, "@vIsEdit", DbType.String, vUserAclSC.IsEdit);
            mDSet = CurrentDatabase.ExecuteDataSet(mDBCommand);
            if (mDSet.Tables.Count > 0)
            {
                mUserID = mDSet.Tables[0].Rows[0]["UserID"].ToString();
            }
            return mUserID;
        }

        public UserAclSC Get(string vUserACLId)
        {
            DataSet mDSet = null;
            UserAclSC mUserAclSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            mStoredProcName = StoredProcedure.spr_UserACL_Get;
            mDbCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);
            //CurrentDatabase.AddInParameter(mDbCommand, "@vUserID", DbType.String, vUserID);
            CurrentDatabase.AddInParameter(mDbCommand, "@vUserACLId", DbType.String, vUserACLId);
            mDSet = CurrentDatabase.ExecuteDataSet(mDbCommand);
            mUserAclSC = new UserAclSC();
            if (mDSet.Tables[0].Rows.Count > 0)
            {

                mUserAclSC.UserID = mDSet.Tables[0].Rows[0]["UserID"].ToString();
                mUserAclSC.UserName = mDSet.Tables[0].Rows[0]["UserName"].ToString();
                mUserAclSC.RoleCode = mDSet.Tables[0].Rows[0]["RoleCode"].ToString();



                mUserAclSC.CreatedBy = mDSet.Tables[0].Rows[0]["CreatedBy"].ToString();
                mUserAclSC.CreatedOn = mDSet.Tables[0].Rows[0]["CreatedOn"].ToString();
                mUserAclSC.ModifiedOn = mDSet.Tables[0].Rows[0]["ModifiedOn"].ToString();
                mUserAclSC.ModifiedBy = mDSet.Tables[0].Rows[0]["ModifiedBy"].ToString();


            }
            return mUserAclSC;
        }

        public DataSet GetRoleList()
        {
            DataSet mDSet = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            mStoredProcName = StoredProcedure.spr_GetRoleList;
            mDbCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);
            mDSet = CurrentDatabase.ExecuteDataSet(mDbCommand);
            return mDSet;
        }

        public DataSet GetList(String vEmpCode)
        {
            DataSet mDSet = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            mStoredProcName = StoredProcedure.spr_UserACL_GetList;

            mDbCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);
            mDSet = CurrentDatabase.ExecuteDataSet(mDbCommand);
            return mDSet;
        }

        public DataSet Validate(UserAclSC vUserAclSC)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            DataSet mDSet = null;

            mDSet = new DataSet();
            mStoredProcName = StoredProcedure.spr_UserAcl_Validate;
            mDbCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);


            CurrentDatabase.AddInParameter(mDbCommand, "@vUserACLId", DbType.String, vUserAclSC.UserACLId);
            CurrentDatabase.AddInParameter(mDbCommand, "@vUserID", DbType.String, vUserAclSC.UserID);
            CurrentDatabase.AddInParameter(mDbCommand, "@vRoleCode", DbType.String, vUserAclSC.RoleCode);
            

            mDSet = CurrentDatabase.ExecuteDataSet(mDbCommand);
            return mDSet;
        }

    }
}


