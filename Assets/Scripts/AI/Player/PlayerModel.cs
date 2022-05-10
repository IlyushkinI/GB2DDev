using System;
using System.Collections.Generic;
using UnityEngine;


namespace AI
{
    public class PlayerModel : IPlayerData
    {

        #region Fields

        private const int MIN_VALUE = 0;
        private const int MAX_VALUE = 999;

        private Dictionary<PlayerDataType, int> _playerData;

        #endregion


        #region CodeLifeCycles

        public PlayerModel()
        {
            var playerDataTypesValues = Enum.GetValues(typeof(PlayerDataType));
            _playerData = new Dictionary<PlayerDataType, int>(playerDataTypesValues.Length);
            foreach (var item in playerDataTypesValues)
            {
                _playerData.Add((PlayerDataType)item, 0);
            }
        }

        #endregion


        #region Methods

        private int ClampValue(int value)
        {
            return Mathf.Clamp(value, MIN_VALUE, MAX_VALUE);
        }

        #endregion


        #region IPlayerData

        public bool TrySetData(PlayerDataType dataType, int value)
        {
            if (ClampValue(_playerData[dataType] + value) != _playerData[dataType])
            {
                _playerData[dataType] = ClampValue(_playerData[dataType] + value);
                return true;
            }

            return false;
        }

        public int GetData(PlayerDataType dataType)
        {
            return _playerData[dataType];
        }

        #endregion

    }
}