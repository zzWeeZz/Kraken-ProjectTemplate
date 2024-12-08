using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpmake;

[module: Sharpmake.Include("*.sharpmake.cs")]

namespace Kraken
{
    [Sharpmake.Generate]
    public class SharpmakeSource : KrakenCSharpProject
    {
        public SharpmakeSource()
        {
            SourceRootPath = @"[project.RootPath]/../Vendor/Sharpmake";
        }
    }

    [Sharpmake.Generate]
    public class ProjectDefinitions : KrakenCSharpProject
    {
        public ProjectDefinitions()
        {
            SourceRootPath = @"[project.RootPath]";
        }

        [Configure()]
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);

            conf.AddPrivateDependency<SharpmakeSource>(target);
        }
    }

    [Sharpmake.Generate]
    public class EntryPoint : KrakenCppProject
    {
        public EntryPoint(){}
    }

    [Sharpmake.Generate]
    public class ProjectSolution : Sharpmake.Solution
    {
        public String WorkingDirectory = @"[solution.SharpmakeCsPath]\..";
        public ProjectSolution()
        {
            Name = "Template";
            AddTargets(new Target(Platform.win64, DevEnv.vs2022, Optimization.Debug | Optimization.Release));
        }

        [Configure()]
        public void ConfigureAll(Configuration conf, Target target)
        {
            conf.SolutionFileName = "[solution.Name]";
            conf.SolutionPath = WorkingDirectory;

            conf.AddProject<EntryPoint>(target);

            conf.AddProject<ProjectDefinitions>(target);
            conf.AddProject<SharpmakeSource>(target);
        }

        [Sharpmake.Main]
        public static void SharpmakeMain(Sharpmake.Arguments arguments)
        {
            arguments.Generate<ProjectSolution>();
        }
    }
}
