using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    public void SetMenu()
    {
        menu.gameObject.SetActive(true);
    }
    public void OK()
    {
        menu.gameObject.SetActive(false);
    }
}
