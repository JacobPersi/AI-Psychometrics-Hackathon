using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Microsoft.AspNetCore.Components.Forms;
using RestSharp;
using System.Text.Json;
using ZenBytesWorkflow.Diagram;
using ZenBytesWorkflow.Util;
using static MudBlazor.Colors;

namespace ZenBytesWorkflow.Model;
public class ValueExtractorNode : BaseWorkflowNode
{
	public string Title { get; set; }
	public string Role { get; set; }
	public string Task { get; set; }
	public string Context { get; set; }
	public string Examples { get; set; }

	public ValueExtractorNode() : base() { }

	public ValueExtractorNode(Point position, string title, string role, string task, string context, string examples)
		: base(position)
	{
		Title = title;
		Role = role;
		Task = task;
		Context = context;
		Examples = examples;
	}

	public override NodeModel CreateNodeView(BlazorDiagram diagram)
	{
		return diagram.Nodes.Add(new WorkflowNodeViewModel(Position)
		{
			NodeType = "Ordinal Scoring (0-5)",
			Title = Title,
			Role = Role,
			Task = Task,
			Context = Context,
			Examples = Examples
		});
	}

	public override async Task<ScoreCard?> ExecuteAsync(List<TextInput> inputs)
	{
		try
		{
			var client = new RestClient("https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent");

			var request = new RestRequest()
				.AddQueryParameter("key", ApiKey.Value)
				.AddHeader("Content-Type", "application/json")
				.AddJsonBody(new
				{
					contents = new[]
					{
						new
						{
							parts = new[]
							{
								new { text = $"Role: {Role}" },
								new { text = $"Task: {Task}" },
								new { text = $"Context: {Context}" },
								new { text = $"Example: {Examples}" },
							}
							.Concat(inputs.Select((value, index) => new { text = $"Input {index + 1}: {value.TextBody}" }))
							.ToArray()
						}
					},
					generationConfig = new
					{
						response_mime_type = "application/json",
						response_schema = new
						{
							type = "object",
							description = "Schema defining a set of scores for 5 inputs on a scale from 0 to 5, where 0 is non-applicable, 1 is low, and 5 is high.",
							properties = new
							{
								input1 = new { type = "integer", format = "int32", description = "The score of Input 1", minimum = 0, maximum = 5 },
								input2 = new { type = "integer", format = "int32", description = "The score of Input 2", minimum = 0, maximum = 5 },
								input3 = new { type = "integer", format = "int32", description = "The score of Input 3", minimum = 0, maximum = 5 },
								input4 = new { type = "integer", format = "int32", description = "The score of Input 4", minimum = 0, maximum = 5 },
								input5 = new { type = "integer", format = "int32", description = "The score of Input 5", minimum = 0, maximum = 5 }
							},
							required = new[] { "input1", "input2", "input3", "input4", "input5" }
						}
					}
				});

			var requestBody = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value;

			var response = await client.PostAsync(request);

			if (response.IsSuccessful && response.Content != null)
			{

				using JsonDocument doc = JsonDocument.Parse(response.Content);
				JsonElement root = doc.RootElement;

				string jsonText = root.GetProperty("candidates")[0]
				   .GetProperty("content")
				   .GetProperty("parts")[0]
				   .GetProperty("text")
				   .GetString();

				var scores = JsonSerializer.Deserialize<ScoreCard>(jsonText);

				return scores;
			}
			else
			{
				throw new Exception($"API call failed: {response.StatusCode} - {response.ErrorMessage}");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
			throw;
		}
	}
}
