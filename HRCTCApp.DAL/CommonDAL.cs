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


namespace HRCTCApp.DAL
{
    public class CommonDAL
    {
        #region " Variables "
        SqlConnection Conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConn"].ToString());
        System.Globalization.DateTimeFormatInfo MyDateTimeFormatInfo = new System.Globalization.DateTimeFormatInfo();
        #endregion

        #region " Constructor "

        public CommonDAL()
        {
            //CurrentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        #region " Functions "

        public static string GetDateInMMDDYYYY(string vDate, char vSeparator)
        {
            string[] mDateArray = null;
            string mDay = string.Empty;
            string mMonth = string.Empty;
            string mYear = string.Empty;
            string mDate = string.Empty;

            if (vDate.Contains("/"))
            {
                vSeparator = '/';
            }
            else if (vDate.Contains("-"))
            {
                vSeparator = '-';
            }
            else if (vDate.Contains(" "))
            {
                vSeparator = ' ';
            }

            mDateArray = vDate.Split(vSeparator);

            if (mDateArray.Length == 1)
            {
                mDate = ("01/01/1900");
            }
            else
            {
                mDay = mDateArray[0];
                mMonth = mDateArray[1];
                mYear = mDateArray[2];

                mDate = (mMonth + "/" + mDay + "/" + mYear);
            }
            return mDate;
        }

        public static string GetDateDisplayFormat()
        {

            string mFormattedDate = string.Empty;

            mFormattedDate = "dd-MM-yyyy";

            return mFormattedDate;
        }

        public static string ConvertDTToDisplayFormat(DateTime vDT)
        {
            string mFormattedDate = string.Empty;

            mFormattedDate = vDT.ToString("dd-MM-yyyy hh:mm tt");

            return mFormattedDate;
        }


        //public static string ConvertToDateStorageFormat(string vDate, char vSeparator = '/')
        //{
        //    string mFormattedDate = null;

        //    return vDate;

        //    mFormattedDate = GetDateInMMDDYYYY(vDate, vSeparator);

        //    return mFormattedDate;
        //}
        public static string GetDateInDDMMMYYYY(string vDate, char vSeparator)
        {
            string[] mDateArray = null;
            string mDay = string.Empty;
            string mMonth = string.Empty;
            string mYear = string.Empty;
            string mDate = string.Empty;

            return vDate;

            mDateArray = vDate.Split(vSeparator);
            if (mDateArray.Length == 1)
            {
                mDateArray = vDate.Split('/');
            }
            if (mDateArray.Length == 1)
            {
                mDate = ("01/01/1900");
            }
            else
            {
                mDay = mDateArray[0].ToString();
                mMonth = GetMonthString(mDateArray[1].ToString());
                mYear = mDateArray[2].ToString();
                mDate = (mDay + "/" + mMonth + "/" + mYear);
            }

            return mDate;
        }

        private static string GetMonthString(string vMonth)
        {
            string[] mMonths = {
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Oct",
                "Nov",
                "Dec"
            };
            int i = 0;
            while (i < mMonths.Length)
            {
                if (mMonths[i] == vMonth)
                {
                    return vMonth;

                }
                i += 1;
            }
            return mMonths[Convert.ToInt32(vMonth) - 1];
        }

        #endregion

        public static T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        public DataSet CHECKDocmentExistsInDraftMode(string DocType, string DocCreator, string WF_StatusDesc)
        {
            SqlCommand Cmd = new SqlCommand();
            DataSet Ds = new DataSet();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand("CHECKDocmentExistsInDraftMode", Conn);
                Cmd.Parameters.AddWithValue("@ParameterFor", SqlDbType.VarChar).Value = DocType;
                Cmd.Parameters.AddWithValue("@Doc_Creator", SqlDbType.VarChar).Value = DocCreator;
                Cmd.Parameters.AddWithValue("@WF_StatusDesc", SqlDbType.VarChar).Value = WF_StatusDesc;
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
                adapter.Fill(Ds);
                Cmd.Dispose();
                Conn.Close();
                return Ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetFinancialMonthYr()
        {
            SqlCommand Cmd = new SqlCommand();
            DataSet Ds = new DataSet();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand("GetFinancialYear", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
                adapter.Fill(Ds);
                Cmd.Dispose();
                Conn.Close();
                return Ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetServerSystemDate()
        {
            SqlCommand Cmd = new SqlCommand();
            DataSet Ds = new DataSet();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand("GetServerSystemDate", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
                adapter.Fill(Ds);
                Cmd.Dispose();
                Conn.Close();
                return Ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataSet GetParameterList(string ParameterFor)
        {
            SqlCommand Cmd = new SqlCommand();
            DataSet Ds = new DataSet();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand("spr_GetParameterDropDownValue", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ParameterFor", SqlDbType.VarChar).Value = ParameterFor;
                SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
                adapter.Fill(Ds);
                Cmd.Dispose();
                Conn.Close();
                return Ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetLocationAndLocationVerifier(string INTERNAL_CODE, string RoleName)
        {
            SqlCommand Cmd = new SqlCommand();
            DataSet Ds = new DataSet();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand("spr_GetAllLocationAndVerifier", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@INTERNAL_CODE", SqlDbType.VarChar).Value = INTERNAL_CODE;
                Cmd.Parameters.AddWithValue("@RoleName", SqlDbType.VarChar).Value = RoleName;
                SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
                adapter.Fill(Ds);
                Cmd.Dispose();
                Conn.Close();
                return Ds;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //public DataSet getMyPurchaseGroupList(String vEmpCode)
        //{
        //    DataSet mDSet = null;
        //    String mStoredProcName = String.Empty;
        //    DbCommand mDbCommand = null;
        //    mDSet = new DataSet();

        //    mStoredProcName = StoredProcedure.spr_GetMyPGroupList;
        //    mDbCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);
        //    CurrentDatabase.AddInParameter(mDbCommand, "@vEmpCode", DbType.String, vEmpCode);
        //    mDSet = CurrentDatabase.ExecuteDataSet(mDbCommand);
        //    return mDSet;
        //}

        //public DataSet GetMultiLookupData(string vLookup, string vColVal, string vBULookup, string vCompLookup)
        //{
        //    DataSet mDSet = null;
        //    String mStoredProcName = String.Empty;
        //    DbCommand mDbCommand = null;
        //    mDSet = new DataSet();

        //    mStoredProcName = StoredProcedure.spr_GetMultiLkpData;
        //    mDbCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);
        //    CurrentDatabase.AddInParameter(mDbCommand, "@vName", DbType.String, vLookup);
        //    CurrentDatabase.AddInParameter(mDbCommand, "@vColumnValue", DbType.String, vColVal);
        //    CurrentDatabase.AddInParameter(mDbCommand, "@vBULookup", DbType.String, vBULookup);
        //    CurrentDatabase.AddInParameter(mDbCommand, "@vCompLookup", DbType.String, vCompLookup);
        //    mDSet = CurrentDatabase.ExecuteDataSet(mDbCommand);

        //    return mDSet;
        //}

        //public DataSet GetCategory(string vBudgetType)
        //{
        //    DataSet mDSet = null;
        //    string mStoredProcName = string.Empty;
        //    DbCommand mDBCommand = null;

        //    mStoredProcName = StoredProcedure.spr_GetCostHeadCategory;
        //    mDBCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);
        //    CurrentDatabase.AddInParameter(mDBCommand, "@vBudgetType", DbType.String, vBudgetType);
        //    mDSet = CurrentDatabase.ExecuteDataSet(mDBCommand);

        //    return mDSet;
        //}
        //public DataSet GetAdvanceDtlsOnInvoiceAttach(string vType, string vDocID)
        //{
        //    DataSet mDSet = null;
        //    String mStoredProcName = String.Empty;
        //    DbCommand mDbCommand = null;
        //    mDSet = new DataSet();

        //    mStoredProcName = StoredProcedure.spr_getAdvanceDtlsOnInvoiceAttach;
        //    mDbCommand = CurrentDatabase.GetStoredProcCommand(mStoredProcName);
        //    CurrentDatabase.AddInParameter(mDbCommand, "@vType", DbType.String, vType);
        //    CurrentDatabase.AddInParameter(mDbCommand, "@vDocID", DbType.String, vDocID);
        //    mDSet = CurrentDatabase.ExecuteDataSet(mDbCommand);
        //    return mDSet;
        //}

        //public static void Debug(string vMsg)
        //{
        //    if (ConfigurationManager.AppSettings["Debug_IsEnabled"] != "Y")
        //        return;

        //    System.IO.File.AppendAllText(ConfigurationManager.AppSettings["Debug_Path"] + "\\Debug_" + DateTime.Today.ToString("ddMMMyyyy") + ".log", vMsg);
        //}
    }
}
