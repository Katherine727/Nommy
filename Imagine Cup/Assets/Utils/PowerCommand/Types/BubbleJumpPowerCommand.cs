//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using Assets.Utils;

namespace Assets.Utils.PowerCommand.Types
{
		public class BubbleJumpPowerCommand: ICommand
		{
			private CharacterController2D CC2D;
			private PlayerInputHandler PIH;
			private Animator A;
			private ButtBubbles BB;

			/// <summary>
			/// Creates the Bubble Jump Power Command
			/// </summary>
			/// <param name="PowerUser">Object which uses the power</param>
			public BubbleJumpPowerCommand (GameObject PowerUser)
			{
				CC2D = PowerUser.GetComponent<CharacterController2D>();			
				PIH = PowerUser.GetComponent<PlayerInputHandler>();
				A = PowerUser.GetComponent<Animator>();
				BB = PowerUser.GetComponent<ButtBubbles>();
			}

			#region ICommand implementation

			public void Execute ()
			{
				if(!CC2D.isGrounded) {
					CC2D.velocity.y = Mathf.Sqrt( PIH.jumpHeight * -PIH.gravity + 10);
					A.Play(Animator.StringToHash("Jumping"));
					BB.ParticleSystemObject.transform.rotation = new Quaternion( PIH.GoingLeft ? 114 : 66, BB.ParticleSystemObject.transform.rotation.y, BB.ParticleSystemObject.transform.rotation.z, BB.ParticleSystemObject.transform.rotation.w);
					BB.ParticleSystemObject.particleSystem.Emit(4);					
				}
			}
			
			#endregion
		}
}

