using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class loadGame : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    bool klick = false;
    public string scen;
    public int svårhetsGrad = 0;
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            klick = true;
        }
        else if (klick)
        {
            klick = false;
            Vector2 musPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (gameObject.GetComponent<SpriteRenderer>().bounds.Contains(musPosition))
            {
                if(svårhetsGrad != 10)
                Variabler.svårighetsGrad = svårhetsGrad;
                SceneManager.LoadScene(scen);
            }
        }
    }
}
