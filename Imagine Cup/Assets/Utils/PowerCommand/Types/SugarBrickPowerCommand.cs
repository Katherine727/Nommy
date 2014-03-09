using System;
using UnityEngine;
using Assets.Utils;

public class SugarBrickPowerCommand :  ICommand {

    private CharacterController2D CC2D;

    /// <summary>
    /// Creates the Bubble Jump Power Command
    /// </summary>
    /// <param name="PowerUser">Object which uses the power</param>
    public SugarBrickPowerCommand(GameObject PowerUser) {
        CC2D = PowerUser.GetComponent<CharacterController2D>();
    }

    #region ICommand implementation

    public void Execute() {
        var obj = (GameObject)GameObject.Instantiate(Resources.Load("SugarBrick"));
        obj.transform.position = CC2D.transform.position;
        obj.GetComponent<SugarBrickBehaviour>().Use(CC2D.GetComponent<PlayerInputHandler>());

    }

    #endregion
}
