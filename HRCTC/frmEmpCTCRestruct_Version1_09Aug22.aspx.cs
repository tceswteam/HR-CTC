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

public partial class _Default : System.Web.UI.Page
{
    public string ConnectionString;
    public SqlConnection Conn = new SqlConnection();
    public SqlCommand Cmd;
    public PDFGeneratorBLL mPDF = new PDFGeneratorBLL();
    protected string _EmpCode;
    UserObjectDAL objUserObjectDAL = new UserObjectDAL();
    UserObjectBLL objUserObjectBLL = new UserObjectBLL(); 

    //public string EmpCode
    //{
    //    get
    //    {
    //        UserObjectSC objUserAclSC = new UserObjectSC();
    //        if(Session["UserObj"] !=null)
    //        {

    //            objUserAclSC = (UserObjectSC)Session["UserObj"];
    //              ViewState["EmpCode"] = objUserAclSC.EmpCode;
    //        }
    //        return ViewState["EmpCode"].ToString();
    //    }
    //    set { ViewState["EmpCode"] = value; }
    //}

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
            hdnIsMealVoucherOpted.Value = "false";
            if (!Page.IsPostBack)
            {
                //mUserObjectBLL.writetolog("frmEmpCTCRestruct Page_Load before data pulling for employee");
                GetCurrentFY();
                DataSet Dset = GetEmployeeBaseCTC();
                //mUserObjectBLL.writetolog("frmEmpCTCRestruct Page_Load after data pulling for employee");
                if (Dset != null)
                {
                    FillSuperanuation();
                    FillEmployeeBaseCTC(Dset);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
                    txtHRA.Attributes.Add("onkeyup", "Compare_Data(" + txtHRA.ClientID + "," + lblHRAMaxCeiling.ClientID + ",event);");
                    Int64 HRA_min, HRA_max;
                    HRA_min = Convert.ToInt64(lblBasicExisting.Text) * 40 / 100;
                    HRA_max = Convert.ToInt64(lblBasicExisting.Text) * 50 / 100;
                    
                    txtHRA.Attributes.Add("onfocusout", "CheckForValueRangeHRA(" + HRA_min + "," + HRA_max+ ","+ txtHRA.ClientID +",'HRA Value');");
                    //txtCA.Attributes.Add("onfocusout", "CheckForValueRangeCA(" + 1600 + ","  + txtCA.ClientID + ",'CA Value');");
                    txtHRA.Attributes.Add("autocomplete", "off");
                    txtCEA.Attributes.Add("autocomplete", "off");
                    txtCHA.Attributes.Add("autocomplete", "off");
                    txtLTA.Attributes.Add("autocomplete", "off");
                    txtMARestruct.Attributes.Add("autocomplete", "off");
                    txtMobile.Attributes.Add("autocomplete", "off");
                    //Commented By Mohan On 27 May 2019
                    //txtNPSSA.Attributes.Add("autocomplete", "off");
                    //End By Mohan
                    txtCA.Attributes.Add("autocomplete", "off");
                    //Added By Mohan On 27 May 2019
                    txtNPSNewAmt.Attributes.Add("autocomplete", "off");
                    //End By Mohan
                    
                    //txtHRA.Attributes.Add("onkeyup", "PerformCalculation();");
                    //Below line commented on 29 June 17 as required from Nayan
                    //txtCA.Attributes.Add("onkeyup", "Compare_Data(" + txtCA.ClientID + "," + lblCAMaxCeiling.ClientID + ",event);");
                    txtLTA.Attributes.Add("onkeyup", "PerformCalculation();");
                    txtHRA.Attributes.Add("onkeyup", "PerformCalculation();");
                    txtCA.Attributes.Add("onkeyup", "PerformCalculation();");
                    txtMARestruct.Attributes.Add("onkeyup", "PerformCalculation();");
                    txtCEA.Attributes.Add("onkeyup", "Compare_Data(" + txtCEA.ClientID + "," + lblCEAMaxCeiling.ClientID + ",event);");
                    txtCHA.Attributes.Add("onkeyup", "Compare_Data(" + txtCHA.ClientID + "," + lblCHAMaxCeiling.ClientID + ",event);");
                    //txtMARestructRestruct.Attributes.Add("onkeyup", "Compare_Data(" + txtMARestructRestruct.ClientID + "," + lblMAMaxCeiling.ClientID + ",event);");
                    txtMobile.Attributes.Add("onkeyup", "Compare_Data(" + txtMobile.ClientID + "," + lblMobileCeilingForEmp.ClientID + ",event);");
                    txtAA.Attributes.Add("onkeyup", "Compare_Data(" + txtAA.ClientID + "," + lblAAMaxCeiling.ClientID + ",event);");
                    //Commented By Mohan On 27 May 2019
                    //txtNPSSA.Attributes.Add("onfocusout", "CheckForSATransferNPS(" + txtAASA.ClientID + ");");
                    //txtNPSSA.Attributes.Add("onkeyup", "OnNPSValueEnter();");                    
                    //End By Mohan
                    //txtAASA.Attributes.Add("onfocusout", "CheckForSATransferAA(" + txtNPSSA.ClientID + ");");
                    txtMobile.Attributes.Add("onfocusout", "CheckForMobileAmount();");

                    //Added By Mohan On 27 May 2019
                    txtNPSNewAmt.Attributes.Add("onkeyup", "PerformCalculation();");
                    txtNPSNewAmt.Attributes.Add("onfocusout", "CheckForValueRangeNPS(" + hiddNPSMinLimit.Value + "," + hiddNPSMaxLimit.Value + "," + txtNPSNewAmt.ClientID + ");");
                    //End By Mohan
                }
            }
        }
        catch (Exception ex )
        {
            objUserObjectDAL.LogError(ex.Message, "Default Page load");
            throw;
        }
    }

    //Filling Dropdown of superanuation 
    protected DataSet FillSuperanuation()
    {
        DataSet mDSet = null;
        String mStoredProcName = String.Empty;
        SqlCommand mDbCommand = null;
        SqlDataAdapter mQueryResult = null;
        mDSet = new DataSet();

        try
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                Conn.Open();
            }
            mStoredProcName = "spr_GetSuperanuationOption";
            mDbCommand = new SqlCommand(mStoredProcName, Conn);
            mDbCommand.CommandType = CommandType.StoredProcedure;
            mDbCommand.Parameters.AddWithValue("@Parameter_For", SqlDbType.VarChar).Value = "SuperanuationOption";

            mDSet = new DataSet();
            mQueryResult = new SqlDataAdapter(mDbCommand);
            mQueryResult.Fill(mDSet);
            ddlSuperanuation.DataSource = mDSet;

            ddlSuperanuation.DataTextField = "Parameter_Name";
            ddlSuperanuation.DataValueField = "Parameter_ID";
            ddlSuperanuation.DataBind();
            return mDSet;
            

            
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillSuperanuation");
            return null;
        }
 
    }

    protected void PrepareActionButton()
    {
        
      
        if (ViewState["IsEmpRestrucSalary"].ToString() == "Yes")
        {
            btnApplyCTC.Visible = false;
            //btnPullback.Visible = true;
            btnPullback0.Visible = true;
            //btnPrintCTCRestruc.Visible = true;
            ////btnPrintCTCRestruc0.Visible = true;////below line commented for disabling the submit option for the employee confortablity on 06 Dec 2016

            //lblEmpCTCRestrucStatus.Text = "You have already restructured salary";
            lblEmpCTCRestrucStatus.Text = "Your new CTC : FY "+hdnFinancialYearCurr.Value+" have been saved";
            lblEmpCTCRestrucStatus.ForeColor = System.Drawing.Color.Green;
            if (hdnIsEmpFinalizedCTCRestruc.Value == "true")
            {
                lblEmpCTCRestrucStatus.Text = "You have submitted restructured CTC : FY " +hdnFinancialYearCurr.Value;
                btnApplyCTC.Visible = false;
                btnPullback0.Visible = false;
                btnPullback.Visible = false;
              //  btnPrintFinal.Visible = true;
                btnPrintCTCRestruc0.Visible = false;
            }
            else
            {
              //  btnPrintFinal.Visible = false;
                btnApplyCTC.Visible = false;
                btnPullback0.Visible = true;
                btnPullback.Visible = true;
                ////btnPrintCTCRestruc0.Visible = true;////below line commented for disabling the submit option for the employee confortablity on 06 Dec 2016
            }
        }
        else
        {
            btnApplyCTC.Visible = true;
            // btnPullback.Visible = false;
            btnPullback0.Visible = false;
        //    btnPrintFinal.Visible = false;
            // btnPrintCTCRestruc.Visible = false;
            btnPrintCTCRestruc0.Visible = false;
            lblEmpCTCRestrucStatus.Text = "You can restructure CTC ";
            lblEmpCTCRestrucStatus.ForeColor = System.Drawing.Color.Red;
        }
        if (Request.QueryString["AdminLogin"] != null)
        {
            string AdminLogin = Request.QueryString["AdminLogin"];
            if (AdminLogin == "Yes")
            {
                btnApplyCTC.Visible = false;
                btnPullback0.Visible = true;
                btnApplyCTC.Visible = false;
                btnPullback0.Visible = true;
                btnPullback.Visible = false;
             //   btnPrintFinal.Visible = false;
                btnPrintCTCRestruc0.Visible = false;
            }
        }
    }

    //Considering Form mode make control editable or readonly
    protected void SetControlBehivor()
    {
        if (ViewState["IsEmpRestrucSalary"].ToString() == "Yes")
        {
            txtAA.Visible = false;
            txtCA.Visible = false;
            txtCEA.Visible = false;
            txtCHA.Visible = false;
            txtHRA.Visible = false;
            txtMARestruct.Visible = false;
            txtMobile.Visible = false;
            txtLTA.Visible = false;
            ddlSuperanuation.Visible = false;
            rdoGetMealVoucher.Visible=false;
            lblGetMealVoucher.Visible = true;

            lblAA.Visible = true;
            lblCA.Visible = true;
            lblCEA.Visible = true;
            lblCHA.Visible = true;
            lblHRA.Visible = true;
            lblMA.Visible = true;
            lblMobile.Visible = true;
            lblLTA.Visible = true;
            
            //Added By Mohan On 27 May 2019
            txtNPSNewAmt.Visible = false;
            lblNPSNewAmt.Visible = true;
            //End By Mohan
            
            txtAA.Visible = false;
            txtMobile.Visible = false;
            lblAA.Visible = true;
            lblMobile.Visible = true;

            lblEmpSelectedSuperanuationOption.Visible = true;
            hdnFormMode.Value = "1";
        }
        else
        {
            txtAA.Visible = true;
            txtCA.Visible = true;
            txtCEA.Visible = true;
            txtCHA.Visible = true;
            txtHRA.Visible = true;
            txtMARestruct.Visible = true;
            txtMobile.Visible = true;
            txtLTA.Visible = true;
            ddlSuperanuation.Visible = true;
            rdoGetMealVoucher.Visible = true;
            lblGetMealVoucher.Visible = false;
            lblAA.Visible = false;
            lblCA.Visible = false;
            lblCEA.Visible = false;
            lblCHA.Visible = false ;
            lblHRA.Visible = false;
            lblMA.Visible = false;
            lblMobile.Visible = false;
            lblLTA.Visible = false;

            //Added By Mohan On 27 May 2019
            txtNPSNewAmt.Visible = true;
            lblNPSNewAmt.Visible = false;

            //if (hdnIsNewEmployee.Value == "Yes")
            //{
            //    txtMobile.Visible = true;
            //    lblMobile.Visible = false;
            //    rdoGetMealVoucher.Enabled = true;
            //}
            //else
            //{
            //    txtMobile.Visible = false;
            //    lblMobile.Visible = true;
            //    rdoGetMealVoucher.Enabled = false;
            //}

            //Added By Mohan On 30 May 2019
            if (hdnIsSAApplicable.Value.Trim() == "Yes")
            {
                
                ddlSuperanuation.Enabled = true;
            }
            else
            {
                ddlSuperanuation.Enabled = false;
            }
            //End By Mohan

            //End By Mohan

            if (ViewState["@IsAdditionalAllowanceApp"] != null)
            {
                string IsAdditionalAllowanceApp = ViewState["@IsAdditionalAllowanceApp"].ToString();
                if (IsAdditionalAllowanceApp == "")
                {
                    txtAA.Visible = false;
                    txtMobile.Visible = false;
                    lblAA.Visible = true;
                    lblMobile.Visible = true;
                }
                else if (IsAdditionalAllowanceApp == "X")
                {                       
                    //txtMobile.Visible = true;
                    //lblMobile.Visible = false;
                    if (hdnIsNewEmployee.Value.Trim() == "Yes")
                    {
                        txtAA.Visible = true;
                        txtMobile.Visible = true;
                        lblMobile.Visible = false;
                        rdoGetMealVoucher.Enabled = true;
                    }
                    else
                    {
                        lblAA.Visible = false;
                        txtMobile.Visible = false;
                        lblMobile.Visible = true;
                        rdoGetMealVoucher.Enabled = false;
                        txtAA.Text = lblAAExisting.Text;
                        lblMobile.Text = lblMobileExisting.Text;
                    }
                }
            }

            ////Commented By Mohan
            //if (CheckDateLessThanOrEqualToToday(lblDateofJoining.Text) == false)
            //{
            //    txtMobile.ReadOnly = true;
            //    lblAA.Text = lblAAExisting.Text;
            //    txtAA.Text = lblAAExisting.Text;
            //    //lblMobile.Visible = true;
            //    rdoGetMealVoucher.Enabled = false;

            //}
            //else
            //{
            //    txtMobile.ReadOnly = false;
            //    // lblAAExisting.Text = lblAA.Text;
            //    // lblMobile.Visible = false;
            //    rdoGetMealVoucher.Enabled = true;
            //}
            //End By Moahb
            lblEmpSelectedSuperanuationOption.Visible = false;
            hdnFormMode.Value = "0";
        }
        if (ViewState["RegimeOptionOpted"] != null)
        {
            string str = ViewState["RegimeOptionOpted"].ToString();
            if (str == "NewRegime")
            {
                txtAA.Text= lblAAExisting.Text;
                txtCA.Text = lblConAlloExisting.Text;
                txtCEA.Text = lblCEAExisting.Text;
                txtCHA.Text = lblCHAExisting.Text;
                txtHRA.Text = lblHRAExisting.Text;
                txtMARestruct.Text = lblMAExisting.Text;
                txtLTA.Text = lblLTAExisting.Text; 

                txtAA.Enabled = false;
                txtCA.Enabled = false;
                txtCEA.Enabled = false;
                txtCHA.Enabled = false;
                txtHRA.Enabled = false;
                txtMARestruct.Enabled = false;
                txtLTA.Enabled = false;
                txtMobile.Enabled = false;
                rdoGetMealVoucher.Enabled = false;
   


            }
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
                        var dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Basic").FirstOrDefault();
                        lblBasicExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        lblBasicFinal.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "HRA").FirstOrDefault();
                        lblHRAExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        lblHRAMaxCeiling.Text = dataRow["MaxCeilingForExcemption"].ToString();
                        lblHRAMaxCeiling.Text = Convert.ToString((Convert.ToUInt64(lblHRAMaxCeiling.Text) * Convert.ToUInt64(lblBasicExisting.Text)) / 100);
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CA").FirstOrDefault();
                        lblConAlloExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        lblCAMaxCeiling.Text = dataRow["MaxCeilingForExcemption"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "LTA").FirstOrDefault();
                        lblLTAExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CEA").FirstOrDefault();
                        lblCEAExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        lblCEAMaxCeiling.Text = dataRow["MaxCeilingForExcemption"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CHA").FirstOrDefault();
                        lblCHAExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        lblCHAMaxCeiling.Text = dataRow["MaxCeilingForExcemption"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "MA").FirstOrDefault();
                        lblMAExisting.Text = dataRow["Emp_Component_Value"].ToString();
                
                        // txtMARestruct.Text = lblMAExisting.Text;
                        lblMAMaxCeiling.Text = dataRow["MaxCeilingForExcemption"].ToString();

                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Mobile").FirstOrDefault();
                        if (dataRow != null)
                        {
                            lblMobileExisting.Text = dataRow["Emp_Component_Value"].ToString();
                            txtMobile.Text = lblMobileExisting.Text;
                            //lblMobileExisting.Text = "0";//Commented on 5 Jun 18
                            //txtMobile.Text = dataRow["Emp_Component_Value"].ToString();
                            lblMobileMaxCeiling.Text = dataRow["MaxCeilingForExcemption"].ToString();
                        }
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "AA").FirstOrDefault();
                        lblAAExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "PF").FirstOrDefault();
                        lblPFExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Gratuity").FirstOrDefault();
                        lblGratuityExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SN").FirstOrDefault();
                        lblSNExisting.Text = dataRow["Emp_Component_Value"].ToString();

                        //Added By Mohan On 27 May 2019
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "NPS").FirstOrDefault();
                        lblNPSExistingAmt.Text = dataRow["Emp_Component_Value"].ToString();
                        UInt64 NPSPer = Convert.ToUInt64(dataRow["MaxCeilingForExcemption"].ToString() != "" ? Convert.ToUInt64(dataRow["MaxCeilingForExcemption"].ToString()) : 0);
                        hiddNPSMaxLimit.Value = Convert.ToString((Convert.ToUInt64(lblBasicFinal.Text) * Convert.ToUInt64(NPSPer)) / 100);
                        hiddNPSMinLimit.Value = Convert.ToString(dataRow["CeilingLimitForEmployee"].ToString());

                        if (lblNPSExistingAmt.Text != "")
                        {
                            if(Convert.ToUInt64(lblNPSExistingAmt.Text) != 0)
                            {
                                txtNPSNewAmt.Text = lblNPSExistingAmt.Text;
                            }
                        }
                        //End By Mohan

                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SA").FirstOrDefault();
                        lblSiteAllowaExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "ESIC").FirstOrDefault();
                        lblESICExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Bonus").FirstOrDefault();
                        lblBonusExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CanteenSubsidy").FirstOrDefault();
                        //lblCanteenExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        //lblAAWithCanteenSubsExisting.Text = "0";
                        //lblAAWithCanteenSubsRestruc.Text = lblCanteenExisting.Text;

                        //lblCanteenExisting.Text = "0";
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "MealCard").FirstOrDefault();
                        lblMealCardExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        lblMealCardRestruc.Text = lblMealCardExisting.Text;
                        //Added By Mohan On 31 May 2019
                        rdoGetMealVoucher.SelectedValue = lblMealCardRestruc.Text;
                        //End By Mohan
                        //Added ByChetna on 16May22
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Mediclaim").FirstOrDefault();
                        lblMediPreExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        lblMediPreNew.Text = lblMediPreExisting.Text;

                        //if (lblMediPreExisting.Text == "" || lblMediPreExisting.Text == "0")
                        //{
                        //    trMedPre.Visible = false;
                        //}
                        //else
                        //{
                        //    trMedPre.Visible = true;

                        //}
                        //End ByChetna on 16May22

                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "IC").FirstOrDefault();
                        lblICExisting.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CC").FirstOrDefault();
                        lblCCExisting.Text = dataRow["Emp_Component_Value"].ToString();

                        lblPFRestruct.Text = lblPFExisting.Text;
                        lblGratuityRestruct.Text = lblGratuityExisting.Text;
                        lblSNRestruct.Text = lblSNExisting.Text;

                        lblSiteAllowaRestruct.Text = lblSiteAllowaExisting.Text;

                        lblESICRestruct.Text = lblESICExisting.Text == "0" ? "0" : lblESICExisting.Text;
                        lblBonusRestruct.Text = lblBonusExisting.Text == "0" ? "0" : lblBonusExisting.Text;
                        lblComputedFixedRestructur.Text = lblComputedFixedExisting.Text;
                        lblICRestruc.Text = lblICExisting.Text;
                        lblCCRestruc.Text = lblCCExisting.Text;

                        //lblMealCardRestruc.Text = "0";  

                        lblICRestruc.Text = lblICExisting.Text;
                        lblCCRestruc.Text = lblCCExisting.Text;

                        //NumberFormatInfo nfo = new NumberFormatInfo();
                        //nfo.CurrencyGroupSeparator = ",";
                        //// you are interested in this part of controlling the group sizes
                        //nfo.CurrencyGroupSizes = new int[] { 3, 2 };
                        //nfo.CurrencySymbol = "";

                        //lblBasicExisting.Text = (Convert.ToInt64(lblBasicExisting.Text).ToString("c0", nfo)); // prints 1,50,00,000

                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        string str = ds.Tables[3].Rows[0]["IsEmpRestrucSalary"].ToString();

                        //Added By Mohan On 29 Apr 2019
                        hdnIsNewEmployee.Value = ds.Tables[3].Rows[0]["IsNewEmployee"].ToString();
                        hdnIsSAApplicable.Value = ds.Tables[3].Rows[0]["IsSAApplicable"].ToString();
                        //End By Mohan

                        if (str == "Yes")
                        {
                            ViewState["IsEmpRestrucSalary"] = "Yes";
                            hdnIsEmpRestructuredCTC.Value = "Yes";
                        }
                        else
                        {
                            ViewState["IsEmpRestrucSalary"] = "No";
                            hdnIsEmpRestructuredCTC.Value = "No";
                        }
                    }

                    if (ds. Tables[2].Rows.Count > 0)
                    {
                        String CanDoSuperanuationChange = ds.Tables[2].Rows[0]["@CanDoSuperanuationChange"].ToString();
                        String CanBonusChange = ds.Tables[2].Rows[0]["@CanBonusChange"].ToString();
                        String CanDoMobileChange = ds.Tables[2].Rows[0]["@CanDoMobileChange"].ToString();
                        String CanRetrucOrNot = ds.Tables[2].Rows[0]["@CanRetrucOrNot"].ToString();
                        String CanAdditionalAllowanceApp = ds.Tables[2].Rows[0]["@IsAdditionalAllowanceApp"].ToString();
                        String SuperannuantionLimit = ds.Tables[2].Rows[0]["@SuperannuantionLimit"].ToString();
                        String SuperannuantionUpperLimit = ds.Tables[2].Rows[0]["@SuperannuationUpperLimit"].ToString();
                        
                        hdnSuperAnnuationLimit.Value = SuperannuantionLimit;
                        hdnSuperAnnuationUpperLimit.Value = SuperannuantionUpperLimit;

                        ViewState["@IsAdditionalAllowanceApp"] = ds.Tables[2].Rows[0]["@IsAdditionalAllowanceApp"].ToString();

                        hdnAddtionalAllowanceApp.Value = ds.Tables[2].Rows[0]["@IsAdditionalAllowanceApp"].ToString();
                        if (hdnAddtionalAllowanceApp.Value == "")
                        {
                            txtAA.Text = lblAAExisting.Text;
                            lblAA.Text = lblAAExisting.Text;
                        }

                        if (CanRetrucOrNot != "X")
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('At Your Grade Restructuring is not applicable!');", true);
                            return;
                        }

                        if (CanDoSuperanuationChange == "X")
                        {
                            string SuperanuationOptionSelected = ds.Tables[5].Rows[0]["SuperanuationOptionSelected"].ToString();
                            hdnSuperanution.Value = "IsApplicable";
                            ddlSuperanuation.SelectedValue = SuperanuationOptionSelected;
                            hdnIsEmpFinalizedCTCRestruc.Value = ds.Tables[5].Rows[0]["IsEmpFinalizedCTCRestruc"].ToString();
                            lblGetMealVoucher.Text=ds.Tables[5].Rows[0]["IsMealCardOpted"].ToString();
                            
                            hdnEmpSelectedSuperanuationOption.Value = SuperanuationOptionSelected;
                            lblEmpSelectedSuperanuationOption.Text = ddlSuperanuation.SelectedItem.Text;
                            
                            hdnSN.Value = lblSNExisting.Text;
                           
                            if (hdnIsEmpFinalizedCTCRestruc.Value == "false")
                            {
                                hdnFormMode.Value = "0";
                                if (ddlSuperanuation.Visible == true)
                                {
                                   //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OnSelectionOfNoChangeInSuperanuation123(this);", true);
                                }
                            }
                            else
                            {
                                hdnFormMode.Value = "1";
                                if (ddlSuperanuation.Visible == true)
                                {
                                 //   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OnSelectionOfNoChangeInSuperanuation123(this);", true);
                                }
                            }
                            Int64 BasicValue = Convert.ToInt64(lblBasicExisting.Text);
                        }
                        else
                        {
                            hdnSuperanution.Value = "IsNotApplicable";
                            lblGetMealVoucher.Text = ds.Tables[5].Rows[0]["IsMealCardOpted"].ToString();                            
                            hdnIsEmpFinalizedCTCRestruc.Value = ds.Tables[5].Rows[0]["IsEmpFinalizedCTCRestruc"].ToString();
                            
                            hdnEmpSelectedSuperanuationOption.Value = "-1";
                           // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "HideSuperAnnuation();", true);
                           // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OnSelectionOfNoChangeInSuperanuation123(this);", true);
                        }

                        if (CanDoMobileChange == "X")
                        {
                            txtMobile.Enabled = true;
                        }
                        else
                        {
                        }
                        if (CanBonusChange == "X")
                        {
                            lblESICRestruct.Text = lblESICExisting.Text;
                            //canculation for merging bonus to IC 
                        }
                        else
                        {
                            //lblICRestruc.Text = (Convert.ToInt64(lblICRestruc.Text) + (Convert.ToInt64(lblBonusExisting.Text) * 12)).ToString();
                            //lblBonusRestruct.Text = "0";

                            //lblICRestruc.Text = (Convert.ToInt64(lblICRestruc.Text) + (Convert.ToInt64(lblBonusExisting.Text) * 12)).ToString();
                            //lblBonusRestruct.Text = "0";
                        }
                    }

                    //Start Of added by chetna for regime option changes in pages old regime no change and new regime change as readonly fields and NPS editable
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        string str = ds.Tables[3].Rows[0]["RegimeOptionOpted"].ToString();

                        ViewState["RegimeOptionOpted"] = str;
                        ltrRegime.Text = ViewState["RegimeOptionOpted"].ToString();

                    }
                    //End Of added by chetna for regime option changes in pages old regime no change and new regime change as readonly fields and NPS editable

                    lblGroupATotalExisting.Text =
                    Convert.ToString(
                    Convert.ToInt64(lblBasicExisting.Text == "" ? "0" : lblBasicExisting.Text) +
                    Convert.ToInt64(lblHRAExisting.Text == "" ? "0" : lblHRAExisting.Text) +
                    Convert.ToInt64(lblConAlloExisting.Text == "" ? "0" : lblConAlloExisting.Text) +
                    Convert.ToInt64(lblLTAExisting.Text == "" ? "0" : lblLTAExisting.Text) +
                    Convert.ToInt64(lblCEAExisting.Text == "" ? "0" : lblCEAExisting.Text) +
                    Convert.ToInt64(lblMAExisting.Text == "" ? "0" : lblMAExisting.Text) +
                    Convert.ToInt64(lblCHAExisting.Text == "" ? "0" : lblCHAExisting.Text) +
                    Convert.ToInt64(lblMobileExisting.Text == "" ? "0" : lblMobileExisting.Text) +
                    Convert.ToInt64(lblAAExisting.Text == "" ? "0" : lblAAExisting.Text));
                   
                    //TextBox1.Text =
                    //Convert.ToString(
                    //Convert.ToInt64(lblHRAExisting.Text == "" ? "0" : lblHRAExisting.Text) +
                    //Convert.ToInt64(lblConAlloExisting.Text == "" ? "0" : lblConAlloExisting.Text) +
                    //Convert.ToInt64(lblLTAExisting.Text == "" ? "0" : lblLTAExisting.Text) +
                    //Convert.ToInt64(lblCEAExisting.Text == "" ? "0" : lblCEAExisting.Text) +
                    //Convert.ToInt64(lblMAExisting.Text == "" ? "0" : lblMAExisting.Text));

                    //lblGroupBTotalLimit.Text = Convert.ToString(
                    //Convert.ToInt64(lblMobileExisting.Text == "" ? "0" : lblMobileExisting.Text) +
                    //Convert.ToInt64(lblAAExisting.Text == "" ? "0" : lblAAExisting.Text));

                    lblGroupBTotalExisting.Text =
                    Convert.ToString(Convert.ToInt64(lblPFExisting.Text == "" ? "0" : lblPFExisting.Text) +
                    Convert.ToInt64(lblGratuityExisting.Text == "" ? "0" : lblGratuityExisting.Text) +
                    Convert.ToInt64(lblSNExisting.Text == "" ? "0" : lblSNExisting.Text) +
                    Convert.ToInt64(lblNPSExistingAmt.Text == "" ? "0" : lblNPSExistingAmt.Text));

                    lblGroupCTotalExisting.Text =
                    Convert.ToString(Convert.ToInt64(lblSiteAllowaExisting.Text == "" ? "0" : lblSiteAllowaExisting.Text) +
                    Convert.ToInt64(lblESICExisting.Text == "" ? "0" : lblESICExisting.Text) + 
                    Convert.ToInt64(lblBonusExisting.Text == "" ? "0" : lblBonusExisting.Text));

                  

                    //Below calculation altered by Chetna on 16May22 for adding Medical Premium amount

                    lblTotalABCDExisting.Text =
                    Convert.ToString(Convert.ToInt64(lblGroupATotalExisting.Text == "" ? "0" : lblGroupATotalExisting.Text) +
                    Convert.ToInt64(lblGroupBTotalExisting.Text == "" ? "0" : lblGroupBTotalExisting.Text) +                    
                    Convert.ToInt64(lblGroupCTotalExisting.Text == "" ? "0" : lblGroupCTotalExisting.Text)+
                    Convert.ToInt64(lblMediPreExisting.Text == "" ? "0" : lblMediPreExisting.Text) +
                    Convert.ToInt64(lblMealCardRestruc.Text == "" ? "0" : lblMealCardRestruc.Text));

                    lblComputedFixedExisting.Text =
                    Convert.ToString((Convert.ToInt64(lblGroupATotalExisting.Text == "" ? "0" : lblGroupATotalExisting.Text) +
                    Convert.ToInt64(lblGroupBTotalExisting.Text == "" ? "0" : lblGroupBTotalExisting.Text) +
                    Convert.ToInt64(lblGroupCTotalExisting.Text == "" ? "0" : lblGroupCTotalExisting.Text) +
                    Convert.ToInt64(lblMediPreExisting.Text == "" ? "0" : lblMediPreExisting.Text) +
                    Convert.ToInt64(lblMealCardRestruc.Text == "" ? "0" : lblMealCardRestruc.Text)) * 12);

                    //Above  calculation altered by Chetna on 16May22 for adding Medical Premium amount



                    lblTotalEExisting.Text = Convert.ToString(Convert.ToInt64(lblICExisting.Text == "" ? "0" : lblICExisting.Text) +
                    Convert.ToInt64(lblCCExisting.Text == "" ? "0" : lblCCExisting.Text));

                    lblComputedCTCExisting.Text = Convert.ToString((Convert.ToInt64(lblComputedFixedExisting.Text == "" ? "0" : lblComputedFixedExisting.Text) +
                    Convert.ToInt64(lblTotalEExisting.Text == "" ? "0" : lblTotalEExisting.Text)));

                    //txtMARestruct.Enabled = true;// make medical allowance disabled //again making textbox enable on 6Jun18

                    if (ds.Tables[4].Rows.Count > 0) //Get the employee Restructred CTC Data
                    {

                        var dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Basic").FirstOrDefault();
                        lblBasicFinal.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "HRA").FirstOrDefault();
                        txtHRA.Text = dataRow["Emp_Component_Value"].ToString();
                        lblHRA.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CA").FirstOrDefault();
                        txtCA.Text = dataRow["Emp_Component_Value"].ToString();
                        lblCA.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "LTA").FirstOrDefault();
                        txtLTA.Text = dataRow["Emp_Component_Value"].ToString();
                        lblLTA.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CEA").FirstOrDefault();
                        txtCEA.Text = dataRow["Emp_Component_Value"].ToString();
                        lblCEA.Text = dataRow["Emp_Component_Value"].ToString();
                        
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CHA").FirstOrDefault();
                        txtCHA.Text = dataRow["Emp_Component_Value"].ToString();
                        lblCHA.Text = dataRow["Emp_Component_Value"].ToString();

                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "MA").FirstOrDefault();
                        txtMARestruct.Text = dataRow["Emp_Component_Value"].ToString();
                        lblMA.Text = dataRow["Emp_Component_Value"].ToString();

                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Mobile").FirstOrDefault();
                        if (dataRow != null)
                        {
                            txtMobile.Text = dataRow["Emp_Component_Value"].ToString();
                            lblMobile.Text = dataRow["Emp_Component_Value"].ToString();
                            //   lblMobileCeilingForEmp.Text = dataRow["CeilingLimitForEmployee"].ToString();
                        }
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "AA").FirstOrDefault();
                        txtAA.Text = dataRow["Emp_Component_Value"].ToString();
                        lblAA.Text = dataRow["Emp_Component_Value"].ToString();

                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "PF").FirstOrDefault();
                        lblPFRestruct.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Gratuity").FirstOrDefault();
                        lblGratuityRestruct.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SN").FirstOrDefault();
                        lblSNRestruct.Text = dataRow["Emp_Component_Value"].ToString();

                        //Commented By Mohan On 27 May 2019
                        //dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SN_To_NPS").FirstOrDefault();
                        //lblNPS.Text = dataRow["Emp_Component_Value"].ToString();
                        //End By Mohan

                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SN_To_AA").FirstOrDefault();
                        lblAAWithSuperannuation.Text = dataRow["Emp_Component_Value"].ToString();

                        //Added By Mohan On 27 May 2019
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "NPS").FirstOrDefault();
                        txtNPSNewAmt.Text = dataRow["Emp_Component_Value"].ToString();
                        lblNPSNewAmt.Text = dataRow["Emp_Component_Value"].ToString();
                        lblNPSDeducFrmAA.Text = "-" + ds.Tables[5].Rows[0]["NPSAmount"].ToString();

                        if (lblNPSExistingAmt.Text != "")
                        {
                            if (Convert.ToUInt64(lblNPSExistingAmt.Text) != 0)
                            {
                                if(Convert.ToUInt64(lblNPSExistingAmt.Text) > Convert.ToUInt64(lblNPSNewAmt.Text))
                                {
                                    lblNPSDeducFrmAA.Text = Convert.ToString(Convert.ToUInt64(lblNPSExistingAmt.Text) - Convert.ToUInt64(lblNPSNewAmt.Text));
                                }
                                else
                                {
                                    lblNPSDeducFrmAA.Text = "-" + Convert.ToString(Convert.ToUInt64(lblNPSNewAmt.Text) - Convert.ToUInt64(lblNPSExistingAmt.Text));
                                }
                            }
                        }

                        //End By Mohan

                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SA").FirstOrDefault();
                        lblSiteAllowaRestruct.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "ESIC").FirstOrDefault();
                        lblESICRestruct.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Bonus").FirstOrDefault();
                        lblBonusRestruct.Text = dataRow["Emp_Component_Value"].ToString();
                        //dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CanteenSubsidy").FirstOrDefault();
                        //lblMealCardRestruc.Text = dataRow["Emp_Component_Value"].ToString();

                        if (lblGetMealVoucher.Text.Trim() == "Yes" && hdnIsNewEmployee.Value.Trim() == "Yes")//'&& hdnIsNewEmployee.Value == "Yes"' Added By Mohan On 31 May 2019
                        {
                            lblMealCardRestruc.Text = ds.Tables[5].Rows[0]["MealCardAmount"].ToString();
                            lblMeamCardDeducFrmAA.Text = "-" + ds.Tables[5].Rows[0]["MealCardAmount"].ToString();
                            //lblAAWithCanteenSubsRestruc.Text = lblCanteenExisting.Text;
                            rdoGetMealVoucher.SelectedValue = ds.Tables[5].Rows[0]["MealCardAmount"].ToString();
                        }
                        else
                        {
                            lblMealCardRestruc.Text = "0";
                            lblMeamCardDeducFrmAA.Text = "0";
                            //lblAAWithCanteenSubsRestruc.Text = lblCanteenExisting.Text;
                            rdoGetMealVoucher.SelectedValue = "0";
                        }

                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "MealCard").FirstOrDefault();
                        lblMealCardRestruc.Text = dataRow["Emp_Component_Value"].ToString();
                        //lblAAWithCanteenSubsRestruc.Text = lblCanteenRestruct.Text;
                        //lblMeamCardDeducFrmAA.Text ="-"+ lblCanteenRestruct.Text;

                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "IC").FirstOrDefault();
                        lblICRestruc.Text = dataRow["Emp_Component_Value"].ToString();
                        dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CC").FirstOrDefault();
                        lblCCRestruc.Text = dataRow["Emp_Component_Value"].ToString();

                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            //lblNPS.Text = dataRow["Emp_Component_Value"].ToString();
                            string str = ds.Tables[5].Rows[0]["SuperanuationOptionSelected"].ToString();
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OnSelectionOfNoChangeInSuperanuation123(" + str + ");", true);
                        }

                        lblGroupATotalRestruc.Text =
                        Convert.ToString(
                        Convert.ToInt64(lblBasicFinal.Text == "" ? "0" : lblBasicFinal.Text) +
                        Convert.ToInt64(txtHRA.Text == "" ? "0" : txtHRA.Text) +
                        Convert.ToInt64(txtCA.Text == "" ? "0" : txtCA.Text) +
                        Convert.ToInt64(txtLTA.Text == "" ? "0" : txtLTA.Text) +
                        Convert.ToInt64(txtCEA.Text == "" ? "0" : txtCEA.Text) +
                        Convert.ToInt64(txtMARestruct.Text == "" ? "0" : txtMARestruct.Text) +
                        Convert.ToInt64(txtMobile.Text == "" ? "0" : txtMobile.Text) +
                        Convert.ToInt64(txtAA.Text == "" ? "0" : txtAA.Text) );

                        lblGroupBTotalRestruc.Text =
                        Convert.ToString(Convert.ToInt64(lblPFRestruct.Text == "" ? "0" : lblPFRestruct.Text) +
                        Convert.ToInt64(lblGratuityRestruct.Text == "" ? "0" : lblGratuityRestruct.Text) +
                        Convert.ToInt64(lblSNRestruct.Text == "" ? "0" : lblSNRestruct.Text) +
                        Convert.ToInt64(txtNPSNewAmt.Text == "" ? "0" : txtNPSNewAmt.Text));

                        lblGroupCTotalRestruc.Text =
                        Convert.ToString(Convert.ToInt64(lblSiteAllowaRestruct.Text == "" ? "0" : lblSiteAllowaRestruct.Text) +
                        Convert.ToInt64(lblESICRestruct.Text == "" ? "0" : lblESICRestruct.Text)+
                         Convert.ToInt64(lblBonusRestruct.Text == "" ? "0" : lblBonusRestruct.Text));



                        //Below calculation altered by Chetna on 16May22 for adding Medical Premium amount
                        lblTotalABCDRestruc.Text =
                        Convert.ToString(Convert.ToInt64(lblGroupATotalRestruc.Text == "" ? "0" : lblGroupATotalRestruc.Text) +
                        Convert.ToInt64(lblGroupBTotalRestruc.Text == "" ? "0" : lblGroupBTotalRestruc.Text) +
                        Convert.ToInt64(lblGroupCTotalRestruc.Text == "" ? "0" : lblGroupCTotalRestruc.Text) +
                        Convert.ToInt64(lblMediPreNew.Text == "" ? "0" : lblMediPreNew.Text) +
                        Convert.ToInt64(lblMealCardRestruc.Text == "" ? "0" : lblMealCardRestruc.Text));

                        lblComputedFixedRestructur.Text = 
                        Convert.ToString((Convert.ToInt64(lblGroupATotalRestruc.Text == "" ? "0" : lblGroupATotalRestruc.Text) +
                        Convert.ToInt64(lblGroupBTotalRestruc.Text == "" ? "0" : lblGroupBTotalRestruc.Text) +
                        Convert.ToInt64(lblGroupCTotalRestruc.Text == "" ? "0" : lblGroupCTotalRestruc.Text) +
                        Convert.ToInt64(lblMediPreNew.Text == "" ? "0" : lblMediPreNew.Text) +
                        Convert.ToInt64(lblMealCardRestruc.Text == "" ? "0" : lblMealCardRestruc.Text)) * 12);
                        //Above calculation altered by Chetna on 16May22 for adding Medical Premium amount

                        lblTotalERestructur.Text = Convert.ToString(Convert.ToInt64(lblICRestruc.Text == "" ? "0" : lblICRestruc.Text) +
                        Convert.ToInt64(lblCCRestruc.Text == "" ? "0" : lblCCRestruc.Text));

                        lblComputedCTCRestruct.Text = 
                            Convert.ToString((Convert.ToInt64(lblComputedFixedRestructur.Text == "" ? "0" : lblComputedFixedRestructur.Text) +
                            Convert.ToInt64(lblTotalERestructur.Text == "" ? "0" : lblTotalERestructur.Text)));

                        lblGroupATotalRestruc.Text = Convert.ToString(
                        Convert.ToInt64(lblBasicFinal.Text == "" ? "0" : lblBasicFinal.Text) +
                        Convert.ToInt64(txtHRA.Text == "" ? "0" : txtHRA.Text) +
                        Convert.ToInt64(txtCA.Text == "" ? "0" : txtCA.Text) +
                        Convert.ToInt64(txtLTA.Text == "" ? "0" : txtLTA.Text) +
                        Convert.ToInt64(txtCEA.Text == "" ? "0" : txtCEA.Text) +
                        Convert.ToInt64(txtCHA.Text == "" ? "0" : txtCEA.Text) +
                        Convert.ToInt64(txtMARestruct.Text == "" ? "0" : txtMARestruct.Text) +
                        Convert.ToInt64(txtMobile.Text == "" ? "0" : txtMobile.Text) +
                        Convert.ToInt64(txtAA.Text == "" ? "0" : txtAA.Text));
                        
                        if (hdnIsEmpFinalizedCTCRestruc.Value == "false")
                        {
                            lblAACalculation.Text = Convert.ToString(
                            Convert.ToInt32(lblAAExisting.Text == "" ? "0" : lblAAExisting.Text) +
                            Convert.ToInt64(lblMobileExisting.Text == "" ? "0" : lblMobileExisting.Text) +
                            //Convert.ToInt64(lblAAWithCanteenSubsRestruc.Text == "" ? "0" : lblAAWithCanteenSubsRestruc.Text) +
                            Convert.ToInt64(lblAAWithSuperannuation.Text == "" ? "0" : lblAAWithSuperannuation.Text) -
                            Convert.ToInt64(rdoGetMealVoucher.SelectedValue == "" ? "0" : rdoGetMealVoucher.SelectedValue) -
                            Convert.ToInt64(txtMobile.Text == "" ? "0" : txtMobile.Text) -
                            Convert.ToInt64(txtNPSNewAmt.Text == "" ? "0" : txtNPSNewAmt.Text)); //Added By Mohan On 30 May 2019
                        }
                        else
                        {
                            lblAACalculation.Text = Convert.ToString(
                            Convert.ToInt32(lblAAExisting.Text == "" ? "0" : lblAAExisting.Text) +
                            Convert.ToInt64(lblMobileExisting.Text == "" ? "0" : lblMobileExisting.Text) +
                            // Convert.ToInt64(lblAAWithCanteenSubsRestruc.Text == "" ? "0" : lblAAWithCanteenSubsRestruc.Text) +
                            Convert.ToInt64(lblAAWithSuperannuation.Text == "" ? "0" : lblAAWithSuperannuation.Text) -
                            Convert.ToInt64(lblMealCardRestruc.Text == "" ? "0" : lblMealCardRestruc.Text) -
                            Convert.ToInt64(lblMobile.Text == "" ? "0" : lblMobile.Text) -
                            Convert.ToInt64(txtNPSNewAmt.Text == "" ? "0" : txtNPSNewAmt.Text));//Added By Mohan On 30 May 2019
                        }
                    }
                }
            }

            //if (CheckDateLessThanOrEqualToToday(lblDateofJoining.Text) == false)
            //{
            //    txtMobile.Visible = false;                
            //    lblMobile.Visible = true;
            //    rdoGetMealVoucher.Enabled = false;

            //}
            //else
            //{
            //    txtMobile.Enabled = true;
            //    lblMobile.Visible = false;
            //    rdoGetMealVoucher.Enabled = true;

            //}

            PrepareActionButton();
            SetControlBehivor();   
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "FillEmployeeBaseCTC");
            throw;
        }
    }

    #region "Not InUse"
    protected void txtCA_TextChanged(object sender, EventArgs e)
    {
        DoThis(txtCA, lblCAMaxCeiling);
    }
    protected void txtMA_TextChanged(object sender, EventArgs e)
    {
        DoThis(txtCA, lblCAMaxCeiling);
    }
    protected void txtLTA_TextChanged(object sender, EventArgs e)
    {
        DoThis(txtCA, lblCAMaxCeiling);
    }
    protected void DoThis(TextBox txt1, Label lbl)
    {
        Int64 GroupATotal =
        Convert.ToInt64(lblHRAExisting.Text == "" ? "0" : lblHRAExisting.Text) +
        Convert.ToInt64(lblConAlloExisting.Text == "" ? "0" : lblConAlloExisting.Text) +
        Convert.ToInt64(lblLTAExisting.Text == "" ? "0" : lblLTAExisting.Text) +
        Convert.ToInt64(lblCEAExisting.Text == "" ? "0" : lblCEAExisting.Text) +
        Convert.ToInt64(lblMAExisting.Text == "" ? "0" : lblMAExisting.Text);

        //TextBox1.Text =(Convert.ToInt64(lblGroupALevEncMax.Text)- GroupATotal).ToString();

        if (Convert.ToInt64(txt1.Text) > Convert.ToInt64(lbl.Text))
        {
            // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "alert('value cannot be greater');", true);
        }
    }
    #endregion   
    
    protected void ddlSuperanuation_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    

    protected bool CheckDateLessThanOrEqualToToday(string dt)
    {
        try
        {
            DateTime dt1 = Convert.ToDateTime(dt);
            String ConfigDate = System.Configuration.ConfigurationManager.AppSettings["DateChkForMobileDMealCrd"].ToString();

            CommonBLL objCommonBLL = new CommonBLL();

            DateTime d;
            if (!DateTime.TryParseExact(dt1.ToString("dd-MMM-yyyy"), "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                return false;
            }
            if (Convert.ToDateTime(dt) < Convert.ToDateTime(ConfigDate))
            {
                return false;
            }
            //if (Convert.ToDateTime(dt) > DateTime.Now)
            //{
            //    return false;
            //}



            return true;
        }
        catch (Exception)
        {
            return false;
            //throw;
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

    protected void btnApplyCTC_Click(object sender, EventArgs e)
     {
         try
         {
             DataSet mDSet = null;
             String mStoredProcName = String.Empty;
             SqlCommand mDbCommand = null;

             Int64 empBaseCTCTotal;
             Int64 empRestructCTCTotal;
             empBaseCTCTotal = Convert.ToInt64(lblComputedCTCExisting.Text);
             empRestructCTCTotal = Convert.ToInt64(hdnComputedCTCRestruct.Value);
             //if (empRestructCTCTotal != empBaseCTCTotal || TextBox1.Text !="0" || lblGroupBTotalLimit.Text !="0")
             if (empRestructCTCTotal != empBaseCTCTotal || TextBox1.Text != "0" || lblGroupBTotalLimit.Text != "0"  )
             {
                 //hdnIsEmpFinalizedCTCRestruc.Value = "false";
                 //mDSet = GetEmployeeBaseCTC();
                 //FillEmployeeBaseCTC(mDSet);
                 //PrepareActionButton();
                 //SetControlBehivor();
                 ClientScriptManager cs = Page.ClientScript;
                 string strScript = "<script type='text/javascript'>alert('Please review the restructured CTC.Base CTC Amount should be equal to restructured CTC. !');</script>";
                 lblSNRestruct.Text = lblSNExisting.Text;
                 cs.RegisterStartupScript(GetType(), "Message", strScript);
                 //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
                 hdnIsEmpFinalizedCTCRestruc.Value = "false";
                 mDSet = GetEmployeeBaseCTC();
                 FillEmployeeBaseCTC(mDSet);
                 PrepareActionButton();
                 SetControlBehivor();

                 return;
             }
             Int64 intBasic40 =Convert.ToInt64( lblBasicExisting.Text)*40/100;
             Int64 intBasic50 =Convert.ToInt64( lblBasicExisting.Text)*50/100;

             if (txtHRA.Text == "" || txtHRA.Text == "0" || (Convert.ToInt64(txtHRA.Text) < intBasic40) || (Convert.ToInt64(txtHRA.Text) > intBasic50))
             {
                 ClientScriptManager cs = Page.ClientScript;
                 string strScript = "<script type='text/javascript'>alert('HRA Should be in the range of 40 to 50 percent of Basic .Please recheck and submit !');</script>";
                 lblSNRestruct.Text = lblSNExisting.Text;
                 cs.RegisterStartupScript(GetType(), "Message", strScript);
                 //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
                 hdnIsEmpFinalizedCTCRestruc.Value = "false";
                 mDSet = GetEmployeeBaseCTC();
                 FillEmployeeBaseCTC(mDSet);
                 PrepareActionButton();
                 SetControlBehivor();

                 return;
             }
             //Commented as should be no limit
             //if (txtCA.Text == "" || txtCA.Text == "0" || (Convert.ToInt64(txtCA.Text) < 1600) )
             //{
             //    ClientScriptManager cs = Page.ClientScript;
             //    string strScript = "<script type='text/javascript'>alert('CA should be at least 1600 !');</script>";
             //    lblSNRestruct.Text = lblSNExisting.Text;
             //    cs.RegisterStartupScript(GetType(), "Message", strScript);
             //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
             //    hdnIsEmpFinalizedCTCRestruc.Value = "false";
             //    mDSet = GetEmployeeBaseCTC();
             //    FillEmployeeBaseCTC(mDSet);
             //    PrepareActionButton();
             //    SetControlBehivor();
             //    return;
             //}

             DataTable dtComplete = new DataTable();
             DataColumn EMPID1 = new DataColumn("EMPID1", System.Type.GetType("System.String"));
             DataColumn Component_ID = new DataColumn("Component_ID", System.Type.GetType("System.String"));
             DataColumn Emp_Component_Value = new DataColumn("Emp_Component_Value", System.Type.GetType("System.Int64"));
             DataColumn FinancialYear = new DataColumn("FinancialYear", System.Type.GetType("System.String"));
             DataColumn EmpGrade = new DataColumn("EmpGrade", System.Type.GetType("System.String"));
             DataColumn SuperAnuationOption = new DataColumn("SuperAnuationOption", System.Type.GetType("System.String"));

             dtComplete.Columns.Add(EMPID1);
             dtComplete.Columns.Add(Component_ID);
             dtComplete.Columns.Add(Emp_Component_Value);
             dtComplete.Columns.Add(FinancialYear);
             dtComplete.Columns.Add(EmpGrade);
             dtComplete.Columns.Add(SuperAnuationOption);

             DataRow dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "BASIC";
             dr["Emp_Component_Value"] = lblBasicFinal.Text == "" ? "0" : lblBasicFinal.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "HRA";
             dr["Emp_Component_Value"] = txtHRA.Text == "" ? "0" : txtHRA.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "CA";
             dr["Emp_Component_Value"] = txtCA.Text == "" ? "0" : txtCA.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "LTA";
             dr["Emp_Component_Value"] = txtLTA.Text == "" ? "0" : txtLTA.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "CEA";
             dr["Emp_Component_Value"] = txtCEA.Text == "" ? "0" : txtCEA.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             //Added below above line for child hostel allowance
             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "CHA";
             dr["Emp_Component_Value"] = txtCHA.Text == "" ? "0" : txtCHA.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);
             //Added below above line for child hostel allowance

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "MA";
             dr["Emp_Component_Value"] = txtMARestruct.Text == "" ? "0" : txtMARestruct.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "Mobile";
             dr["Emp_Component_Value"] = txtMobile.Text == "" ? "0" : txtMobile.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             txtAA.Text = (Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();
             lblAA.Text = (Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();

            //Added by Mohan on 05 Jul 2019
            calculateAA();

            //Commented by Mohan on 05 Jul 2019
            //lblAACalculation.Text = (Convert.ToInt64(txtAA.Text) + Convert.ToInt64(lblMobileExisting.Text) + 
            //    Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();
            //End By Mohan

            //lblAACalculation.Text = (Convert.ToInt64(txtAA.Text) + Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAWithCanteenSubsRestruc.Text) + Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();

            dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "AA";
             dr["Emp_Component_Value"] = txtAA.Text == "" ? "0" : txtAA.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "Mediclaim";
             dr["Emp_Component_Value"] = lblMediPreNew.Text == "" ? "0" : lblMediPreNew.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

            dr = null;
            dr = dtComplete.NewRow();
            dr["EMPID1"] = lblEmpCode.Text;
            dr["Component_ID"] = "MealCard";
            dr["Emp_Component_Value"] = lblMealCardRestruc.Text == "" ? "0" : lblMealCardRestruc.Text;
            dr["FinancialYear"] = hdnFinancialYearCurr.Value;
            dr["EmpGrade"] = lblGrade.Text;
            dtComplete.Rows.Add(dr);


            dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "RevisedAA";
             dr["Emp_Component_Value"] = lblAACalculation.Text == "" ? "0" : lblAACalculation.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             /*
                 SNMAA
                 SNTRFAA
                 SNTRFNPS
              */

             if (ddlSuperanuation.SelectedValue == "3" || ddlSuperanuation.SelectedValue == "5")
             {
                 dr = null;
                 dr = dtComplete.NewRow();
                 dr["EMPID1"] = lblEmpCode.Text;
                 dr["Component_ID"] = "SN_To_NPS";
                 dr["Emp_Component_Value"] = lblNPS.Text == "" ? "0" : lblNPS.Text;
                 dr["FinancialYear"] = hdnFinancialYearCurr.Value;
                 dr["EmpGrade"] = lblGrade.Text;
                 dtComplete.Rows.Add(dr);

                 dr = null;
                 dr = dtComplete.NewRow();
                 dr["EMPID1"] = lblEmpCode.Text;
                 dr["Component_ID"] = "SN_To_AA";
                 dr["Emp_Component_Value"] = lblAAWithSuperannuation.Text == "" ? "0" : lblAAWithSuperannuation.Text;
                 dr["FinancialYear"] = hdnFinancialYearCurr.Value;
                 dr["EmpGrade"] = lblGrade.Text;
                 dtComplete.Rows.Add(dr);

                 dr = null;
                 dr = dtComplete.NewRow();
                 dr["EMPID1"] = lblEmpCode.Text;
                 dr["Component_ID"] = "SN";
                 dr["Emp_Component_Value"] = hdnSN.Value== "" ? "0" : hdnSN.Value;
                 dr["FinancialYear"] = hdnFinancialYearCurr.Value;
                 dr["EmpGrade"] = lblGrade.Text;
                 dtComplete.Rows.Add(dr);
             }
             else
             {
                 dr = null;
                 dr = dtComplete.NewRow();
                 dr["EMPID1"] = lblEmpCode.Text;
                 dr["Component_ID"] = "SN_To_NPS";
                 dr["Emp_Component_Value"] = txtNPSNewAmt.Text == "" ? "0" : txtNPSNewAmt.Text; ;
                 dr["FinancialYear"] = hdnFinancialYearCurr.Value;
                 dr["EmpGrade"] = lblGrade.Text;
                 dtComplete.Rows.Add(dr);

                 dr = null;
                 dr = dtComplete.NewRow();
                 dr["EMPID1"] = lblEmpCode.Text;
                 dr["Component_ID"] = "SN_To_AA";
                 dr["Emp_Component_Value"] = "0";
                 dr["FinancialYear"] = hdnFinancialYearCurr.Value;
                 dr["EmpGrade"] = lblGrade.Text;
                 dtComplete.Rows.Add(dr);

                dr = null;
                dr = dtComplete.NewRow();
                dr["EMPID1"] = lblEmpCode.Text;
                dr["Component_ID"] = "SN_To_AA";
                dr["Emp_Component_Value"] = lblAAWithSuperannuation.Text == "" ? "0" : lblAAWithSuperannuation.Text;
                dr["FinancialYear"] = hdnFinancialYearCurr.Value;
                dr["EmpGrade"] = lblGrade.Text;
                dtComplete.Rows.Add(dr);

                dr = null;
                 dr = dtComplete.NewRow();
                 dr["EMPID1"] = lblEmpCode.Text;
                 dr["Component_ID"] = "SN";
                 dr["Emp_Component_Value"] = lblSNRestruct.Text == "" ? "0" : lblSNRestruct.Text;
                 dr["FinancialYear"] = hdnFinancialYearCurr.Value;
                 dr["EmpGrade"] = lblGrade.Text;
                 dtComplete.Rows.Add(dr);
             }

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "PF";
             dr["Emp_Component_Value"] = lblPFRestruct.Text == "" ? "0" : lblPFRestruct.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "Gratuity";
             dr["Emp_Component_Value"] = lblGratuityRestruct.Text == "" ? "0" : lblGratuityRestruct.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             //Added By Mohan On 27 May 2019
             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "NPS";
             dr["Emp_Component_Value"] = txtNPSNewAmt.Text == "" ? "0" : txtNPSNewAmt.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);
             //End By Mohan

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "SA";
             dr["Emp_Component_Value"] = lblSiteAllowaRestruct.Text == "" ? "0" : lblSiteAllowaRestruct.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "ESIC";
             dr["Emp_Component_Value"] = lblESICRestruct.Text == "" ? "0" : lblESICRestruct.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);


             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "Bonus";
             dr["Emp_Component_Value"] = lblBonusRestruct.Text == "" ? "0" : lblBonusRestruct.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);


             //dr = null;
             //dr = dtComplete.NewRow();
             //dr["EMPID1"] = lblEmpCode.Text;
             //dr["Component_ID"] = "CanteenSubsidy";
             //dr["Emp_Component_Value"] = lblCanteenExisting.Text == "" ? "0" : lblCanteenExisting.Text;
             ////dr["Emp_Component_Value"] = lblCanteenRestruct.Text == "" ? "0" : lblCanteenRestruct.Text;
             //dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             //dr["EmpGrade"] = lblGrade.Text;
             //dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "IC";
             dr["Emp_Component_Value"] = lblICRestruc.Text == "" ? "0" : lblICRestruc.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "CC";
             dr["Emp_Component_Value"] = lblCCRestruc.Text == "" ? "0" : lblCCRestruc.Text;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);
             dtComplete.TableName = "dtComplete";

             dr = null;
             dr = dtComplete.NewRow();
             dr["EMPID1"] = lblEmpCode.Text;
             dr["Component_ID"] = "CTCAnnum";
             dr["Emp_Component_Value"] = hdnComputedCTCRestruct.Value == "" ? "0" : hdnComputedCTCRestruct.Value;
             dr["FinancialYear"] = hdnFinancialYearCurr.Value;
             dr["EmpGrade"] = lblGrade.Text;
             dtComplete.Rows.Add(dr);
             dtComplete.TableName = "dtComplete";

             DataTable dtnew = dtComplete;
             
             string SelectedSuperanuationOption = ddlSuperanuation.SelectedValue;
             
             //if (SelectedSuperanuationOption == "1")
             //    SelectedSuperanuationOption = "1";
             //else if (SelectedSuperanuationOption == "2")
             //    SelectedSuperanuationOption = "26";
             //else if (SelectedSuperanuationOption == "3")
             //    SelectedSuperanuationOption = "27";
             //else if (SelectedSuperanuationOption == "4")
             //    SelectedSuperanuationOption = "32";

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
                 mDbCommand.Parameters.AddWithValue("@tblEmployeeRestructuredSalary", SqlDbType.VarChar).Value = ConvertDatatableToXML(dtComplete);
                 mDbCommand.Parameters.AddWithValue("@EMPID", SqlDbType.VarChar).Value = lblEmpCode.Text;
                 mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
                 mDbCommand.Parameters.AddWithValue("@IsEmpFinalizedCTCRestruc", SqlDbType.VarChar).Value = "false";
                 mDbCommand.Parameters.AddWithValue("@SuperanuationOption", SqlDbType.VarChar).Value = SelectedSuperanuationOption;
                 if (rdoGetMealVoucher.SelectedValue == "0")
                 {
                     mDbCommand.Parameters.AddWithValue("@MealCardOption", SqlDbType.VarChar).Value = "No";
                     mDbCommand.Parameters.AddWithValue("@MealCardAmount", SqlDbType.VarChar).Value = rdoGetMealVoucher.SelectedValue;                    

                 }
                 else
                 {
                     mDbCommand.Parameters.AddWithValue("@MealCardOption", SqlDbType.VarChar).Value = "Yes";
                     mDbCommand.Parameters.AddWithValue("@MealCardAmount", SqlDbType.VarChar).Value = rdoGetMealVoucher.SelectedValue;
                 }
                 mDbCommand.Parameters.AddWithValue("@NPSAmount", SqlDbType.VarChar).Value = txtNPSNewAmt.Text;
                 
                 mDbCommand.ExecuteNonQuery();
                 Response.Redirect("Dummy.aspx", false);
                 mDSet = null;
                 mDSet = new DataSet();
                 hdnIsEmpFinalizedCTCRestruc.Value = "false";
                 mDSet = GetEmployeeBaseCTC();
                 FillEmployeeBaseCTC(mDSet);
                 PrepareActionButton();
                 SetControlBehivor();
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
                
             }
             catch (Exception ex)
             {

             }
         }
         catch (Exception ex )
         {
             objUserObjectDAL.LogError(ex.Message, "btnApplyCTC_Click");
             throw;
         }
        
    }
    
    private void calculateAA()
    {
        string NPSAmt = "0";

        if (Convert.ToInt64(lblNPSExistingAmt.Text) != 0)
        {
            if (Convert.ToInt64(lblNPSExistingAmt.Text) > Convert.ToInt64(txtNPSNewAmt.Text))
            {
                NPSAmt = (Convert.ToInt64(lblNPSExistingAmt.Text) - Convert.ToInt64(txtNPSNewAmt.Text)).ToString();
            }
            else
            {
                NPSAmt =  "-" + (Convert.ToInt64(txtNPSNewAmt.Text) - Convert.ToInt64(lblNPSExistingAmt.Text)).ToString();
            }
        }
        else
        {
            if (Convert.ToInt64(txtNPSNewAmt.Text) == 0)
            {
                NPSAmt = txtNPSNewAmt.Text;
            }
            else
            {
                NPSAmt = "-" + txtNPSNewAmt.Text;
            }
        }

        //if(ddlSuperanuation.SelectedValue == "1")
        //{
        //    lblAAWithSuperannuation.Text = "0";
        // }

        if (hdnIsNewEmployee.Value.Trim() == "Yes" && (hdnIsSAApplicable.Value.Trim() == "Yes" || hdnIsSAApplicable.Value.Trim() == "No"))
        {
            lblAACalculation.Text =
                (Convert.ToInt64(txtAA.Text) +
                 Convert.ToInt64(lblMobileExisting.Text) +
                 Convert.ToInt64(lblAAWithSuperannuation.Text) -
                 Convert.ToInt64(rdoGetMealVoucher.SelectedValue) +
                 Convert.ToInt64(NPSAmt)).ToString();
        }
        else if (hdnIsNewEmployee.Value.Trim() == "No" && hdnIsSAApplicable.Value.Trim() == "No")
        {
            lblAACalculation.Text =
               (Convert.ToInt64(txtAA.Text) +
                Convert.ToInt64(NPSAmt)).ToString();
        }
        else if (hdnIsNewEmployee.Value.Trim() == "No" && hdnIsSAApplicable.Value.Trim() == "Yes")
        {
            lblAACalculation.Text =
               (Convert.ToInt64(txtAA.Text) +
                Convert.ToInt64(lblAAWithSuperannuation.Text) +
                Convert.ToInt64(NPSAmt)).ToString();
        }
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
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
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
        string confirmValue = Request.Form["confirm_value_print"];

        if (confirmValue == "Yes")
        {
          //  btnPrintCTCRestruc_Click(sender, e);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "SCRIPT", "window.open('http://www.google.com','','');window.location = '" + "Default2.aspx?EmpCode=101440" + "';", true);

            DataSet mDSet = null;
            String mStoredProcName = String.Empty;
            SqlCommand mDbCommand = null;

            Int64 empBaseCTCTotal;
            Int64 empRestructCTCTotal;
            empBaseCTCTotal = Convert.ToInt64(lblComputedCTCExisting.Text);
            empRestructCTCTotal = Convert.ToInt64(hdnComputedCTCRestruct.Value);
            if (empRestructCTCTotal != empBaseCTCTotal || TextBox1.Text != "0" || lblGroupBTotalLimit.Text != "0")
            {
                //hdnIsEmpFinalizedCTCRestruc.Value = "false";
                //mDSet = GetEmployeeBaseCTC();
                //FillEmployeeBaseCTC(mDSet);
                //PrepareActionButton();
                //SetControlBehivor();
                ClientScriptManager cs = Page.ClientScript;
                string strScript = "<script type='text/javascript'>alert('Please review the restructured CTC.Base CTC Amount should be equal to restructured CTC. !');</script>";
                lblSNRestruct.Text = lblSNExisting.Text;
                cs.RegisterStartupScript(GetType(), "Message", strScript);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
                hdnIsEmpFinalizedCTCRestruc.Value = "false";
                mDSet = GetEmployeeBaseCTC();
                FillEmployeeBaseCTC(mDSet);
                PrepareActionButton();
                SetControlBehivor();
                return;
            }


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
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
                mDbCommand.Parameters.AddWithValue("@IsEmpFinalizedCTCRestruc", SqlDbType.VarChar).Value = "true";
                mDbCommand.ExecuteNonQuery();
                Response.Redirect("Dummy.aspx", false);
            }
               
            catch (Exception ex)
            {
                objUserObjectDAL.LogError(ex.Message, "OnConfirmPrint");
            }
             DataSet Dset = GetEmployeeBaseCTC();
             if (Dset != null)
             {
                 FillSuperanuation();
                 FillEmployeeBaseCTC(Dset);
             }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Print cancel !');", true);
        }
    }
    
    protected void btnPrintFinal_Click(object sender, EventArgs e)
    {
       // Response.Redirect("Default2.aspx?EmpCode=" + this.CurrUser.EmpCode + "&FinYear="+hdnFinancialYearCurr.Value);
        ////mDSet = null;
        ////mDSet = new DataSet();
        ////hdnFormMode.Value = "1";
        ////mDSet = GetEmployeeBaseCTC();
        ////FillEmployeeBaseCTC(mDSet);
        ////PrepareActionButton();
        ////SetControlBehivor();
        //if (ViewState["ds_RestructuredCTC"] != null)
        //{
        //    DataSet mDset = (DataSet)ViewState["ds_RestructuredCTC"];
        //    mPDF.GetPDF(mDset);
        //}
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
            mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;

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
        Response.Redirect("Default2.aspx?EmpCode=" + this.CurrUser.EmpCode + "&FinYear="+ hdnFinancialYearCurr.Value);
        //ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

        ////ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/MyStoredProcedureReport.rdl");
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportTest.rdlc");
        ////ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/TestReport.rdlc");
        ////ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/MyNewTestReport.rdl");
        ////ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report.rdlc");

        //DataSet ds = GetEmployeeCTC();

        //Microsoft.Reporting.WebForms.ReportDataSource datasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", ds.Tables[0]);


        ////ReportViewer1.ShowParameterPrompts = false;
        //ReportViewer1.LocalReport.DataSources.Clear();
        //ReportViewer1.LocalReport.DataSources.Add(datasource);

        ////System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
        ////ps.Landscape = false;
        ////ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
        ////ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
        ////ReportViewer1.SetPageSettings(ps);
    }
    
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "CloseWindow();");
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
            mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;
            mDbCommand.Parameters.AddWithValue("@IsEmpFinalizedCTCRestruc", SqlDbType.VarChar).Value = "false";
            mDbCommand.Parameters.AddWithValue("@SuperanuationOption", SqlDbType.VarChar).Value = "1";
            mDbCommand.Parameters.AddWithValue("@NPSAmount", SqlDbType.VarChar).Value = "0";
            mDbCommand.ExecuteNonQuery();
             
            mDSet = null;
            mDSet = new DataSet();
            hdnFormMode.Value = "0";
            //Added By Mohan On 31 May 2019
            ResetFields();
            //End By Mohan
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
    
    public void ResetFields()
    {
        txtHRA.Text = "0";
        txtCA.Text = "0";
        txtLTA.Text = "0";
        txtCEA.Text = "0";
        txtCHA.Text = "0";
        txtMARestruct.Text = "0";
        txtMobile.Text = "0";
        txtAA.Text = "0";
        lblMeamCardDeducFrmAA.Text = "0";
        lblAAWithSuperannuation.Text = "0";
        lblAACalculation.Text = "";
        rdoGetMealVoucher.SelectedValue = "0";
        txtNPSNewAmt.Text = "0";
        rdoGetMealVoucher_SelectedIndexChanged(null,null);
    }

    protected void lnkDummy_Click(object sender, EventArgs e)
    {

    }
    
    protected void hdnSAOLD_ValueChanged(object sender, EventArgs e)
    {

    }
    
    protected void btnApplySAChanges_Click(object sender, EventArgs e)
    {
        //Commented By Mohan On 27 May 2019
        //if (Convert.ToInt32(txtNPSSA.Text) > 0    && Convert.ToInt32(txtNPSSA.Text) < 500 )
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Value in NPS can either be zero or more than Rs. 499/- per month.');  ", true);
        //    txtNPSSA.Text = "0";
        //    txtAASA.Text = "0";
        //    //Commented By Mohan On 27 May 2019
        //    //lblNPS.Text = txtNPSSA.Text;
        //    //End By Mohan
        //    lblAAWithSuperannuation.Text = txtAASA.Text;
        //    txtNPSSA.Text = "0";
        //    txtAASA.Text = "0";
        //    lblSNRestruct.Text = lblSNExisting.Text;
        //    ddlSuperanuation.SelectedIndex = 0;
        //}
        //else
        //{

        if (Convert.ToInt64(txtNPSNewAmt.Text) == 0)
        {
            lblNPSDeducFrmAA.Text = "0";
        }
        else
        {
            if (Convert.ToInt64(lblNPSExistingAmt.Text) != 0)
            {
                if (Convert.ToInt64(lblNPSExistingAmt.Text) > Convert.ToInt64(txtNPSNewAmt.Text))
                {
                    lblNPSDeducFrmAA.Text = (Convert.ToInt64(lblNPSExistingAmt.Text) - Convert.ToInt64(txtNPSNewAmt.Text)).ToString();
                }
                else
                {
                    lblNPSDeducFrmAA.Text = "-" + (Convert.ToInt64(txtNPSNewAmt.Text) - Convert.ToInt64(lblNPSExistingAmt.Text)).ToString();
                }
            }
            else
            {
                lblNPSDeducFrmAA.Text = "-" + txtNPSNewAmt.Text;
            }
        }

        //lblNPS.Text = txtNPSSA.Text;
        //txtAASA.Text = (Convert.ToInt64(hdnBalnceSA.Value == "" ? "0" : hdnBalnceSA.Value) - Convert.ToInt64(txtNPSSA.Text)).ToString();
        //End By Mohan
        //Added By Mohan On 27 May 2019
        txtAASA.Text = hdnBalnceSA.Value == "" ? "0" : hdnBalnceSA.Value;
            //End By mohan

            lblAAWithSuperannuation.Text = txtAASA.Text;
            if (rdoGetMealVoucher.SelectedValue == "0")
            {
                hdnIsMealVoucherOpted.Value = "0";
                txtAA.Text = (Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();
                lblAACalculation.Text = (Convert.ToInt64(lblAAExisting.Text) + Convert.ToInt64(lblMobileExisting.Text) - Convert.ToInt64(txtMobile.Text) + 
                Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();
                //lblAACalculation.Text = (Convert.ToInt64(lblAAExisting.Text) + Convert.ToInt64(lblMobileExisting.Text) - Convert.ToInt64(txtMobile.Text) + Convert.ToInt64(lblAAWithCanteenSubsRestruc.Text) + Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();
                lblMealCardRestruc.Text = "0";
            }
            else
            {
                hdnIsMealVoucherOpted.Value = rdoGetMealVoucher.SelectedValue;
                txtAA.Text = (Convert.ToInt64(lblMobileExisting.Text == "" ? "0" : lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();
                lblAACalculation.Text = (Convert.ToInt64(lblAAExisting.Text) + Convert.ToInt64(lblMobileExisting.Text) - Convert.ToInt64(txtMobile.Text) +
                Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();
                //lblAACalculation.Text = (Convert.ToInt64(lblAAExisting.Text) + Convert.ToInt64(lblMobileExisting.Text) - Convert.ToInt64(txtMobile.Text) + Convert.ToInt64(lblAAWithCanteenSubsRestruc.Text) + Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();
            }
            //Commented By Mohan On 27 May 2019
            //lblSNRestruct.Text = (Convert.ToInt32(lblSNExisting.Text) - (Convert.ToInt32(txtNPSSA.Text) + Convert.ToInt32(txtAASA.Text))).ToString();
            //End By Mohan
            //Added By Mohan On 27 May 2019
            //lblSNRestruct.Text = (Convert.ToInt32(lblSNExisting.Text) - Convert.ToInt32(txtAASA.Text)).ToString();
            lblSNRestruct.Text = hdnSN.Value;
            //End By Mohan    
           // hdnSN.Value = lblSNRestruct.Text;
            //Commented By Mohan On 27 May 2019 
            //txtNPSSA.Text = "0";
            //End By mohan
            txtAASA.Text = "0";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
        //}        
    }
    
    protected void rdoGetMealVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Added By Mohan On 30 May 2019
        if (Convert.ToInt64(txtNPSNewAmt.Text) == 0)
        {
            lblNPSDeducFrmAA.Text = "0";
        }
        else
        {
            if (Convert.ToInt64(lblNPSExistingAmt.Text) != 0)
            {
                if (Convert.ToInt64(lblNPSExistingAmt.Text) > Convert.ToInt64(txtNPSNewAmt.Text))
                {
                    lblNPSDeducFrmAA.Text = (Convert.ToInt64(lblNPSExistingAmt.Text) - Convert.ToInt64(txtNPSNewAmt.Text)).ToString();
                }
                else
                {
                    lblNPSDeducFrmAA.Text = "-" + (Convert.ToInt64(txtNPSNewAmt.Text) - Convert.ToInt64(lblNPSExistingAmt.Text)).ToString();
                }
            }
            else
            {
                lblNPSDeducFrmAA.Text = "-" + txtNPSNewAmt.Text;
            }
        }
        //End By Mohan
        if (rdoGetMealVoucher.SelectedValue == "0")
        {
            Int64 NPSAmount = (txtNPSNewAmt.Text != "" ? Convert.ToInt64(txtNPSNewAmt.Text) : 0);

            hdnIsMealVoucherOpted.Value = "0";
            txtAA.Text = (Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();
            lblAA.Text = (Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();

            //lblAACalculation.Text = (Convert.ToInt64(lblAAExisting.Text) + Convert.ToInt64(lblMobileExisting.Text) - Convert.ToInt64(txtMobile.Text)
            //     + Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue) - NPSAmount).ToString();

            //Added by Mohan on 05 Jul 2019
            calculateAA();

            lblMealCardRestruc.Text = "0";
            lblMeamCardDeducFrmAA.Text = "0";
        }
        else
        {
            Int64 NPSAmount = (txtNPSNewAmt.Text != "" ? Convert.ToInt64(txtNPSNewAmt.Text) : 0);

            hdnIsMealVoucherOpted.Value = rdoGetMealVoucher.SelectedValue ;
            //txtAA.Text = (Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();
            //txtAA.Text = (Convert.ToInt64(txtAA.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();
            lblAA.Text = (Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();
            txtAA.Text = (Convert.ToInt64(lblMobileExisting.Text) + Convert.ToInt64(lblAAExisting.Text) - Convert.ToInt64(txtMobile.Text)).ToString();
            //lblAA.Text = (Convert.ToInt64(lblAA.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();
            lblMealCardRestruc.Text = Convert.ToInt64(rdoGetMealVoucher.SelectedValue).ToString();
            lblMeamCardDeducFrmAA.Text = "-"+ rdoGetMealVoucher.SelectedValue;

            //Added by Mohan on 05 Jul 2019
            calculateAA();

            //lblAACalculation.Text = (Convert.ToInt64(lblAAExisting.Text) + Convert.ToInt64(lblMobileExisting.Text) - Convert.ToInt64(txtMobile.Text )
            //    + Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue) - NPSAmount).ToString();
            //lblAACalculation.Text = (Convert.ToInt64(lblAAExisting.Text) + Convert.ToInt64(lblMobileExisting.Text) - Convert.ToInt64(txtMobile.Text) + Convert.ToInt64(lblAAWithCanteenSubsRestruc.Text) + Convert.ToInt64(lblAAWithSuperannuation.Text) - Convert.ToInt64(rdoGetMealVoucher.SelectedValue)).ToString();
        }

        //string savalue=hdnSuperanution.Value;
        //if (savalue == "IsNotApplicable")
        //{ 
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "HideSuperAnnuation(); ", true); 
        //}
        

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "PerformCalculation(); ", true);
    }
    protected void btnPrintFinal_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnPrintFinal_Click1(object sender, ImageClickEventArgs e)
    {
         Response.Redirect("Default2.aspx?EmpCode=" + this.CurrUser.EmpCode + "&FinYear="+hdnFinancialYearCurr.Value);
    }
}
        




