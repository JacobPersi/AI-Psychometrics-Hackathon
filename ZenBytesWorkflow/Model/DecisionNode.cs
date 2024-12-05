using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
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
}
