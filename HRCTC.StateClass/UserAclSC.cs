using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UserAclSC
{
    private String _CreatedBy = String.Empty;
    private String _CreatedOn = String.Empty;
    private String _ModifiedBy = String.Empty;
    private String _ModifiedOn = String.Empty;
    public string _IsEdit = String.Empty;
    public string _UserID = String.Empty;
    public string _ddlRoles = String.Empty;

    public int _UserACLId = 0;

     public int UserACLId
    {
        get { return _UserACLId; }
        set { _UserACLId = value; }
    }

    public String UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }

    public String _UserName = String.Empty;
    public String UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }


    public String ModifiedOn
    {
        get { return _ModifiedOn; }
        set { _ModifiedOn = value; }
    }



    public String ModifiedBy
    {
        get { return _ModifiedBy; }
        set { _ModifiedBy = value; }
    }

    public String RoleCode
    {
        get { return _ddlRoles; }
        set { _ddlRoles = value; }
    }




    public String IsEdit
    {
        get { return _IsEdit; }
        set { _IsEdit = value; }
    }



    public String CreatedBy
    {
        get { return _CreatedBy; }
        set { _CreatedBy = value; }
    }

    public String CreatedOn
    {
        get { return _CreatedOn;}
        set { _CreatedOn = value;}
    }

   
}
