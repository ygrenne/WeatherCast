using System.Net.Http;
using Weather.Enums;

namespace Weather.Services
{
    public interface IDailyQueryBuilder
    {
        IDailyQueryBuilder City(string city);

        IDailyQueryBuilder Units(UnitsEnum unit);

        IDailyQueryBuilder Lang(LanguageEnum lang);

        IDailyQueryBuilder ForDays(int amount);

        HttpRequestMessage Build();
    }
}