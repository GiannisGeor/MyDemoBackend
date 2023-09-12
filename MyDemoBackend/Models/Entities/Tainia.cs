namespace Models.Entities
{
    public class Tainia : EntityBase
    {
        /// <summary>
        /// O Titlos ths tainias
        /// </summary>
        public string Titlos { get; set; }

        /// <summary>
        /// H Xronia pou bgike h Tainia
        /// </summary>
        public int Xronia { get; set; }

        /// <summary>
        /// Ennonei to table Tainia me to Kaseta
        /// </summary>
        public List<Kaseta> Kasetes { get; set; } = new List<Kaseta>();

        /// <summary>
        /// Ennonei to table Tainia me to Tn_sn
        /// </summary>
        public List<Tn_sn> TainiesSintelestes { get; set; } = new List<Tn_sn>();
    }
}
