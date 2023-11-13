namespace Services.ResponseDtos.Auth
{
    public class QRCodeResponseDto
    {
        // String to generate the QRCode
        public string Uri { get; set; }

        // The alphanumeric string in case the qrcode logo doesnt work
        public string Key { get; set; }

    }
}
