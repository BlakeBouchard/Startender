using UnityEngine;
using System.Collections;

public class DrinkBookButton : MonoBehaviour
{
	public SliderDrag drinkbook;

	// Use this for initialization
	void Start ()
	{
		GameObject go = GameObject.Find("Slider");
		this.drinkbook = go.GetComponent(typeof(SliderDrag)) as SliderDrag;
	}

	// Called when a collider attached to this object is touched
	void OnTouchDown(Touch touch)
	{
        ActivateButton();
	}
	
	// Called when mouse is clicked on a collider attached to this object
	void OnMouseDown()
	{
        ActivateButton();
	}

    void ActivateButton()
    {
        if (this.name == "PrevDrink")
        {
            Debug.Log("Toggle previous drink in book");
            this.drinkbook.prevDrink();
        }
        else
        {
            Debug.Log("Toggle next drink in book");
            this.drinkbook.nextDrink();
        }
    }
	
	// Update is called once per frame
	void Update ()
	{

	}
}

