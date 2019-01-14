using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolumetricFogAndMist {
				
				public class CameraMove : MonoBehaviour {
								
								// Update is called once per frame
								void Update () {
												float ang = Time.deltaTime * 10f;
												Camera.main.transform.RotateAround (Vector3.zero, Vector3.up, ang);
												Camera.main.transform.LookAt (new Vector3 (0, 0, 0));
												Camera.main.transform.rotation *= Quaternion.Euler (Mathf.Sin (Time.time * 0.05f) * 60f - 45f, 0, 0);
								}
				}

}