using System;
using DHToolbox.Runtime.DHToolboxAssembly.Identification;
using DHToolbox.Runtime.DHToolboxAssembly.Singleton;

namespace DHToolbox.Runtime.DHToolboxAssembly.Resource
{
    public class ResourceSetupRepository : Singleton<ResourceSetupRepository>
    {
        private ResourceSetup[] resourceSetups;

        public ResourceSetup GetById(Id id) => Array.Find(resourceSetups, setup => setup.Id.Equals(id));

        public ResourceSetupRepository()
        {
            resourceSetups = UnityEngine.Resources.LoadAll<ResourceSetup>("ResourceSetups");
        }
    }
}