using UnityEngine;
using UnityEngine.Playables;

public class ForcePlay : MonoBehaviour
{
    public PlayableDirector director;

    void Start()
    {
        if (director != null)
            director.Play(); // Force the timeline to play when scene loads
    }
}
