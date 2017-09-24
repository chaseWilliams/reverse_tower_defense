using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour {

    public Transform target;
    public Transform part_to_rotate;
    public GameObject bullet_prefab;
    public float rotate_speed = 3f;
    public float health = 20f;
    public float range = 10f;
    public float fire_rate = 3f;
    public float damage = 2f;

    private float shooting_countdown = 0;
    private Vector3 bullet_fire_offset;

    void Start() {
        part_to_rotate = transform.Find("Head");
        bullet_fire_offset = new Vector3(0, 0, 1.04f);
    }

	void Update()
	{
        UpdateTarget();
        if (target == null){
            return;
        }


		Vector3 dir = target.position - part_to_rotate.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(part_to_rotate.rotation, lookRotation, Time.deltaTime * rotate_speed).eulerAngles;
		part_to_rotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

		if (shooting_countdown <= 0f)
		{
			Shoot();
			shooting_countdown = fire_rate;
		}

		shooting_countdown -= Time.deltaTime;
	}

    void Shoot() {
        Instantiate(bullet_prefab, part_to_rotate.transform.position + bullet_fire_offset, Quaternion.LookRotation(part_to_rotate.transform.forward));
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest_enemy = null;
        float closest_disance = Mathf.Infinity;
        float enemy_distance;
        foreach (var enemy in enemies)
        {
            enemy_distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (enemy_distance < closest_disance && enemy_distance < range)
            {
                closest_disance = enemy_distance;
                closest_enemy = enemy;
            }
        }

        if (closest_enemy != null)
        {
            Debug.Log("target found");
            target = closest_enemy.transform;
        }
        else
        {
            target = null;
        }

    }

}
