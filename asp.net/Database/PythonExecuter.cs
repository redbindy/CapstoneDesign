using System.Diagnostics;
using System.Text;

namespace Capstone.Database
{
    public static class PythonExecuter
    {
        private const string DB_PATH = @"wwwroot/db/";
#if DEBUG
        private const int TIMER_INTERVAL_MS = 1000 * 10;
#else
        private const int TIMER_INTERVAL_MS = 1000 * 60 * 90;
#endif
        private static readonly System.Timers.Timer EXECUTER_TIMER = new System.Timers.Timer();
        private static StringBuilder visitorHTML = new StringBuilder(Program.DEFAULT_CAPACITY);

        static PythonExecuter()
        {
            executeVisitorPy();

            EXECUTER_TIMER.Interval = TIMER_INTERVAL_MS;
            EXECUTER_TIMER.Elapsed += (sender, e) => executeVisitorPy();
            EXECUTER_TIMER.AutoReset = true;

            EXECUTER_TIMER.Start();
        }

        public static string GetVisitorHTML()
        {
            Debug.Assert(visitorHTML.Length != 0);
            return visitorHTML.ToString();
        }

        private static void executeVisitorPy()
        {
            try
            {
                ProcessStartInfo scriptInfo = new ProcessStartInfo($"{DB_PATH}/script/");
                scriptInfo.FileName = "python.exe";
                scriptInfo.Arguments = "visitor.py";
                scriptInfo.UseShellExecute = false;
                scriptInfo.RedirectStandardOutput = true;
                scriptInfo.RedirectStandardError = true;

                using (Process? process = Process.Start(scriptInfo))
                {
                    string output = process.StandardOutput.ReadLine();
                    string error = process.StandardError.ReadToEnd();

                    Console.WriteLine(output);
                    Console.WriteLine(error);
                }

                using (StreamReader sr = new StreamReader(File.Open($"{DB_PATH}/VisitorGraph.html", FileMode.Open)))
                {
                    visitorHTML.Clear();
                    while (!sr.EndOfStream)
                    {
                        visitorHTML.AppendLine(sr.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
            }
        }
    }
}
