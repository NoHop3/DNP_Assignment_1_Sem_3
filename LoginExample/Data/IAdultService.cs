using System.Collections.Generic;
using LoginExample.Models;
using Models;

namespace LoginExample.Data
{
    public interface IAdultService
    {
        IList<Adult> GetAdults();
        IList<User> GetUsers();
        public User ValidateUser(string userName, string password);
        public void AddNewUser(User user);
        void AddPerson(Adult adult);
        void RemovePerson(int adultId);
        Adult Get(int id);
    }
}