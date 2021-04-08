using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace FaregosoftJR.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new FaregosoftJR.App(), args);
            host.Run();
        }
    }
}
