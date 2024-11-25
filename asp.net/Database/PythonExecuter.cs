using System.Diagnostics;

namespace Capstone.Database
{
    public class PythonExecuter
    {
        public static void ExecuteVisitorPy()
        {
            try
            {
                const string SCRIPT_PATH = @"wwwroot/db/scripts/visitor.py";


                ProcessStartInfo start = new ProcessStartInfo(SCRIPT_PATH);
                start.FileName = "python.exe";
                start.Arguments = SCRIPT_PATH;
                
                using (Process process = Process.Start(start))
                {

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
