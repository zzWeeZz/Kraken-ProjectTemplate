using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kraken;
using Sharpmake;

namespace Kraken
{
    public class KrakenCSharpProject : CSharpProject
    {
        public KrakenCSharpProject()
        {
            RootPath = @"[project.SharpmakeCsPath]";
            SourceRootPath = @"[project.RootPath]";
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
            conf.ProjectPath = @"[project.SharpmakeCsPath]\projects";
            conf.ProjectFileName = @"[project.Name]";
            conf.TargetPath = @"[conf.ProjectPath]\output\[target.DevEnv]\[target.Framework]\[conf.Name]";
            conf.IntermediatePath = @"[conf.ProjectPath]\temp\[project.Name]\[target.DevEnv]\[target.Framework]\[conf.Name]";

            if (target.Optimization == Optimization.Debug)
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDebugDLL);
            else
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDLL);

            conf.Options.Add(Sharpmake.Options.CSharp.TreatWarningsAsErrors.Enabled);
        }
    }

    public class KrakenCppProject : Project
    {
        public KrakenCppProject()
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

            conf.Options.Add(Sharpmake.Options.Vc.Compiler.JumboBuild.Enable);
            conf.Options.Add(Sharpmake.Options.Vc.Compiler.CppLanguageStandard.CPP20);
            conf.Options.Add(Sharpmake.Options.Vc.)
        }
    }
}
