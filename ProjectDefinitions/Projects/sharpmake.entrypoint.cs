using Sharpmake;

namespace Kraken
{
    [Sharpmake.Generate]
    public class EntryPoint : KrakenProject
    {
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.Exe;
            conf.VcxprojUserFile = new Configuration.VcxprojUserFileSettings
            {
                LocalDebuggerWorkingDirectory = "$(SolutionDir)" // Run from the repository root.
            };

            conf.AddPrivateDependency<Core>(target);
        }
    }
}
