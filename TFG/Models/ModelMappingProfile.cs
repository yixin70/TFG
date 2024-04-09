using InstagramApiSharp.Classes.Models;

namespace TFG.Models
{
    public class ModelMappingProfile : AutoMapper.Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<InstaMedia, InstagramMedia>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(sour => sour.InstaIdentifier))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(sour => sour.Images[0].Uri))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(sour => sour.TakenAt));
            //.ForMember(dest => dest.ImageData, opt => opt.MapFrom(sour => File.ReadAllBytes(sour.Images[0].Uri)));

            CreateMap<InstaCarouselItem, InstagramMedia>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(sour => sour.InstaIdentifier))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(sour => sour.Images[0].Uri));

            CreateMap<InstaStoryItem, InstagramStory>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(sour => sour.TakenAt))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(sour => sour.ImageList[0].Uri));

        }
    }
}
