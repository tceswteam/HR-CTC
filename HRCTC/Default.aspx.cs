using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HRCTCApp.BLL;
using HRCTCApp.DAL;
using HRCTC.StateClass;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Threading;

public partial class frmGenerateSystemPwd : System.Web.UI.Page
{
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
    public SqlConnection Conn = new SqlConnection();
    public SqlCommand Cmd;
    UserObjectBLL mUserObjectBLL = new UserObjectBLL();
      
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
  
            mUserObjectBLL = new UserObjectBLL();
            this.CurrUser = mUserObjectBLL.GetUserSession();
            if (!IsPostBack)
            {
                GetCurrentFY();
                txtEmail.Text = this.CurrUser.EmailAdd;
                //txtEmail.Text = "chetnak@tce.co.in";
            }
        }
        catch (Exception ex)
        {
            mUserObjectBLL.writetolog(ex.Message);
            throw;
        }
    }
    protected void btnGeneratePwd_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet mDSet = null;
            String mStoredProcName = String.Empty;
            SqlCommand mDbCommand = null;
            SqlDataAdapter mQueryResult = null;
            mDSet = new DataSet();
            
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
                    Conn.Open();
                }
                mStoredProcName = "sp_GeneratePwdForEmp";
                mDbCommand = new SqlCommand(mStoredProcName, Conn);
                mDbCommand.CommandType = CommandType.StoredProcedure;
                mDbCommand.Parameters.AddWithValue("@EmpCode", SqlDbType.VarChar).Value = this.CurrUser.EmpCode;
                mDbCommand.Parameters.AddWithValue("@FinancialYear", SqlDbType.VarChar).Value = hdnFinancialYearCurr.Value;

                mDSet = new DataSet();
                mQueryResult = new SqlDataAdapter(mDbCommand);
                mQueryResult.Fill(mDSet);
                Session["UserPwdSession"] = mDSet;


                if (Session["UserPwdSession"] != null)
                {
                    DataSet ds_new = (DataSet)Session["UserPwdSession"];
                    string userid = "", pwd = "";
                    if (ds_new != null)
                    {
                        userid = ds_new.Tables[0].Rows[0]["EMPCODE"].ToString();
                        pwd = ds_new.Tables[0].Rows[0]["CTC_OTP"].ToString();
                    }
                    this.CurrUser.OTPPassword = pwd;
                    this.CurrUser.EmpCode = userid;
                    //string mailFrom, displayname, BCClist, ServerIPHost, Host;
                    //mailFrom = ""; displayname = ""; BCClist = ""; ServerIPHost = ""; Host = "";
                   
                        string connstr = System.Configuration.ConfigurationSettings.AppSettings["DBConn"].ToString();

                        string body = "<html><body><div style='font:Verdana;fontsize:12px;' > <p>Dear Colleague,</p><p>You have accessed Compensation Restructuring (CompStruct) Portal for which One Time Password (OTP) is <b><span style='color:maroon;'> " + pwd + "</span></b> </p> Regards, <br/> Corporate HR  </div></body></html>  ";
                       
                        //// to be un commented laters for Live release 
                        
                        //SendMail(txtEmail.Text, "", "", "OTP for CompStruct", body);

                        // SendMail("chetnak@tce.co.in", "chetnak@tce.co.in", "", "OTP for CompStruct", body);
                        if (ds_new.Tables[1].Rows[0]["ConfigDesc"].ToString() == "IsOTPMandate")
                        {
                            if (ds_new.Tables[1].Rows[0]["ConfigValue"].ToString() == "N")
                            {
                              Response.Redirect(ds_new.Tables[2].Rows[0]["ConfigValue"].ToString(), false); // if need to by pass the OTP generating page and redirect to Regime option page
                                 
                            }
                            else
                            {
                                //SendMail(txtEmail.Text, "", "", "OTP for CompStruct", body);//To send mail to employee for OTP
                               // SendMail(txtEmail.Text, "", "", "OTP for CompStruct", body);//When not to send mail to employee for OTP
                                Response.Redirect(ds_new.Tables[2].Rows[0]["ConfigValue"].ToString(), false); // if need to by pass the OTP generating page and redirect to Regime option page
                                //Response.Redirect("Default1.aspx", false);// if need to go though the proper  OTP generating cycle this line to be uncommented   
                            }
                        }
                        //Response.Redirect("Default1.aspx", false);// if need to go though the proper  OTP generating cycle this line to be uncommented   
                     
                        ////to be un commented laters for Live release 

                        //Response.Redirect("frmEmpCTCRestruct.aspx", false);
                             
                   
                


                    
                    ////need to comment below lines for sending email and redirecting to page where OTP will be asked for bypassing the OTP logic
                    //    to be un commented laters for Live release 
                    //  SendMail(txtEmail.Text, "", "", "OTP for CompStruct", body);
                    // SendMail("chetnak@tce.co.in", "chetnak@tce.co.in", "", "OTP for CompStruct", body);

                    //Response.Redirect("Default1.aspx", false);// if need to go though the proper  OTP generating cycle this line to be uncommented  
                    // Response.Redirect("frmEmpCTCRestruct_Version1.aspx", false); //  if need to by pass the OTP generating page and redirect to comp retruct page
                    //  Response.Redirect("SalaryIncomeTaxCalc.aspx", false); // if need to by pass the OTP generating page and redirect to Regime option page
                    //Response.Redirect("frmEmpCTCRestruct.aspx", false);
                        
                    ////to be un commented laters for Live release 

                    //Response.Redirect("frmEmpCTCRestruct.aspx", false);
                        
                    
                }
          
        }
        catch (Exception ex)
        {
            mUserObjectBLL.writetolog(ex.Message);  
            throw;
        }
       
 
        //Response.Redirect("frmEMPCTCRestruct.aspx", false);
    }

    public void SendMail(string ToEmail, string cc1, string cc2, string subject, string body)
    {
        try
        {
            Conn.Close();
            Conn.Open();
            String SmtpError = String.Empty;

            SmtpClient mSmtpClient = new SmtpClient("172.16.10.76", 25);
            MailMessage mMailMessage = new MailMessage();
            mMailMessage.IsBodyHtml = true;

            mMailMessage.From = new MailAddress("corporatehr@tce.co.in", "CompStruct Helpdesk");
            if (ToEmail != "" && ToEmail.Contains(".in"))
            {
                mMailMessage.To.Add(new MailAddress(ToEmail));
            }

            if (cc1 != "" && cc1.Contains(".in"))
            {
                mMailMessage.CC.Add(new MailAddress(cc1));
            }
            if (cc2 != "" && cc2.Contains(".in"))
            {
                mMailMessage.CC.Add(new MailAddress(cc2));
            }

            mMailMessage.Subject = subject;
            mMailMessage.Body = body;
            mMailMessage.IsBodyHtml = true;
            mMailMessage.Priority = MailPriority.High;

            try
            {
                mSmtpClient.Send(mMailMessage);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
            finally
            {

            }

        }
        catch (Exception ex)
        {
            mUserObjectBLL.writetolog(ex.Message);  
            throw;
        }
     
    }
    private void GetCurrentFY()
    {
        DataSet mDSet = null;
        CommonBLL mCommonBLL = new CommonBLL();
        mDSet = mCommonBLL.GetFinancialMonthYr();
        hdnFinancialYearCurr.Value = mDSet.Tables[7].Rows[0]["Text"].ToString();
        //hdnFinancialYearPrev.Value = mDSet.Tables[7].Rows[1]["Text"].ToString();
        //ltrFinYr.Text = hdnFinancialYearCurr.Value;

    }
}