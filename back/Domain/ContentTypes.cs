using System.Text.Json.Serialization;

namespace Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContentTypes
{
    FallBack = 0,
    Video,
    Comment,
    User
}