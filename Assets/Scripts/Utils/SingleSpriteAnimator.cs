/*
**			Created by Christian Viñolo on 28/02/2023
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace abunity.Utils
{
	public class SingleSpriteAnimator : MonoBehaviour
	{
		/*#####################################################################
		#############################  CONSTANTS  #############################
		#####################################################################*/
		#region Constants

		#endregion

		/*#####################################################################
		##############################  FIELDS  ###############################
		#####################################################################*/
		#region Fields

		[SerializeField] private SpriteRenderer rend = null;
		[SerializeField] private Sprite[] sprites = null;
		[Space]
		[SerializeField] private int delay = 100;
		[SerializeField] private bool playAutomatically = true;
		[SerializeField] private bool loop = true;

		private CancellationTokenSource cancelTokenSource;

		#endregion

		/*#####################################################################
		#############################  PROPERTIES  ############################
		#####################################################################*/
		#region Properties

		#endregion

		/*#####################################################################
		###############################  UNITY  ###############################
		#####################################################################*/
		#region Unity Methods

		private void Awake()
		{
			if (playAutomatically)
			{
				Play();
			}
		}

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		private async UniTaskVoid UpdateSprites()
		{
			cancelTokenSource?.Cancel();

			cancelTokenSource = new();
			var cancelToken = cancelTokenSource.Token;

			int i = 0;

			while (!cancelToken.IsCancellationRequested)
			{
				if (!loop && i >= sprites.Length)
				{
					return;
				}

				rend.sprite = sprites[i++];

				if (loop)
				{
					i %= sprites.Length;
				}

				await UniTask.Delay(delay, cancellationToken: cancelToken);
			}
		}

		public void Play()
		{
			UpdateSprites().Forget();
		}

		public void Stop()
		{
			cancelTokenSource.Cancel();
		}

		#endregion
	}
}