namespace Entity.Exeptions.Eimzo;

public class SignatureNotValidException : Exception
{
    public SignatureNotValidException(string message)
        : base(message)
    { }
}