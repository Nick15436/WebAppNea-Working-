using Supabase.Postgrest.Attributes; // Imports the tools to for the code to "talk" to the database
using Supabase.Postgrest.Models;

namespace WebAppNea;

// Shows that this class represents the profiles table in supabase
[Table("profiles")]
public class UserProfile : BaseModel
{
    // This is the Primary Key. 'false' means that Supabase handles its generation.
    [PrimaryKey("id", false)] 
    public string Id { get; set; }

    // These are all of the attributes/fields/columns of the database.
    [Column("username")]
    public string Username { get; set; }

    [Column("full_name")]
    public string FullName { get; set; }

    [Column("favorite_tickers")]
    public List<string> FavoriteTickers { get; set; } = new();

    [Column("default_ticker")]
    public string DefaultTicker { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}