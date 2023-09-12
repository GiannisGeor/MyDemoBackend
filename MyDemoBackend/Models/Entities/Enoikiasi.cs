namespace Models.Entities
{
    public class Enoikiasi
    {
        /// <summary>
        /// To ID pou proerxetai apo to table tou Pelati 
        /// </summary>
        public int IDPelati { get; set; }

        /// <summary>
        /// To ID pou proerxetai apo to table ths Kasetas
        /// </summary>
        public int IDKasetas { get; set; }

        /// <summary>
        /// Imerominia opou o pelatis enoikiase mia Kaseta
        /// </summary>
        public DateTime Apo { get; set; }

        /// <summary>
        /// Imerominia opou o pelatis epestrepse mia Kaseta 
        /// </summary>
        public DateTime? Eos { get; set; }

        /// <summary>
        /// Enonei to table tou Pelati me to Enoikiasi
        /// </summary>
        public Pelatis Pelatis { get; set; } = new Pelatis();

        /// <summary>
        /// Enonei to table ths Kasetas me to Enoikiasi
        /// </summary>
        public Kaseta Kaseta { get; set; } = new Kaseta();
    }
}
