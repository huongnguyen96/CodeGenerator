using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common
{
    public class FilterEntity
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public OrderType OrderType { get; set; }

        public FilterEntity()
        {
            Skip = 0;
            Take = 10;
            OrderType = OrderType.ASC;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderType
    {
        ASC,
        DESC
    }
}
