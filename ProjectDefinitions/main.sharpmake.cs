using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpmake;

namespace Template
{
    [Sharpmake.Generate]
    public class EntryPoint : Project
    {
        public EntryPoint()
        {
            RootPath = @"[project.SharpmakeCsPath]\..\[project.Name]";
            SourceRootPath = @"[project.RootPath]\src";
            AddTargets(new Target(
                      // we want a target that builds for both 32 and 64-bit Windows.
                      Platform.win64,

                      // we only care about Visual Studio 2015. (Edit as needed.)
                      DevEnv.vs2022,

                      // of course, we want a debug and a release configuration.
                      Optimization.Debug | Optimization.Release));
        }

        [Configure()]
        public virtual void ConfigureAll(Configuration conf, Target target)
        {
            conf.ProjectPath = @"[project.RootPath]";

            if (target.Optimization == Optimization.Debug)
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDebugDLL);
            else
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDLL);
        }
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
        }

        [Sharpmake.Main]
        public static void SharpmakeMain(Sharpmake.Arguments arguments)
        {
            arguments.Generate<ProjectSolution>();
        }
    }
}
