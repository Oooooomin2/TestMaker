using DDD.Domain.Models;

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

        internal static User UserCreateData()
        {
            return new User
            {
                LoginId = "test2@testmail.com",
                UserName = "testuser2",
                Password = "123456",
                ConfirmPassword = "123456",
                Salt = "",
                SelfIntroduction = "this is a self introduction2.",
                Icon = new byte[] { 0x01 }
            };
        }
    }
}
