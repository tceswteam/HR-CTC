using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dummy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Server.Transfer("frmEmpCTCRestruct.aspx");
        Response.Redirect("frmEmpCTCRestruct_Version1.aspx", false);
    }
}