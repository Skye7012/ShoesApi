using System;
using System.Collections.Generic;

namespace ShoesApi
{
    /// <summary>
    /// Обувь
    /// </summary>
    public partial class Shoe
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? BrandId { get; set; }
        public int? DestinationId { get; set; }
        public int? SeasonId { get; set; }
        /// <summary>
        /// Название изображения (для пути)
        /// </summary>
        public string Image { get; set; } = null!;
        /// <summary>
        /// Цена
        /// </summary>
        public int? Price { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Destination? Destination { get; set; }
        public virtual Season? Season { get; set; }
    }
}
