using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{	[Header("Mouse Sensetivity")]
	[SerializeField] float sensX;
	[SerializeField] float sensY;

	public Transform orientation;

	float xRotation;
	float yRotation;
	// Start is called before the first frame update
	void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
		//get mouse input

		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

		yRotation += mouseX;
		xRotation -= mouseY;

		xRotation = Mathf.Clamp(xRotation, -90, 90);

		//rotate cam and orientation
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);

	}

	public void ChangeCamRotation(Vector3 _newRotation)
    {
		transform.rotation = Quaternion.Euler(_newRotation.x, _newRotation.y, 0);
		orientation.rotation = Quaternion.Euler(0, _newRotation.y, 0);
	}
}
