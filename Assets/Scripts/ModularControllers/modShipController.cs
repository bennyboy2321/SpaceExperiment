using UnityEngine;
using System.Collections;

public class modShipController : MonoBehaviour 
{
	public Transform MyShip;
	
	public Transform InstalledChassis;
	public Transform InstalleddEngine;
	public Transform InstalledHyperdrive;
	public bool SublightActive;
	public bool HasDynamicLights;
	public float CurrentSpeed;
	public float TimerTime = 10f;
	public bool TimerActive = false;
	
	//public for debug only
	float CurrentAccel;
	public float CurrentMaxSpeed;
	float CurrentTurnRate;
	float CurrentPowerUsageENG;
	float CurrentMinSpeed;
	//change when final.
	
	public Light[] ShipLights;
	public bool IsThrusting;
	
	void Update()
	{
		
		if (HasDynamicLights == true)
		{
			DynamicLights();	
		}
		
		if (CurrentSpeed >= CurrentMaxSpeed)
		{
			//CurrentSpeed = CurrentMaxSpeed;	
			print("Overspeed");
		}
		
		if (CurrentSpeed <= CurrentMinSpeed)
		{
			//CurrentSpeed = CurrentMinSpeed;	
			print("Stall");
		}
		
		#region
		var Engines = InstalleddEngine.GetComponent<modEngineController>();
		var Hyperdrive = InstalledHyperdrive.GetComponent<modSublightController>();
		
		if(SublightActive == true)
		{
			CurrentMaxSpeed = Hyperdrive.MaxSpeed;
			CurrentAccel = Hyperdrive.MaxAccelSpeed;
			CurrentTurnRate = Hyperdrive.MaxTurnRate;
			CurrentPowerUsageENG = Hyperdrive.PowerConsumptionRate;
			CurrentMinSpeed = Hyperdrive.MinSpeed;
			TimerTime = 10f;
			TimerActive = false;
		}
		else
		{
			CurrentMaxSpeed = Engines.MaxSpeed;
			CurrentAccel = Engines.MaxAccelSpeed;
			CurrentTurnRate = Engines.MaxTurnRate;
			CurrentPowerUsageENG = Engines.PowerConsumptionRate;
			CurrentMinSpeed = Engines.MinSpeed;
			if (CurrentSpeed >= CurrentMaxSpeed + 1f)
			{
				TimerActive = true;
				TimerTime -= 1f*Time.deltaTime;
			}
		}
		
		print ("|| MaxSpeed: " + CurrentMaxSpeed + " || Accell: " + CurrentAccel + " || Turn: " + CurrentTurnRate + " || PowerUsage(Eng): " + CurrentPowerUsageENG + " ||");
		#endregion //Pull speeds from Engine and Hyperdrive
		
		
		
		#region
		if (Input.GetAxis("Thrusters") > 0) // Thrusting Forwards input
		{
			IsThrusting = true; //Even if player is TRYING to thrust, it will still drain power.
			if(SublightActive == false)
			{
				if(CurrentSpeed < CurrentMaxSpeed)
				{
					EngThrustForward();
				}
				else
				{
					EngineTimer();
				}
			}
			else
			{
				SubThrustForward();	
			}
		}
		
		if (Input.GetAxis("Thrusters") < 0) // Thrusting Backwards input
		{
			IsThrusting = true; //Even if player is TRYING to thrust, it will still drain power.

			if(SublightActive == false)
			{
				if(CurrentSpeed >= CurrentMinSpeed)
				{
					EngThrustReverse();	
				}
				else
				{
					Debug.Log("Can't go slower!");
					CurrentSpeed = CurrentMinSpeed;	
				}
			}
			else
			{
				SubThrustReverse();	
			}
		}
		#endregion //Thruster Inputs
		
		
		
		#region
		if (Input.GetAxis("Rotation") < 0)
		{
			if (SublightActive != true)
			{
				EngTurnLeft();
			}
			else
			{
				SubTurnLeft();	
			}
		}
		
		if (Input.GetAxis("Rotation") > 0)
		{
			if (SublightActive != true)
			{
				EngTurnRight();
			}
			else
			{
				SubTurnRight();	
			}
		}
		#endregion // Rotation inputs
		
		
		
		#region
		if (Input.GetButtonDown("EngineToggle"))
		{
			SublightActive = !SublightActive;	
		}
		#endregion // Toggling Hyperdrive
	}
	
	void FixedUpdate()
	{
		IsThrusting = false;
		MyShip.rigidbody.AddForce(MyShip.forward * CurrentSpeed * Time.deltaTime); //Actually adds the speed here.
		
		
		if ((SublightActive == false) & (CurrentSpeed > CurrentMaxSpeed))
		{
			
		}
	}	
	
	#region
	void EngThrustForward()//ENGINES thrust FORWARDS commands
	{
		print ("Engines Forward");
		CurrentSpeed += CurrentAccel * Time.deltaTime;
	}
	
	void SubThrustForward()//Sublight thrust FORWARDS commands
	{
		print ("Sublights Forward");
		CurrentSpeed += CurrentAccel * Time.deltaTime;
	}
	
	void EngThrustReverse()//ENGINES thrust REVERSE commands
	{
		print ("Engines Reverse");
		CurrentSpeed -= (CurrentAccel*1.5f) * Time.deltaTime;
	}
	
	void SubThrustReverse()//Sublight thrust REVERSE commands
	{
		print ("Sublights Reverse");
		CurrentSpeed -= (CurrentAccel*1.5f) * Time.deltaTime;
	}
	#endregion //Thrust commands
	
	
	
	#region
	void EngTurnLeft()
	{
		MyShip.rigidbody.AddRelativeTorque(0,-(CurrentTurnRate*Time.deltaTime),0);
	}
	
	void EngTurnRight()
	{
		MyShip.rigidbody.AddRelativeTorque(0,(CurrentTurnRate*Time.deltaTime),0);
	}
	
	void SubTurnLeft()
	{
		MyShip.rigidbody.AddRelativeTorque(0,-(CurrentTurnRate*Time.deltaTime),0);	
	}
	
	void SubTurnRight()
	{
		
		MyShip.rigidbody.AddRelativeTorque(0,(CurrentTurnRate*Time.deltaTime),0);	}
	#endregion //Rotation commands
	
	
	
		void DynamicLights()
	{

		var EngCol = InstalleddEngine.GetComponent<modEngineController>().EngLightColour;
		var SubCol = InstalledHyperdrive.GetComponent<modSublightController>().SubLightColour;
		
		
		foreach (Light EngineLight in ShipLights)
		{
			if (SublightActive == false)
			{
				EngineLight.range = (CurrentSpeed / 25f);
				EngineLight.color = EngCol;
			}
			else
			{
				EngineLight.range = (CurrentSpeed / 30f);
				EngineLight.color = SubCol;
			}
			
			
			if (EngineLight.range >=0.5f)
			{
				EngineLight.range = 0.5f;	
			}
			
			if (EngineLight.range <= 0.1f)
			{
				EngineLight.range = 0.1f;	
			}
		}
		
	}
	
	
	void EngineTimer()
	{
		if (TimerActive == true)
		{
			if(CurrentSpeed >= (InstalleddEngine.GetComponent<modEngineController>().MaxSpeed))
				{
				print("Engines critical");
				CurrentSpeed -= (CurrentAccel*2.5f)*Time.deltaTime;
				}
			else
				{
				CurrentSpeed = CurrentMaxSpeed;
				}
		}
	}
}
