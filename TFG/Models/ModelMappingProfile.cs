using InstagramApiSharp.Classes.Models;

namespace TFG.Models
{
    public class ModelMappingProfile : AutoMapper.Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<InstaMedia, InstagramLog>()
                .ForMember(dest => dest.MediaId, opt => opt.MapFrom(sour => sour.InstaIdentifier))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(sour => sour.Images[0].Uri))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(sour => DateTime.Now))
                .ForMember(dest => dest.DateTakenAt, opt => opt.MapFrom(sour => sour.TakenAt));
            //.ForMember(dest => dest.ImageData, opt => opt.MapFrom(sour => File.ReadAllBytes(sour.Images[0].Uri)));

            CreateMap<InstaCarouselItem, InstagramLog>()
                .ForMember(dest => dest.MediaId, opt => opt.MapFrom(sour => sour.InstaIdentifier))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(sour => DateTime.Now))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(sour => sour.Images[0].Uri));

            CreateMap<InstaStoryItem, InstagramLog>()
                .ForMember(dest => dest.DateTakenAt, opt => opt.MapFrom(sour => sour.TakenAt))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(sour => DateTime.Now))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(sour => sour.ImageList[0].Uri));

        }
    }
}
