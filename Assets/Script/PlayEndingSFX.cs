using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayEndingSFX : MonoBehaviour
{

    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {

        score = ScoreManager.Instance.GetScore();

        if (score >= 20)
        {
            AudioManager.Instance?.PlayBounce();
        }
        else if (score >= 15)
        {
            AudioManager.Instance?.PlayGhostDie();
        }
        else
        {
            AudioManager.Instance?.PlayTrack3();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
