using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(player_motor))]
public class player : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float look_sensitivity = 3f;
	[SerializeField]
	private Camera cam;
    [SerializeField]
    private float weapon_damage;

    private player_motor motor;
    private bool paused;
    private RaycastHit hit;


    private void Start()
    {
        motor = GetComponent<player_motor>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            paused = !paused;
        }

        PerformMovement();


        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void PerformMovement() {

        if (paused) {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            
            motor.Move(Vector3.zero);
            motor.Rotate(Vector3.zero);
            motor.RotateCamera(Vector3.zero);
            return;
        }

		if (Cursor.lockState != CursorLockMode.Locked)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		float xMove = Input.GetAxisRaw("Horizontal");
		float yMove = Input.GetAxisRaw("Vertical");

		Vector3 move_horizontal = transform.right * xMove;
		Vector3 move_vertical = transform.forward * yMove;

		Vector3 velocity = (move_vertical + move_horizontal).normalized * speed;

		motor.Move(velocity);

		// player rotation

		float yRot = Input.GetAxisRaw("Mouse X");

		Vector3 rotation = new Vector3(0f, yRot, 0) * look_sensitivity;

		motor.Rotate(rotation);

		// camera rotation

		float xRot = Input.GetAxisRaw("Mouse Y");

		Vector3 camera_rotation = new Vector3(xRot, 0f, 0f) * look_sensitivity;

		motor.RotateCamera(camera_rotation);
    }

    void Shoot () {
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10f))
		{
            GameObject object_hit = hit.transform.gameObject;
            if (object_hit.tag == "turret") {
                object_hit.GetComponentInParent<turret>().SendMessage("Damage", weapon_damage);
            }
                
		}
    }
}
