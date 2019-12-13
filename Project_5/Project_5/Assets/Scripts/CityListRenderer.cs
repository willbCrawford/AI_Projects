using Assets.Scripts;
using Project_3.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class CityListRenderer : MonoBehaviour
{
    [SerializeField] GameObject BaseCity;
    List<GameObject> SearchSpace;
    int SearchSpaceSize;
    GameObject[] Roads;

    public void OnSearchPathCreated(List<IChromosome<City>> searchPath)
    {
        SearchSpace = new List<GameObject>(searchPath.Count);
        SearchSpaceSize = searchPath.Count;
        Roads = new GameObject[SearchSpaceSize + 1];

        for (int i = 0; i < searchPath.Count; i++)
        {
            foreach (Transform child in BaseCity.transform)
                child.gameObject.GetComponent<TextMesh>().text = searchPath[i].GetT().Node.ToString();
            //BaseCity.GetComponent<TextMesh>().text = searchPath[i].Node.ToString();
            SearchSpace.Add(Instantiate(BaseCity, new Vector3((float)searchPath[i].GetT().X, (float)searchPath[i].GetT().Y), BaseCity.transform.rotation));

            Roads[i] = new GameObject();
            Roads[i].AddComponent<LineRenderer>();
        }
        Roads[Roads.Length - 1] = new GameObject();
        Roads[Roads.Length - 1].AddComponent<LineRenderer>();
    }

    public void OnCurrentPathFinal(List<IChromosome<City>> currentPath)
    {
        Vector3 fromCity;
        Vector3 toCity;
        for (int i = 0; i < currentPath.Count; i++)
        {
            //Debug.Log("Currently drawing " + i + " city with node number: " + currentPath[i].Node);

            if (i + 1 < currentPath.Count)
            {
                //Debug.DrawRay(new Vector3((float)currentPath[i].X, (float)currentPath[i].Y), new Vector3((float)currentPath[i + 1].X, (float)currentPath[i + 1].Y), Color.red, 10.0f);
                fromCity = new Vector3((float)currentPath[i].GetT().X, (float)currentPath[i].GetT().Y);
                toCity = new Vector3((float)currentPath[i + 1].GetT().X, (float)currentPath[i + 1].GetT().Y);

                Roads[i].GetComponent<LineRenderer>().SetPositions(new Vector3[] { fromCity, toCity });
            }
        }
        Roads[Roads.Length - 1].GetComponent<LineRenderer>().SetPositions(new Vector3[] {
                                                                            new Vector3((float)currentPath[currentPath.Count - 1].GetT().X, 
                                                                                        (float)currentPath[currentPath.Count - 1].GetT().Y),
                                                                            new Vector3((float)currentPath[0].GetT().X, (float)currentPath[0].GetT().Y) });
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
