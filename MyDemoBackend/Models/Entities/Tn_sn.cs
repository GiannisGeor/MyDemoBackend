namespace Models.Entities
{
    public class Tn_sn
    {
        public int IDTainias { get; set; }
        public int IDSintelesti { get; set; }
        public string Rolos { get; set; }
        public Tainia Tainia { get; set; }
        public Sintelestis Sintelestis { get; set; }
    }
}
