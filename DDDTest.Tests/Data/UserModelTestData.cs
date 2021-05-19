using DDD.Domain.Models;
using DDD.Domain.ViewModels.Users;

namespace DDDTest.Tests.ViewModelData
{
    internal static class UserModelTestData
    {
        internal static User UserModelData()
        {
            return new User
            {
                LoginId = "test@testmail.com",
                UserName = "testuser",
                Password = "12345",
                ConfirmPassword = "12345",
                Salt = "",
                SelfIntroduction = "this is a self introduction.",
                Icon = new byte[] { 0x01 }
            };
        }

        internal static UserCreateViewModel UserCreateData()
        {
            return new UserCreateViewModel
            {
                LoginId = "test2@testmail.com",
                UserName = "testuser2",
                Password = "123456",
                ConfirmPassword = "123456",
                Salt = ""
            };
        }
    }
}
