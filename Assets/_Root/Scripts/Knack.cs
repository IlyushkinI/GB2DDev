using UnityEngine.UI;
using UnityEngine;

    internal class Knack : IObserver
    {
        private int crimePlayer;
        private Button skipBattle;

        public Knack(Button skipBattle)
        {
            this.skipBattle = skipBattle;
        CrimePlayer();
        }

        public void Update(DataPlayer dataPlayer, DataType dataType)
        {
            crimePlayer = dataPlayer.CountCrime;
            CrimePlayer();
        }

        public void CrimePlayer()
        {
        if (crimePlayer >= 0 && crimePlayer <=2)
            skipBattle.gameObject.SetActive(true);
        else
            skipBattle.gameObject.SetActive(false);

        }

    }

