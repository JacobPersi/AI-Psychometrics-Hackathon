namespace ZenBytesWorkflow.Model;

public class TextInput
{
	public DateTime? DateTime { get; set; }
	public string DisplayName { get; set; }
	public string TextBody { get; set; }
	public bool Expanded { get; set; } = false;
}