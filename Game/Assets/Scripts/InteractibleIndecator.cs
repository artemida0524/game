using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

[Serializable]
public class Indecator
{
    public int[] counts;
}


public class InteractibleIndecator : MonoBehaviour
{
    private const string INDECATOR_PATH = "Indecator";
    [SerializeField] public IndecatorItem[] itemsIndecator;
    [SerializeField] private Image deathImage;
    [SerializeField] private GameObject buttonRevival;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject mainPlayer;
    private TakeObject takeObject;
    private Color eclipse;
    private Animator animator;

    private float timeOut = 30f; //change
    private float time;

    private float timeOutHealthUp = 3f;
    private float timeHealthUp;

    private float timeOutHealthDown = 2f;
    private float timeHealthDown;

    private bool isDeathAnimation = true;
    private bool isRevival = false;
    private bool isRevivalAnimation = true;

    private void Start()
    {
        takeObject = GetComponent<TakeObject>();
        animator = GetComponent<Animator>();
        if (PlayerPrefs.HasKey(INDECATOR_PATH))
        {
            Indecator newData = new Indecator();
            newData.counts = new int[itemsIndecator.Length];

            string json = PlayerPrefs.GetString(INDECATOR_PATH);

            newData = JsonUtility.FromJson<Indecator>(json);

            for (int i = 0; i < itemsIndecator.Length; i++)
            {
                itemsIndecator[i].count.text = $"{newData.counts[i]}";
            }
        }

        itemsIndecator[0].count.text = "100"; //health
        itemsIndecator[1].count.text = "100";  //food
        itemsIndecator[2].count.text = "100";  //water

    }

    private void Update()
    {
        time += Time.deltaTime;
        timeHealthUp += Time.deltaTime;
        timeHealthDown += Time.deltaTime;




        if (timeHealthUp > timeOutHealthUp)
        {
            if (itemsIndecator[1].count.text == "100" && int.Parse(itemsIndecator[2].count.text) >= 50)
            {
                itemsIndecator[0].count.text = $"{int.Parse(itemsIndecator[0].count.text) + 1}";
            }
            timeHealthUp = 0f;
        }

        if (time > timeOut)
        {
            for (int i = 1; i <= 2; i++)
            {
                itemsIndecator[i].count.text = $"{int.Parse(itemsIndecator[i].count.text) - 1}";
            }
            time = 0;
        }


        if (int.Parse(itemsIndecator[1].count.text) == 0 || int.Parse(itemsIndecator[2].count.text) == 0)
        {
            if (timeHealthDown > timeOutHealthDown)
            {
                itemsIndecator[0].count.text = $"{int.Parse(itemsIndecator[0].count.text) - 1}";
                timeHealthDown = 0f;
            }
        }



        foreach (var item in itemsIndecator)
        {
            item.image.fillAmount = float.Parse(item.count.text) / 100;

            item.count.text = $"{Mathf.Clamp(int.Parse(item.count.text), 0, 100)}";

        }

        if (itemsIndecator[0].count.text == "0")
        {
            if (isDeathAnimation)
            {
                animator.SetTrigger("Death");
                isDeathAnimation = false;
            }
            Death();

        }

        if(isRevival)
        {
            if (isRevivalAnimation)
            {
                animator.SetTrigger("Revival");
                isRevivalAnimation = false;
            }
            Revival();
        }


        Indecator newData = new Indecator();

        newData.counts = new int[itemsIndecator.Length];

        for (int i = 0; i < itemsIndecator.Length; i++)
        {
            newData.counts[i] = int.Parse(itemsIndecator[i].count.text);
        }

        string json = JsonUtility.ToJson(newData);

        PlayerPrefs.SetString(INDECATOR_PATH, json);
    }


    public void Death()
    {
        takeObject.isTake = false;
        gameManager.SetActive(false);
        //GetComponent<Player1>().enabled = false;
        eclipse += new Color(0, 0, 0, Time.deltaTime * 0.3f);

        deathImage.color = eclipse;

        if(eclipse.a >= 1)
        {
            eclipse.a = 1;

            itemsIndecator[0].count.text = "100";

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            buttonRevival.SetActive(true);
        }
    }

    public void SetBoolIsRevival()
    {
        isRevival = true;
    }

    private void Revival()
    {
        mainPlayer.transform.position = new Vector3(74.55f, 9.77f, 480.33f);

        takeObject.inventoryData.inventory.Clear();
        takeObject.isTake = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        buttonRevival.SetActive(false);

        eclipse -= new Color(0, 0, 0, Time.deltaTime * 0.3f);

        deathImage.color = eclipse;

        itemsIndecator[1].count.text = "100";
        itemsIndecator[2].count.text = "100";

        Destroy(takeObject.objInHand1);

        Destroy(takeObject.objInHand2);

        takeObject.objInHand1 = null;
        takeObject.objInHand2 = null;

        if (eclipse.a <= 0)
        {
            isRevival = false;
            //GetComponent<Player1>().enabled = true;
            gameManager.SetActive(true);
        }
        
    }

}
