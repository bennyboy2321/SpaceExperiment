using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour 
{
	public Camera FirstPersonCam;
	public Camera ThirdPersonCam;
	
	public bool ThirdPersonDefault;
	
	void Start()
	{
		DisableAll();
		if (ThirdPersonDefault == true) {
			Enable(ThirdPersonCam);	
		}
		else {
			Enable(FirstPersonCam);
		}
	}
	
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("CameraChange")) {
			Toggle(FirstPersonCam);
			Toggle(ThirdPersonCam);
		}
	}
	
	void Enable(Camera cam)
	{
		cam.enabled = true;
		cam.GetComponent<AudioListener>().enabled = true;
	}
	
	void Disable(Camera cam)
	{
		cam.enabled = false;
		cam.GetComponent<AudioListener>().enabled = false;
	}
	
	void Toggle(Camera cam)
	{
		if(cam.enabled)
			Disable(cam);
		else
			Enable(cam);
	}
	
	void DisableAll()
	{
		Disable(FirstPersonCam);
		Disable(ThirdPersonCam);
	}
}
