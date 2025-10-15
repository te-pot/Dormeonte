using UnityEngine;
public class ShowRandom : MonoBehaviour
{
    [Header("Random Choice")]
    public GameObject random1;
    public GameObject random2;
    public GameObject random3;
  

    private GameObject[] random;

    void Start()
    {
        random = new[] { random1, random2, random3 };
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
