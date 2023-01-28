using System;
using DHToolbox.Runtime.Singleton;
using Foundations.Scripts.Identification;

namespace Foundations.Scripts.Resource
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