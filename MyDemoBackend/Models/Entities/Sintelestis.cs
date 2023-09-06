namespace Models.Entities
{
    public class Sintelestis : EntityBase
    {
        public string Onoma { get; set; }
        public List<Tn_sn> TainiesSintelestes { get; set; } = new List<Tn_sn>();
    }
}
