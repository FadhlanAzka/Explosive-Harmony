using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StrikeController : MonoBehaviour
{
    public TextMeshProUGUI StrikesIndicator;

    public int StrikeCount = 0;
    public int StrikeLimit = 0;

    void Start()
    {
        StrikesIndicator.text = "";
        StrikesIndicator.color = Color.red;  // Mengatur warna teks menjadi merah
    }

    void Update()
    {

    }

    public void getStrike()
    {
        StrikeCount++;
        StrikeLimit++;

        if (StrikeCount > 3)
        {
            StrikeCount = 3;
        }

        StrikesIndicator.text = new string('X', StrikeCount);
    }
}