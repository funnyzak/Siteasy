﻿using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Payment;
using STA.Entity;
namespace STA.Page.User
{
    public class Inboxpm : UserBase
    {
        protected override void PageShow()
        {
            if (!IsLogin()) return;
        }
    }
}
