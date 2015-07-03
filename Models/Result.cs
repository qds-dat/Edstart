using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    public class Result
    {
        public bool State;
        public string Message;
        public Object RetVal;

        public Result Fail(string Message) {
            this.State = false;
            this.Message = Message;
            return this;
        }

        public Result Success(Object RetVal)
        {
            this.State = true;
            this.RetVal = RetVal;
            return this;
        }
    }


}