namespace Models.Projections
{
    public class NeaTainiaKaiSintelestesProjection
    {
        /// <summary>
        /// O Titlos ths tainias
        /// </summary>
        public string Titlos { get; set; }

        /// <summary>
        /// H Xronia pou bgike h Tainia
        /// </summary>
        public int Xronia { get; set; }

        public List<SintelestesKaiRoloiProjection> SintelestesKaiRoloi { get; set; } = new List<SintelestesKaiRoloiProjection>();
    }

    public class SintelestesKaiRoloiProjection
    {
        public string RolosSintelesti { get; set; }
        public string OnomaSintelesti { get; set; }
    }
}
