using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using HRCTC.StateClass;
using HRCTCApp.DAL;
using HRCTCApp.BLL;

public partial class SalaryIncomeTaxCalc : System.Web.UI.Page
{
    public string ConnectionString;
    public SqlConnection Conn = new SqlConnection();
    public SqlCommand Cmd;
//    public PDFGeneratorBLL mPDF = new PDFGeneratorBLL();
    protected string _EmpCode;
    UserObjectDAL objUserObjectDAL = new UserObjectDAL();
    UserObjectBLL objUserObjectBLL = new UserObjectBLL();



    UserObjectSC _UserObj;

    public UserObjectSC CurrUser
    {
        get
        {
            return _UserObj;
        }
        set
        {
            _UserObj = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserObjectBLL mUserObjectBLL = null;
            mUserObjectBLL = new UserObjectBLL();
            //mUserObjectBLL.writetolog("frmEmpCTCRestruct Page_Load before session initialization");          
            this.CurrUser = mUserObjectBLL.GetUserSession();
            //mUserObjectBLL.writetolog("frmEmpCTCRestruct Page_Load after session initialization");
            GetCurrentFY();
            if (!Page.IsPostBack)
            { 
                DataSet Dset = GetEmployeeBaseCTC();
                //mUserObjectBLL.writetolog("frmEmpCTCRestruct Page_Load after data pulling for employee");
                if (Dset != null)
                {
                    //FillSuperanuation();
                    FillEmployeeBaseCTC(Dset);
                }
                Dset = null;
                Dset = GetEmpRegimeOption();
                if (Dset != null)
                {
                    //FillSuperanuation();
                    AsPerEMPRegimeStatua(Dset);
                }
            }
        }
        catch (Exception ex)
        {
           // objUserObjectDAL.LogError(ex.Message, "Default Page load");
            throw;
        }
    }
    //Procedure calling for filling Form with CTC details
    protected DataSet GetEmployeeBaseCTC()
    {
        try
        {
            DataSet mDSet = null;
            String mStoredProcName = String.Empty;
            SqlCommand mDbCommand = null;
            SqlDataAdapter mQueryResult = null;
            mDSet = new DataSet();
            //this.EmpCode;
            String empcode = this.CurrUser.EmpCode;
            //String empcode = "108185";
            //String empcode = "101260";
            //String empcode = "101467";
            //String empcode = "107807";
            //String empcode = "101457";
            //String empcode = "101440";
            //this.EmpCode = "108185";
            try
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                    Conn.Open();
                }
                mStoredProcName = "GetEmployeeBaseCTC";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = this.CurrUser.EmpCode;
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value =  hdnFinancialYearCurr.Value;

                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);
                return mDSet;
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "GetEmployeeBaseCTC");
                return null;
            }
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillSuperanuation");
            throw;
        }
    }
    //Fill complete CTC and Restructured (CTC if any) of an Employee
    protected void FillEmployeeBaseCTC(DataSet ds)
    {
        try
        {
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    ViewState["ds_RestructuredCTC"] = ds;
                    Session["ds_RestructuredCTC"] = ds;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblEmployeeName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                        lblGrade.Text = ds.Tables[0].Rows[0]["Grade"].ToString();
                        lblEmpCode.Text = ds.Tables[0].Rows[0]["EmpID"].ToString();
                        lblEmpPanCard.Text = ds.Tables[0].Rows[0]["Emp_PAN"].ToString();
                        lblDateofJoining.Text = ds.Tables[0].Rows[0]["DateofJoining"].ToString();
                    }
                  

                    
                 }
             }
            }

            
      
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillEmployeeBaseCTC");
            throw;
        }
    }

    protected void AsPerEMPRegimeStatua(DataSet ds)
    {
        try
        {
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                   
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["RegimeOption"].ToString() == "NOREGIMESELECTED")
                        {
                            trRegimeOptionNotSelect.Visible = true;
                            trRegimeOptionSelected.Visible = false;
                        }
                        else
                        {
                            lblEmpRegimeOption.Text = ds.Tables[0].Rows[0]["RegimeOption"].ToString();
                            trRegimeOptionNotSelect.Visible = false;
                            trRegimeOptionSelected.Visible = true;
                            if (ds.Tables[2].Rows[0]["RepeatedRegimeAllow"].ToString() == "Y")
                            {
                                trRegimeOptionNotSelect.Visible = true;
                            }
                            else
                            {
                                trRegimeOptionNotSelect.Visible = false;
                            }
                            if (ds.Tables[3].Rows[0]["CanDoMobileChange"].ToString() == "X")
                            {
                                btnProceed.Visible = true;
                            }
                            else
                            {
                                btnProceed.Visible = false;
                            }
                        }                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillEmployeeBaseCTC");
            throw;
        }
    }

    protected DataSet GetEmpRegimeOption()
    {
        try
        {
            DataSet mDSet = null;
            String mStoredProcName = String.Empty;
            SqlCommand mDbCommand = null;
            SqlDataAdapter mQueryResult = null;
            mDSet = new DataSet();
            //this.EmpCode;
            String empcode = this.CurrUser.EmpCode;
            //String empcode = "108185";
            //String empcode = "101260";
            //String empcode = "101467";
            //String empcode = "107807";
            //String empcode = "101457";
            //String empcode = "101440";
            //this.EmpCode = "108185";
            try
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                    Conn.Open();
                }
                mStoredProcName = "GetEmpRegimeStats";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = this.CurrUser.EmpCode;
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;

                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);
                return mDSet;
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "GetEmployeeBaseCTC");
                return null;
            }
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillSuperanuation");
            throw;
        }
    }
    private void GetCurrentFY()
    {
        DataSet mDSet = null;
        CommonBLL mCommonBLL = new CommonBLL();
        mDSet = mCommonBLL.GetFinancialMonthYr();
        hdnFinancialYearCurr.Value = mDSet.Tables[7].Rows[0]["Text"].ToString();
        hdnFinancialYearPrev.Value = mDSet.Tables[7].Rows[1]["Text"].ToString();
      //  ltrFinYr.Text = hdnFinancialYearCurr.Value;
    }
    protected void btnCalcTax_Click(object sender, EventArgs e)
    {
        Response.Redirect("CalcTaxAsPerOption.aspx", false);
    }
    protected void btnPrintCTCRestruc0_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnSubmitRegime_Click(object sender, ImageClickEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value_print"];

        if (confirmValue == "Yes")
        {

            try
            {
                DataSet mDSet = null;
                String mStoredProcName = String.Empty;
                SqlCommand mDbCommand = null;
                SqlDataAdapter mQueryResult = null;
                mDSet = new DataSet();
                //this.EmpCode;
                String empcode = this.CurrUser.EmpCode;
                //String empcode = "108185";
                //String empcode = "101260";
                //String empcode = "101467";
                //String empcode = "107807";
                //String empcode = "101457";
                //String empcode = "101440";
                //this.EmpCode = "108185";
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                    {
                        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                        Conn.Open();
                    }
                    mStoredProcName = "sp_UpdRegimeEmpOptn";
                    mDbCommand = new SqlCommand(mStoredProcName, Conn);
                    mDbCommand.CommandType = CommandType.StoredProcedure;
                    mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = this.CurrUser.EmpCode;
                    mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value                                              ;
                    mDbCommand.Parameters.AddWithValue("@RegimeOption", SqlDbType.VarChar).Value  = rdbRegime.SelectedValue;
                    mDSet = new DataSet();
                    mQueryResult = new SqlDataAdapter(mDbCommand);
                    mQueryResult.Fill(mDSet);

                    if (mDSet !=null && mDSet.Tables.Count>0 && mDSet.Tables[0].Rows.Count>0)
                    {
                        mDSet = null;
                         mStoredProcName = String.Empty;
                         mDbCommand = null;
                         mQueryResult = null;
                        mDSet = new DataSet();
                        //this.EmpCode;
                         empcode = this.CurrUser.EmpCode;
                        //String empcode = "108185";
                        //String empcode = "101260";
                        //String empcode = "101467";
                        //String empcode = "107807";
                        //String empcode = "101457";
                        //String empcode = "101440";
                        //this.EmpCode = "108185";
                        try
                        {
                            if (Conn.State == ConnectionState.Closed)
                            {
                                Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                                Conn.Open();
                            }
                            mStoredProcName = "sp_PageAfterOTP_forNew";
                            mDbCommand = new SqlCommand(mStoredProcName, Conn);
                            mDbCommand.CommandType = CommandType.StoredProcedure;

                            mDSet = new DataSet();
                            mQueryResult = new SqlDataAdapter(mDbCommand);
                            mQueryResult.Fill(mDSet);

                            if (mDSet != null && mDSet.Tables.Count > 0 && mDSet.Tables[0].Rows.Count > 0)
                            {
                                Response.Redirect(mDSet.Tables[0].Rows[0]["ConfigValue"].ToString(), false);
                            }
                            //return mDSet;
                        }
                        catch (Exception ex)
                        {
                            objUserObjectDAL.LogError(ex.Message, "btnSubmitRegime_Click");
                            //return null;
                        }
                        //if (mDSet.Tables[0].Rows[0]["CanDoMobileChange"].ToString() == "X")
                        //{
                        //    Response.Redirect("frmEmpCTCRestructNewWindow.aspx?Regime=" + rdbRegime.SelectedValue.ToString(), false); 
                        //}
                        //else
                        //{
                        //    Response.Redirect("Logout.aspx", false);
                        //}
                    }                     
                    //return mDSet;
                }
                catch (Exception ex)
                {
                    objUserObjectDAL.LogError(ex.Message, "btnSubmitRegime_Click");
                    //return null;
                }
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "btnSubmitRegime_Click");
                throw;
            }            
        }
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        

            DataSet mDSet = null;
            String mStoredProcName = String.Empty;
            SqlCommand mDbCommand = null;
            SqlDataAdapter mQueryResult = null;
            mDSet = new DataSet();
            //this.EmpCode;
            String empcode = this.CurrUser.EmpCode;
            //String empcode = "108185";
            //String empcode = "101260";
            //String empcode = "101467";
            //String empcode = "107807";
            //String empcode = "101457";
            //String empcode = "101440";
            //this.EmpCode = "108185";
            try
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                    Conn.Open();
                }
                mStoredProcName = "sp_PageAfterOTP_forNew";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;

                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);

                if (mDSet != null && mDSet.Tables.Count > 0 && mDSet.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect(mDSet.Tables[0].Rows[0]["ConfigValue"].ToString(), false);
                }
                //return mDSet;
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "btnSubmitRegime_Click");
                //return null;
            }
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnCalcTax_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CalcTaxAsPerOption.aspx", false);

    }
}