using UnityEngine;
using System.Collections;

public class AsteroidRandomizer: MonoBehaviour {
	public float Minsize;
	public float MaxsizeX;
	public float MaxsizeY;
	public float MaxsizeZ;
	public Transform ThisObject;
	
	public bool IsMover;
	public float MaxSpeed;

	void Start()
	{
		float SizeX = Random.Range(Minsize, MaxsizeX);
		float SizeY = Random.Range(Minsize, MaxsizeY);
		float SizeZ = Random.Range(Minsize, MaxsizeZ);
		
		//ThisObject.transform = new Vector3(SizeX,SizeY,SizeZ);
		ThisObject.transform.localScale = new Vector3(SizeX, SizeY, SizeZ);
		//if(mMo != null) {
			//mMo.SetVolume(SizeX * SizeY * SizeZ); 	
		//}
	}
	
	void Update()
	{
		if (IsMover == true)
		{
			var RandomSpeed = Random.Range(0f,MaxSpeed)*Time.deltaTime;
			ThisObject.rigidbody.AddRelativeTorque(new Vector3(RandomSpeed,RandomSpeed,RandomSpeed));
		}
			//print (RandomSpeed);
	}
}
