namespace Divorcify.Models
{
	public class Party : IRavenModel
	{
		public string Id { get; set; }
		public string User1Id { get; set; }
		public User User1 { get; set; }
		public string User2Id { get; set; }
		public User User2 { get; set; }
	}
}