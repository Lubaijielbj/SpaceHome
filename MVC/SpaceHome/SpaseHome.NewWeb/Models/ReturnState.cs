using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaseHome.NewWeb.Models
{
    public struct ReturnState
    {
        public StateType stateType { get; set; }
        public object message { get; set; }
    }

    public enum StateType
    {
        error=0,
        success=1,
    }
}