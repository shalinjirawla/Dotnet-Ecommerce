using ECommerce.Models.Interfaces;
using ECommerce.Models.Models;

namespace ECommerce.Models.Repository
{
    public class UserRepository:IUserRepository
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructors
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUserbyId(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.Add(user);
            if (_context.SaveChanges() > 0)
                return user.Id;
            else
                return 0;
        }
        #endregion
    }
}
