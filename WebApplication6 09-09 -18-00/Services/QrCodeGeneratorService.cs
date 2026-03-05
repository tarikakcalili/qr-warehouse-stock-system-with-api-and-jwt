using QRCoder;

namespace MegaQr.Web.Services;
public class QrCodeGeneratorService
{
    public static string GenerateQrCodeSvg(string text)
    {
        using (var qrGenerator = new QRCodeGenerator())
        {
            var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new SvgQRCode(qrCodeData);
            string qrCodeSvg = qrCode.GetGraphic(5);
            return qrCodeSvg;
        }
    }
}
