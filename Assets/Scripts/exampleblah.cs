/*
using UnityEngine;
using System.Collections;

public class exampleblah : MonoBehaviour {

	bool IsSublight;
	
	public float MaxSubSpeed=300f;
	public float MaxCruiseSpeed=100f;
	
	public float MaxSubAccel=30f;
	public float MaxCruiseAccel=8f;
	
	float CurrentSpeed;
	float CurrentMaxSpeed;
	float CurrentAccel;
	
	public float CountdownTime=10f;
	bool IsCountdownActive;
	
	
	void Update()
	{
		if (IsSublight == true)
		{
			CurrentMaxSpeed = MaxSubSpeed;
			CurrentAccel = MaxSubAccel;
		}
		else
		{
			CurrentMaxSpeed = MaxCruiseSpeed;
			CurrentAccel = MaxCruiseAccel;	
		}
		
		if((IsSublight == true) & CurrentSpeed > MaxCruiseSpeed)
		{
			print ("Overspeed");
			CurrentSpeed = CurrentSpeed - (CurrentAccel*2*Time.deltaTime);
		}
		
		//InputSpeedUp
		CurrentSpeed = CurrentSpeed + (CurrentAccel*Time.deltaTime);
		//
		
		//InputSlowDown
		CurrentSpeed = CurrentSpeed - (CurrentAccel*Time.deltaTime);
		//
		
		//inputToggle
		IsSublight = !IsSublight;
		IsCountdownActive = true;
		//
	}
}
 */