using System;
namespace SSCreator {
    public class FileHelper {
        public static string getSSCreatorPath(string path) {
            string ssCreator = "SSCreator";
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return System.IO.Path.Combine(home, ssCreator, path);
        }
    }
}
