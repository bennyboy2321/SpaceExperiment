using UnityEngine;
using System.Collections;

public class modCamera : MonoBehaviour 
{
	public Transform Target;
	
	public float MaxZoom;
	public float CurrentZoom; //
	public float MinZoom;
	
	public float MaxVert;
	public float CurrentVert; //
	public float MinVert;
	
	public float CurrentRotation;//
	
	public float Rate;
	
	
	void Start()
	{
	CurrentZoom = (MinZoom+MaxZoom)/2f;
	CurrentVert = (MinVert+MaxVert)/2f;	
	}
	void Update()
	{
		if(Input.GetAxisRaw("CamZoom") > 0)
		{
			if 	(CurrentZoom >= MinZoom)
			{
				CurrentZoom -= (Rate/6) * Time.deltaTime;
			}
			else 	
			{
				CurrentZoom = MinZoom;
				Debug.Log("Can't zoom out further");
			}
		}
		
		if(Input.GetAxisRaw("CamZoom") < 0)
		{
			if 	(CurrentZoom <= MaxZoom)
			{
				CurrentZoom += (Rate/6) * Time.deltaTime;
			}
			else 	
			{
				CurrentZoom = MaxZoom;
				Debug.Log("Can't zoom in any further");
			}
		}
		
		
		if (Input.GetAxisRaw("CamRotateVer") < 0)
		{
			if (CurrentVert <= MaxVert)
			{
				CurrentVert += (Rate) * Time.deltaTime;
			}
			else
			{
				CurrentVert = MaxVert;
				Debug.Log("Can't cam any higher");
			}
		}
		
		
		if (Input.GetAxisRaw("CamRotateVer") > 0)
		{
			if (CurrentVert >= MinVert)
			{
				CurrentVert -= (Rate) * Time.deltaTime;
			}
			else
			{
				CurrentVert = MinVert;
				Debug.Log("Can't cam any lower");
			}
		}
		
		if (Input.GetAxisRaw("CamRotateHor") < 0)
		{
			CurrentRotation += Rate * Time.deltaTime;
			
			if(CurrentRotation >= 360f)
			{
			CurrentRotation = 0f;	
			}
		}
		
		if (Input.GetAxisRaw("CamRotateHor") > 0)
		{
			CurrentRotation -= Rate * Time.deltaTime;
			
			if(CurrentRotation <= 0f)
			{
			CurrentRotation = 360f;	
			}
		}
	}
	
	
	void FixedUpdate()
	{

		
		Vector3 ZoomAmt = new Vector3(0,0,CurrentZoom);
		transform.position = Target.position + ZoomAmt;
		
		transform.RotateAround(Target.position, Vector3.left, CurrentVert);
		transform.RotateAround(Target.position, Vector3.up, CurrentRotation);
		
		transform.LookAt(Target);
	}
	

}
