using UnityEngine;

namespace Assets.Utils.PowerCommand.Types {
    public class CandySpittingPowerCommand : ICommand {
        private CharacterController2D CC2D;
        private PlayerInputHandler PIH;
        /// <summary>
        /// Creates the Candy Spitting  Power Command
        /// </summary>
        /// <param name="PowerUser">Object which uses the power</param>
        public CandySpittingPowerCommand(GameObject PowerUser) {
            CC2D = PowerUser.GetComponent<CharacterController2D>();
            PIH = PowerUser.GetComponent<PlayerInputHandler>();
        }

        #region ICommand implementation

        public void Execute() {
            var obj = (GameObject)GameObject.Instantiate(Resources.Load("Powers/Candy Spitting/Candy Bullet"), PIH.gameObject.transform.position + new Vector3(1.2f * (PIH.GoingLeft ? -1 : 1), 0, 0), Quaternion.identity);
            obj.GetComponent<CandySpittingBehaviour>().Use(CC2D.GetComponent<PlayerInputHandler>());

        }
        #endregion
    }
}
