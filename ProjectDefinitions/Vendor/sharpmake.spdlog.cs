using Sharpmake;

namespace Kraken
{
    /// <summary>
    /// spdlog, downloaded from GitHub at a pinned tag and built from its own CMakeLists.txt
    /// (CMake is not run). The sources are cached in Vendor\_deps and reused on later runs.
    ///
    /// To add another library the same way: copy this file, change the URL, the tag and the
    /// CMake target name, and add the project as a dependency where it is used.
    /// </summary>
    [Sharpmake.Generate]
    public class Spdlog : CMakeProject
    {
        public Spdlog()
        {
            Name = "spdlog";
            IsFileNameToLower = false;
            IsTargetFileNameToLower = false;

            CMakeFetchFrom("https://github.com/gabime/spdlog", "v1.15.0").CacheDirectory = Settings.RootPath + @"\Vendor\_deps";
            CMakeTargetName = "spdlog";

            AddTargets(Settings.Targets);
        }

        [Configure]
        public void ConfigureAll(Configuration conf, Target target)
        {
            conf.ProjectPath = Settings.RootPath + @"\Vendor\Projects\[project.Name]";

            if (target.Optimization == Optimization.Debug)
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDebugDLL);
            else
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDLL);

            Settings.SetOutputPaths(conf, target);
        }
    }
}
