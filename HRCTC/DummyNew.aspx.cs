using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DummyNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Server.Transfer("frmEmpCTCRestruct.aspx");
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('This provision is not mapped for you.Kinldy check with HR!');", true);
        Response.Redirect("Login.aspx", false);
    }
}