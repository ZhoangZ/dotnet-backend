using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.FileProcess
{
    public class FileProcess

    {
       
       
        public static string PATH = Directory.GetCurrentDirectory()+"\\static-file";

        public static string getFullPath(string pathRelative)
        {
            return PATH + "\\" + pathRelative;
        }
        public static bool pathIsExists()
        {
           
            if (!Directory.Exists(PATH))
            {

                Console.WriteLine("PATH not exists");
                Console.WriteLine(PATH);
                return false;
                
            }

            return true;

        }
        public static bool fileIsExists(string pathRelative)
        {
            if (pathIsExists())
            {
                string FilePath = PATH + "\\" + pathRelative;
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("File not exists");
                    return false;
                }
                return true;

            }
            return false;


        }
    }
}
