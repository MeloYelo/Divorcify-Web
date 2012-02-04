using System;

namespace Divorcify.Models
{
	public class Event : IRavenModel
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Location { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Note { get; set; }
		public string CreateUserId { get; set; }
		public DateTime CreateDateTime { get; set; }
		public string ConfirmUserId { get; set; }
		public DateTime ConfirmDateTime { get; set; }
		public bool IsActive { get; set; }
	}

}