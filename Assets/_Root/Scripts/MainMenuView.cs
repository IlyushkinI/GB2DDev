using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Game.Menu
{
    internal sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button startGame;

        public void Init(UnityAction onAction)
        {
            startGame.onClick.AddListener(onAction);
        }

        private void OnDestroy()
        {
            startGame.onClick.RemoveAllListeners();
        }

    }

}