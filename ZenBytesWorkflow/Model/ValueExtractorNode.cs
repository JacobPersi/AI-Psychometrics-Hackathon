using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using ZenBytesWorkflow.Diagram;

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
			NodeType = "Value Extractor",
			Title = Title,
			Role = Role,
			Task = Task,
			Context = Context,
			Examples = Examples
		});
	}

    public override async Task ExecuteAsync(TextInput textInput)
    {
    }
}
