using UnityEngine;

class BoxGenerator : MonoBehaviour
{
    public int boxCount { get; private set; }
    
    [SerializeField] private GameObject boxPrefab;

    void Start()
    {
        boxCount = 0;
    }

    public void PlaceBox(Vector3 position, Vector3 velocity)
    {
        GameObject generated = Instantiate(boxPrefab, position, Quaternion.identity);
        generated.GetComponent<BoxBehaviour>().Thrown(velocity);
        ++boxCount;
    }
}
