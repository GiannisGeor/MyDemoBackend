namespace Models.Entities
{
    public class Pelatis : EntityBase
    {
        /// <summary>
        /// To onoma tou Pelati
        /// </summary>
        public string Onoma { get; set; }

        /// <summary>
        /// To tilefono tou pelati
        /// </summary>
        public string Tilefono { get; set; }

        /// <summary>
        /// Enonei to table tou Pelati me to Enoikiasi
        /// </summary>
        public List<Enoikiasi> Enoikiasis { get; set; } = new List<Enoikiasi>();
    }
}
