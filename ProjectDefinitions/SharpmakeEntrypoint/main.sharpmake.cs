using Sharpmake;

// Forward slashes work on every platform; backslashes only on Windows.
[module: Sharpmake.Include("../Common/*.cs")]
[module: Sharpmake.Include("../Vendor/*.cs")]
[module: Sharpmake.Include("../Projects/*.cs")]
[module: Sharpmake.Include("../Solutions/*.cs")]

namespace Kraken
{
    public static class Startup
    {
        [Sharpmake.Main]
        public static void Main(Sharpmake.Arguments arguments)
        {
            arguments.Generate<EngineSolution>();
        }
    }
}
