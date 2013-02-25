using UnityEngine;
using System.Collections;
using System.Linq;

public class NebulaField: MonoBehaviour {
	public Transform[] NebulaPrefab;
	public float FieldRadius;
	public float NebulaCount;
	//public Transform CubeGen;
	public float MaxHeight;
	public float MaxWidth;
	public float MaxLength;

	public bool RandomColours;
	
	Transform ActiveNebula;
	
	void OnDrawGizmos() {
		var center = new Vector3(MaxWidth-(MaxWidth/2),MaxHeight-(MaxHeight/2),MaxLength-(MaxLength/2));
	Gizmos.color = Color.green;
	Gizmos.DrawWireCube(transform.position+center, new Vector3(MaxWidth,MaxHeight,MaxLength));
	}
	
	
	
	void Start()
	 {
		var ActiveNebula = NebulaPrefab[Random.Range(0, NebulaPrefab.Length)];
		
		
  		for(int i=0; i<NebulaCount; i++) {
   		var nebula = Instantiate(ActiveNebula = NebulaPrefab[Random.Range(0, NebulaPrefab.Length)]) as Transform;
  			nebula.parent = transform;
  			nebula.localPosition = new Vector3(Random.Range(0f, MaxWidth), Random.Range(0f, MaxHeight), Random.Range(0f, MaxLength));
   			nebula.localRotation = Random.rotation;

  }
 }
	
	void Update()
	{

	}
}
