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

        m_screenSize = new Vector2(Screen.width, Screen.height);
    }

    Vector2 m_screenSize;
    bool waitscreenNotMoving = false;

    void Update()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        if (m_screenSize != screenSize)
        {
            m_screenSize = screenSize;
            waitscreenNotMoving = true;
            //LoopEditor.inst.Recreate();
            Debug.Log("resize");
        }
        else if (waitscreenNotMoving)
        {
            waitscreenNotMoving = false;
            LoopEditor.inst.Recreate();
        }
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
        PostPickupZone.inst.Activate();
    }

    public LayerMask GroundLayer;
}