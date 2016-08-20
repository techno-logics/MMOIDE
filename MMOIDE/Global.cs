using NodeEditorFramework;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System;

public class Global : Singleton<Global>
{
    protected Global() { } // guarantee this will be always a singleton only - can't use the constructor!

    public Node StartNode;

    public NodeCanvas curCanvas;

    public Node GetStartNode()
    {
        for (int i = 0; i < curCanvas.nodes.Count; i++)
        {
            if (curCanvas.nodes[i].GetID == "startNode")
            {
                UnityEngine.Debug.Log("Found start node on nodeCanvas!");
                return curCanvas.nodes[i];
            }
        }

        UnityEngine.Debug.Log("Could not find start node on nodeCanvas");

        return StartNode;
    }

    public void CompileCode(string code, string function_call_code)
    {
        string source = @"
		namespace Foo
		{
		    public class Bar

		    {
		        static void Main(string[] args)
		        {
		            Bar.MainFunc();
		        }

		        public static void MainFunc()
		        {"
            +
            code
            +
                "}\n"

                +
            @"

            }
		}
		";

        UnityEngine.Debug.Log(source);
        /*
        UnityEngine.Debug.Log ("Final Compiled Code: " + source);

        // Setup for compiling
        var provider_options = new Dictionary<string, string>
        {
            {"CompilerVersion","v3.5"}
        };

        var provider = new Microsoft.CSharp.CSharpCodeProvider(provider_options);
        var compiler_params = new System.CodeDom.Compiler.CompilerParameters();
	
        string outfile = "Compiled/" + curGraph.graphName + ".exe";
        compiler_params.OutputAssembly = outfile ;
        /*compiler_params.GenerateExecutable = true;

        // Compile
        var results = provider.CompileAssemblyFromSource(compiler_params, source);

        // Print out any Errors
        UnityEngine.Debug.Log("Output file: " + outfile);
        UnityEngine.Debug.Log("Number of Errors: " + results.Errors.Count.ToString());

        //foreach (System.CodeDom.Compiler.CompilerError err in results.Errors)
        //{
        //	UnityEngine.Debug.Log("ERROR : " + err.ErrorText);
        //}
        //*/

        File.WriteAllText("Tmp/" + Global.Instance.curCanvas.name + ".code", source);

        UnityEngine.Debug.Log("Code written at loc " + "Tmp/" + Global.Instance.curCanvas.name + ".code");

        try
        {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            //myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            //string path = "C:\\Users\\Brian\\Desktop\\testFile.bat";

            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.RedirectStandardError = true;

            myProcess.StartInfo.FileName = "Debugger.exe";
            myProcess.StartInfo.Arguments = "Tmp/" + Global.Instance.curCanvas.name + ".code " + "Compiled/" + Global.Instance.curCanvas.name + ".exe";
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();

            //* Read the output (or the error)
            string output = myProcess.StandardOutput.ReadToEnd();
            UnityEngine.Debug.Log(output);
            string err = myProcess.StandardError.ReadToEnd();

            if (err != "")
                UnityEngine.Debug.Log(err);

            myProcess.WaitForExit();
            int ExitCode = myProcess.ExitCode;

            //print(ExitCode);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.ToString());
        }
    }

}