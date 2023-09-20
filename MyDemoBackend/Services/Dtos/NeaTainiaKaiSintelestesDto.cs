namespace Services.Dtos
{
    public class NeaTainiaKaiSintelestesDto
    {
        /// <summary>
        /// O Titlos ths tainias
        /// </summary>
        public string Titlos { get; set; }

        /// <summary>
        /// H Xronia pou bgike h Tainia
        /// </summary>
        public int Xronia { get; set; }

        public List<SintelestesKaiRoloiDto> SintelestesKaiRoloi { get; set; } = new List<SintelestesKaiRoloiDto>();
    }

    public class SintelestesKaiRoloiDto
    {
        public string Rolos { get; set; }
        public string Onoma { get; set; }
    }
}
