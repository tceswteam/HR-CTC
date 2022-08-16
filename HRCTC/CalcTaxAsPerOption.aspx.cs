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
    //public PDFGeneratorBLL mPDF = new PDFGeneratorBLL();
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
           
            if (!Page.IsPostBack)
            {
                GetCurrentFY();
                DataSet Dset = GetEmpLastFinYrDedDet();
                //mUserObjectBLL.writetolog("frmEmpCTCRestruct Page_Load after data pulling for employee");
                if (Dset != null)
                {
                    //FillSuperanuation();
                    FillEmpDed(Dset);
                }

                Dset = GetEmployeeBaseCTC();
                //mUserObjectBLL.writetolog("frmEmpCTCRestruct Page_Load after data pulling for employee");
                if (Dset != null)
                {
                    if (Dset.Tables[0].Rows.Count > 0)
                    {
                        lblEmployeeName.Text = Dset.Tables[0].Rows[0]["EmpName"].ToString();
                        lblGrade.Text = Dset.Tables[0].Rows[0]["Grade"].ToString();
                        lblEmpCode.Text = Dset.Tables[0].Rows[0]["EmpID"].ToString();
                        lblEmpPanCard.Text = Dset.Tables[0].Rows[0]["Emp_PAN"].ToString();
                        lblDateofJoining.Text = Dset.Tables[0].Rows[0]["DateofJoining"].ToString();
                    }
                }

                txtstandardded.Attributes.Add("onfocusout", "Compare_Data(" + txtstandardded.ClientID + "," + hdnStdDed.ClientID + ",event);");
                txtProfessional.Attributes.Add("onfocusout", "Compare_Data(" + txtProfessional.ClientID + "," + hdnProfTax.ClientID + ",event);");
                txtEmployeeContriNPS80CCD.Attributes.Add("onfocusout", "Compare_Data(" + txtEmployeeContriNPS80CCD.ClientID + "," + hdnNPS80cc.ClientID + ",event);");
                txtRebate80C.Attributes.Add("onfocusout", "Compare_Data(" + txtRebate80C.ClientID + "," + hdnc80reb.ClientID + ",event);");
                txthousingloan.Attributes.Add("onfocusout", "Compare_Data(" + txthousingloan.ClientID + "," + hdnHouInts.ClientID + ",event);");
                txtMediclaim.Attributes.Add("onfocusout", "Compare_Data(" + txtMediclaim.ClientID + "," + hdnMedClm.ClientID + ",event);");

                txtEstimatedAnnual.Attributes.Add("onkeyup", "PerformCalculation();");
                txtstandardded.Attributes.Add("onkeyup", "PerformCalculation();");
                txtHRAExisting.Attributes.Add("onkeyup", "PerformCalculation();");
                txtConAlloExisting.Attributes.Add("onkeyup", "PerformCalculation();");
                txtLTAExisting.Attributes.Add("onkeyup", "PerformCalculation();");
                txtOthers.Attributes.Add("onkeyup", "PerformCalculation();");
                txtProfessional.Attributes.Add("onkeyup", "PerformCalculation();");
                txthousingloan.Attributes.Add("onkeyup", "PerformCalculation();");
                txtMediclaim.Attributes.Add("onkeyup", "PerformCalculation();");
                txtdonation.Attributes.Add("onkeyup", "PerformCalculation();");
                txtDisability.Attributes.Add("onkeyup", "PerformCalculation();");
                txtEduLoan.Attributes.Add("onkeyup", "PerformCalculation();");
                txtEmployeeContriNPS80CCD.Attributes.Add("onkeyup", "PerformCalculation();");
                txtEmployerContriNPS80CCD2.Attributes.Add("onkeyup", "PerformCalculation();");
                txtRebate80C.Attributes.Add("onkeyup", "PerformCalculation();");

            }
        }
        catch (Exception ex)
        {
           // objUserObjectDAL.LogError(ex.Message, "Default Page load");
            throw;
        }
    }
    protected DataSet GetEmpLastFinYrDedDet()
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
               // objUserObjectDAL.LogError("Check for financial year coming", hdnFinancialYearPrev.Value);
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                    Conn.Open();
                }
                mStoredProcName = "sp_GetEmpLastYrDed";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = this.CurrUser.EmpCode;
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearPrev.Value;
                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);
                return mDSet;
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "sp_GetEmpLastYrDed");
                return null;
            }       
    }

    protected void FillEmpDed(DataSet ds)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //objUserObjectDAL.LogError("Check for financial year coming", ds.Tables[0].Rows[0]["tot_earn"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["standard_ded"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["hra_rebate"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["conv_reb"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["lta_reb"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["other_reb"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["prof_tax"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["housing_int"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["mediclaim_reb"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["g80_reb"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["dd80_d80u"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["edu_loan"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["nps_80ccd_1b"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["nps_80ccd_2"].ToString()
            //    + " " + ds.Tables[0].Rows[0]["c80_reb"].ToString()
            //    );

            lblEstimatedAnnual.Text = ds.Tables[0].Rows[0]["tot_earn"].ToString();
            lblstandardded.Text = ds.Tables[0].Rows[0]["standard_ded"].ToString();
            lblHRAExisting.Text = ds.Tables[0].Rows[0]["hra_rebate"].ToString();
            lblConAlloExisting.Text = ds.Tables[0].Rows[0]["conv_reb"].ToString();
            lblLTAExisting.Text = ds.Tables[0].Rows[0]["lta_reb"].ToString();
            lblOthers.Text = ds.Tables[0].Rows[0]["other_reb"].ToString();
            lblProfessional.Text = ds.Tables[0].Rows[0]["prof_tax"].ToString();
            lblhousingloan.Text = ds.Tables[0].Rows[0]["housing_int"].ToString();
            lblMediclaim.Text = ds.Tables[0].Rows[0]["mediclaim_reb"].ToString();
            lbldonation.Text = ds.Tables[0].Rows[0]["g80_reb"].ToString();
            lblDisability.Text = ds.Tables[0].Rows[0]["dd80_d80u"].ToString();
            lblEduLoan.Text = ds.Tables[0].Rows[0]["edu_loan"].ToString();
            lblEmployeeContriNPS80CCD.Text = ds.Tables[0].Rows[0]["nps_80ccd_1b"].ToString();
            lblEmployerContriNPS80CCD2.Text = ds.Tables[0].Rows[0]["nps_80ccd_2"].ToString();
            lblRebate80C.Text = ds.Tables[0].Rows[0]["c80_reb"].ToString();
            lblGroupATotalExisting.Text = ds.Tables[0].Rows[0]["TotalDed"].ToString();

            hdnc80reb.Value = ds.Tables[1].Rows[0]["c80_reb"].ToString(); 
            hdnMedClm.Value = ds.Tables[1].Rows[0]["mediclaim_reb"].ToString(); 
            hdnNPS80cc.Value = ds.Tables[1].Rows[0]["nps_80ccd_1b"].ToString();
            hdnProfTax.Value = ds.Tables[1].Rows[0]["prof_tax"].ToString();
            hdnStdDed.Value = ds.Tables[1].Rows[0]["standard_ded"].ToString();
            hdnHouInts.Value = ds.Tables[1].Rows[0]["housing_int"].ToString(); 

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
    private void GetCurrentFY()
    {
        DataSet mDSet = null;
        CommonBLL mCommonBLL = new CommonBLL();
        mDSet = mCommonBLL.GetFinancialMonthYr();
        hdnFinancialYearCurr.Value = mDSet.Tables[7].Rows[0]["Text"].ToString();
        hdnFinancialYearPrev.Value = mDSet.Tables[7].Rows[1]["Text"].ToString();
        ltrFinYr.Text = hdnFinancialYearCurr.Value;
    }
    protected void btnCalcuTaxForRegime_Click(object sender, EventArgs e)
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
                mStoredProcName = "[sp_CalcTaxAsPerRegime]";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@NetIncome", SqlDbType.VarChar).Value = Convert.ToInt64(hdnOLDRegimeNetIncome.Value == "" ? "0" : hdnOLDRegimeNetIncome.Value);
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
                mDbCommand.Parameters.AddWithValue("@itslab_type", SqlDbType.VarChar).Value = "Itold-slab";
                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);

                if (mDSet != null && mDSet.Tables.Count>0 && mDSet.Tables[0].Rows.Count>0)
                {
//                    lnlOLDRegimeTaxCalc
//lblNewRegimeTaxCalc
                    lblGroupATotalRestruc.Text = hdnTotal.Value;
                    lblOldRegimeInc.Text=hdnOLDRegimeNetIncome.Value;
                    lnlOLDRegimeTaxCalc.Text = mDSet.Tables[0].Rows[0]["@TAX_COMPLETE"].ToString();
                }
      

                mStoredProcName = "[sp_CalcTaxAsPerRegime]";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@NetIncome", SqlDbType.VarChar).Value = Convert.ToInt64(hdnNEWRegimeNetIncome.Value == "" ? "0" : hdnNEWRegimeNetIncome.Value);
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
                mDbCommand.Parameters.AddWithValue("@itslab_type", SqlDbType.VarChar).Value = "itnew-slab";
                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);
                if (mDSet != null && mDSet.Tables.Count > 0 && mDSet.Tables[0].Rows.Count > 0)
                {
                    //                    lnlOLDRegimeTaxCalc
                    //lblNewRegimeTaxCalc
                    lblGroupATotalRestruc.Text = hdnTotal.Value;
                    lblNewRegimeInc.Text = hdnNEWRegimeNetIncome.Value;
                    lblNewRegimeTaxCalc.Text = mDSet.Tables[0].Rows[0]["@TAX_COMPLETE"].ToString();
                }

                //return mDSet;
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "GetEmployeeBaseCTC");
                //return null;
            }
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillSuperanuation");
            throw;
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalaryIncomeTaxCalc.aspx",false);
    }
    protected void btnTaxNotif_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "OpenTaxNotif", "OpenTaxNotif();");
    }
    protected void btnCalcTax_Click(object sender, ImageClickEventArgs e)
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
                mStoredProcName = "[sp_CalcTaxAsPerRegime]";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@NetIncome", SqlDbType.VarChar).Value = Convert.ToInt64(hdnOLDRegimeNetIncome.Value == "" ? "0" : hdnOLDRegimeNetIncome.Value);
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
                mDbCommand.Parameters.AddWithValue("@itslab_type", SqlDbType.VarChar).Value = "Itold-slab";
                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);

                if (mDSet != null && mDSet.Tables.Count > 0 && mDSet.Tables[0].Rows.Count > 0)
                {
                    //                    lnlOLDRegimeTaxCalc
                    //lblNewRegimeTaxCalc
                    lblGroupATotalRestruc.Text = hdnTotal.Value;
                    lblOldRegimeInc.Text = hdnOLDRegimeNetIncome.Value;
                    lnlOLDRegimeTaxCalc.Text = mDSet.Tables[0].Rows[0]["@TAX_COMPLETE"].ToString();
                }


                mStoredProcName = "[sp_CalcTaxAsPerRegime]";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@NetIncome", SqlDbType.VarChar).Value = Convert.ToInt64(hdnNEWRegimeNetIncome.Value == "" ? "0" : hdnNEWRegimeNetIncome.Value);
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
                mDbCommand.Parameters.AddWithValue("@itslab_type", SqlDbType.VarChar).Value = "itnew-slab";
                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);
                if (mDSet != null && mDSet.Tables.Count > 0 && mDSet.Tables[0].Rows.Count > 0)
                {
                    //                    lnlOLDRegimeTaxCalc
                    //lblNewRegimeTaxCalc
                    lblGroupATotalRestruc.Text = hdnTotal.Value;
                    lblNewRegimeInc.Text = hdnNEWRegimeNetIncome.Value;
                    lblNewRegimeTaxCalc.Text = mDSet.Tables[0].Rows[0]["@TAX_COMPLETE"].ToString();
                }

                //return mDSet;
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "GetEmployeeBaseCTC");
                //return null;
            }
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillSuperanuation");
            throw;
        }

    }
    protected void btnBack_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SalaryIncomeTaxCalc.aspx", false);
    }
}