namespace Models.Entities
{
    public class Kaseta : EntityBase
    {

        /// <summary>
        /// O Tipos ths Kasetas (CD H Kaseta)
        /// </summary>
        public string Tipos { get; set; }


        /// <summary>
        /// H Posotita ton kaseton pou yparxoun diathesimes
        /// </summary>
        public int Posotita { get; set; }


        /// <summary>
        /// H timi ths kathe kasetas
        /// </summary>
        public decimal Timi { get; set; }


        /// <summary>
        /// To Id tou table Tainia 
        /// </summary>
        public int IDTainias { get; set; }

        /// <summary>
        /// To table Tainia
        /// </summary>
        public Tainia Tainia { get; set; } = new Tainia();

        /// <summary>
        /// Enonei to table Kaseta me to Enoikiasi
        /// </summary>
        public List<Enoikiasi> Enoikiasis { get; set; } = new List<Enoikiasi>();
    }
}
