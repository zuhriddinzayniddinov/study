namespace Entity.Exeptions.Eimzo;

public class EImzoApiException : Exception
{
    public EImzoApiException(string message)
        : base(message)
    { }
}