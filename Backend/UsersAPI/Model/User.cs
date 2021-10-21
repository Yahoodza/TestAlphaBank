using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersAPI.Model
{
    public class User
    {
        public string Id { get; set; }
        public string FIO { get; set; }
        public string UserLogin { get; set; }
        public DateTime? DateAddUser { get; set; }
    }
}
