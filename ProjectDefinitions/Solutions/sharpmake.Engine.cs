using Sharpmake;

namespace Kraken
{
    [Sharpmake.Generate]
    public class EngineSolution : Sharpmake.Solution
    {
        public EngineSolution()
        {
            Name = "Engine";
            IsFileNameToLower = false;
            AddTargets(Settings.Targets);
        }

        [Configure]
        public void ConfigureAll(Configuration conf, Target target)
        {
            conf.SolutionFileName = "[solution.Name]";
            // The repository root, next to the Build_*.bat scripts.
            conf.SolutionPath = Settings.RootPath;

            // The first project added is the startup project.
            conf.AddProject<EntryPoint>(target);
            conf.AddProject<Core>(target);
        }
    }
}
