using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour {

    public float health_;

    private void Update()
    {
        if (health_ < 0) {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage_) {
        health_ -= damage_;
    }
}
