namespace AI
{
    public interface IAIUIView
    {
        int MoneySet { set; }
        int HealthSet { set; }
        int ForceSet { set; }
        float EnemyPowerSet { set; }
        bool EnableButtonGo { set; get; }
    }
}
