using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayerOnDestroy : MonoBehaviour {

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    public void UpdateSpawnPoint() {
        Debug.Log($"New spawn point! {spawnPosition}");
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;    
    }

    public void Respawn() {
        // Le gameObject actuellement sur le point d'être détruit est cloné, mais...
        GameObject clone = Instantiate(gameObject, spawnPosition, spawnRotation);

        // ... c'est un poil plus tricky qu'espéré :
        // Lorsque que le clone est réalisé le gameObject source (celui qui va être détruit) 
        // a déjà été désactivé par le moteur de jeu. Les composants associés ont aussi été 
        // désactivés.
        // Il faut donc:

        // 1. Réactiver le gameObject lui-même.
        clone.SetActive(true);
   
        // 2. Réactiver les composants associés.
        // Astuce: en ciblant "clone.GetComponents<MonoBehaviour>()" nous demandons à Unity 
        // de nous retourner tous les composants de type MonoBehaviour, or tous les scripts 
        // réalisés dans Unity "étendent" Monobehaviour (cf ligne 5), nous récupérons donc 
        // ici tous les composants, quelqu'ils soient, et nous les réactivons.
        foreach(var component in clone.GetComponents<MonoBehaviour>()) {
            component.enabled = true;
        }
    }

    void Start() {
        UpdateSpawnPoint();
    }

    bool applicationIsQuitting = false;
    void OnApplicationQuit() {
        applicationIsQuitting = true;
    }

    void OnDestroy() {
        // Si l'application est sur le point de quitter (ex sortie du mode play dans l'éditeur).
        // alors on se casse de la méthode "OnDestroy" sans plus attendre ("return").
        if (applicationIsQuitting) {
            return;
        }

        // Sinon on respawn le gameObject associé.
        Respawn();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Respawn") == true) {
            UpdateSpawnPoint();
        }
    }
}
