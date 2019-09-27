using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Pjäs
{
    public Pjäs(string färgg, int värdet)
    {
        färg = färgg;
        värde = värdet;
        if (färg == "Vit")
            värde *= -1;
    }
    public Pjäs(string färgg, Vector2 location, string pjäsNamn, int värdet)
    {
        värde = värdet;
        färg = färgg;
        if (färg == "Vit")
            värde *= -1;
        objekt = new GameObject();
        SpriteRenderer render = objekt.AddComponent<SpriteRenderer>();
        objekt.transform.position = location;
        objekt.transform.localScale = new Vector2(0.5f, 0.5f);
        render.sprite = Resources.Load<Sprite>(pjäsNamn + färg);
        if (färg == "Svart")
            render.color = new Color(200f/255f, 200f/255f, 200f/255f);
    }
    public GameObject objekt;
    public string färg;
    public int värde;
    public int connectionVärde;
    public int riktning = 1;
    public virtual List<Board> allaMoves(Board originalBoard, int x, int y)
    {
        return null;
    }

    protected string positionFärg(Board bräda, int x, int y)
    {
        if (x > 7 || x < 0 || y > 7 || y < 0)//position är utanför brädan
            return "";
        if (bräda.Pjäser[y, x] == null)
            return "Tom";
        else
            return bräda.Pjäser[y, x].färg;
    }

}

