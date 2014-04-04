using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

    // Each prefab that is attached to this array will be instantiated at the beginning of the scene
    // NOTE: This array should not contain the "Player" object
    // That object needs to have more sophisticated generation
    public Transform[] prefabs;
    // public Ingredient[] ingredients;
    
    // Use this for initialization
	void Start ()
    {
        SpawnAllPrefabs(this.prefabs);
        // GameObject drinkManager = GameObject.Find("Drink Manager");
        // ingredients = drinkManager.GetComponentsInChildren<Ingredient>();
    }

    void SpawnAllPrefabs(Transform[] prefabs)
    {
        foreach (Transform prefab in prefabs)
        {
            SpawnPrefab(prefab);
        }
    }

    void SpawnPrefab(Transform prefab)
    {
        Transform clone = Instantiate(prefab) as Transform;
        clone.name = prefab.name;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
    }
}
