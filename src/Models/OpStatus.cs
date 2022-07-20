using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace userDemo1.Models
{
    public enum OpStatus
    {
        Open,
        InProcess,
        OnHold,
        Packing,
        Shipping,
        Delivered,
        Closed
    }
}