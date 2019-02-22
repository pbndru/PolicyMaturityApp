
namespace PolicyMaturity.Contracts
{
    public interface IPolicyManager
    {
        bool Execute(string path, bool hasHeaders);
    }
}
