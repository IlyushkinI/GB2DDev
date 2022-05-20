using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class MainMenuView : MonoBehaviour
{

    [Space]
    [SerializeField]
    private TMP_Dropdown _dropdownInputSelect;

    [Space]
    [SerializeField]
    private Button _buttonStart;

    [SerializeField]
    private Button _buttonShed;

    [SerializeField]
    private Button _buttonStartBattle;

    [SerializeField]
    private Button _buttonReward;

    [SerializeField]
    private Button _buttonExit;

    [Space]
    [SerializeField]
    private Transform _rootGameObject;

    public bool isActive
    {
        set => _rootGameObject.gameObject.SetActive(value);
        get => _rootGameObject.gameObject.activeSelf;
    }

    public TMP_Dropdown DropdownInputSelect => _dropdownInputSelect;

    public void Init(UnityAction startGame, UnityAction<int> changeInputType, UnityAction enterShed, UnityAction startBattle, UnityAction openRewards, UnityAction exitGame)
    {
        _buttonStart.onClick.AddListener(startGame);
        _dropdownInputSelect.onValueChanged.AddListener(changeInputType);
        _buttonShed.onClick.AddListener(enterShed);
        _buttonStartBattle.onClick.AddListener(startBattle);
        _buttonReward.onClick.AddListener(openRewards);
        _buttonExit.onClick.AddListener(exitGame);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _dropdownInputSelect.onValueChanged.RemoveAllListeners();
        _buttonShed.onClick.RemoveAllListeners();
        _buttonStartBattle.onClick.RemoveAllListeners();
        _buttonReward.onClick.RemoveAllListeners();
        _buttonExit.onClick.RemoveAllListeners();
    }

}