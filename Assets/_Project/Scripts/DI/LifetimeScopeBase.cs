using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Bnny.Scripts.DI
{
    public class LifetimeScopeBase : LifetimeScope
    {
        protected IContainerBuilder ContainerBuilder { get; private set; }

        protected sealed override void Configure(IContainerBuilder builder)
        {
            ContainerBuilder = builder;
            InitializeBindings();

            builder.RegisterBuildCallback(OnContainerBuilt);
        }

        protected virtual void InitializeBindings() { }

        private void OnContainerBuilt(IObjectResolver container)
        {
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

            if (rootGameObjects != null)
            {
                foreach (GameObject rootGameObject in rootGameObjects)
                {
                    container.InjectGameObject(rootGameObject);
                }
            }
            else
            {
                return;
            }
        }
    }
}
