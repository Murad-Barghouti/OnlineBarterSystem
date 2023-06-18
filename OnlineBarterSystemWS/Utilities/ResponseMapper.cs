using AutoMapper;
using OnlineBarterSystemDAL.Models;
using OnlineBarterSystemWS.Generic.Models.Response;
using OnlineBarterSystemWS.Models.Response;

namespace OnlineBarterSystemWS.Utilities
{
    public class ResponseMapper : IResponseMapper
    {
        private readonly IMapper _mapper;

        public ResponseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<City, CityResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<AspNetUser, UserResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(response => response.City, entity => entity.MapFrom(model => model.City));
                cfg.CreateMap<BarterState, BarterStateResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<SubCategory, SubCategoryResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(response => response.Category, entity => entity.MapFrom(model => model.Category));
                cfg.CreateMap<Category, CategoryResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<Barter, BarterResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(response => response.GiveType, entity => entity.MapFrom(model => model.GiveType))
                .ForMember(response => response.ReceiveType, entity => entity.MapFrom(model => model.ReceiveType))
                .ForMember(response => response.GiveType, entity => entity.MapFrom(model => model.GiveType));
            });

            _mapper = config.CreateMapper();
        }
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : AEntityResponse
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public List<TDestination> Map<TSource, TDestination>(List<TSource> source) where TDestination : AEntityResponse
        {
            return _mapper.Map<List<TSource>, List<TDestination>>(source);

        }
    }
}
