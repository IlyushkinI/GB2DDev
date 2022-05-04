using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonEnterShed;

    public void Init(UnityAction startGame)
    {
        _buttonStart.onClick.AddListener(startGame);
    }

    public void EnterShed(UnityAction enterShed)
    {
        _buttonEnterShed.onClick.AddListener(enterShed);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonEnterShed.onClick.RemoveAllListeners();
    }
}