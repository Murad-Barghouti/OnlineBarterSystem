using AutoMapper;
using OnlineBarterSystemDAL.HelperModels;
using OnlineBarterSystemWS.Generic.Models.Request;
using OnlineBarterSystemWS.Models.Request;

namespace OnlineBarterSystemWS.Utilities
{
    public class RequestMapper : IRequestMapper
    {
        private readonly IMapper _mapper;

        public RequestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SignUpModel, HSignUpModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<SignInModel, HSignInModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            });
            _mapper = config.CreateMapper();
        }
        public TDestination Map<TSource, TDestination>(TSource source) where TSource : AEntityRequest
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public List<TDestination> Map<TSource, TDestination>(List<TSource> source) where TSource : AEntityRequest
        {
            return _mapper.Map<List<TSource>, List<TDestination>>(source);

        }
    }
}
