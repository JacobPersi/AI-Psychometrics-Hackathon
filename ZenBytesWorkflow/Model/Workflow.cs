using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Routers;
using System.Text.Json;

namespace ZenBytesWorkflow.Model;

public class Workflow
{
	public List<BaseWorkflowNode> Nodes { get; set; } = new List<BaseWorkflowNode>();
	public List<WorkflowLink> Links { get; set; } = new List<WorkflowLink>();

	public Workflow() { }

	public void Build(BlazorDiagram diagram)
	{
		var nodeMapping = new Dictionary<int, NodeModel>();

		foreach (var workflowNode in Nodes)
		{
			var node = workflowNode.CreateNodeView(diagram);
			nodeMapping[workflowNode.Id] = node;
		}

		foreach (var workflowLink in Links)
		{
			if (!nodeMapping.TryGetValue(workflowLink.SourceId, out var sourceNode) ||
				!nodeMapping.TryGetValue(workflowLink.TargetId, out var targetNode))
			{
				throw new InvalidOperationException("Invalid link reference.");
			}

			var link = diagram.Links.Add(new LinkModel(
				new ShapeIntersectionAnchor(sourceNode),
				new ShapeIntersectionAnchor(targetNode)
			));

			link.TargetMarker = LinkMarker.Arrow;
			link.Router = new OrthogonalRouter();
		}
	}

	public void ExportToJson(string filePath)
	{
		var options = new JsonSerializerOptions { WriteIndented = true };
		File.WriteAllText(filePath, JsonSerializer.Serialize(this, options));
	}

	public static Workflow LoadFromJson(string filePath)
	{
		var json = File.ReadAllText(filePath);
		return JsonSerializer.Deserialize<Workflow>(json);
	}
}
