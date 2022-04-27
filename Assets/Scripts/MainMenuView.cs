using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{

    [SerializeField] private Button _buttonStart;

    [Space]
    [SerializeField]
    private Dropdown _dropdownInputSelect;

    public Dropdown DropdownInputSelect => _dropdownInputSelect;

    public void Init(UnityAction startGame, UnityAction<int> changeInputType)
    {
        _buttonStart.onClick.AddListener(startGame);
        _dropdownInputSelect.onValueChanged.AddListener(changeInputType);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _dropdownInputSelect.onValueChanged.RemoveAllListeners();
    }
}