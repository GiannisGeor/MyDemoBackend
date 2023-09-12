namespace Models.Entities
{
    public class Sintelestis : EntityBase
    {
        /// <summary>
        /// Onoma tou Sintelesti
        /// </summary>
        public string Onoma { get; set; }

        /// <summary>
        /// Enonei to table tou Sintelesti me to Tn_sn
        /// </summary>
        public List<Tn_sn> TainiesSintelestes { get; set; } = new List<Tn_sn>();
    }
}
