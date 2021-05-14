using DDD.Domain.Models;
using System;
using System.Collections.Generic;

namespace DDDTest.Tests.Data
{
    internal static class TestModelData
    {
        internal static List<Test> TestData()
        {
            return new List<Test>()
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

                    },
                    User = new User
                    {
                        LoginId = "test@gmail.com",
                        UserName = "testUser",
                        Password = "password",
                        Salt = "testSalt",
                        SelfIntroduction = "test",
                        Icon = new byte[] { 0x01 }
                    }
                }
            };
        }

        internal static Test TestDataForScoreMultiCorrectCheck()
        {
            return new Test
            {
                Number = 1,
                UserId = 1,
                Questions = new List<Question>()
                {
                    new Question
                    {
                        TestId = 1,
                        Choices = new List<Choice>()
                        {
                            new Choice
                            {
                                IsAnswer = true,
                                IsUsersAnswerCheck = true,
                                QuestionId = 1
                            },
                            new Choice
                            {
                                IsAnswer = false,
                                IsUsersAnswerCheck = false,
                                QuestionId = 1
                            },
                            new Choice
                            {
                                IsAnswer = true,
                                IsUsersAnswerCheck = true,
                                QuestionId = 1
                            },
                            new Choice
                            {
                                IsAnswer = false,
                                IsUsersAnswerCheck = false,
                                QuestionId = 1
                            },
                        }
                    }

                }
            };
        }

        internal static Test TestDataForScoreSingleCorrectCheck()
        {
            return new Test
            {
                Number = 1,
                UserId = 1,
                Questions = new List<Question>()
                {
                    new Question
                    {
                        TestId = 1,
                        Choices = new List<Choice>()
                        {
                            new Choice
                            {
                                IsAnswer = true,
                                IsUsersAnswerRadio = 1,
                                QuestionId = 1
                            }
                        }
                    }

                }
            };
        }

        internal static Test TestDataForScoreSingleFailCheck()
        {
            return new Test
            {
                Number = 1,
                UserId = 1,
                Questions = new List<Question>()
                {
                    new Question
                    {
                        TestId = 1,
                        Choices = new List<Choice>()
                        {
                            new Choice
                            {
                                IsAnswer = true,
                                IsUsersAnswerRadio = 2,
                                QuestionId = 1
                            }
                        }
                    }

                }
            };
        }
    }
}
