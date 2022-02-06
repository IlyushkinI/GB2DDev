using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerModel
{
    public GameState GameState { get; set; }
    public CarModel Car { get; set; }

    public PlayerModel()
    {
        GameState = GameState.None;
        Car = new CarModel(5);
    }

}
