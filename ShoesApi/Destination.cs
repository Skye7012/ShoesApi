using System;
using System.Collections.Generic;

namespace ShoesApi
{
    /// <summary>
    /// Назначение обуви
    /// </summary>
    public partial class Destination
    {
        public Destination()
        {
            Shoes = new HashSet<Shoe>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Shoe> Shoes { get; set; }
    }
}
