using UnityEngine;
using System.Collections;

public class ShipInfo : MonoBehaviour 
{

	public Transform Target;
	public float rCurrentSpeed;
		
	void Update()
	{
		rCurrentSpeed = Target.GetComponent<ShipController>().CurrentSpeed;
		Debug.Log(rCurrentSpeed);
	}
}
