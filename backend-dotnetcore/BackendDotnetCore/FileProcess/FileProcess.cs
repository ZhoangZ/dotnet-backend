using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.FileProcess
{
    public class FileProcess

    {
        //public static string PATH = "D:\\ImageWeb\\shareimage";
        public static string PATH = "D:\\Github\\DotNet\\MySharedImage\\image";

        public static string getFullPath(string pathRelative)
        {
            return PATH + "\\" + pathRelative;
        }
        public static bool pathIsExists()
        {
           
            if (!Directory.Exists(PATH))
            {
                Console.WriteLine("PATH not exists");
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
