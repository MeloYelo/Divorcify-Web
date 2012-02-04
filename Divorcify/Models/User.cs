using System;

namespace Divorcify.Models
{
	public class User:IRavenModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}