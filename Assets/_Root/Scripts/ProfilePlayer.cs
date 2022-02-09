using RaceMobile.Car;
using RaceMobile.Tools.Reactive;


internal class ProfilePlayer
{
    public SubscriptionProperty<GameState> GameStatus { get; private set; }
    public CarModel CurrentCar { get; private set; }

    public ProfilePlayer()
    {
        GameStatus = new SubscriptionProperty<GameState>();
        GameStatus.Value = GameState.None;
        CurrentCar = new CarModel(5);
    }

}
