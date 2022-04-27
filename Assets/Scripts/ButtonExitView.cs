using UnityEditor;
using UnityEngine;


public sealed class ButtonExitView : MonoBehaviour
{

    public void DoExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
