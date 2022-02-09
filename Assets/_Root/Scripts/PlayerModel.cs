using RaceMobile.Car;
using RaceMobile.Tools.Reactive;

internal class PlayerModel
{
    public SubscriptionProperty<GameState> GameStatus { get; private set; }
    public CarModel Car { get; private set; }

    public PlayerModel()
    {
        GameStatus = new SubscriptionProperty<GameState>();
        GameStatus.Value = GameState.None;
        Car = new CarModel(5);
    }

}
