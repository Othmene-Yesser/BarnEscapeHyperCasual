using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinSelector : MonoBehaviour
{
    [SerializeField] GameObject[] Skins;

    private void Awake()
    {
        int skinIndex = Random.Range(0, Skins.Length);
        Instantiate(Skins[skinIndex], transform.position, transform.rotation);
    }
}
