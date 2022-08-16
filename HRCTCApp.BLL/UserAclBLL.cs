using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using HRCTC.StateClass;
using HRCTCApp.DAL;
using System.Web.SessionState;
using System.Configuration;
using System.Web.UI;
using System.Web;

namespace HRCTCApp.BLL
{
    public class UserAclBLL
    {


        public void Save(UserAclSC vUserAclSC)
        {
            UserAclDAL mUserAclDAL = null;
            TransactionScope mTransactionScope = null;
            string mUserID = string.Empty;
            mTransactionScope = new TransactionScope();
            using (mTransactionScope)
            {
                mUserAclDAL = new UserAclDAL();
                mUserID = mUserAclDAL.Save(vUserAclSC);

                mTransactionScope.Complete();
            }
            mTransactionScope.Dispose();
        }

        public UserAclSC Get(string vUserACLId)
        {

            UserAclSC mUserAclSC = null;
            UserAclDAL mUserAclDAL = null;

            mUserAclSC = new UserAclSC();
            mUserAclDAL = new UserAclDAL();

            mUserAclSC = mUserAclDAL.Get(vUserACLId);
            return mUserAclSC;
        }

        public DataSet GetList(String mUserAclSC)
        {
            DataSet mDataSet = null;
            UserAclDAL mUserAclDAL = null;

            mUserAclDAL = new UserAclDAL();
            mDataSet = new DataSet();

            mDataSet = mUserAclDAL.GetList(mUserAclSC);

            return mDataSet;
        }

        public DataSet GetRolesList()
        {
            DataSet mDataSet = null;
            UserAclDAL mUserAclDAL = null;

            mUserAclDAL = new UserAclDAL();
            mDataSet = new DataSet();

            mDataSet = mUserAclDAL.GetRoleList();

            return mDataSet;
        }
        public DataSet Validate(UserAclSC vUserAclSC)
        {
            DataSet mDataSet = null;
            mDataSet = new DataSet();
            UserAclDAL mUserAclDAL = null;
            mUserAclDAL = new UserAclDAL();

            mDataSet = mUserAclDAL.Validate(vUserAclSC);

            return mDataSet;

        }


    }
}



