using System.Collections.Generic;
using LoginExample.Models;
using Models;

namespace LoginExample.Data
{
    public interface IAdultService
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task<IList<User>> GetUsersAsync();
        Task ValidateUserAsync(string userName, string password);
        Task AddNewUserAsync(User user);
        Task AddPersonAsync(Adult adult);
        Task RemovePersonAsync(int adultId);
        Task<Adult> GetAsync(int id);
    }
}