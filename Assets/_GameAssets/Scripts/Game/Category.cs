using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Category : MonoBehaviour
{
    Level[] levels;
    CategoryManager categoryManager;

    public int categoryIndentaion;

    private void Awake()
    {
        levels = new Level[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            levels[i] = transform.GetChild(i).GetComponent<Level>();
        }

        categoryManager = transform.parent.GetComponent<CategoryManager>();
    }

    private void Start()
    {
        categoryIndentaion = categoryManager.CategoryNumber(this);

        for (int i = 0; i < transform.childCount; i++)
        {
            levels[i].AssignValuesForLevel(i + (categoryIndentaion * 12));
        }
    }

    public void ClearAllKeysPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(Strings.CoinTag, 74);
    }
}
