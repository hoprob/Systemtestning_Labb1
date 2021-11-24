using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    abstract class User
    {
        private string _fullName;
        private string _userId;
        private string _passWord;

        public string FullName { get => _fullName; }

        public User(string UserId, string FullName, string Password)
        {
            _userId = UserId;
            _fullName = FullName;
            _passWord = Password;
        }

        public static bool CheckUserName(List<User> users, string inputId)
        {
            return users.Exists(u => u._userId == inputId);
        }
        public static User CheckPassWord(List<User> users, string inputId, string inputPassWord)
        {
            var user = users.Find(u => u._userId == inputId);
            if (user._passWord == inputPassWord)
                return user;
            else
                return null;
        }

    }
}