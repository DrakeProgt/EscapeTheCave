using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfMovement : MonoBehaviour
{

	[HideInInspector] public Vector3 targetPosition;
	[HideInInspector] public bool active = false;
	[HideInInspector] public bool smooth = false;
	[HideInInspector] public bool lookAtMovementDirectionEnabled = true;
	[HideInInspector] public float speed = 3.0f;
	[HideInInspector] public float rotationSpeedDumping = 100f;
	private float rotationProgress = 0;

	private Vector3 direction;
	public void Goto(Vector3 position)
	{
		rotationProgress = 0;
		targetPosition = position;
		direction = (targetPosition - transform.position).normalized;
		active = true;
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (active)
		{
			if (lookAtMovementDirectionEnabled) lookAtMovementDirection();
			active = !(moveTo(targetPosition, speed));
		}	
	}
	
	private bool moveTo(Vector3 target, float speed)
	{
		float distance = Vector3.Distance(transform.position, target);	
		
		if (smooth && distance < speed)
		{
			transform.position += direction * distance * Time.deltaTime;
		} else if (!smooth && distance < (speed * Time.deltaTime))
		{
			transform.position += direction * distance * Time.deltaTime;
		}
		else
		{
			transform.position += direction * speed * Time.deltaTime;
		}
		
		return (distance < 0.1f);
	}

	private void lookAtMovementDirection()
	{
		rotationProgress += Time.deltaTime * speed * rotationSpeedDumping;
		if (rotationProgress >= 1) rotationProgress = 1;
		
		transform.rotation = 
			Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotationProgress);
	}
}
