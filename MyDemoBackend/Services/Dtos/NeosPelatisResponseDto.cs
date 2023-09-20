namespace Services.Dtos
{
    public class NeosPelatisResponseDto
    {
        public int Id { get; set; }

        /// <summary>
        /// To onoma tou Pelati
        /// </summary>
        public string Onoma { get; set; }

        /// <summary>
        /// To tilefono tou pelati
        /// </summary>
        public string Tilefono { get; set; }
    }
}
