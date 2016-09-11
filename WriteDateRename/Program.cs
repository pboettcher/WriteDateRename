using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteDateRename
{
    class Program
    {
        static void Main(string[] args)
        {
            string curDir = Directory.GetCurrentDirectory();
            Console.WriteLine("This app will rename files in current directory according to their write timestamp.\nCurrent directory is {0}\nType \"continue\" if you want to proceed.", curDir);
            string userInput = Console.ReadLine();
            if (userInput=="continue")
            {
                string[] files = Directory.GetFiles(curDir);
                foreach (string fileName in files)
                {
                    DateTime wt = File.GetLastWriteTime(fileName);
                    try
                    {
                        File.Move(fileName, string.Format("{0:yyyyMMdd_HHmmss_fff}{1}", wt, Path.GetExtension(fileName)));
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("from:\"{0}\" to:\"{1:yyyyMMdd_HHmmss}{2}\" {3}\n", Path.GetFileName(fileName), wt, Path.GetExtension(fileName), ex.Message);
                        try
                        {
                            File.Move(fileName, string.Format("{0:yyyyMMdd_HHmmss} {1}", wt, Path.GetFileName(fileName)));
                        }
                        catch (IOException ex1)
                        {
                            Console.WriteLine("from:\"{0}\" to:\"{1:yyyyMMdd_HHmmss}{2}\" {3}\n", Path.GetFileName(fileName), wt, Path.GetExtension(fileName), ex1.Message);
                        }
                    }
                }
            }
            else
                Console.WriteLine("No action performed. Exiting.");
        }
    }
}
