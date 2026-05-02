using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReportReasons
{
    FallBack = 0,
    [Description("Спам")] Spam,
    [Description("Непристоний контент")] InappropriateContent,
    [Description("Мова ворожнечі")] HateSpeech,
    [Description("Цькування або булінг")] Violence,
    [Description("Дезінформація")] MissInformation,
    [Description("Інше")] Other
}