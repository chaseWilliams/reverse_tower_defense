using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class troop : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float weapon_damage;

    private bool paused;
    private RaycastHit hit;
    private NavMeshAgent agent;
    private Transform target;

    public Transform stronghold;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = stronghold.position;

    }

    private void Update()
    {

    }


    void Shoot () {
		if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
		{
            GameObject object_hit = hit.transform.gameObject;
            if (object_hit.tag == "turret_part") {
                object_hit.GetComponentInParent<turret>().SendMessage("Damage", weapon_damage);
            }
                
		}
    }
}
