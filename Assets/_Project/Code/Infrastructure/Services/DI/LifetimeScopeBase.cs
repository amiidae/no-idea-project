using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.DI
{
    public abstract class LifetimeScopeBase : LifetimeScope
    {
        protected IContainerBuilder ContainerBuilder { get; private set; }
        
        protected sealed override void Configure(IContainerBuilder builder)
        {
            ContainerBuilder = builder;
            InstallBindings();
            builder.RegisterBuildCallback(OnContainerBuilt);
        }

        protected virtual void InstallBindings()
        {
          
        }

        private void OnContainerBuilt(IObjectResolver container)
        {
            Scene currentScene = gameObject.scene;
            
            if(currentScene.rootCount == 0)
                return;

            GameObject[] rootGameObjects = currentScene.GetRootGameObjects();
            
            foreach (GameObject rootGameObject in rootGameObjects)
            {
                container.InjectGameObject(rootGameObject);
            }
        }
    }
}