using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using RestSharp;
using ZenBytesWorkflow.Diagram;

namespace ZenBytesWorkflow.Model;
public class DecisionNode : BaseWorkflowNode
{
	public string Title { get; set; }
	public string InstructionText { get; set; }
	public string ExampleText { get; set; }

	public DecisionNode() : base() { }

	public DecisionNode(Point position, string title, string instructionText, string exampleText)
		: base(position)
	{
		Title = title;
		InstructionText = instructionText;
		ExampleText = exampleText;
	}

	public override NodeModel CreateNodeView(BlazorDiagram diagram)
	{
		return diagram.Nodes.Add(new WorkflowNodeViewModel(Position)
		{
			NodeType = "True/False Decision",
			Title = Title,
			InstructionText = InstructionText,
			ExampleText = ExampleText
		});
	}
	public async Task<string> GenerateContentFromGemini(string apiKey, string prompt)
	{
		try
		{
			var client = new RestClient("https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent");

			var request = new RestRequest()
				.AddQueryParameter("key", apiKey)
				.AddHeader("Content-Type", "application/json")
				.AddJsonBody(new
				{
					contents = new[]
					{
						new
						{
							parts = new[]
							{
								new { text = prompt }
							}
						}
					},
					generationConfig = new { response_mime_type = "application/json" }
				});

			var response = await client.PostAsync(request);

			if (response.IsSuccessful && response.Content != null)
			{
				return response.Content;
			}
			else
			{
				throw new Exception($"API call failed: {response.StatusCode} - {response.ErrorMessage}");
			}
		}
		catch (Exception ex)
		{
			// Handle or log exceptions as necessary
			Console.WriteLine($"Error: {ex.Message}");
			throw;
		}
	}
}
