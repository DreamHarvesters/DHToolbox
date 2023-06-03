using System;

namespace DHToolbox.Runtime.DHToolboxAssembly.Utils
{
    public static class ResourcePathUtility
    {
        public static string GetRelativePathToResourcesFolder(string path)
        {
            string resourcesFolder = "Resources";
            int resourcesIndex = path.IndexOf(resourcesFolder);
            if (resourcesIndex == -1)
            {
                throw new Exception("Given path does not reside within the Resources folder.");
            }

            int startIndex = resourcesIndex + resourcesFolder.Length;
            int endIndex = path.LastIndexOfAny(new[] { '.', '/' });
            if (endIndex == -1 || endIndex < startIndex)
            {
                throw new Exception("Given path does not have a valid file or folder name.");
            }

            string relativePath = path.Substring(startIndex, endIndex - startIndex);

            return relativePath;
        }
    }
}