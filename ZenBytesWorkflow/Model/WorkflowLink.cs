using System.Text.Json.Serialization;

namespace ZenBytesWorkflow.Model;

public class WorkflowLink
{
	public int SourceId { get; set; }
	public int TargetId { get; set; }

	[JsonConstructor]
	public WorkflowLink(int sourceId, int targetId)
	{
		SourceId = sourceId;
		TargetId = targetId;
	}

	public WorkflowLink(BaseWorkflowNode source, BaseWorkflowNode target)
	{
		SourceId = source.Id;
		TargetId = target.Id;
	}
}
