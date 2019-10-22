using System;
using System.Web;

namespace STA.Core.ScheduledEvents
{
	/// <summary>
	/// Interface for defining an event.
	/// </summary>
	public interface IEvent
	{
		void Execute(object state);
	}
}
