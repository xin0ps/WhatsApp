using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Model.Models;

namespace DataAcces.Context
{
    public class WPDBContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string constr = "Data Source=LAPTOP-JLBUNNBV\\SQLEXPRESS;Initial Catalog=WhatsappDB; User Id=resul;Password=resul6155; Integrated Security=True;Encrypt=True;Trust Server Certificate=True; Pooling=true;Max Pool Size=20;";
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(constr);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages) 
                .WithOne(m => m.Sender) 
                .HasForeignKey(m => m.SenderId) 
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages) 
                .WithOne(m => m.Receiver) 
                .HasForeignKey(m => m.ReceiverId) 
                .OnDelete(DeleteBehavior.Restrict); 

           
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender) 
                .WithMany(u => u.SentMessages) 
                .HasForeignKey(m => m.SenderId); 

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver) 
                .WithMany(u => u.ReceivedMessages) 
                .HasForeignKey(m => m.ReceiverId); 
        }


        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Message>? Messages { get; set; }
    }
}
