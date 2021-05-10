using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models;
using TestMaker.Models.ViewModels;

namespace DDDTest.Tests.ViewModelData
{
    internal static class TestViewModelTestData
    {
        internal static TestViewModel TestViewModelData()
        {
            return new TestViewModel
            {
                Tests = new Test
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
                },
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
            };
        }

        internal static TestViewModel TestViewModelUpdateData()
        {
            return new TestViewModel
            {
                Tests = new Test
                {
                    TestId = 2,
                    Title = "TestTitle_Update",
                    Number = 2,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    UserId = 1,
                    Questions = new List<Question>()
                    {
                        new Question
                        {
                            QuestionText = "TestQuestion1_Update",
                            TestId = 1,
                            Choices = new List<Choice>()
                            {
                                new Choice
                                {
                                    ChoiceText = "testChoice1_Update",
                                    IsAnswer = false,
                                    IsUsersAnswerCheck = true,
                                    IsUsersAnswerRadio = 0,
                                    QuestionId = 1
                                },
                                new Choice
                                {
                                    ChoiceText = "testChoice2",
                                    IsAnswer = true,
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
            };
        }
    }
}
