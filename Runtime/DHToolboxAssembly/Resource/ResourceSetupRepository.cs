using System;
using DHToolbox.Runtime.DHToolboxAssembly.Identification;
using DHToolbox.Runtime.DHToolboxAssembly.Singleton;
using DHToolbox.Runtime.DHToolboxAssembly.Utils;

namespace DHToolbox.Runtime.DHToolboxAssembly.Resource
{
    public class ResourceSetupRepository : Singleton<ResourceSetupRepository>
    {
        private ResourceSetup[] resourceSetups;

        public ResourceSetup GetById(Id id) => Array.Find(resourceSetups, setup => setup.Id.Equals(id));

        public void Foreach(Action<ResourceSetup> dlg) => Array.ForEach(resourceSetups, dlg);

        public ResourceSetupRepository()
        {
            resourceSetups = UnityEngine.Resources.LoadAll<ResourceSetup>(
                ResourcePathUtility.GetRelativePathToResourcesFolder(Constants.ResourceSetupsFolderPath));
        }
    }
}