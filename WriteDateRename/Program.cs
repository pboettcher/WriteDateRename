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
        private static string GetNewFileName(string fileName, int index, bool keepName)
        {
            string newName = string.Format("{0:yyyyMMdd_HHmmss}", File.GetLastWriteTime(fileName));
            if (index > 0)
                newName += string.Format("-{0}", index);
            if (keepName)
                newName += " " + Path.GetFileNameWithoutExtension(fileName);
            newName += Path.GetExtension(fileName);
            return newName;
        }

        private static string GetNewFileName(string fileName, bool keepName)
        {
            int index = 0;
            string newName = string.Empty;
            do
            {
                newName = GetNewFileName(fileName, index, keepName);
                index++;
            }
            while (File.Exists(newName));
            return newName;
        }

        static void Main(string[] args)
        {
            string curDir = Directory.GetCurrentDirectory();
            Console.WriteLine("This app will rename files in current directory according to their write timestamp.\nCurrent directory is\n{0}\nType \"continue\" if you want to proceed.", curDir);
            string userInput = Console.ReadLine();
            if (userInput == "continue")
            {
                Console.WriteLine("Keep original file names as part of new names? (Type \"yes\" or \"dontkeep\")");
                userInput = Console.ReadLine();
                bool keepNames = (userInput != "dontkeep");
                if ((userInput != "yes") && (userInput != "dontkeep"))
                {
                    Console.WriteLine("Unrecognized input. No action performed. Exiting.");
                    return;
                }
                string[] files = Directory.GetFiles(curDir);
                foreach (string fileName in files)
                {
                    string newName = GetNewFileName(fileName, keepNames);
                    try
                    {
                        File.Move(fileName, newName);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("from:\"{0}\" to:\"{1}\" {3}\n", Path.GetFileName(fileName), newName, ex.Message);
                    }
                }
            }
            else
                Console.WriteLine("No action performed. Exiting.");
        }
    }
}
