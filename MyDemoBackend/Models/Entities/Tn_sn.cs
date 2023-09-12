namespace Models.Entities
{
    public class Tn_sn
    {
        /// <summary>
        /// To Id tou table Tainias
        /// </summary>
        public int IDTainias { get; set; }

        /// <summary>
        ///To Id tou table Sintelesti
        /// </summary>
        public int IDSintelesti { get; set; }

        /// <summary>
        /// O Rolos tou Sintelesti sthn antistoixh tainia
        /// </summary>
        public string Rolos { get; set; }

        /// <summary>
        /// To table Tainia
        /// </summary>
        public Tainia Tainia { get; set; }

        /// <summary>
        /// To table Sintelestis
        /// </summary>
        public Sintelestis Sintelestis { get; set; }
    }
}
