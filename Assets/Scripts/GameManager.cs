using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int currentDay = 1;
    [SerializeField] int maxDay = 3;
    public int numDeliveryZone = 8;
    [SerializeField] float increaseDeliveryZone = 1.6f;

    public int numPickupZone = 1;
    [SerializeField] int increasePickupZone = 1;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(ReloadGame(0));
    }

    public void NextDay()
    {
        currentDay++;
        if (currentDay > maxDay)
        {
            Debug.Log("end game");

            Destroy(gameObject);
            SceneManager.LoadScene(0);

            return;
        }

        numDeliveryZone = (int)(numDeliveryZone * increaseDeliveryZone);
        numPickupZone += increasePickupZone;

        StartCoroutine(ReloadGame(.1f));
    }

    IEnumerator ReloadGame(float time)
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(time);
        DeliveryManager.inst.InitADay();
    }

    public LayerMask GroundLayer;
}