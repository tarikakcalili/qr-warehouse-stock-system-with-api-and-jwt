namespace MegaQr.Api.Models.DTOs;
//Client'e Mesaj Yazdırmak İçin
public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}
