using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerProject.Controllers.Resources;
using TestMakerProject.Models;

namespace TestMakerProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Test, TestResource>();
            CreateMap<User, UserResource>();
            CreateMap<Question, QuestionResource>();
            CreateMap<Choice, ChoiceResource>();

            CreateMap<TestResource, Test>()
              .ForMember(o => o.TestId, p => p.MapFrom(q => q.TestId))
              .ForMember(o => o.Title, p => p.MapFrom(q => q.Title))
              .ForMember(o => o.Number, p => p.MapFrom(q => q.Number))
              .ForMember(o => o.CreatedTime, p => p.MapFrom(q => q.CreatedTime))
              .ForMember(o => o.UpdatedTime, p => p.MapFrom(q => q.UpdatedTime))
              .ForMember(o => o.UserId, p => p.MapFrom(q => q.UserId))
              .ForMember(o => o.Questions, p => p.MapFrom(q => q.Questions));

            CreateMap<QuestionResource, Question>()
              .ForMember(o => o.QuestionId, p => p.MapFrom(q => q.QuestionId))
              .ForMember(o => o.QuestionText, p => p.MapFrom(q => q.QuestionText))
              .ForMember(o => o.TestId, p => p.MapFrom(q => q.TestId))
              .ForMember(o => o.Choices, p => p.MapFrom(q => q.Choices));

            CreateMap<ChoiceResource, Choice>()
              .ForMember(o => o.ChoiceId, p => p.MapFrom(q => q.ChoiceId))
              .ForMember(o => o.ChoiceText, p => p.MapFrom(q => q.ChoiceText))
              .ForMember(o => o.IsAnswer, p => p.MapFrom(q => q.IsAnswer))
              .ForMember(o => o.IsUsersAnswerCheck, p => p.MapFrom(q => q.IsUsersAnswerCheck))
              .ForMember(o => o.IsUsersAnswerRadio, p => p.MapFrom(q => q.IsUsersAnswerRadio));
    }
    }
}
