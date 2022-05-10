using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class MainMenuView : MonoBehaviour
{

    [Space]
    [SerializeField]
    private Dropdown _dropdownInputSelect;

    [Space]
    [SerializeField]
    private Button _buttonStart;

    [SerializeField]
    private Button _buttonShed;

    [SerializeField]
    private Button _buttonStartBattle;

    [Space]
    [SerializeField]
    private Transform _rootGameObject;

    public bool isActive
    {
        set => _rootGameObject.gameObject.SetActive(value);
        get => _rootGameObject.gameObject.activeSelf;
    }

    public Dropdown DropdownInputSelect => _dropdownInputSelect;

    public void Init(UnityAction startGame, UnityAction<int> changeInputType, UnityAction enterShed, UnityAction startBattle)
    {
        _buttonStart.onClick.AddListener(startGame);
        _dropdownInputSelect.onValueChanged.AddListener(changeInputType);
        _buttonShed.onClick.AddListener(enterShed);
        _buttonStartBattle.onClick.AddListener(startBattle);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _dropdownInputSelect.onValueChanged.RemoveAllListeners();
        _buttonShed.onClick.RemoveAllListeners();
        _buttonStartBattle.onClick.RemoveAllListeners();
    }
}