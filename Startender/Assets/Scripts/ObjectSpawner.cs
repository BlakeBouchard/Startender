using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

    // Each prefab that is attached to this array will be instantiated at the beginning of the scene
    // NOTE: This array should not contain the "Player" object
    // That object needs to have more sophisticated generation
    public Transform[] prefabs;

	public Transform[] backgrounds;
    // public Ingredient[] ingredients;
    
    // Use this for initialization
	void Start ()
    {
		LoadBackground();
        SpawnAllPrefabs();
        // GameObject drinkManager = GameObject.Find("Drink Manager");
        // ingredients = drinkManager.GetComponentsInChildren<Ingredient>();
    }

	void LoadBackground() {
		int rand = Random.Range(0, this.backgrounds.Length);
		this.SpawnPrefab(backgrounds[rand]);
	}

    void SpawnAllPrefabs()
    {
        foreach (Transform prefab in this.prefabs)
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
