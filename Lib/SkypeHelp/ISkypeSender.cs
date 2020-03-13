using System.Net.Http;
using System.Threading.Tasks;

namespace LibForCore.SkypeHelp
{
    public interface ISkypeSender
    {
        Task SendToGroup(string from, string Msg);
        void SetUp(HttpClient clientOutside, string skGroup);
    }
}
