using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Constants
{
    public class TokenInfoConstant
    {
        public const int tokenExpiresInSeconds = 15 * 60;
        public const int tokenExpiresInMinutes = 15;
        public const int refreshTokenExpiresInSeconds = 7 * 24 * 60 * 60;
        public const int refreshTokenExpiresInDays = 7;
    }
}
