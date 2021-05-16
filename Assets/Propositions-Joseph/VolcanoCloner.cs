using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoCloner : MonoBehaviour
{
    public GameObject source;

    [Range(0.05f, 1f)]
    public float fixedDelay = 0.1f;

    [Range(0f, 5f)]
    public float variableDelay = 0.1f;

    [Range(1, 10)]
    public int cloneCountPerEruption = 3;

    void Start() {
        // C'est la partie compliqué de Unity, pour faire appel à une fonction 
        // asynchrone en C# il faut utiliser la méthode "StartCoroutine".
        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
        StartCoroutine(WaitAndClone());
    }

    void MakeAClone() {
        // Cette fonction sert à créer UN clone.
        // Cela commence par la création du clone...
        Vector3 position = transform.position;
        Quaternion rotation = Random.rotation;
        GameObject clone = Instantiate(source, position, rotation);
        
        // ...cela continue avec l'initialisaton du composant "ScaleOverTime"...
        float lifeMax = 3f;
        float scale = Random.Range(0.5f, 1f);
        clone.GetComponent<ScaleOverTime>().Initialize(lifeMax, scale);

        // et cela termine avec la destruction programmée du clone (lorsque le délai
        // "lifeMax" se sera écoulé).
        Destroy(clone, lifeMax);
    }

    IEnumerator WaitAndClone() {
        // Cette méthode commence par "patienter" un certain temps (le délai d'attente
        // est la somme d'une partie fixe avec une partie aléatoire variable)...
        float delay = fixedDelay + Random.Range(0, variableDelay);
        yield return new WaitForSeconds(delay);

        // ...avant de créer les clones
        if (source != null) {
            for (int i = 0; i < cloneCountPerEruption; i++) {
                MakeAClone();
            }
        }

        yield return WaitAndClone();
    }

    private void OnDrawGizmos() {
        // Pour debug :
        // Dessine un cube rouge à l'emplacement du "Volcan".
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
