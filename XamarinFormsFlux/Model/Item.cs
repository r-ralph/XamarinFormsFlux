using SQLite;

namespace XamarinFormsFlux.Model
{
	[Table("Items")]
	public class Item
	{
		[PrimaryKey, Column("_id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("user_name")]
		public string UserName { get; set; }

		[Column("url")]
		public string Url { get; set; }

		public override string ToString()
		{
			return string.Format("{0}/{1}", UserName, Name);
		}
	}
}
