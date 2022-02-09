using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;
using UnityEngine;

namespace RaceMobile.Car
{
    internal class CarController : BaseController
    {
        private readonly ResourcePath viewPath = new ResourcePath() { PathResource = "Prefabs/Transport/Car" };
        private CarView carView;

        public CarController()
        {
            carView = LoadView();
        }

        private CarView LoadView()
        {
            var prefabCar = ResourceLoader.LoadPrefab(viewPath);
            var go = Object.Instantiate(prefabCar);
            AddGameObject(go);

            return go.GetComponent<CarView>();
        }

        public GameObject GetViewObject()
        {
            return carView.gameObject;
        }


    }
}
