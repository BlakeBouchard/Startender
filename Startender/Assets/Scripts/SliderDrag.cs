using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliderDrag : MonoBehaviour {

    private Vector3 startPosition;
    private Vector3 maxLeftPosition;
	public GameObject box;
    
    public List<Drink> drinkList;
	public int drinkListPointer;
	public List<GameObject> ingredientTexts;

	public TextMesh drinkBookDrinkTitle;

    // Set this if we want the slider to snap back to its original position on release
    public bool snapsBack = false;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
        float maxLeftX = startPosition.x * -1;
        maxLeftPosition = new Vector3(maxLeftX, startPosition.y, startPosition.z); 

		this.drinkListPointer = 0;

        //feed drinks to Drink Book
        GameObject dmo = GameObject.Find("Drink Manager");
        DrinkManager dm = (DrinkManager) dmo.GetComponent(typeof(DrinkManager));
        this.drinkList = dm.GetDrinks();

		this.box = GameObject.Find("Slider Box");

		GameObject drinkBookDrinkTitleGO = GameObject.Find("DrinkBookDrinkTitle");
		this.drinkBookDrinkTitle = (TextMesh)drinkBookDrinkTitleGO.GetComponent(typeof(TextMesh));
		this.drinkBookDrinkTitle.renderer.sortingOrder = 100;
		this.drinkBookDrinkTitle.renderer.sortingLayerName = "DrinkBook";

		this.ingredientTexts = new List<GameObject>();
	}

    public void prevDrink()
    {
		if (this.drinkListPointer > 0)
        {
			this.drinkListPointer--;
		}
        else
        {
			this.drinkListPointer = this.drinkList.Count - 1;
		}
	}

	public void nextDrink()
    {
		if (this.drinkListPointer < this.drinkList.Count - 1)
        {
			this.drinkListPointer++;
		}
        else
        {
			this.drinkListPointer = 0;
		}
	}

    // This function should send the slider back to its starting position 
    void SnapBack ()
    {
        this.transform.position = startPosition;
    }

    // This code should handle moving the slider from left to right
    // The position should be sent in world coordinates, ie. converted from camera coordinates
    void MoveSliderHorizontally (Vector3 newPosition)
    {
        //if we are going off the left hand side, lock
        if (newPosition.x <= this.maxLeftPosition.x)
        {
            this.transform.position = this.maxLeftPosition;
        }
        //if we are going off the right hand side, lock
        else if (newPosition.x >= this.startPosition.x)
        {
            this.transform.position = this.startPosition;
        }
        //otherwise slide freely 
        else
        {
            this.transform.position = new Vector3(newPosition.x, this.startPosition.y, this.startPosition.z);
        }
    }

    // Called when user touches a collider attached to this object
    void OnTouchDrag (Touch touch)
    {
        MoveSliderHorizontally(Camera.main.ScreenToWorldPoint(touch.position));
    }

    // Called when user lifts their finger from a collider attached to this object
    void OnTouchUp (Touch touch)
    {
        OnMouseUp();
    }

    // Called when user clicks mouse on a collider attached to this object
    void OnMouseDown ()
    {
        Debug.Log("Clicked slider button");
    }

    // Called when user clicks a collider attached to this object
    void OnMouseDrag ()
    {
        MoveSliderHorizontally(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    // Called when user releases the mouse button while dragging a collider attached to this object
    void OnMouseUp ()
    {
        if (snapsBack)
        {
            SnapBack();
        }
    }

	void OnGUI()
    {

	}
	
	private TextMesh createTextObject(Vector3 position, Vector3 translation, Material material, string text)
    {

		GameObject textObj = new GameObject("IngredientLabel");
		textObj.transform.position = position + translation;
		textObj.name = "IngredientLabel";
		MeshRenderer mr = textObj.AddComponent<MeshRenderer>();
		mr.material = material;
		TextMesh tm = textObj.AddComponent<TextMesh>();
		tm.renderer.sortingOrder = 100;
		tm.renderer.sortingLayerName = "DrinkBook";
		tm.text = text;
		tm.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		tm.characterSize = 0.4f;

		this.ingredientTexts.Add(textObj);
		
		return tm;

	}

	// Update is called once per frame
	void Update ()
    {
		Drink drink = this.drinkList[this.drinkListPointer];
		this.drinkBookDrinkTitle.text = drink.GetDrinkName();

		float paddingY = 0.2f;
		float paddingX = 0.2f;

		float down = -1 * this.drinkBookDrinkTitle.renderer.bounds.size.y - paddingY;
		Vector3 position = new Vector3(this.drinkBookDrinkTitle.transform.position.x + paddingX, this.drinkBookDrinkTitle.transform.position.y, -4);
		Vector3 translation = new Vector3(0, down, 0);

		List<GameObject>.Enumerator e = this.ingredientTexts.GetEnumerator();
		while(e.MoveNext())
        {
			GameObject g = e.Current;
			Destroy(g);
		}
		this.ingredientTexts.Clear();
			
		Ingredient[] ingredients = drink.GetIngredients();
		
		for (int x = 0; x < ingredients.Length; x++)
        {
			Ingredient ingredient = ingredients[x];
			TextMesh tm = this.createTextObject(position, translation, this.drinkBookDrinkTitle.renderer.material, ingredient.name);

			//prepare next ingredient position
			down = -1 * tm.renderer.bounds.size.y - paddingY;
			position = tm.transform.position;
			translation = new Vector3(0, down, 0);
		} 
	}
}
