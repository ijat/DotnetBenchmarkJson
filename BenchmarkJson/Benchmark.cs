using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkJson.Views;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BenchmarkJson;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[Orderer(SummaryOrderPolicy.SlowestToFastest)]
[CategoriesColumn]
[Config(typeof(BenchmarkConfig))]
public class Benchmark
{
    private const string RawJsonBody =
        "{\"update_id\":12345,\"message\":{\"message_id\":16,\"from\":{\"id\":12344445,\"is_bot\":true,\"first_name\":\"Group\",\"username\":\"GroupAnonymousBot\"},\"author_signature\":\"MyTeam\",\"sender_chat\":{\"id\":-1123123123,\"title\":\"Group Anonymous Admin\",\"username\":\"tjerkjtklerjtkejtljerlktjgfd\",\"type\":\"supergroup\"},\"chat\":{\"id\":-1123123123,\"title\":\"Group Anonymous Admin\",\"username\":\"tjerkjtklerjtkejtljerlktjgfd\",\"type\":\"supergroup\"},\"date\":1704103319,\"text\":\"test test\",\"entities\":[{\"offset\":0,\"length\":16,\"type\":\"mention\"}]}}";
    private static readonly UpdateView UpdateView = JsonSerializer.Deserialize<UpdateView>(RawJsonBody)!;
    
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        TypeInfoResolver = SourceGenerationContext.Default
    };

    #region Deserialize
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("deserialize")]
    public void NewtonsoftJson_Deserialize_Default()
    {
        JsonConvert.DeserializeObject<UpdateView>(RawJsonBody);
    }

    [Benchmark]
    [BenchmarkCategory("deserialize")]
    public void SystemJson_Deserialize_Default()
    {
        JsonSerializer.Deserialize<UpdateView>(RawJsonBody);
    }

    [Benchmark]
    [BenchmarkCategory("deserialize")]
    public void SystemJson_Deserialize_SourceGenerated()
    {
        JsonSerializer.Deserialize<UpdateView>(RawJsonBody, JsonSerializerOptions);
    }
    #endregion

    #region Serialize
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("serialize")]
    public void NewtonsoftJson_Serialize_Default()
    {
        JsonConvert.SerializeObject(UpdateView);
    }

    [Benchmark]
    [BenchmarkCategory("serialize")]
    public void SystemJson_Serialize_Default()
    {
        JsonSerializer.Serialize(UpdateView);
    }

    [Benchmark]
    [BenchmarkCategory("serialize")]
    public void SystemJson_Serialize_SourceGenerated()
    {
        JsonSerializer.Serialize(UpdateView, JsonSerializerOptions);
    }
    #endregion
}