﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Models;
using TestMaker.Models.ViewModels;

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
    }
}