using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data
{
    public class ContactModel: BaseModel
    {
        public string PhoneNumber { get; set; }
        public PhoneBookUserModel PhoneBookUser { get; set; }
        public int? PhoneBookUserId { get; set; }
    }
}
