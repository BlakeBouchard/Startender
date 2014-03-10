using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour {
    
    private float accelerationThreshold = 0.3f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.accelerationEventCount > 0)
        {
            foreach (AccelerationEvent accelerationEvent in Input.accelerationEvents)
            {
                if (accelerationEvent.acceleration.magnitude > accelerationThreshold)
                {
                    this.DeviceShaken();
                }
            }
        }
	}

    // This should be called when the device shakes
    private void DeviceShaken()
    {
        
    }
}
