﻿using AutoMapper;
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
                cfg.CreateMap<AspNetUser, UserResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter();
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
