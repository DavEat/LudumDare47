using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    Score[] scoresByDay = new Score[3];

    Score[] needScoreForStar = new Score[]
    {
        new Score(90, 48, 16),
        new Score(120, 120, 30),
        new Score(120, 120, 30)
    };

    void Awake()
    {
        scoresByDay = new Score[]
        {
             new Score(),  new Score(),  new Score()
        };
    }

    public void DisplayScore(bool end)
    {
        int day = GameManager.inst.currentDay - 1;

        float dst  = scoresByDay[day].dst;
        float time = scoresByDay[day].time;
        int   inte = scoresByDay[day].inte;

        byte starCount = 0;

        if (dst  <= needScoreForStar[day].dst)  starCount |= 1;
        if (time <= needScoreForStar[day].time) starCount |= 2;
        if (inte <= needScoreForStar[day].inte) starCount |= 4;

        CanvasManager.inst.Display_ScoreScreen(dst, time, inte, starCount, end);
    }

    public void AddDst(float dst)
    {
        if (GameManager.inst.dayEnded) return;
        int day = GameManager.inst.currentDay - 1;
        scoresByDay[day].dst += dst;
    }
    public void AddTime(float time)
    {
        if (GameManager.inst.dayEnded) return;
        scoresByDay[GameManager.inst.currentDay - 1].time += time;
    }
    public void AddInteraction()
    {
        if (GameManager.inst.dayEnded) return;
        scoresByDay[GameManager.inst.currentDay - 1].inte++;
    }

    public void ResetCurrentDay()
    {
        scoresByDay[GameManager.inst.currentDay - 1].Reset();
    }

    [System.Serializable]
    public class Score
    {
        public float dst;
        public float time;
        public int inte;

        public Score()
        {
            this.dst  = 0;
            this.time = 0;
            this.inte = 0;
        }
        public Score(float dst, float time, int inte)
        {
            this.dst = dst;
            this.time = time;
            this.inte = inte;
        }
        public void Reset()
        {
            this.dst = 0;
            this.time = 0;
            this.inte = 0;
        }
    }
}