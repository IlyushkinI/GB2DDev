using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    public sealed class AIUIView : MonoBehaviour, IAIUIView
    {

        #region Fields

        [Space]
        [SerializeField]
        private TextMeshProUGUI _textDataMoney;

        [SerializeField]
        private TextMeshProUGUI _textDataHealth;

        [SerializeField]
        private TextMeshProUGUI _textDataForce;

        [Space]
        [SerializeField]
        private TextMeshProUGUI _textDataEnemy;

        [Space]
        [SerializeField]
        private Button _buttonGo;

        [SerializeField]
        private Button _buttonRestart;

        [SerializeField]
        private Button _buttonExit;

        [Space]
        [SerializeField]
        private GameObject _panelEndGame;

        [SerializeField]
        private TextMeshProUGUI _textWin;

        [SerializeField]
        private TextMeshProUGUI _textLose;

        [Space]
        [SerializeField]
        private List<Selectable> _interactables;

        #endregion


        #region IAIUIView

        public int MoneySet { set => _textDataMoney.text = $"{value:D3} {PlayerDataType.Money.ToUnit()}"; }
        public int HealthSet { set => _textDataHealth.text = $"{value:D3} {PlayerDataType.Health.ToUnit()}"; }
        public int ForceSet { set => _textDataForce.text = $"{value:D3} {PlayerDataType.Force.ToUnit()}"; }
        public float EnemyPowerSet { set => _textDataEnemy.text = $"{value:00.0}"; }
        public bool EnableButtonGo
        {
            get => _buttonGo.interactable;
            set => _buttonGo.interactable = value;
        }
        public bool EnableButtonRestart
        {
            set => _buttonRestart.gameObject.SetActive(value);
        }
        public bool SetIteractableForAll
        {
            set
            {
                foreach (var item in _interactables)
                {
                    item.interactable = value;
                }
            }
        }

        public void ShowEndGame(bool isShow, bool isWin)
        {
            _panelEndGame.gameObject.SetActive(isShow);
            _textWin.gameObject.SetActive(isWin);
            _textLose.gameObject.SetActive(!isWin);
        }

        #endregion

    }
}
