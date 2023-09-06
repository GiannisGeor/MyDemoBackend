namespace Models.Entities
{
    public class Kaseta : EntityBase
    {
        public string Tipos { get; set; }
        public int Posotita { get; set; }
        public decimal Timi { get; set; }
        public int IDTainias { get; set; }
        public Tainia Tainia { get; set; } = new Tainia();
        public List<Enoikiasi> Enoikiasis { get; set; } = new List<Enoikiasi>();
    }
}
