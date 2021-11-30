﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    abstract class User
    {
        protected string _userId;
        protected string _fullName;
        protected string _password;

        public string FullName { get => _fullName; }
        public string UserID { get => _userId; }

        public User(string UserId, string FullName, string Password)
        {
            _userId = UserId;
            _fullName = FullName;
            _password = Password;
        }
    
        public static bool CheckUserName(List<User> users, string inputId)
        {
            return users.Exists(u => u._userId == inputId);
        }
        public static User CheckPassword(List<User> users, string inputId, string inputPassword)
        {
            var user = users.Find(u => u._userId == inputId);
            if (user._password == inputPassword)
                return user;
            else
                return null;
        }
        public static bool CheckPassword(Customer customer, string inputPassword)
            => (customer._password == inputPassword);
        public string ToSave()
        {
            string details = String.Empty;
            if (this is Admin)
            {
                details = $"{_userId}________{_fullName}________{_password}" +
                    $"________Admin\n";
            }
            else if (this is Customer)
            {
                details = $"{_userId}________{_fullName}________{_password}" +
                    $"________Customer\n";
            }
            return details;
        }
    }
}