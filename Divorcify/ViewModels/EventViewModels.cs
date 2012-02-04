using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Divorcify.Models;

namespace Divorcify.ViewModels
{
	public class EventCreateViewModel
	{
		[Required]
		public string UserId { get; set; }
		[Required]
		public string Title { get; set; }
		public string Location { get; set; }
		[Required]
		public DateTime? Start { get; set; }
		[Required]
		public DateTime? End { get; set; }
		public string Note { get; set; }

		public List<Event> Events { get; set; }
	}

	public class Header
	{
		public string Text { get; set; }
		public DateTime Date { get; set; }
		public List<Event> Events { get; set; }
	}

	public class EventListViewModel
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public List<Header> Headers { get; set; }
	}
}