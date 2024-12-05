using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using ZenBytesWorkflow.Diagram;

namespace ZenBytesWorkflow.Model;

public class ValueExtractorNode : BaseWorkflowNode
{
	public string Title { get; set; }

	public ValueExtractorNode() : base() { }

	public ValueExtractorNode(Point position, string title)
		: base(position)
	{
		Title = title;
	}

	public override NodeModel CreateNodeView(BlazorDiagram diagram)
	{
		return diagram.Nodes.Add(new WorkflowNodeViewModel(Position)
		{
			NodeType = "Value Extractor",
			Title = Title
		});
	}

    public override async Task ExecuteAsync(TextInput textInput)
    {
    }
}
