namespace Weather.Services
{
    public interface IOpenWeatherQueryBuilder
    {
        IDailyQueryBuilder Daily();
    }
}