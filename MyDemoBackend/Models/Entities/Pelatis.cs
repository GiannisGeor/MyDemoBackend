namespace Models.Entities
{
    public class Pelatis : EntityBase
    {
        public string Onoma { get; set; }
        public string Tilefono { get; set; }
        public List<Enoikiasi> Enoikiasis { get; set; } = new List<Enoikiasi>();
    }
}
