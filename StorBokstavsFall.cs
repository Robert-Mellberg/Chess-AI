using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorBokstavsFall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public int count = 0;
    public float hastighet = 0;
	void Update () {
        count++;
        if(count > 0)
        {
            if (transform.position.y < 0 && Mathf.Abs(hastighet) < 0.01f)
            {
                Debug.Log(transform.position.y + "  " + Mathf.Abs(hastighet));
                goto skip;
            }
            hastighet -= 0.005f;
            transform.position = new Vector2(transform.position.x, transform.position.y + hastighet);
            if(transform.position.y < 0)
            {
                transform.position = new Vector2(transform.position.x, 0);
                hastighet *= -0.6f;
            }
        }
        skip:
        if(count == 325)
        {
            SceneManager.LoadScene("Mode");
        }
	}
}
