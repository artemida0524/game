using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;

public class InteractibleIndecatorGlobalScene : MonoBehaviour
{
    public IndecatorItem indecatorItemLighting;
    [SerializeField] private TextMeshProUGUI countTime;
    public GlobalCameraInteractible globalCameraInteractible;
    public GlobalCameraMove globalCameraMove;

    public TextMeshProUGUI timerGoing;
    public bool isSetTimerGoing = false;
    public float timerCount = 0;

    public const string PATH_INDECATOR_GLOBAL_SCENE = "indecatorGlobalScene";

    private float timeUpdateNewLighting = 2f; //change
    private float time = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey(PATH_INDECATOR_GLOBAL_SCENE))
        {
            indecatorItemLighting.count.text = PlayerPrefs.GetString(PATH_INDECATOR_GLOBAL_SCENE);
        }
        indecatorItemLighting.count.text = "100";

    }

    private void Update()
    {

        PlayerPrefs.SetString(PATH_INDECATOR_GLOBAL_SCENE, indecatorItemLighting.count.text);



        indecatorItemLighting.image.fillAmount = float.Parse(indecatorItemLighting.count.text) / 100;

        indecatorItemLighting.count.text = $"{Mathf.Clamp(int.Parse(indecatorItemLighting.count.text), 0, 100)}";




        time += Time.deltaTime;

        if (time > timeUpdateNewLighting)
        {
            indecatorItemLighting.count.text = $"{int.Parse(indecatorItemLighting.count.text) + 1}";
            time = 0;
        }

        if (indecatorItemLighting.count.text != "100")
        {

            countTime.text = $"{(int)time}";
        }

        if (isSetTimerGoing)
        {
            SetTime();
        }
    }
    private void SetTime()
    {
        globalCameraInteractible.camera.GetComponent<GlobalCameraMove>().isInteractable = false;
        timerCount -= Time.deltaTime;

        if (timerCount <= 0)
        {
            timerCount = 0;
            isSetTimerGoing = false;

            GameManagerGlobalScene.lastVisitesScene = globalCameraInteractible.lastCheckComponent.nameScene;
            globalCameraMove.isInteractable = true;
        }
        int time = (int)timerCount;
        timerGoing.text = DataUpdater.TimeConvert(time);
    }

}