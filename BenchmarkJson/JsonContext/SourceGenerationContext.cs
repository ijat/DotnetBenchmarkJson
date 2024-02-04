using System.Text.Json.Serialization;
using BenchmarkJson.Views;

namespace BenchmarkJson;

[JsonSerializable(typeof(UpdateView))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}