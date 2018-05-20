using System.IO;
using System.Reflection;

namespace Komorebi
{
    class FileManager
    {
        public static string getMediaFile(string filename)
        {
            //return Path.GetFileName(filename);
            //RTR
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../media/" + filename);
        }
    }
}
