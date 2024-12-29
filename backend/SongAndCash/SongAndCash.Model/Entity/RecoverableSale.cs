namespace SongAndCash.Model.Entity;

public class RecoverableSale
{
    public int Id { get; set; }
    
    public User User { get; set; }
    public int UserId { get; set; }
}