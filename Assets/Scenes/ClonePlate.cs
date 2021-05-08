using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePlate : MonoBehaviour
{
    public Transform cloneLocation;

    private void OnTriggerEnter(Collider other) {
        // On clone "l'autre" à une position précise (cloneLocation).
        Vector3 position = cloneLocation.position;
        Quaternion rotation = Quaternion.identity;
        Instantiate(other.gameObject, position, rotation);

        // activation / désactivation de certains enfants.
        transform.Find("Cube").gameObject.SetActive(false);
        transform.Find("Top").gameObject.SetActive(true);
        
        // Destruction du collider (n'importe lequel) associé à clonePlate.
        Destroy(GetComponent<Collider>());
    }
}