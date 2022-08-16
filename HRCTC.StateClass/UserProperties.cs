
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


[Serializable()]
public class UserADSProperties
{

    string _LoginID = string.Empty;
    string _Code = string.Empty;
    string _Name = string.Empty;
    
    string _Email = string.Empty;
    public string LoginID
    {
        get { return _LoginID; }
        set { _LoginID = value; }
    }

    public string Code
    {
        get { return _Code; }
        set { _Code = value; }
    }

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public string EmailAddress
    {
        get { return _Email; }
        set { _Email = value; }
    }

}
