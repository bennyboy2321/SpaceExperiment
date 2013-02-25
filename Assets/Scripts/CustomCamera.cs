using UnityEngine;
using System.Collections;

public class CustomCamera : MonoBehaviour 
{
	
	public float Var1;
	public float Var2; //distance
	public float Var3;
	
	public float Var4;
	
	public float Rate;
	
	public float MinDist = 10f;
	public float MaxDist = 40f;
	
	public float MaxVer = 10f;
	public float MinVer = 60f;
	public Transform target; // drag the target here (or find it at Start)
	public Transform ThisCamera;
	void start()
	{


	}
	
	void Update()
	{
		if(Input.GetAxisRaw("CamZoom") > 0)
		{
			Var2 += (Rate/6) * Time.deltaTime;
			
			if (Var2 >= MaxDist)
			{
				Var2 = MaxDist;	
			}
		}
		
		
		if(Input.GetAxisRaw("CamZoom") < 0)
		{
			Var2 -= (Rate/6) * Time.deltaTime;
			
			if (Var2 <= MinDist)
			{
				Var2 = MinDist;	
			}
		}
		
				if(Input.GetAxisRaw("CamRotateVer") < 0)
		{
				Var4 -= (Rate/2)*Time.deltaTime;
				if (Var4 <= MaxVer)
			{
				Var4 = MaxVer;
			}

		}
		
		
		if(Input.GetAxisRaw("CamRotateHor") < 0)
		{
			Var1 += (Rate/2)*Time.deltaTime;
			if (Var1 == 360f)
			{
				Var1 = 0f;
			}

		}
		
		
		if(Input.GetAxisRaw("CamRotateHor") > 0)
		{
			Var1 -= (Rate/2)*Time.deltaTime;
			if (Var1 == 0f)
			{
				Var1 = 360f;
			}

		}
		
		
		if(Input.GetAxisRaw("CamRotateVer") > 0)
		{
			Var4 += (Rate/2)*Time.deltaTime;
			if (Var4 >= MinVer)
			{
				Var4 = MinVer;
			}

		}
	}
	void LateUpdate(){
		//Vector3 offset = new Vector3(Var1, Var2, Var3); // Dunno / Angle / Distance
 		//Quaternion camerarotate = Quaternion.Euler(new Vector3(Var4, 0, 0));
 		//transform.position = target.position + offset;
		//transform.rotation = camerarotate;
		
		Vector3 Zoom = new Vector3(0f,Var2,0f);
		transform.position = target.position + Zoom;
		transform.RotateAround(target.position, Vector3.left, Var4);
		transform.RotateAround(target.position, Vector3.up, Var1);
		//Quaternion CamRotation = Quaternion.Euler(new Vector3(Var4,0f,0f));
		//transform.rotation = CamRotation;
		
 		transform.LookAt(target);
}
}
