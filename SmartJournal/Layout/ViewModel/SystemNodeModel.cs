using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace SmartJournal.Layout.ViewModel;

public class SystemNodeModel : NodeModel
{
	public SystemNodeModel(Point? position = null) : base(position)
	{
	}

	public SystemNodeModel(string id, Point? position = null) : base(id, position)
	{
	}
}