using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minecraft_Server_Maker.Scripts
{
    public class FileHelper
    {
        public static bool IsFileEditable(string FilePath, string postFix)
        {
            bool isEditable = true;

            if (File.Exists(FilePath))
            {
                try
                {
                    string defFilePath = FilePath + postFix;
                    string fileCopyPath = FilePath + "_copy" + postFix;
                    File.Copy(defFilePath, fileCopyPath);
                    File.Delete(fileCopyPath);
                }
                catch
                {
                    return false;
                }
            }

            return isEditable;
        }
        public static bool IsDirectoryEditable(string dirPath)
        {
            bool isEditable = true;

            if (Directory.Exists(dirPath))
            {
                try
                {
                    string fileCopyPath = dirPath + "_copy";
                    Directory.Move(dirPath, fileCopyPath);
                    Directory.Move(fileCopyPath, dirPath);
                    Directory.Delete(fileCopyPath);
                    return true;
                }
                catch(IOException ex)
                {
                    return false;
                }
            }

            return isEditable;
        }
    }
}
