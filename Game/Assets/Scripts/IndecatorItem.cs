using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IndecatorItem : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI count;
    [SerializeField] public Image image;

    public IndecatorItem(string count)
    {
        this.count.text = count;

    }
}
