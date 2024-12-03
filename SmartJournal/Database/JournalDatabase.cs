using Blazor.IndexedDB;
using Microsoft.JSInterop;
using SmartJournal.Entities;

namespace SmartJournal.Database;

public class JournalDatabase : IndexedDb
{
	public JournalDatabase(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version) { }

	public IndexedSet<Workflow> Workflows { get; set; }
}
