using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace ZenBytesWorkflow.Model;

public abstract class BaseWorkflowNode
{
	private static int _idCounter = 1;
	public int Id { get; }
	public Point Position { get; set; }

	public BaseWorkflowNode() { }

	protected BaseWorkflowNode(Point position)
	{
		Id = _idCounter++;
		Position = position;
	}

	public abstract NodeModel CreateNodeView(BlazorDiagram diagram);

    public abstract Task<ScoreCard?> ExecuteAsync(List<TextInput> textInput);

}