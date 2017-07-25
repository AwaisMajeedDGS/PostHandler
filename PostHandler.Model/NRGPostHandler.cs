using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostHandler.Model
{
    public class NRGPostHandler
    {
        public string CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string city { get; set; }
        public string State { get; set; }
        public string NRGAuthkey { get; set; }

        public class StoredProcedure
        {
            public const string InsertNRGPostedData = "[dbo].[NRG_POST_Data_ADD]";
        }
    }


}
