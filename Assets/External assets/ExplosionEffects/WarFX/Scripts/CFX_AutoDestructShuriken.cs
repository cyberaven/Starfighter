using System.Collections;
using UnityEngine;

namespace External_assets.ExplosionEffects.WarFX.Scripts
{
	[RequireComponent(typeof(ParticleSystem))]
	public class CFX_AutoDestructShuriken : MonoBehaviour
	{
		public bool OnlyDeactivate;
	
		void OnEnable()
		{
			StartCoroutine("CheckIfAlive");
		}
	
		IEnumerator CheckIfAlive ()
		{
			while(true)
			{
				yield return new WaitForSeconds(0.5f);
				if(!GetComponent<ParticleSystem>().IsAlive(true))
				{
					if(OnlyDeactivate)
					{
#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
#else
						this.gameObject.SetActive(false);
#endif
					}
					else
						GameObject.Destroy(this.gameObject);
					break;
				}
			}
		}
	}
}
