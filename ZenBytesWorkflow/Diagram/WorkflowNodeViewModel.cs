using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Geometry;

namespace ZenBytesWorkflow.Diagram;

public class WorkflowNodeViewModel : NodeModel
{
	public string NodeType { get; set; }
	public string InstructionText { get; set; }
	public string ExampleText { get; set; }

	public WorkflowNodeViewModel(Point? position = null) : base(position)
	{
	}

	public WorkflowNodeViewModel(string id, Point? position = null) : base(id, position)
	{
	}
}
