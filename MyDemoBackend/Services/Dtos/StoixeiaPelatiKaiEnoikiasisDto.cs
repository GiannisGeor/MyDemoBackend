namespace Services.Dtos
{
    public class StoixeiaPelatiKaiEnoikiasisDto
    {
        public string OnomaPelati { get; set; }
        public List<KasetesTimesDto> KasetesTimes { get; set; } = new List<KasetesTimesDto>();
    }

    public class KasetesTimesDto
    {
        public int IdKasetas { get; set; }
        public decimal TimiKasetas { get; set; }
    }
}
