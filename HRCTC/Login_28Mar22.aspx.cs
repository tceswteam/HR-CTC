
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


public partial class Login : System.Web.UI.Page
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
            UserObjectBLL mUserBLL = null;
            mUserBLL = new UserObjectBLL();

            DataSet ds = mUserBLL.AccessApplicationConfig();
            if (ds != null)
            {
                if (ds.Tables[0].Rows[0]["ConfigDesc"].ToString() == "CanAccessSiteOrNot")
                    if (ds.Tables[0].Rows[0]["ConfigValue"].ToString() == "N")
                    {
                        Server.Transfer("UnderConstruction.aspx");
                    }
            }

           // lblVersion.Text = ConfigurationManager.AppSettings["Version"];
            //txtPassword.Attributes.Add("value", "pass123$");
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
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('before GetAuthenticatedUserDetails');", true);//
            string strAuth = mUserBLL.GetLDAPAuthenticationOnly(txtUserName.Text, txtPassword.Text, "");
            if (strAuth == "Authenticated")
            {
                mUserSC = mUserBLL.GetAuthenticatedUserDetails(txtUserName.Text, txtPassword.Text, "");
                // ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('after GetAuthenticatedUserDetails');", true);//
                if (mUserSC != null)
                {
                    //ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('mUserSC != null');", true);//
                    Session["UserObj"] = mUserSC;
                    FormsAuthentication.RedirectFromLoginPage(mUserID, false);
                }
                else
                {
                    //Response.Write("<script>alert('Incorrect LoginId entered . Authentication failed')</script>");
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('User not mapped in System .Please contact HR-Admin for access. Authentication Failed');", true);//
                }
            }
            else
            {
                objUserObjectDAL.LogError("Error", "Sign in of application");
                ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('Incorrect Login:Either wrong User ID Or wrong password.');", true); 
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
