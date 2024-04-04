namespace PassIn.Communication.Responses;
public class ResponseErrorJson
{
    public string Message { get; set; } = string.Empty;

    public ResponseErrorJson(String message)
    {
        Message = message;
    }
}
