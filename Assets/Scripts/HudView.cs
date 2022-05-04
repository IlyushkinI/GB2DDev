using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HudView : MonoBehaviour
{
    [SerializeField] private Button _buttonExitToMain;

    public void ExitButton(UnityAction exitToMain)
    {
        _buttonExitToMain.onClick.AddListener(exitToMain);
    }

    protected void OnDestroy()
    {
        _buttonExitToMain.onClick.RemoveAllListeners();
    }
}
