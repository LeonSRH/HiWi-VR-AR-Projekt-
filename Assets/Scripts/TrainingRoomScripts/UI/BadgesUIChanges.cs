using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgesUIChanges : MonoBehaviour
{
    private Transform player;

    public Transform Aufgaben;
    public Transform Zombies;

    public Transform aufgabenBar;
    public Transform zombiesBar;

    public Text aufgabenText;
    public Text zombiesText;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("3rdPersonPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        setZombiesCounter();
        setAufgabenCounter();

        if (player.GetComponent<PlayerPoints>().getQuestsCompleted() < 3)
        {
            Aufgaben.GetChild(1).gameObject.SetActive(true);
            Aufgaben.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Aufgaben.GetChild(0).gameObject.SetActive(true);
            Aufgaben.GetChild(1).gameObject.SetActive(false);
        }

        if (player.GetComponent<PlayerPoints>().getZombiesHealed() < 3)
        {
            Zombies.GetChild(1).gameObject.SetActive(true);
            Zombies.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Zombies.GetChild(0).gameObject.SetActive(true);
            Zombies.GetChild(1).gameObject.SetActive(false);
        }

    }

    private void setZombiesCounter()
    {
        float zombieAmount = (float)player.GetComponent<PlayerPoints>().getZombiesHealed() * (0.33f);
        zombiesBar.GetComponent<Image>().fillAmount = zombieAmount;
        zombiesText.text = "" + player.GetComponent<PlayerPoints>().getZombiesHealed() + "/3";

    }

    private void setAufgabenCounter()
    {
        float questAmount = (float)player.GetComponent<PlayerPoints>().getQuestsCompleted() * (0.33f);
        aufgabenBar.GetComponent<Image>().fillAmount = questAmount;

        aufgabenText.text = "" + player.GetComponent<PlayerPoints>().getQuestsCompleted() + "/3";
    }
}
