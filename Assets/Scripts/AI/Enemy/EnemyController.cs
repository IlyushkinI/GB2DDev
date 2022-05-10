using System.Collections.Generic;


namespace AI
{
    public sealed class EnemyController : BaseController, IObserver, IObservable
    {

        #region Fields

        private EnemyModel _enemyModel;
        private List<IObserver> _observers;

        #endregion


        #region CodeLifeCycles

        public EnemyController(EnemyView enemyView)
        {
            _enemyModel = new EnemyModel();

            _observers = new List<IObserver>();
        }

        #endregion


        #region IObserver

        public void Catch(PlayerDataType _, IPlayerData data)
        {
            _enemyModel.CalculatePower(data);
            UnityEngine.Debug.Log(_enemyModel.GetPower);
            Notify(_);
        }

        public void Catch(float data) { }

        #endregion


        #region IObservable

        public void Notify(PlayerDataType _)
        {
            _observers.ForEach(i => i.Catch(_enemyModel.GetPower));
        }

        public void Subscribe(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        #endregion

    }
}