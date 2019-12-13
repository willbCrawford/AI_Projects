using Project_3.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class CityListRenderer : MonoBehaviour
{
    [SerializeField] GameObject BaseCity;
    List<GameObject> SearchSpace;

    public void OnSearchPathCreated(List<City> searchPath)
    {
        SearchSpace = new List<GameObject>(searchPath.Count);

        for (int i = 0; i < searchPath.Count; i++)
        {
            foreach (Transform child in BaseCity.transform)
                child.gameObject.GetComponent<TextMesh>().text = searchPath[i].Node.ToString();
            //BaseCity.GetComponent<TextMesh>().text = searchPath[i].Node.ToString();
            SearchSpace.Add(Instantiate(BaseCity, new Vector3((float)searchPath[i].X, (float)searchPath[i].Y), BaseCity.transform.rotation));
        }
    }

    public void OnCurrentPathFinal(List<City> currentPath)
    {
        Debug.Log("Received path ended event");
        Vector3 fromCity;
        Vector3 toCity;
        for (int i = 0; i < currentPath.Count; i++)
        {
            //Debug.Log("Currently drawing " + i + " city with node number: " + currentPath[i].Node);

            if (i + 1 < currentPath.Count)
            {
                //Debug.DrawRay(new Vector3((float)currentPath[i].X, (float)currentPath[i].Y), new Vector3((float)currentPath[i + 1].X, (float)currentPath[i + 1].Y), Color.red, 10.0f);
                fromCity = new Vector3((float)currentPath[i].X, (float)currentPath[i].Y);
                toCity = new Vector3((float)currentPath[i + 1].X, (float)currentPath[i + 1].Y);
                //Debug.Log("To " + (i + 1) + " city with node number: " + currentPath[i + 1].Node);

                var gameObject = new GameObject();
                var lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.SetPositions(new Vector3[] { fromCity, toCity });
            }
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
