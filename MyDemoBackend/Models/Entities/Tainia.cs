namespace Models.Entities
{
    public class Tainia : EntityBase
    {
        public string Titlos { get; set; }
        public int Xronia { get; set; }
        public List<Kaseta> Kasetes { get; set; } = new List<Kaseta>();
        public List<Tn_sn> TainiesSintelestes { get; set; } = new List<Tn_sn>();
    }
}
