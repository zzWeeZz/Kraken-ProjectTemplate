using Sharpmake;

namespace Kraken
{
    [Sharpmake.Generate]
    public class Core : KrakenProject
    {
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.Lib;

            // Private: only Core's .cpp files include spdlog. Make it public if a Core header
            // includes spdlog, so the projects using Core get its include paths too.
            conf.AddPrivateDependency<Spdlog>(target);

            // spdlog (through fmt) asks the code using it to compile with /utf-8. Sharpmake
            // doesn't forward compiler options to dependents, so it is added here.
            conf.AdditionalCompilerOptions.Add("/utf-8");
        }
    }
}
