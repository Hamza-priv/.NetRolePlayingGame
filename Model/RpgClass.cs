using System.Text.Json.Serialization;

namespace Model
{
    // To show names in api instead of numbers

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Cleric = 2,
        Wizard = 3,
        Healer = 4
    }
}