using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VolumetricFogAndMist {
				public class DemoFogOfWar : MonoBehaviour {
								bool fogCuttingOn = true;
								VolumetricFog fog;
								Text status;

								void Start() {
												fog = VolumetricFog.instance;
												fog.fogOfWarEnabled = fogCuttingOn;
												fog.ResetFogOfWar ();
												status = GameObject.Find("Status").GetComponent<Text>();
								}

								void Update () {
												if (Input.GetKeyDown (KeyCode.F)) {
																switch (fog.preset) {
																case FOG_PRESET.Custom: case FOG_PRESET.Clear:
																				fog.preset = FOG_PRESET.Mist;
																				break;
																case FOG_PRESET.Mist:
																				fog.preset = FOG_PRESET.WindyMist;
																				break;
																case FOG_PRESET.WindyMist:
																				fog.preset = FOG_PRESET.GroundFog;
																				break;
																case FOG_PRESET.GroundFog:
																				fog.preset = FOG_PRESET.FrostedGround;
																				break;
																case FOG_PRESET.FrostedGround:
																				fog.preset = FOG_PRESET.FoggyLake;
																				break;
																case FOG_PRESET.FoggyLake:
																				fog.preset = FOG_PRESET.Fog;
																				break;
																case FOG_PRESET.Fog:
																				fog.preset = FOG_PRESET.HeavyFog;
																				break;
																case FOG_PRESET.HeavyFog:
																				fog.preset = FOG_PRESET.LowClouds;
																				break;
																case FOG_PRESET.LowClouds:
																				fog.preset = FOG_PRESET.SeaClouds;
																				break;
																case FOG_PRESET.SeaClouds:
																				fog.preset = FOG_PRESET.Smoke;
																				break;
																case FOG_PRESET.Smoke:
																				fog.preset = FOG_PRESET.ToxicSwamp;
																				break;
																case FOG_PRESET.ToxicSwamp:
																				fog.preset = FOG_PRESET.SandStorm1;
																				break;
																case FOG_PRESET.SandStorm1:
																				fog.preset = FOG_PRESET.SandStorm2;
																				break;
																case FOG_PRESET.SandStorm2:
																				fog.preset = FOG_PRESET.Mist;
																				break;
																}
												} else if (Input.GetKeyDown (KeyCode.T)) {
																fog.enabled = !fog.enabled;
												} else if (Input.GetKeyDown (KeyCode.C)) {
																fogCuttingOn = !fogCuttingOn;
																fog.fogOfWarEnabled = fogCuttingOn;
																fog.ResetFogOfWar ();
												} else if (Input.GetKeyDown (KeyCode.R)) {
																fog.ResetFogOfWar ();
												}
			
												if (fogCuttingOn) {
																fog.SetFogOfWarAlpha (Camera.main.transform.position, 4, 0);
												}

												string text = VolumetricFog.instance.GetCurrentPresetName ();
												if (fogCuttingOn) text += "  *** FOG CUTTING ON ***";
												status.text = text;
								}

								void AssignCustomTexture () {
												VolumetricFog fog = VolumetricFog.instance;
												Texture2D tex = Resources.Load<Texture2D> ("Textures/alphaDemo");
												Color32[] colors = tex.GetPixels32 ();
												fog.fogOfWarTextureSize = 64;
												fog.fogOfWarTextureData = colors;
								}
							
				}
}