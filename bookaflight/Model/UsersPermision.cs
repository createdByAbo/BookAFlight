using System;
using System.Collections.Generic;

namespace bookaflight.Model
{
    public partial class UsersPermision
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public int IsWorker { get; set; }
    }
}
