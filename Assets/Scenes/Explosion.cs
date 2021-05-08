using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int count = 16;
    public GameObject shard;

    public void Explode() {

        if (shard == null) {
            Debug.LogWarning("Ne peut pas exploser : \"shard\" est null !!!");
            return;
        }

        for (int i = 0; i < count; i += 1) {
            Instantiate(shard, transform.position, Quaternion.identity);
        }
    }

    public bool trigger = false;
    void Update() {
        if (trigger == true) {
            trigger = false;
            Explode();
        }
    }
}