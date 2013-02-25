using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public Transform ThisShip; 
	public Light[] ShipLights;
	
	public float MaxPower;
	public float PowerRegenRate;
	
	public float CurrentPower;
	public float CurrentSpeed;
	//public float CurrentRotateSpeed;
	
	public float MaxSpeed;
	public float MaxSubSpeed;
	
	public float CurrentAccel;
	public float CurrentMaxSpeed;
	
	public float Accel;
	public float SubAccel;
	public float CruiseSpeed;
	public float MinSpeed;
	public float RotateSpeed;
	
	public float MaintainY;
	public float MaintainRotationX;
	
	float TransitionTime = 10f;
	
	public AudioClip EngineSoundLoop;
	float MinPitch = 1f;
	float MaxPitch = 3f;	
	
	bool IsThrusting;
	public bool KeepLevel = true;
	
	bool Countdown;
	
	public bool CruiseActive;
	public bool SublightActive;
	public Color CruiseColour;
	public Color SublightColour;
	// Update is called once per frame
	
	void Start()
	{		
		CurrentSpeed = CruiseSpeed;
		//audio.pitch = StartPitch;
		SublightActive = false;
		CruiseActive = true;
	}
	
	void Update () 
	{
		DynamicLights();
		EngineSound();
		ActiveEngines();
		
		if (Countdown == true)
		{
			TransitionTime -= 1f*Time.deltaTime;

		}
		
			if(TransitionTime <= 0)
			{
				TransitionTime = 10f;
				Countdown = false;
			}
		
		if (CruiseActive == true)
		{
			CurrentAccel = Accel;
			
			if(CurrentSpeed >= CurrentMaxSpeed + 0.9f)
				{
					CurrentSpeed = CurrentSpeed - 10f*Time.deltaTime;
					print("Overspeed!");
				}		
		}
		
			if(Countdown == false)
			{
					if(CurrentSpeed >= CurrentMaxSpeed)
					{
						if(CruiseActive == true)
						{
							MaxSpeed = MaxSpeed;
						}
					}
			}
		if (SublightActive == true)
		{
			CurrentAccel = SubAccel;
			TransitionTime = 10f;
			if(CurrentSpeed >= 	MaxSubSpeed)
			{
				CurrentSpeed = MaxSubSpeed;	
			}
		}
		
		if ((CruiseActive == true) & (CurrentSpeed >= MaxSpeed+1f))
		{
			
			if (TransitionTime >= 1)
			{
				print ("ENGINES CRITICAL!");	
			}
			else 
			{
				print("SHIP DESTROYED");	
			}
		}

		
		#region
		if (KeepLevel == true)
		{
		var CurrentPos = new Vector3(ThisShip.rigidbody.position.x,MaintainY,ThisShip.rigidbody.position.z);
		//var CurrentRot =  new Vector3(MaintainRotationX, transform.rotation.y, transform.rotation.z);
		transform.position = CurrentPos;
		//transform.rotation = CurrentRot;
		}
		#endregion //Keep Level (Input and Commands)
		
		#region
		if (Input.GetAxis("Rotation") > 0)
		{
			TurnRight();
		}
		#endregion //Turning Right (Input)
		
		#region
		if (Input.GetAxis("Rotation") < 0)
		{
			TurnLeft();
		}
		#endregion //Turning Left (Input)
		
		#region 
		if (Input.GetAxis("Thrusters") < 0) //Slowing Down
		{
			
			CurrentSpeed = CurrentSpeed - CurrentAccel*2f*Time.deltaTime;

			
			
			if (CurrentSpeed >= MinSpeed)
			{
				SlowDown();
			}
			else
			{
			Debug.Log("Can't go any slower!");	
			CurrentSpeed = MinSpeed;
			}
		}
		#endregion //Slowing down (Input)
		
		#region
		if (Input.GetAxis("Thrusters") > 0) //Speeding Up
		{

			CurrentSpeed = CurrentSpeed + CurrentAccel*Time.deltaTime;
			if (CurrentSpeed <= CurrentMaxSpeed)
			{
			SpeedUp();

			}
			else
			{
			Debug.Log("Can't go any faster!");	
			//CurrentSpeed = CurrentMaxSpeed;

			}
		}
		
		#endregion //Speeding Up (Input)
		
		//Debug.Log ("Current speed is" + CurrentSpeed);
	}
	
	void TurnRight()
	{
			Debug.Log("Turning Right");
			ThisShip.rigidbody.AddRelativeTorque(0,0,(RotateSpeed*Time.deltaTime));
			IsThrusting = true;
	}
	
	void TurnLeft()
	{
			Debug.Log("Turning Left");	
			ThisShip.rigidbody.AddRelativeTorque(0,0,-(RotateSpeed*Time.deltaTime));
			IsThrusting = true;
	}
	
	void SpeedUp()
	{
			Debug.Log("Power Up");	
			IsThrusting = true;
	}
	
	void SlowDown()
	{
			Debug.Log("Power Down");
			IsThrusting = true;
			
	}
	
	void FixedUpdate()
	{
		IsThrusting = false;
		ThisShip.rigidbody.AddForce(ThisShip.up * CurrentSpeed * Time.deltaTime);
	}
	
	
	void DynamicLights()
	{
		foreach (Light EngineLight in ShipLights)
		{
			if (SublightActive == true)
			{
			EngineLight.range = (CurrentSpeed / 500f);
			}
			else
			{
			EngineLight.range = (CurrentSpeed / 50f);
			}
			
			if (EngineLight.range >=2.8f)
			{
				EngineLight.range = 2.8f;	
			}
			
			if (EngineLight.range <= 0.75f)
			{
				EngineLight.range = 0.75f;	
			}
			
			if (SublightActive == true)
			{
				EngineLight.color = SublightColour	;
			}
			else
			{
				EngineLight.color = CruiseColour;	
			}



		}
	}
	
	
	void	EngineSound()
	{
		var pitchModifier = MaxPitch - MinPitch;
		audio.pitch = MinPitch + (CurrentSpeed/MaxSpeed)*pitchModifier;

	}
	
	void	ActiveEngines()
	{
		
		ToggleEngineMode();
		
		if (SublightActive == true)
		{
			CruiseActive = false;
			print ("Sublight");	
			CurrentMaxSpeed = MaxSubSpeed;
			CurrentAccel = SubAccel;
		}
		
		if (CruiseActive == true)
		{
			SublightActive = false;
			print ("Cruise");
			CurrentMaxSpeed = MaxSpeed;
			CurrentAccel = Accel;
		}
		
		print(CurrentAccel);
	}
	
	void ToggleEngineMode()
	{
		if (Input.GetButtonDown("EngineToggle"))
		{
			SublightActive = !SublightActive;
			CruiseActive = !CruiseActive;


		}
		
		if (Input.GetButton("EngineToggle"))
		{
						Countdown = true;
		}
	}
}


