using UnityEngine;

public class CarController : BaseController, IAbilityActivator
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/Car"};
    private readonly CarView _carView;

    public CarController(ProfilePlayer profilePlayer)
    {
        _carView = LoadView();
        profilePlayer.RigidbodyCar = _carView.GetComponent<Rigidbody2D>();
    }

    private CarView LoadView()
    {
        var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
        AddGameObjects(objView);
        
        return objView.GetComponent<CarView>();
    }

    public GameObject GetViewObject()
    {
        return _carView.gameObject;
    }
}

