using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class player_motor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camera_rotation = Vector3.zero;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move (Vector3 _velocity) {
        velocity = _velocity;
    }

	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

    public void RotateCamera(Vector3 _camera_rotation) {
        camera_rotation = _camera_rotation;
    }

    private void FixedUpdate()
    {
        // movement
        if (velocity != Vector3.zero) {
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }

        // player rotation
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        // camera rotation
        if (cam != null) {
            cam.transform.Rotate(-camera_rotation);
        }
    }
}
