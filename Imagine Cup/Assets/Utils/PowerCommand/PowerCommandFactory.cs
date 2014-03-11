using System;
using UnityEngine;

using Assets.Utils;
using Assets.Utils.PowerCommand.Types;

namespace Assets.Utils.PowerCommand
{
		/// <summary>
		/// Factory, which creates Exact Power Command for our needs depending on given PowerType.
		/// </summary>
		public class PowerCommandFactory
		{
			private GameObject _PowerUser;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="PowerUser">GameObject which will use the power; we often need it's transform or components so it's reference is passed</param>
			public PowerCommandFactory(GameObject PowerUser)
			{
				_PowerUser = PowerUser;
			}

			/// <summary>
			/// Create exact Power Command
			/// </summary>
			/// <returns>The power command.</returns>
			/// <param name="PowerType">Power type which we want</param>
			public ICommand CreatePowerCommand(PowerEnum PowerType){
				switch(PowerType) {
					case PowerEnum.BubbleJump:
						return new BubbleJumpPowerCommand(_PowerUser);
                    case PowerEnum.SugarBrick:
                        return new SugarBrickPowerCommand(_PowerUser);
                    default:
                        return new BubbleJumpPowerCommand(_PowerUser);
					//TODO
				}
			}
		}
}

