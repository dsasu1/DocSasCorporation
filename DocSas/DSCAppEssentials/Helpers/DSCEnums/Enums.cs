using System;
using System.Collections.Generic;
using System.Text;

namespace DSCAppEssentials.Helpers.DSCEnums
{
    public enum DSCHttpStatus : int
    {
        OK = 200,
        NotFound = 404,
        Unauthorized = 401
    }

    public enum DSCVerifyType
    {
        Register,
        ForgotPassword
    }

    public enum DSCMediaType
    {
        Call,
        Text,
        Email
    }

    public enum DSCPosition
    {
        Right,
        Left
    }

    public enum DSCCodeType
    {
        User,
        Property
    }

}
