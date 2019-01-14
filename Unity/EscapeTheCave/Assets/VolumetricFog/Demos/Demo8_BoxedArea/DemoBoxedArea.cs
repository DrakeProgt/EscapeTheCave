using UnityEngine;
using UnityEngine.UI;

namespace VolumetricFogAndMist {
				public class DemoBoxedArea : MonoBehaviour {


								void Update () {
												VolumetricFog fog = VolumetricFog.instance;
												if (Input.GetKeyDown (KeyCode.T)) {
																fog.enabled = !fog.enabled;
												}
								}

				}
}