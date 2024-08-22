using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryManager : MonoBehaviour
{
    Category[] categories;

    TextMeshProUGUI categoryNumberDisplay;
    Image backImage;
    Image nextImage;

    int categoryNumber;
    int categoriesLength;


    private void OnEnable()
    {
        if (categoryNumberDisplay != null && backImage != null && nextImage != null && categories.Length > 0)
        {
            if (categoryNumber != 0)
                return;

            backImage.color = Color.gray;
            backImage.gameObject.GetComponent<Button>().interactable = false;

            nextImage.color = Color.black;
            nextImage.gameObject.GetComponent<Button>().interactable = true;
            return;
        }

        

        categoryNumberDisplay = GameObject.FindGameObjectWithTag("LevelNumberDisplay").GetComponent<TextMeshProUGUI>();

        GameObject[] menuButtons = GameObject.FindGameObjectsWithTag("LevelMenuButton");
        backImage = menuButtons[0].GetComponent<Image>();
        nextImage = menuButtons[1].GetComponent<Image>();

        categoryNumber = 0;

        //------------------

        backImage.color = Color.gray;
        backImage.gameObject.GetComponent<Button>().interactable = false;

        nextImage.color = Color.black;
        nextImage.gameObject.GetComponent<Button>().interactable = true;

        categoriesLength = transform.childCount;
        categories = new Category[categoriesLength];
        for (int i = 0; i < categoriesLength; i++)
        {
            categories[i] = transform.GetChild(i).GetComponent<Category>();
        }

        categoryNumberDisplay.text = (categoryNumber + 1).ToString();
    }
    //private void Awake()
    //{
        
    //}
    //private void Start()
    //{
        
    //}

    public int CategoryNumber(Category category)
    {
        for (int i = 0; i < categoriesLength; i++)
        {
            if (categories[i] == category)
            {
                return i;
            }
        }
        Debug.LogError("The game just got bombed 'Category Manager in levels' ");
        return -1;
    }

    public void Next()
    {
        if (categoryNumber == categoriesLength - 1)
            return;

        categories[categoryNumber].gameObject.SetActive(false);
        //! Indent the category number and set the page according to it
        categoryNumber++;
        categories[categoryNumber].gameObject.SetActive(true);
        categoryNumberDisplay.text = (categoryNumber + 1).ToString();

        CheckIfButtonIsInteractableBack();
    }
    public void Back()
    {
        if (categoryNumber == 0)
            return;

        categories[categoryNumber].gameObject.SetActive(false);
        //! Outdent the category number and set the page according to it
        categoryNumber--;
        categories[categoryNumber].gameObject.SetActive(true);
        categoryNumberDisplay.text = (categoryNumber + 1).ToString();

        CheckIfButtonIsInteractableNext();
    }
    public void CheckIfButtonIsInteractableNext()
    {
        if (categoryNumber == categoriesLength - 1)
        {
            nextImage.color = Color.gray;
            nextImage.gameObject.GetComponent<Button>().interactable = false;
            return;
        }
        nextImage.color = Color.black;
        nextImage.gameObject.GetComponent<Button>().interactable = true;
    }
    public void CheckIfButtonIsInteractableBack()
    {
        if (categoryNumber == 0)
        {
            backImage.color = Color.gray;
            backImage.gameObject.GetComponent<Button>().interactable = false;
            return;
        }
        backImage.color = Color.black;
        backImage.gameObject.GetComponent<Button>().interactable = true;
    }
}
