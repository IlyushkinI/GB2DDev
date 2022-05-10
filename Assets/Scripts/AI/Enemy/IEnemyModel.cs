namespace AI
{
    public interface IEnemyModel
    {
        float GetPower { get; }
        void CalculatePower(IPlayerData data);
    }
}