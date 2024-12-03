namespace SmartJournal.Entities;

public class Workflow
{
	[System.ComponentModel.DataAnnotations.Key]
	public int Id { get; set; }
	public string DisplayName { get; set; }
	public int StartingNodeId { get; set; }

}
