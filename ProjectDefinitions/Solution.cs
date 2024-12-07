namespace Template
{
    [Sharpmake.Generate]
    public class CommonProject : Project
    {
        public CommonProject()
        {
            RootPath = @"[project.SharpmakeCsPath]\codebase\[project.Name]";
            SourceRootPath = @"[project.RootPath]";
            AddTargets(Common.CommonTarget);
        }
   
        [Configure()]
        public virtual void ConfigureAll(Configuration conf, Target target)
        {
            conf.ProjectPath = @"[project.SharpmakeCsPath]\projects\";
            conf.ProjectFileName = @"[project.Name].[target.DevEnv].[target.Framework]";
            conf.IntermediatePath = @"[conf.ProjectPath]\temp\[project.Name]\[target.DevEnv]\[target.Framework]\[conf.Name]";
   
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            if (target.Optimization == Optimization.Debug)
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDebugDLL);
            else
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDLL);
        }
    }

    [Sharpmake.Generate]
    public class ProjectSolution : Sharpmake.Solution
    {
        public ProjectSolution()
        {
            Name = "Template";
            AddTargets(new Target(Platform.win64, DevEnv.vs2019, Optimization.Debug | Optimization.Release));
        }

        [Configure()]
        public void ConfigureAll(Configuration conf, Target target)
        {
            conf.SolutionFileName = "[solution.Name]_[target.DevEnv]_[target.Platform]";
            conf.SolutionPath = @"[solution.SharpmakeCsPath]\projects";
            conf.AddProject<HelloAssemblyProject>(target);
        }

        [Sharpmake.Main]
        public static void SharpmakeMain(Sharpmake.Arguments arguments)
        {
            arguments.Generate<HelloWorldSolution>();
        }
    }
}