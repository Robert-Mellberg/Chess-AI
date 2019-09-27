using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BokstavsFall : MonoBehaviour {

	// Use this for initialization
	void Start () {
        xPosition = transform.position.x;
        studsPunktX = xPosition;
        yPosition = transform.position.y;
        amplifier = 4 / (((xPosition - slutPositionX)/2) * ((xPosition - slutPositionX) / 2));
        Debug.Log(amplifier + " " + name + "  " + xPosition + "  " + slutPositionX);

        riktning = slutPositionX - studsPunktX;
        riktning = riktning /100;
	}
    float hastighet = 0;
    // Update is called once per frame
    bool landat = false;
    float riktning;

	void Update () {

        if (!landat)
        {
            if(hastighet > -0.2f)
                hastighet -= 0.002f;
            yPosition += hastighet;
            transform.position = new Vector2(xPosition, yPosition);
            if (transform.position.y < -2)
            {
                landat = true;
                transform.position = new Vector2(transform.position.x, -2);
            }
        }
        else // efter biten landat
        {
            xPosition += riktning;
            transform.Rotate(new Vector3(0,0,3.6f));
            transform.position = new Vector2(xPosition, beräknaYVärde());
            if (Mathf.Abs(xPosition) > Mathf.Abs(slutPositionX))
            {
                transform.position = new Vector2(slutPositionX,-2);
                transform.eulerAngles = new Vector3(0,0,0);
                enabled = false;
            }
        }
	}

    public float slutPositionX;
    float studsPunktX;
    float amplifier;
    float yFörskjutning = -2;
    float xPosition;
    float yPosition;
    private float beräknaYVärde()
    {
        float y = amplifier*(xPosition - slutPositionX)*(studsPunktX-xPosition)+yFörskjutning;
        return y;
    }
}
