! using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public float NotSpawnAfterMeIncrement = 0;
    public List<AllObjects> objectList;

    private List<GameObject> sceneObjects;
    private List<Vector3> availablePositions; // They are bassicly objects
	public List<Vector3> usedPositions;

	private int nextSegment = 15;

    
    private Transform player;

    private void Start()
    {
        availablePositions = new List<Vector3>();
        usedPositions = new List<Vector3>();
        sceneObjects = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CreatingSegment();
        CreatingSegment();
        CreatingSegment();
    }


    public void CreatingSegment() // Corrected
    {
        AddingNewPositions();

        ClearingUsedPositions();

        SpawningObjects(); // Corrected

    }

    // #1
    void AddingNewPositions()
    {
        for (int z = nextSegment; z < nextSegment + 10; z++)
        {
            for (int x = 0; x < 5; x++)
            {
                bool acquired = false;
                for (int t = 0; t < usedPositions.Count; t++)
                {
                    if (new Vector3(x, 0f, z) == usedPositions[t])
                    {
                        acquired = true;
                        break;
                    }
                }
                if (!acquired)
                    availablePositions.Add(new Vector3(x, 0f, z));
            }
        }
        nextSegment += 10;
    }

    //#2
    void ClearingUsedPositions()
    {
        if (usedPositions.Count != 0)
        {
            for (int u = 0; u < usedPositions.Count; u++)
            {
                if (usedPositions[u].z <= player.position.z)
                    usedPositions.RemoveAt(u);
            }
        }
    }

    //#3
    void SpawningObjects() // Solved!
    {
        for (int o = 0; o < objectList.Count; o++)
        {
            int indexOrder = Random.Range(0, objectList[o].Objects.Count);

            for (int i = 0; i < availablePositions.Count; i++)
            {
                int objectIndex = ChoosingObject(i ,o ,ref indexOrder);
                if (objectIndex != -1)
                {
                    bool Found = false;
                    GameObject SpawnedObject = null;
                    for (int p = 0; p < sceneObjects.Count; p++)
                    {
                        if(sceneObjects[p].name == objectList[o].Objects[objectIndex].prefab.name + "(Clone)" && sceneObjects[p].transform.position.z + 7f <= player.position.z)
                        {
                            Found = true;
                            SpawnedObject = sceneObjects[p];
                            break;
                        }
                    }
                    if (Found == false)
                    {
                        SpawnedObject = Instantiate(objectList[o].Objects[objectIndex].prefab) as GameObject;
                        sceneObjects.Add(SpawnedObject);
                    }

                    SpawnedObject.transform.position = availablePositions[i];
                    
                    if(SpawnedObject.tag == "Coin")
                    {
                        SpawnedObject.GetComponent<Coin>().Get = false;
                        SpawnedObject.transform.Find("Border").gameObject.GetComponent<MeshRenderer>().enabled = true;
                        SpawnedObject.transform.Find("Core").gameObject.GetComponent<MeshRenderer>().enabled = true;
                        SpawnedObject.transform.Find("CoinSplash").gameObject.SetActive(false);
                    }

                    SpawnedObject = null;
                    for (float m = 0; m < objectList[o].Objects[objectIndex].space.x; m++)
                    {
                        for (float v = 0; v < objectList[o].Objects[objectIndex].space.z + NotSpawnAfterMeIncrement; v++)
                        {
                            usedPositions.Add(new Vector3(availablePositions[i].x + m, availablePositions[i].y, availablePositions[i].z + v));
                        }
                    }
                }

            }
        }
        availablePositions.Clear();
    }
       int ChoosingObject(int i, int o, ref int indexOrder)
    {
        bool acquired = false;
        int spawn = -1;

        indexOrder++;
        if (indexOrder >= objectList[o].Objects.Count)
            indexOrder = 0;

        int random = Random.Range(1, 102);
        if (random <= objectList[o].Objects[indexOrder].spawnRate)
        {
            if (availablePositions[i].x <= objectList[o].Objects[indexOrder].availableSpawn)
            {

                for (int t = 0; t < usedPositions.Count; t++)
                {
                    for (float m = 0; m < objectList[o].Objects[indexOrder].space.x; m++)
                    {
                        for (float v = 0; v < objectList[o].Objects[indexOrder].space.z + NotSpawnAfterMeIncrement; v++)
                        {
                            if ((availablePositions[i].x + m) == usedPositions[t].x && (availablePositions[i].z + v) == usedPositions[t].z)
                            {
                                acquired = true;
                                break;
                            }
                        }
                        if (acquired == true)
                            break;
                    }
                    if (acquired == true)
                        break;
                }
                if (acquired == false)
                    spawn = indexOrder;
                else
                    spawn = ChoosingObject(i, o ,ref indexOrder);
            }
        }
        return spawn;
    }

    public void RemoveFromSceneObjects(GameObject ToRemove)
    {
        sceneObjects.Remove(ToRemove);
    }
}