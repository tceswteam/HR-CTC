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

public partial class Default1 : System.Web.UI.Page
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
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('OTP generated for Compensation Re-structuring and an email is sent you.');", true);
            mUserObjectBLL = new UserObjectBLL();
            this.CurrUser = mUserObjectBLL.GetUserSession();
        }
        catch (Exception ex)
        {
            mUserObjectBLL.writetolog(ex.Message);
            throw;
        }
    }
    protected void btnLoginForCompRestruc_Click(object sender, EventArgs e)
    {
        try
        {
            string connstr = System.Configuration.ConfigurationSettings.AppSettings["DBConn"].ToString();
            using (SqlConnection cn = new SqlConnection(connstr))
            {

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.CommandTimeout = 10000;
                SqlCmd.Connection = cn;
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //SqlCmd.CommandText = "GetEventDetails_ByCurrentDate";
                SqlCmd.CommandText = "VALIDATE_EMP_TOP";
                SqlCmd.Parameters.AddWithValue("@EmpCode", SqlDbType.VarChar).Value = this.CurrUser.EmpCode;
                SqlCmd.Parameters.AddWithValue("@OTP", SqlDbType.VarChar).Value =txtOTP.Text;
                SqlDataAdapter SqldbDa = new SqlDataAdapter(SqlCmd);
                DataSet Ds = new DataSet();
                SqldbDa.Fill(Ds);
                if (Ds != null)
                {
                    if (Ds.Tables.Count > 0)
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            if (Ds.Tables[0].Rows[0]["VALIDATION_FLAG"].ToString() == "TRUE")
                            {
                                //string pagetoredirect = Ds.Tables[1].Rows[0]["RedirectToPage"].ToString();
                               // Response.Redirect(Ds.Tables[2].Rows[0]["ConfigValue"].ToString(), false);
                                Response.Redirect(Ds.Tables[2].Rows[0]["ConfigValue"].ToString(), false);

                                
                                //Response.Redirect(pagetoredirect, false);
                            }
                            else
                            {
                                lblMessage.Text = "Please enter valid OTP";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex) 
        {
            mUserObjectBLL.writetolog(ex.Message);
            throw;
        }
       
    }
}