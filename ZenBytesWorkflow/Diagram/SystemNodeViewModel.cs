using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace ZenBytesWorkflow.Diagram;

public class SystemNodeViewModel : NodeModel
{
	public SystemComponentType Type { get; set; }

	public SystemNodeViewModel(Point? position = null) : base(position)
	{
	}

	public SystemNodeViewModel(string id, Point? position = null) : base(id, position)
	{
	}
}

public enum SystemComponentType
{
	StartNode,
	EndNode
}