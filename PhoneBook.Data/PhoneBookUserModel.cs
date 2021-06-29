using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Data
{
    [Table("PhoneBook")]
    public class PhoneBookUserModel: BaseModel
    {
        public ICollection<ContactModel> Contacts { get; set; }
    }
}
