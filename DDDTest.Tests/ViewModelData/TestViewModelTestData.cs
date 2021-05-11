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

        internal static TestViewModel TestViewModelDataForScoreTestMultiCorrectCheck()
        {
            return new TestViewModel
            {
                Tests = new Test
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
                }
            };
        }

        internal static TestViewModel TestViewModelDataForScoreTestSingleCorrectCheck()
        {
            return new TestViewModel
            {
                Tests = new Test
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
                }
            };
        }

        internal static TestViewModel TestViewModelDataForScoreTestSingleFailCheck()
        {
            return new TestViewModel
            {
                Tests = new Test
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
                }
            };
        }
    }
}
