namespace Movie.Domain.Entities;

public class MovieItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Genre { get; set; }
}