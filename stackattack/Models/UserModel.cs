using stackattack.Users;

namespace stackattack.Models
{
    public class UserModel
    {
        private IUser user;

        public int ID { get { return this.user == null ? int.MinValue : this.user.ID; } }

        public UserModel(IUser user)
        {
            this.user = user;
        }
    }
}