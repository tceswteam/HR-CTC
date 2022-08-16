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

public partial class SessionExpired : System.Web.UI.Page
{
    public string ReturnURL
    {
        get
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                return HttpUtility.UrlEncode(Request.Url.Query).Replace("%3fReturnURL%3d","?ReturnUrl=");
            else
                return "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("Login.aspx?ReturnURL=" + ReturnURL);
    }
}
