using UnityEngine;
public class ShowRandom2 : MonoBehaviour
{
    [Header("Random Choice")]
    public GameObject random1;
    public GameObject random2;
    public GameObject random3;
    public GameObject random4;
    public GameObject random5;
    public GameObject random6;



    private GameObject[] random;

    void Start()
    {
        random = new[] { random1, random2, random3, random3, random4, random5, random6 };
        DisplayRandom();

    }


    void Update()
    {

    }

    void DisplayRandom()
    {
        int randomIndex = Random.Range(0, random.Length);
        GameObject randomShow = random[randomIndex];

        randomShow.SetActive(true);


    }
}
