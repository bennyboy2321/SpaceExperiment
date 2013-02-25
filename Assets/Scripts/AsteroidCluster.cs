using UnityEngine;
using System.Collections;
using System.Linq;

public class AsteroidCluster: MonoBehaviour {
	public Transform AsteroidPrefab;
	public float FieldRadius;
	public float AsteroidMin;
	public float AsteroidMax;
	public Transform CubeGen;
	public float MinAll;
	public float MaxHeight;
	public float MaxWidth;
	public float MaxLength;
	
	float RandomH;
	float RandomW;
	float RandomL;
	
	
	void Start()
	 {
		RandomH = Random.Range(MinAll, MaxHeight);
		RandomW = Random.Range(MinAll, MaxWidth);
		RandomL = Random.Range(MinAll, MaxLength);	
		var AsteroidCount = Random.Range(AsteroidMin,AsteroidMax);
		
		
  		for(int i=0; i<AsteroidCount; i++) 
		{
   		var asteroid = Instantiate(AsteroidPrefab) as Transform;
  			asteroid.parent = transform;
  			asteroid.localPosition = new Vector3(Random.Range(0f, RandomW), Random.Range(0f, RandomH), Random.Range(0f, RandomL));
   			asteroid.localRotation = Random.rotation;
  		}
 	}
	
	void OnDrawGizmos() 
	{
		var center = new Vector3(RandomW-(RandomW/2),RandomH-(RandomH/2),RandomL-(RandomL/2));
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.position+center, new Vector3(RandomW,RandomH,RandomL));
	}
}

