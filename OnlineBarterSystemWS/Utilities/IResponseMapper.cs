using OnlineBarterSystemWS.Generic.Models.Response;

namespace OnlineBarterSystemWS.Utilities
{
    public interface IResponseMapper
    {
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : AEntityResponse;

        List<TDestination> Map<TSource, TDestination>(List<TSource> source) where TDestination : AEntityResponse;
    }
}
