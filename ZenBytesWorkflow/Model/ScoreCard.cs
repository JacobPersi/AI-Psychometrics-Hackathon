using System.Text.Json.Serialization;

namespace ZenBytesWorkflow.Model;

public class ScoreCard
{
	[JsonPropertyName("input1")]
	public int Input1 { get; set; }

	[JsonPropertyName("input2")]
	public int Input2 { get; set; }

	[JsonPropertyName("input3")]
	public int Input3 { get; set; }

	[JsonPropertyName("input4")]
	public int Input4 { get; set; }

	[JsonPropertyName("input5")]
	public int Input5 { get; set; }
}
