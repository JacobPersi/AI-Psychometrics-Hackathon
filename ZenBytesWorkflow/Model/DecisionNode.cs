using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using RestSharp;
using System.Text.Json;
using ZenBytesWorkflow.Diagram;
using ZenBytesWorkflow.Util;

namespace ZenBytesWorkflow.Model;
public class DecisionNode : BaseWorkflowNode
{
	public string Title { get; set; }
	public string Role { get; set; }
	public string Task { get; set; }
	public string Context { get; set; }
	public string Examples { get; set; }

	public DecisionNode() : base() { }

	public DecisionNode(Point position, string title, string role, string task, string context, string examples)
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
			NodeType = "True/False Decision",
			Title = Title,
			Role = Role,
			Task = Task,
			Context = Context,
			Examples = Examples
		});
	}

	public override async Task ExecuteAsync(TextInput textInput)
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
								new { text = Role },
								new { text = Task },
								new { text = Context },
								new { text = textInput.TextBody }
							}
						}
					},
					generationConfig = new
					{
						response_mime_type = "application/json",
						response_schema = new
						{
							type = "BOOLEAN",
						}
					}
				});

			var response = await client.PostAsync(request);

			if (response.IsSuccessful && response.Content != null)
			{
				/*
				JsonSerializer.Deserialize<TextInput>(content);

				dynamic jsonObject = JsonConvert.DeserializeObject(response.Content);

				bool hasSleepRef = jsonObject?.Count > 0; // Check if any results exist

				if (hasSleepRef)
				{
					// Text references sleep
					// Perform actions for "True" branch
				}
				else
				{
					// Text doesn't reference sleep
					// Perform actions for "False" branch
				}
				*/
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
