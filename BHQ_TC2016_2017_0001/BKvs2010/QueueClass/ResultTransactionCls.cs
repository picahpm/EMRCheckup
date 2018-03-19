using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKvs2010.QueueClass
{
    public enum ResultTransactionCls
    {
        Complete,
        Incomplete,
        Cancel,
        Error,
        ChangeSite,
        SendCheckB,
        ReSendManualSite2
    }
}
