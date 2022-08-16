
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HRCTCApp.BLL;
using HRCTCApp.DAL;
using HRCTC.StateClass;


public partial class AdminLogin : System.Web.UI.Page
{
    #region " Variables "

    UserObjectSC _UserObj;
    UserObjectDAL objUserObjectDAL = new UserObjectDAL();

    #endregion

    #region " Properties "

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

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //UserObjectBLL mUserBLL = null;
            //mUserBLL = new UserObjectBLL();

            //DataSet ds = mUserBLL.AccessApplicationConfig();
            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows[1]["ConfigDesc"].ToString() == "SuperAdmin")
            //        if (ds.Tables[0].Rows[1]["ConfigValue"].ToString() != "7654321")
            //        {
            //            Server.Transfer("UnderConstruction.aspx");
            //        }
            //}

           //// lblVersion.Text = ConfigurationManager.AppSettings["Version"];
           // //txtPassword.Attributes.Add("value", "pass123$");
        }
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        UserObjectSC mUserSC = null;
        UserObjectBLL mUserBLL = null;

        String mUserID = String.Empty;
        String mPasswd = String.Empty;
        try
        {
            mUserSC = new UserObjectSC();
            mUserBLL = new UserObjectBLL();
            mUserID = txtUserName.Text.ToString().Trim();
            mPasswd = txtPassword.Text.ToString().Trim();
        
                mUserSC = mUserBLL.GetAuthenticatedUserDetailsFromAdmin(txtUserName.Text, txtPassword.Text);
                // ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('after GetAuthenticatedUserDetails');", true);//
                if (mUserSC != null)
                {
                    //ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('mUserSC != null');", true);//
                    Session["UserObj"] = mUserSC;
                    Response.Redirect("frmEmpCTCRestruct_Version1.aspx?AdminLogin=Yes", false);
//                    FormsAuthentication.RedirectFromLoginPage(mUserID, false);
                }
                else
                {
                    //Response.Write("<script>alert('Incorrect LoginId entered . Authentication failed')</script>");
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('User not mapped in System .Please contact HR-Admin for access. Authentication Failed');", true);//
                }
           
        }
        catch (Exception ex)
        {
            objUserObjectDAL.LogError(ex.Message, "Sign in of application");
            //Response.Write("Error Message : " + ex.ToString());
            ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('Incorrect Login:Either wrong User ID Or wrong password.');", true);
        }
    }
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {

    }
}
