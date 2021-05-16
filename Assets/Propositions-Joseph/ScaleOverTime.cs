using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    float life = 0;
    float lifeMax = 1;
    float initialScale = 1;

    public void Initialize(float lifeMax, float initialScale) {
        this.lifeMax = lifeMax;
        this.initialScale = initialScale;
    }

    // Update is called once per frame
    void Update() {
        
        // Ce script a pour but de redimensionner le gameObject associé en fonction
        // du temps écoulé.
        // La fonction mathématique (1 - t ^ 8) employée peut être visualisée ici :
        // https://www.desmos.com/calculator/z103ozusfm?lang=fr

        life += Time.deltaTime;
        float progress = life / lifeMax;
        float timeScale = 1 - Mathf.Pow(progress, 8);
        transform.localScale = Vector3.one * timeScale * initialScale;    
    }
}
