using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class User:BaseEntity
    {

       
           
            public string UserName { get; set; }
            public string Password { get; set; }

            public virtual ICollection<Message> SentMessages { get; set; }
            public virtual ICollection<Message> ReceivedMessages { get; set; }
        

    }
}
