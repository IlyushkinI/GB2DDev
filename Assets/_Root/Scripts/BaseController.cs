using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RaceMobile.Base
{
    internal class BaseController : IDisposable
    {
        private List<BaseController> controllers = new List<BaseController>();
        private List<GameObject> gameObjects = new List<GameObject>();
        private bool isDisposed;

        public void Dispose()
        {
            if (isDisposed)
                return;

            isDisposed = true;

            foreach (var controller in controllers)
            {
                controller.Dispose();
            }
            controllers.Clear();

            foreach (var go in gameObjects)
            {
                Object.Destroy(go);
            }
            gameObjects.Clear();

            OnDispose();

        }

        protected void AddController(BaseController controller)
        {
            controllers.Add(controller);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        protected virtual void OnDispose() { }
    }
}
