using AppForTests.Shared.Models;
using AppForTests.Shared.Models.DbModels;
using AppForTests.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Domain
{
    public static class AuthManager
    {
        private static AuthedUserModel _currentUser;

        public static AuthedUserModel CurrentUser { get => _currentUser; }

        public static async Task LogInAsync(string login, string password)
        {
            using TestsDBContext dao = new();

            var user = await dao.Users
                .FirstOrDefaultAsync(u => u.Login == login && u.Password == password);

            if (user == null)
                throw new Exception("Неверный логин или пароль");

            _currentUser = new AuthedUserModel(user.Id, user.Login, user.Password, user.FullName, user.IsAdmin);
        }

        public static async Task RegisterAsync(UserRegisterModel userInfo)
        {
            if (
                userInfo.Login.IsNullOrEmpty()
                ||
                userInfo.FIO.IsNullOrEmpty()
                ||
                userInfo.Password.IsNullOrEmpty()
                )
                throw new Exception("Не все поля заполнены!");

            if (
                userInfo.Password.Length < 6
                ||
                !userInfo.Password.Any(char.IsDigit)
                ||
                !userInfo.Password.Any(char.IsLetter)
                ||
                !userInfo.Password.Any(char.IsUpper)
                ||
                !userInfo.Password.Any(char.IsLower)
                )
                throw new Exception("Неверный формат пароля! Пароль должен быть не менее 6 символов, содеражть цифры, строчные и заглавные буквы.");

            if (userInfo.Password != userInfo.ConfirmPassword)
                throw new Exception("Пароли не совпадают!");



            using TestsDBContext dao = new();

            var user = new User
            {
                Login = userInfo.Login,
                Password = userInfo.Password,
                FullName = userInfo.FIO,
                IsAdmin = false
            };

            try
            {
                await dao.Users.AddAsync(user);
                await dao.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                throw new Exception("Пользователь с таким логином уже существует!");
            }
        }
    }
}
