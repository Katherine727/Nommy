using UnityEngine;
using Assets.Utils;

public class SugarBrickPowerCommand :  ICommand {

    private CharacterController2D CC2D;
	private PlayerInputHandler PIH;
    /// <summary>
    /// Creates the Bubble Jump Power Command
    /// </summary>
    /// <param name="PowerUser">Object which uses the power</param>
    public SugarBrickPowerCommand(GameObject PowerUser) {
        CC2D = PowerUser.GetComponent<CharacterController2D>();
		PIH = PowerUser.GetComponent<PlayerInputHandler>();
    }

    #region ICommand implementation

    public void Execute() {
		var obj = (GameObject)GameObject.Instantiate(Resources.Load("Powers/Sugar Brick/SugarBrick"), PIH.gameObject.transform.position + new Vector3( 1.2f * (PIH.GoingLeft ? -1 : 1), 0, 0), Quaternion.identity);
        obj.GetComponent<SugarBrickBehaviour>().Use(CC2D.GetComponent<PlayerInputHandler>());

    }

    #endregion
}
