using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float damage = 10f;
    public float speed = 5f;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float distance = speed * Time.deltaTime;
        rb.MovePosition(transform.position + transform.forward * distance);
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("collision");
        GameObject obj = collision.gameObject;
        health obj_health = obj.GetComponent<health>();
        if (obj_health != null) {
            Debug.Log("damaging an object");
            obj_health.Damage(damage);
        }
        Destroy(gameObject);
    }
}
