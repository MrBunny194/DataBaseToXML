namespace Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public int YearId { get; set; }
        public virtual Year Year { get; set; }
    }
}
