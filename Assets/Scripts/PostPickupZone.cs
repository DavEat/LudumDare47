using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostPickupZone : SquareZone
{
    public static PostPickupZone inst;
    bool m_pickedup = false;

    void Awake()
    {
        inst = this;
    }
    public override void Activate(bool pickup = false)
    {
        m_pickedup = false;
        base.Activate(true);
    }
    void Update()
    {
        if (!m_pickedup && Inside(PlayerPosition.inst.frontWheel))
        {
            Debug.Log("Load Post");
            DeliveryManager.inst.InitADay();
            gameObject.SetActive(false);
            SoundManager.inst.PlayPickUpPaper();
        }
    }
}
