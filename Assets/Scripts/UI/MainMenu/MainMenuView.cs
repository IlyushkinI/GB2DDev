using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class MainMenuView : MonoBehaviour
{

    [SerializeField]
    private Button _buttonStart;

    [Space]
    [SerializeField]
    private Dropdown _dropdownInputSelect;

    [Space]
    [SerializeField]
    private Button _buttonShed;

    [Space]
    [SerializeField]
    private Transform _rootGameObject;

    public bool isActive
    {
        set => _rootGameObject.gameObject.SetActive(value);
        get => _rootGameObject.gameObject.activeSelf;
    }

    public Dropdown DropdownInputSelect => _dropdownInputSelect;

    public void Init(UnityAction startGame, UnityAction<int> changeInputType, UnityAction enterShed)
    {
        _buttonStart.onClick.AddListener(startGame);
        _dropdownInputSelect.onValueChanged.AddListener(changeInputType);
        _buttonShed.onClick.AddListener(enterShed);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _dropdownInputSelect.onValueChanged.RemoveAllListeners();
        _buttonShed.onClick.RemoveAllListeners();
    }
}