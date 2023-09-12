namespace Models.Projections
{
    public class StoixeiaPelatiKaiEnoikiasisProjection
    {
        public string OnomaPelati { get; set; }

        public List<KasetesTimesProjection> KasetesTimes { get; set; } = new List<KasetesTimesProjection>();
    }

    public class KasetesTimesProjection
    {
        public int IdKasetas { get; set; }
        public decimal TimiKasetas { get; set; }
    }
}
