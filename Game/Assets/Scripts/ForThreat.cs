using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class ForThreat : MonoBehaviour
{
    private const string PATH = "HPTHREAT";
    Color colorDetect = new Color(1f, 0f, 0f, 255f);
    Color colorDefault = new Color(1f, 1f, 1f, 255f);
    private int bulletHp = 13;
    [SerializeField] Material material;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] private int hp;
    [Serializable]
    class HP
    {
        public int hp;

        public HP(int hp)
        {
            this.hp = hp;
        }
    }


    private void Awake()
    {
        if (PlayerPrefs.HasKey(PATH))
        {
            string json = PlayerPrefs.GetString(PATH);
            HP value = JsonUtility.FromJson<HP>(json);
            hp = value.hp;
        }

    }

    private void Start()
    {
        material.color = colorDefault;
        hpText.text = hp.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "bullet")
        {
            hp -= bulletHp;

            if(hp <= 0)
            {
                Destroy(gameObject);
            }

            HP value = new HP(hp);

            string json = JsonUtility.ToJson(value);

            PlayerPrefs.SetString(PATH, json);

            hpText.text = hp.ToString();
            material.color = colorDetect;
            StartCoroutine(reverse());

        }

        if(collision.collider.tag == "Player")
        {
            collision.collider.gameObject.transform.position = new Vector3(532.84f, 389.85f, 1028.44f);
        }
    }
    private IEnumerator reverse()
    {
        yield return new WaitForSeconds(0.1f);
        material.color = colorDefault;
    }
}
