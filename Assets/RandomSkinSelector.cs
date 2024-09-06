using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinSelector : MonoBehaviour
{
    [SerializeField] GameObject[] Skins;

    private void Awake()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        int skinIndex = Random.Range(0, Skins.Length);
        Instantiate(Skins[skinIndex], transform.position, transform.rotation);
    }
}
