using UnityEngine;
using System.Collections;

namespace VolumetricFogAndMist {
				public class DemoDepthBlur : MonoBehaviour {

								void Start () {
												// This is for the sprite test: enable shadows on the elephant - SpriteRenderer has shadows disabled by default
												GameObject elephant = GameObject.Find ("Elephant");
												elephant.GetComponent<Renderer> ().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
								}


								void Update () {
												VolumetricFog fog = VolumetricFog.instance;

												if (Input.GetKeyDown (KeyCode.T)) {
																fog.fogBlur = !fog.fogBlur;
												}
								}

				}
}