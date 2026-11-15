using System.IO;
using System.Runtime.CompilerServices;
using Sharpmake;

namespace Kraken
{
    /// <summary>
    /// Settings shared by every project and solution of the template. Change them here once.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Root of the repository, found from the location of this file
        /// (ProjectDefinitions/Common/), so it doesn't depend on which .sharpmake.cs file
        /// a project is declared in.
        /// </summary>
        public static readonly string RootPath = FindRootPath();

        private static string FindRootPath([CallerFilePath] string thisFile = "")
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(thisFile), "..", ".."));
        }

        public static Target[] Targets => new[]
        {
            new Target(Platform.win64, DevEnv.vs2022, Optimization.Debug | Optimization.Release)
        };

        /// <summary>
        /// Puts the executables in bin\ and the libraries and intermediate files in bin-int\ at
        /// the repository root, one folder per configuration.
        /// </summary>
        public static void SetOutputPaths(Project.Configuration conf, Target target)
        {
            conf.TargetPath = RootPath + @"\bin\[target.Platform]_[target.Optimization]";
            conf.IntermediatePath = RootPath + @"\bin-int\[target.Platform]_[target.Optimization]\[project.Name]";
            conf.TargetLibraryPath = RootPath + @"\bin-int\[target.Platform]_[target.Optimization]\[project.Name]";
        }
    }

    /// <summary>
    /// Base class for the C++ projects of the template. A project lives in a folder named like
    /// the project at the repository root, with its code in src\.
    /// </summary>
    public abstract class KrakenProject : Project
    {
        protected KrakenProject()
        {
            IsFileNameToLower = false;
            IsTargetFileNameToLower = false;
            RootPath = Settings.RootPath + @"\[project.Name]";
            SourceRootPath = @"[project.RootPath]\src";
            AddTargets(Settings.Targets);
        }

        [Configure]
        public virtual void ConfigureAll(Configuration conf, Target target)
        {
            conf.ProjectPath = @"[project.RootPath]";
            conf.IncludePaths.Add(@"[project.SourceRootPath]");
            conf.Options.Add(Options.Vc.Compiler.CppLanguageStandard.CPP20);
            conf.Options.Add(Options.Vc.General.WarningLevel.Level4);

            if (target.Optimization == Optimization.Debug)
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDebugDLL);
            else
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDLL);

            Settings.SetOutputPaths(conf, target);
        }
    }
}
