using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        protected static string NewPassword()
        {
            Regex passCheck = new Regex(@"(?=.*[0-9])(?=.*[A-ZÅÄÖ]).{8,}$");
            string inputPassword;
            string validatePassword;
            Bank.ReturnInstruction(0);
            do
            {
                Console.Write("\n\n\n\n  Ange ett lösenord: ");
                inputPassword = Bank.GetPassword();
                while (!passCheck.IsMatch(inputPassword) && inputPassword.ToUpper() != "R")
                {
                    Console.Clear();
                    Bank.ReturnInstruction(0);
                    Console.WriteLine("\n\n  Lösenordet måste vara minst 8 tecken," +
                        " innehålla minst 1 siffra och minst 1 stor bokstav.\n" +
                        "    Vänligen ange ett annat lösenord!");
                    Console.Write("\n  Ange ett lösenord:");
                    inputPassword = Bank.GetPassword();
                }
                if (inputPassword.ToUpper() == "R")
                    return inputPassword;
                Console.Write("\n\n   Vänligen vefifiera lösenordet: ");
                validatePassword = Bank.GetPassword();
                if (validatePassword.ToUpper() == "R")
                    return validatePassword;
                if (inputPassword != validatePassword)
                { 
                    Console.Clear();
                    Bank.ReturnInstruction(0);
                    Console.WriteLine("\n\n    Lösenorden matchar inte. Försök igen!");
                }
            } while (inputPassword != validatePassword);
            return inputPassword;
        }
        public bool ChangePassword()
        {
            string newPassword = NewPassword();
            if (newPassword.ToUpper() != "R")
            {
                _password = newPassword;
                return true;
            }
            else
                return false;
        }
    }
}