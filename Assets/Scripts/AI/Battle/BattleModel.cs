namespace AI
{
    public class BattleModel : IBattleModel
    {

        #region Fields

        private readonly float _enemyPowerMin = 5.0f;
        private readonly int _playerCrimeLevelMin = 3;

        #endregion


        #region IBattleModel

        public bool IsNeedBattle(IPlayerData playerData)
        {
            return playerData.GetData(PlayerDataType.CrimeLevel) >= _playerCrimeLevelMin;
        }

        public bool IsPlayerWin(IEnemyModel enemy)
        {
            return enemy.GetPower < _enemyPowerMin;
        }

        #endregion

    }
}