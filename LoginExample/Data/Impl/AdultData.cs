using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LoginExample.Models;
using Models;

namespace LoginExample.Data.Impl
{
    public class AdultData : IAdultService, IUserService
    {
        public IList<Adult> Adults { get; private set; }
        public IList<User> Users { get; private set; }

        private readonly string adultsFile = "adults.json";
        private readonly string usersFile = "users.json";

        public FileContext()
        {
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
            Users = File.Exists(usersFile) ? ReadData<User>(usersFile) : new List<User>();
        }

        private IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(s))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }

        public void SaveChanges()
        {

            // storing persons
            string jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(adultsFile, false))
            {
                outputFile.Write(jsonAdults);
            }
            string jsonUsers = JsonSerializer.Serialize(Users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(usersFile, false))
            {
                outputFile.Write(jsonUsers);
            }
        }

        public IList<Adult> GetAdults()
        {
            List<Adult> temp = new List<Adult>(Adults);
            return temp;
        }

        public IList<User> GetUsers()
        {
            List<User> temp = new List<User>(Users);
            return temp;
        }
        
        public User ValidateUser(string userName, string password)
        {
            User first = Users.FirstOrDefault(user => user.UserName.Equals(userName));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }

        public void AddNewUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

        public void AddPerson(Adult adult)
        {
            int max;
            try
            {
                max = Adults.Max(adult => adult.Id);
                adult.Id = (++max);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            Adults.Add(adult);
            SaveChanges();
        }

        public void RemovePerson(int adultId)
        {
            Adult adultToRemove = Adults.First(adult1 => adult1.Id == adultId);
            Adults.Remove(adultToRemove);
            SaveChanges();
        }

        public Adult Get(int id)
        {
            return Adults.FirstOrDefault(adult => adult.Id == id);
        }
    }
}