using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Models;
using TestMaker.Models.ViewModels;

namespace DDDTest.Tests.ViewModelData
{
    internal static class HomeIndexViewModelTestData
    {
        internal static HomeIndexViewModel HomeIndexViewModelData()
        {
            return new HomeIndexViewModel
            {
                Tests = new List<Test>()
                {
                    new Test
                    {
                        Title = "TestTitle",
                        Number = 2,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
                        UserId = 1,
                        Questions = new List<Question>()
                        {
                            new Question
                            {
                                QuestionText = "TestQuestion1",
                                TestId = 1,
                                Choices = new List<Choice>()
                                {
                                    new Choice
                                    {
                                        ChoiceText = "testChoice1",
                                        IsAnswer = true,
                                        IsUsersAnswerCheck = true,
                                        IsUsersAnswerRadio = 0,
                                        QuestionId = 1
                                    },
                                    new Choice
                                    {
                                        ChoiceText = "testChoice2",
                                        IsAnswer = false,
                                        IsUsersAnswerCheck = true,
                                        IsUsersAnswerRadio = 0,
                                        QuestionId = 1
                                    },
                                    new Choice
                                    {
                                        ChoiceText = "testChoice3",
                                        IsAnswer = true,
                                        IsUsersAnswerCheck = false,
                                        IsUsersAnswerRadio = 0,
                                        QuestionId = 1
                                    },
                                    new Choice
                                    {
                                        ChoiceText = "testChoice1",
                                        IsAnswer = false,
                                        IsUsersAnswerCheck = false,
                                        IsUsersAnswerRadio = 0,
                                        QuestionId = 1
                                    },
                                }
                            }

                        }
                    }
                },
                Users = new List<User>()
                {
                    new User
                    {
                        LoginId = "test@gmail.com",
                        UserName = "testUser",
                        Password = "password",
                        Salt = "testSalt",
                        SelfIntroduction = "test",
                        Icon = new byte[]{ 0x01 }
                    },
                    new User
                    {
                        LoginId = "test2@gmail.com",
                        UserName = "testUser2",
                        Password = "password2",
                        Salt = "testSalt2",
                        SelfIntroduction = "test2",
                        Icon = new byte[]{ 0x02 }
                    }
                }
            };
        }
    }
}
