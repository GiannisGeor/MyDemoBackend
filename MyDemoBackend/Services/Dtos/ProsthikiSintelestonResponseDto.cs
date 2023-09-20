namespace Services.Dtos
{
    public class ProsthikiSintelestonResponseDto
    {
        public int Id { get; set; }

        public List<SintelestesKaiTn_snResponseDto> TainiesSintelestes { get; set; } = new List<SintelestesKaiTn_snResponseDto>();
    }
}
