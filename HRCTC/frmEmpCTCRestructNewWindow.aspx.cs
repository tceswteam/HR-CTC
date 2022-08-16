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

public partial class frmEmpCTCRestructNewWindow : System.Web.UI.Page
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
            this.CurrUser = mUserObjectBLL.GetUserSession();
            if (!Page.IsPostBack)
            {
                string RegimeVal;
                if (Request.QueryString["Regime"] != null)
                {
                    RegimeVal = Request.QueryString["Regime"].ToString();
                    lblRegimeOption.Text = Request.QueryString["Regime"].ToString();
                }
                GetCurrentFY();
                DataSet Dset = GetEmployeeBaseCTC();
                if (Dset != null)
                {
                    FillEmployeeBaseCTC(Dset);
                    txtMobile.Attributes.Add("autocomplete", "off");
                    txtMobile.Attributes.Add("onkeyup", "Compare_Data(" + txtMobile.ClientID + "," + lblMobileMaxCeiling.ClientID + ",event);");
                    txtAA.Attributes.Add("onkeyup", "Compare_Data(" + txtAA.ClientID + "," + lblAAMaxCeiling.ClientID + ",event);");                  
                    //txtMobile.Attributes.Add("onfocusout", "CheckForMobileAmount();");
                    PrepareActionButton();
                }
            }
        }
        catch (Exception ex )
        {
            objUserObjectDAL.LogError(ex.Message, "Default Page load");
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
        DateTime dt1 = Convert.ToDateTime( mDSet.Tables[3].Rows[0][0].ToString());
        dt1=dt1.AddYears(-1);
        lblYearEndDate.Text = Convert.ToDateTime(dt1.ToString()).ToShortDateString();
        lblYearStartDate.Text = Convert.ToDateTime(mDSet.Tables[2].Rows[0][0].ToString()).ToShortDateString();
        ltrFinYr.Text = hdnFinancialYearCurr.Value;
    }

    protected void PrepareActionButton()
    {
        DataSet mDSet = null;
        mDSet = new DataSet();
        String mStoredProcName = String.Empty;
        SqlCommand mDbCommand = null;
        try
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                Conn.Open();
            }
            mStoredProcName = "sp_GetEmpMobMealCompUpdOrNot";
            mDbCommand = new SqlCommand(mStoredProcName, Conn);
            mDbCommand.CommandType = CommandType.StoredProcedure;
            mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = this.CurrUser.EmpCode;
            mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
            SqlDataAdapter mQueryResult = new SqlDataAdapter(mDbCommand);
            mQueryResult.Fill(mDSet);
            if (mDSet != null)
            {
                if (mDSet.Tables[0].Rows[0][0].ToString() == "True")
                {
                    btnPrintCTCRestruc0.Visible = false;
                    rdoGetMealVoucher.Visible = false;
                    txtMobile.Visible = false;
                    lblMealCardCaption.Visible = false;
                    txtMobile.Text = mDSet.Tables[1].Rows[0]["Emp_Component_Value"].ToString(); //Component_ID=8
                    lblMobile.Text = txtMobile.Text;
                    lblCanteenRestruct.Text = mDSet.Tables[2].Rows[0]["Emp_Component_Value"].ToString(); //Component_ID=9
                    txtAA.Text = mDSet.Tables[3].Rows[0]["Emp_Component_Value"].ToString(); //Component_ID=37
                }
                else
                { 
                    btnPrintCTCRestruc0.Visible = true;
                    rdoGetMealVoucher.Visible = true;
                    txtMobile.Enabled = true ;
                }
            }
        }
        catch (Exception ex)
        {
            Conn.Close();
            objUserObjectDAL.LogError(ex.Message, "sp_GetEmpMobMealCompUpdOrNot");
        }       
    }

    //Considering Form mode make control editable or readonly
    protected void SetControlBehivor()
    {     
       
       
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
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearPrev.Value;
                //mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;

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
        catch (Exception ex )
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
                        lblEmpCode.Text =  ds.Tables[0].Rows[0]["EmpID"].ToString() ;                        
                        lblEmpPanCard.Text = ds.Tables[0].Rows[0]["Emp_PAN"].ToString();
                        lblDateofJoining.Text = ds.Tables[0].Rows[0]["DateofJoining"].ToString();
                    }
                    int i;
                    i = 0;

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        var dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Basic").FirstOrDefault();                    

                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Mobile").FirstOrDefault();
                        if (dataRow != null)
                        {
                           // lblMobileExisting.Text = "0";
                            lblMobileExisting.Text = dataRow["Emp_Component_Value"].ToString();
                            txtMobile.Text = "";
                            lblMobileMaxCeiling.Text = dataRow["MaxCeilingForExcemption"].ToString();
                            lblMobile.Text = txtMobile.Text;
                        }
                        
                        //dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "RevisedAA").FirstOrDefault();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "AA").FirstOrDefault();
                        if (dataRow != null)
                        {
                            lblAAExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        }
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "MealCard").FirstOrDefault();
                        if (dataRow != null)
                        {
                            lblMealExisting.Text =  dataRow["Emp_Component_Value"].ToString();

                            hdnMealCardValue.Value = lblMealExisting.Text;
                        }
                        // lblCanteenRestruct.Text = lblMealExisting.Text;
                        // txtAA.Text = lblAAExisting.Text;
                    }
                }
            }
            PrepareActionButton();
            SetControlBehivor();   
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillEmployeeBaseCTC");
            throw;
        }
      
    }

  

    protected void btnApplyCTC_Click(object sender, EventArgs e)
     {
         
    }
    private String _con_ans_pullback = String.Empty;

    public String con_ans_pullback
        {
            get { return _con_ans_pullback; }
            set { _con_ans_pullback = value; }
        }
  
   

    protected void btnPrintCTCRestruc_Click(object sender, EventArgs e)
    {      
        DataSet mDSet = null;
        String mStoredProcName = String.Empty;
        SqlCommand mDbCommand = null;

       
            try
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                    Conn.Open();
                }
                SqlDataAdapter mQueryResult = null;
                mStoredProcName = "spr_UpdateEmpCTCRestructureStatus";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@EmpID", SqlDbType.VarChar).Value = lblEmpCode.Text;
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = "2017";
                mDbCommand.Parameters.AddWithValue("@IsEmpFinalizedCTCRestruc", SqlDbType.VarChar).Value = "true";
                mDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "btnPrintCTCRestruc_Click");
            }
    }
    // By using this method we can convert datatable to xml
    public string ConvertDatatableToXML(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        //xmlstr = xmlstr.Replace("<DocumentElement>", "");
        return (xmlstr);
    }    

    protected void hdnBtnForPrint_Click(object sender, EventArgs e)
    {
    }

    protected void hdnbtnForRevoke_Click(object sender, EventArgs e)
    {
    }

    //public void OnConfirmPullback(object sender, EventArgs e)
    //{
    //    string confirmValue = Request.Form["confirm_value_pullback"];
    //    if (confirmValue == "Yes")
    //    {
    //        btnPullback0_Click(sender, e);
    //        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!');", true);
    //    }
    //    else
    //    {
    //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No change in restructured salary !');", true);
    //    }
    //}
    protected void OnConfirmPrint(object sender, EventArgs e)
    { 
    }
    protected void btnPrintFinal_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default2.aspx?EmpCode=" + this.CurrUser.EmpCode + "&FinYear=2017");
    }
    private DataSet GetEmployeeCTC()
    {
        string conString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        DataSet mDSet = new DataSet();
        SqlCommand mDbCommand = new SqlCommand();
        SqlConnection Conn = new SqlConnection(conString);
        try
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                Conn.Open();
            }
            mDbCommand.CommandText = "GETOnlyRestrucCTC_NEW";
            mDbCommand.Connection = Conn;
            mDbCommand.CommandType = CommandType.StoredProcedure;
            mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = this.CurrUser.EmpCode;
            mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = "2017";

            mDSet = new DataSet();
            SqlDataAdapter mQueryResult = new SqlDataAdapter(mDbCommand);
            mQueryResult.Fill(mDSet);
            return mDSet;
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "GetEmployeeCTC");
            return null;
        }
    }
    private RestructCTCDataSet GetData(string query)
    {
        try
        {
            string conString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (RestructCTCDataSet dsCustomers = new RestructCTCDataSet())
                    {
                        sda.Fill(dsCustomers, "DataTable1");
                        return dsCustomers;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "GetData");
            throw;
        }
    }
    protected void btnPrintRDLC_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default2.aspx?EmpCode=" + this.CurrUser.EmpCode + "&FinYear=2017");       
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "CloseWindow();");
    }
    protected void trigger9_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void OnConfirmPullback(object sender, ImageClickEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value_pullback"];
        if (confirmValue == "Yes")
        {
            btnPullback0_Click(sender, e);
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!');", true);
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No change in restructured salary !');", true);
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Logout.aspx");
        //objUserObjectBLL.LogoutUser();
    }
    protected void btnPullback0_Click(object sender, ImageClickEventArgs e)
    {
        string str = con_ans_pullback;
        DataSet mDSet = null;
        String mStoredProcName = String.Empty;
        SqlCommand mDbCommand = null;
        try
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                Conn.Open();
            }
            SqlDataAdapter mQueryResult = null;
            mStoredProcName = "spr_EmployeeRestructuredSalary";
            mDbCommand = new SqlCommand(mStoredProcName, Conn);
            mDbCommand.CommandType = CommandType.StoredProcedure;
            mDbCommand.Parameters.AddWithValue("@tblEmployeeRestructuredSalary", SqlDbType.VarChar).Value = null;
            mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = lblEmpCode.Text;
            mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = "2017";
            mDbCommand.Parameters.AddWithValue("@IsEmpFinalizedCTCRestruc", SqlDbType.VarChar).Value = "false";
            mDbCommand.Parameters.AddWithValue("@SuperanuationOption", SqlDbType.VarChar).Value = "1";
            mDbCommand.ExecuteNonQuery();
             
            mDSet = null;
            mDSet = new DataSet();
            hdnFormMode.Value = "0";
            mDSet = GetEmployeeBaseCTC();
            FillEmployeeBaseCTC(mDSet);
            PrepareActionButton();
            SetControlBehivor();


            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "btnPullback_Click");
        }
    }
    protected void btnExit_Click1(object sender, ImageClickEventArgs e)
    {   
    }
    protected void ddlSuperanuation_SelectedIndexChanged1(object sender, EventArgs e)
    {
    }
    protected void txtLTA_TextChanged1(object sender, EventArgs e)
    {
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void lnkDummy_Click(object sender, EventArgs e)
    {
    }
    protected void hdnSAOLD_ValueChanged(object sender, EventArgs e)
    {
    }
    protected void txtHRA_TextChanged(object sender, EventArgs e)
    {
    }
    protected void btnApplySAChanges_Click(object sender, EventArgs e)
    {   
    }
    protected void rdoGetMealVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 CurrMealCardVal, CurrMobileVal,MealExisting,MobilesExiting;
        string txtAA1;
        txtAA1 = lblAAExisting.Text == "" ? "0" : lblAAExisting.Text;
        Int64 AA = Convert.ToInt64(txtAA1);

        CurrMealCardVal = Convert.ToInt64(rdoGetMealVoucher.SelectedValue == null || rdoGetMealVoucher.SelectedValue == "" ? "0" : rdoGetMealVoucher.SelectedValue.ToString());

        CurrMobileVal = Convert.ToInt64(txtMobile.Text == null || txtMobile.Text == "" ? "0" : txtMobile.Text);

        MealExisting = Convert.ToInt64(lblMealExisting.Text == null || lblMealExisting.Text == "" ? "0" : lblMealExisting.Text);
        MobilesExiting = Convert.ToInt64(lblMobileExisting.Text == null || lblMobileExisting.Text == "" ? "0" : lblMobileExisting.Text);

        if (CurrMealCardVal != MealExisting)
        {
            if (CurrMealCardVal > MealExisting)
            {
                CurrMealCardVal = CurrMealCardVal - MealExisting;
            }
            else
            {
                CurrMealCardVal = - ( MealExisting - CurrMealCardVal);
            }
        }
        else
        {
            CurrMealCardVal = 0;
        }
        if (CurrMobileVal != MobilesExiting)
        {
            if (CurrMobileVal > MealExisting)
            {
                CurrMobileVal = CurrMobileVal - MobilesExiting;
            }
            else
            {
                CurrMobileVal = -( MobilesExiting - CurrMobileVal);
            }
        }
        else
        {
            CurrMobileVal = 0;
        }
        hdnMealCardValue.Value = rdoGetMealVoucher.SelectedValue;
        AA = AA - CurrMobileVal - CurrMealCardVal;
        txtAA.Text = AA.ToString();
        lblCanteenRestruct.Text = rdoGetMealVoucher.SelectedValue;

    }
    protected void txtMobile_TextChanged(object sender, EventArgs e)
    {

        Int64 CurrMealCardVal, CurrMobileVal, MealExisting, MobilesExiting;
        string txtAA1;
        txtAA1 = lblAAExisting.Text == "" ? "0" : lblAAExisting.Text;
        Int64 AA = Convert.ToInt64(txtAA1);

        CurrMealCardVal = Convert.ToInt64(rdoGetMealVoucher.SelectedValue == null || rdoGetMealVoucher.SelectedValue == "" ? "0" : rdoGetMealVoucher.SelectedValue.ToString());

        CurrMobileVal = Convert.ToInt64(txtMobile.Text == null || txtMobile.Text == "" ? "0" : txtMobile.Text);

        MealExisting = Convert.ToInt64(lblMealExisting.Text == null || lblMealExisting.Text == "" ? "0" : lblMealExisting.Text);
        MobilesExiting = Convert.ToInt64(lblMobileExisting.Text == null || lblMobileExisting.Text == "" ? "0" : lblMobileExisting.Text);

        if (CurrMealCardVal != MealExisting)
        {
            if (CurrMealCardVal > MealExisting)
            {
                CurrMealCardVal = CurrMealCardVal - MealExisting;
            }
            else
            {
                CurrMealCardVal = -(MealExisting - CurrMealCardVal);
            }
        }
        else
        {
            CurrMealCardVal = 0;
        }
        if (CurrMobileVal != MobilesExiting)
        {
            if (CurrMobileVal > MealExisting)
            {
                CurrMobileVal = CurrMobileVal - MobilesExiting;
            }
            else
            {
                CurrMobileVal = -(MobilesExiting - CurrMobileVal);
            }
        }
        else
        {
            CurrMobileVal = 0;
        }
        hdnMealCardValue.Value = rdoGetMealVoucher.SelectedValue;
        AA = AA - CurrMobileVal - CurrMealCardVal;
        txtAA.Text = AA.ToString();
        lblCanteenRestruct.Text = rdoGetMealVoucher.SelectedValue;

    }
    protected void OnConfirmPrint(object sender, ImageClickEventArgs e)
    {
        if (txtAA.Text == "" || txtMobile.Text == "" || lblCanteenRestruct.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please input for the Mobile and Meal card option to Save changes !');", true);
        }
        else
        {

            DataSet mDSet = null;
            String mStoredProcName = String.Empty;
            SqlCommand mDbCommand = null;
            try
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                    Conn.Open();
                }
                SqlDataAdapter mQueryResult = null;
                mStoredProcName = "sp_UpdateEmpMobMealAA";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                
                mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = lblEmpCode.Text;
                mDbCommand.Parameters.AddWithValue("@Component_ID", SqlDbType.VarChar).Value = "38";//RevisedAA
                //mDbCommand.Parameters.AddWithValue("@Component_ID", SqlDbType.VarChar).Value = "9";//AA
                mDbCommand.Parameters.AddWithValue("@Component_Value", SqlDbType.VarChar).Value = txtAA.Text;
                mDbCommand.Parameters.AddWithValue("@Grade", SqlDbType.VarChar).Value = lblGrade.Text;
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value =hdnFinancialYearCurr.Value;             
                
                mDbCommand.ExecuteNonQuery();
                Conn.Close();
                Conn.Open();

                mStoredProcName = "sp_UpdateEmpMobMealAA";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;

                mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = lblEmpCode.Text;
                mDbCommand.Parameters.AddWithValue("@Component_ID", SqlDbType.VarChar).Value = "8";//AA
                mDbCommand.Parameters.AddWithValue("@Component_Value", SqlDbType.VarChar).Value = txtMobile.Text;
                mDbCommand.Parameters.AddWithValue("@Grade", SqlDbType.VarChar).Value = lblGrade.Text;
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;

                mDbCommand.ExecuteNonQuery();
                Conn.Close();
                Conn.Open();

                mStoredProcName = "sp_UpdateEmpMobMealAA";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;

                mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = lblEmpCode.Text;
                mDbCommand.Parameters.AddWithValue("@Component_ID", SqlDbType.VarChar).Value = "37";//AA
                mDbCommand.Parameters.AddWithValue("@Component_Value", SqlDbType.VarChar).Value = lblCanteenRestruct.Text;
                mDbCommand.Parameters.AddWithValue("@Grade", SqlDbType.VarChar).Value = lblGrade.Text;
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;

                SqlDataAdapter adapter = new SqlDataAdapter(mDbCommand);
                DataSet Ds = new DataSet();
                adapter.Fill(Ds);
                mDbCommand.Dispose();
                

                Conn.Close();
                if (Ds != null)
                {
                    if (Ds.Tables[0].Rows[0][0].ToString() == "True")
                    {
                        btnPrintCTCRestruc0.Visible = false;
                        lblMobile.Text = txtMobile.Text;
                        txtMobile.Visible = false;
                        lblMealCardCaption.Visible = false;
                        lblMobile.Visible = true;
                        rdoGetMealVoucher.Visible = false;
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "RedirectToLogin();", true);
                        Response.Redirect("DummyNew.aspx");
                    }
                }
              //  Conn.Open();
              // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
            }
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "btnPullback_Click");
            }
        }
    }
    
}
        




