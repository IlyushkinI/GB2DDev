using RaceMobile.Car;
using RaceMobile.Tools.Reactive;
using RaceMobile.AnaliticsTools;
using RaceMobile.Reward;


internal class ProfilePlayer
{
    public SubscriptionProperty<GameState> GameStatus { get; }
    public CarModel CurrentCar { get; }

    public IAnaliticTools analiticTools { get; }
    public CurrencyModel CurrencyModel { get; }

    public ProfilePlayer(float speed, IAnaliticTools analiticTools)
    {
        GameStatus = new SubscriptionProperty<GameState>();
        GameStatus.Value = GameState.None;
        CurrentCar = new CarModel(speed);
        this.analiticTools = analiticTools;

        CurrencyModel = new CurrencyModel();

    }

}
