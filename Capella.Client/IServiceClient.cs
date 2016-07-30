namespace Capella.Client
{
    public interface IWebClient
    {
        object Execute(CallContext ctx);
    }
}