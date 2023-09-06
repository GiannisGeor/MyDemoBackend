namespace Models.Entities
{
    public class Enoikiasi
    {
        public int IDPelati { get; set; }
        public int IDKasetas { get; set; }
        public DateTime Apo { get; set; }
        public DateTime? Eos { get; set; }
        public Pelatis Pelatis { get; set; } = new Pelatis();
        public Kaseta Kaseta { get; set; } = new Kaseta();
    }
}
