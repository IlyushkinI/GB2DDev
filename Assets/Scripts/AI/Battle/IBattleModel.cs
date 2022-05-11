namespace AI
{
    public interface IBattleModel
    {
        bool IsPlayerWin(IEnemyModel enemy);
        bool IsNeedBattle(IPlayerData playerData);
    }
}