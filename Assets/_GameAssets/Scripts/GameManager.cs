using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gamePanel;
    [SerializeField] SeekerAI[] seekers;
    [SerializeField] AllyAI[] allies;
    [SerializeField] float levelTime = 120f;

    Slider slider;
    BoxCollider winZone;

    float gameTime;

    GameObject door;

    IEnumerator gameTimeTicker;

    private void Awake()
    {
        door = FindObjectOfType<Door>().gameObject;
        slider = FindObjectOfType<Slider>();
        winZone = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        gameTime = levelTime;
        gameTimeTicker = TimeTicker();
        StartCoroutine(gameTimeTicker);
    }

    public void CheckIfCollectedAllALlies()
    {
        int i = 0;
        int arrayLength = allies.Length;
        while (i < arrayLength && allies[i].hasBeenCaptured)
        {
            i++;
        }
        if (i == arrayLength)
        {
            //! Open Door
            door.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Strings.PlayerTag))
        {
            Debug.Log("Win");
            StopAllSeeker();
            StopCoroutine(gameTimeTicker);
            Win();
        }
    }

    IEnumerator TimeTicker()
    {
        while (gameTime > 0f)
        {
            gameTime -= Time.deltaTime;
            slider.value = gameTime / levelTime;
            yield return null;
        }
        Debug.Log("Lost");
        StopAllSeeker();
        Lose();
    }

    private void StopAllSeeker()
    {
        for (int i = 0; i < seekers.Length; i++)
        {
            seekers[i].Idle();
        }
    }

    private void Win()
    {
        winZone.enabled = false;
        gamePanel.SetActive(true);
        gamePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Winner";
    }
    private void Lose()
    {
        winZone.enabled= false;
        gamePanel.SetActive(true);
        gamePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Loser";
    }

}
