using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animering : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //importera sprites
        bilder = new Sprite[antalBilder];
        for(int i = 0; i < antalBilder; i++)
        {
            bilder[i] = Resources.Load<Sprite>(bildSerie + (i+1));
        }
        bild = GameObject.Find(gameobjectNamn).GetComponent<SpriteRenderer>();
	}

    public int antalBilder;
    int riktning = -1;
    public string bildSerie;
    public string gameobjectNamn;
    int bildIndex = 0;
    int count = 0;
    Sprite[] bilder;
    SpriteRenderer bild;
	// Update is called once per frame
	void Update () {
        count++;
        if (count % 10 == 0)
        {
            if (bildIndex == antalBilder - 1 || bildIndex == 0)
            {
                riktning *= -1;
            }
            bildIndex += riktning;
            bild.sprite = bilder[bildIndex];
        }
	}
}
