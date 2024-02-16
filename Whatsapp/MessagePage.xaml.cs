using DataAcces.Repositories;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Whatsapp
{
    /// <summary>
    /// Interaction logic for MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page,INotifyPropertyChanged
    {


        public Thread thread1 { get; set; }

        public User CurrentUser {  get; set; }

        private ObservableCollection<Message> _selectedMessages;

        public ObservableCollection<Message> SelectedMessages
        {
            get { return _selectedMessages; }
            set { _selectedMessages = value;OnPropertyChanged();}
        }


        private User _selectedUser;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value;OnPropertyChanged(); isenabled(); }
        }




        private ObservableCollection<User> _popularuser;

        public ObservableCollection<User> PopularUser
        {
            get { return _popularuser; }
            set { _popularuser = value; OnPropertyChanged(); }
        }



        public BaseRepo<Message> MessageRepo { get; set; }
        

        private ObservableCollection<Message> _allusermessages;

        public ObservableCollection<Message> AllCurrentUserMessages
        {
            get { return _allusermessages; }
            set { _allusermessages = value; OnPropertyChanged(); }
        }


        public MessagePage(User  user)
        {

            thread1 = new Thread(worker1);
            thread1.IsBackground = true;
            thread1.Start();


            CurrentUser = user;
            MessageRepo = new BaseRepo<Message>();
           
            AllCurrentUserMessages= new ObservableCollection<Message>(MessageRepo.GetAll().Where(m => (m.SenderId == CurrentUser.Id) || (m.ReceiverId == CurrentUser.Id)));

            PopularUser=new ObservableCollection<User>();
            SelectedMessages=new ObservableCollection<Message>();
           
            FindPopularUsers();
            DataContext = this;
            InitializeComponent();
        }


        public void worker1()
        {
            
            while (true)
            {
                Thread.Sleep(1500);
                var messages = new BaseRepo<Message>();
                AllCurrentUserMessages = new ObservableCollection<Message>(messages.GetAll().Where(m => (m.SenderId == CurrentUser.Id) || (m.ReceiverId == CurrentUser.Id)));
                if (SelectedUser != null)
                {
                    sortmessage();
                }
            }
        }


        public int lastindex()
        {
            Message msg = SelectedMessages.Last();

            int index = SelectedMessages.IndexOf(msg);
            return index;
        }

        private void isenabled()
        {
            msgText.IsEnabled = true;
            sentbutton.IsEnabled = true;
            messagelist.IsEnabled = true;
        }

        private void FindPopularUsers()
        {
          
            var uniqueUserIds = new HashSet<int>();

           
            foreach (var message in AllCurrentUserMessages)
            {
                uniqueUserIds.Add(message.SenderId);
                uniqueUserIds.Add(message.ReceiverId);
            }
            uniqueUserIds.Remove(CurrentUser.Id);
           
            var userRepository = new BaseRepo<User>(); 
            PopularUser = new ObservableCollection<User>(
                userRepository.GetAll().Where(u => uniqueUserIds.Contains(u.Id))

            );
            
        }


        private void sortmessage()
        {
            var messages = AllCurrentUserMessages.Where(u => (u.ReceiverId ==SelectedUser.Id) || (u.SenderId == SelectedUser.Id)).ToList();
            var msg = messages.OrderBy(m => m.TimeStamp).ToList();
            SelectedMessages = new ObservableCollection<Message>(msg);
      

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void SentClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(msgText.Text))
            {
                Message message = new Message();
                message.SenderId = CurrentUser.Id;
                message.ReceiverId = SelectedUser.Id;
                message.Content = msgText.Text;

                msgText.Clear();
                MessageRepo.Add(message);
                MessageRepo.SaveChanges();
            }
        }

        private void SentClick()
        {
            if (!string.IsNullOrEmpty(msgText.Text))
            {
                Message message = new Message();
                message.SenderId = CurrentUser.Id;
                message.ReceiverId = SelectedUser.Id;
                message.Content = msgText.Text;

                msgText.Clear();
                MessageRepo.Add(message);
                MessageRepo.SaveChanges();
            }
        }

        private void EnterClick(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SentClick();
            }
        }
    }
}
