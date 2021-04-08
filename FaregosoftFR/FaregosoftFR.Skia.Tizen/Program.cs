using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace FaregosoftFR.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new FaregosoftFR.App(), args);
            host.Run();
        }
    }
}
