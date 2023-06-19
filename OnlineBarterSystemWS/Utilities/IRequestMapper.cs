using OnlineBarterSystemWS.Generic.Models.Request;

namespace OnlineBarterSystemWS.Utilities
{
    public interface IRequestMapper
    {
        TDestination Map<TSource, TDestination>(TSource source) where TSource : AEntityRequest;
        List<TDestination> Map<TSource, TDestination>(List<TSource> source) where TSource : AEntityRequest;
    }
}
