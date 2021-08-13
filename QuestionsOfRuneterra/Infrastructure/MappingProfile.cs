namespace QuestionsOfRuneterra.Infrastructure
{
    using AutoMapper;
    using QuestionsOfRuneterra.Data.Models;
    using QuestionsOfRuneterra.Models.Answers;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Answer, AnswerServiceModel>()
               .ReverseMap();
        }
    }
}
