namespace Services.Dtos
{
    public class NeaTainiaKaiSintelestesResponseDto
    {
        public int Id { get; set; }

        /// <summary>
        /// O Titlos ths tainias
        /// </summary>
        public string Titlos { get; set; }

        /// <summary>
        /// H Xronia pou bgike h Tainia
        /// </summary>
        public int Xronia { get; set; }

        public List<SintelestesKaiTn_snResponseDto> TainiesSintelestes { get; set; } = new List<SintelestesKaiTn_snResponseDto>();
    }
    public class SintelestesKaiTn_snResponseDto
    {
        public string Rolos { get; set; }

        public SintelestisResponseDto Sintelestis { get; set; }
    }

    public class SintelestisResponseDto
    {
        public string Onoma { get; set; }

    }
}
