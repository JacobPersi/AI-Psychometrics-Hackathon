using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using ZenBytesWorkflow.Diagram;

namespace ZenBytesWorkflow.Model;

public class SystemNode : BaseWorkflowNode
{
	private SystemComponentType _nodeType;

	public SystemNode() : base() { }

	public SystemNode(Point position, SystemComponentType type)
		: base(position)
	{
		_nodeType = type;
	}

	public override NodeModel CreateNodeView(BlazorDiagram diagram)
	{
		return diagram.Nodes.Add(new SystemNodeViewModel(Position)
		{
			Type = _nodeType
		});
	}
}
