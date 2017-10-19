using System.Collections.Generic;

namespace Data.Models
{
    public class Year
    {
        public int Id { get; set; }
        public int Date { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
