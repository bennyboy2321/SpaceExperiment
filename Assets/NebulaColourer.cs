using UnityEngine;
using System.Collections;

public class NebulaColourer : MonoBehaviour {
	
	public Color[] ColourChoices;
	public Transform Nebula;
	// Use this for initialization
	void start () 
	{
	 	Nebula.renderer.material.color = new Color(Random.Range(0f,255f),Random.Range(0f,255f),Random.Range(0f,255f),1);
	}
	
}
