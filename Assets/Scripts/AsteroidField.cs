using UnityEngine;
using System.Collections;
using System.Linq;

public class AsteroidField: MonoBehaviour {
	public Transform ClusterPrefab;
	public float FieldRadius;
	public float AsteroidCount;
	public Transform CubeGen;
	public float MaxHeight;
	public float MaxWidth;
	public float MaxLength;
	
	void OnDrawGizmos() {
		var center = new Vector3(MaxWidth-(MaxWidth/2),MaxHeight-(MaxHeight/2),MaxLength-(MaxLength/2));
	Gizmos.color = Color.red;
	Gizmos.DrawWireCube(transform.position+center, new Vector3(MaxWidth,MaxHeight,MaxLength));

	}
	
	
	
	void Start()
	 {
  		for(int i=0; i<AsteroidCount; i++) {
   		var cluster = Instantiate(ClusterPrefab) as Transform;
  			cluster.parent = transform;
  			cluster.localPosition = new Vector3(Random.Range(0f, MaxWidth), Random.Range(0f, MaxHeight), Random.Range(0f, MaxLength));
   			//cluster.localRotation = Random.rotation;
  }
 }
}
