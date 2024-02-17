using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Message:BaseEntity
    {
       
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

        public Message()
        {
            TimeStamp= DateTime.Now;
        }
        public int SenderId { get; set; } 
        public int ReceiverId { get; set; } 

        public virtual User Sender { get; set; } 
        public virtual User Receiver { get; set; } 
    }
}
