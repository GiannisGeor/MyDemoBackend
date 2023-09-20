namespace Services.Dtos
{
    public class NeaKasetaDto
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
        /// To table Tainia
        /// </summary>
        public OnomaTainiasDto Tainia { get; set; } = new OnomaTainiasDto();

    }

    public class OnomaTainiasDto
    {
        /// <summary>
        /// O Titlos ths tainias
        /// </summary>
        public string Titlos { get; set; }
    }
}
