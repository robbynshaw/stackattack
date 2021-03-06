﻿using stackattack.Users;

namespace stackattack.Models
{
    public class UserModel
    {
        private IUser user;

        public long ID { get { return this.user == null ? int.MinValue : this.user.ID; } }
        public long TotalScore { get { return this.user == null ? 0 : this.user.TotalScore; } }

        public UserModel(IUser user)
        {
            this.user = user;
        }
    }
}