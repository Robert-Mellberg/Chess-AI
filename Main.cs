using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        selectRam = GameObject.Find("SelectRam");
        turnIndicator = GameObject.Find("turnIndicator");
        skapaBräda();
    }
    GameObject selectRam;
    GameObject turnIndicator;
    float turnFärg = 0;
    bool flyttaPjäs = false;
    Pjäs flyttObjekt;
    Pjäs tagenPjäs;
    Vector2 flyttPosition;
    Vector2 startPosition;
    int antalTagnaSvarta = 0;
    int antalTagnaVita = 0;
    bool klick = false;
    int botDragCount = 6;
	void Update ()
    {
        botDragCount++;
        if(botDragCount == 5)
        {
            botDrag();
        }
        if(flyttaPjäs)
        {
            GameObject.Find("chessIndicator").transform.position = new Vector2(25f, 25f);
            flyttObjekt.objekt.transform.position += ((Vector3)(flyttPosition - startPosition)).normalized / 25;
            if ((flyttPosition - (Vector2)flyttObjekt.objekt.transform.position).magnitude < 0.05f)
            {
                flyttObjekt.objekt.transform.position = flyttPosition;
                if (tagenPjäs != null)
                {
                    tagenPjäs.objekt.transform.position = new Vector2(20, 20);
                    if(tagenPjäs.färg == "Vit")
                    {
                        tagenPjäs.objekt.transform.position = new Vector2((antalTagnaVita%3)-7.1f, 3-antalTagnaVita/3);
                        antalTagnaVita++;
                    }
                    else
                    {
                        tagenPjäs.objekt.transform.position = new Vector2((antalTagnaSvarta % 3) - 7.1f, antalTagnaSvarta / 3 - 4);
                        antalTagnaSvarta++;
                    }
                    tagenPjäs = null;
                }
                flyttObjekt.objekt.GetComponent<SpriteRenderer>().sortingOrder = 0;
                if (flyttObjekt.färg == "Vit")
                {
                    botDragCount = 0;
                }
                if (Rekursiv.checkSchackmatt(currentBoard, 1, 0) || Rekursiv.checkSchackmatt(currentBoard, 1, 1))
                {
                    botDragCount = 10;
                    GameObject.Find("Schackmatt").transform.position = new Vector2(5.6f, 0);
                    GameObject.Find("Playagain").transform.position = new Vector2(5.6f, -2.05f);
                    Destroy(turnIndicator);
                    Destroy(GameObject.Find("Bakknapp"));
                    Destroy(GameObject.Find("Reverseknapp"));
                    Destroy(GameObject.Find("ValdBit"));
                    Destroy(GameObject.Find("chessIndicator"));
                    if (flyttObjekt.färg != "Vit")
                        GetComponent<AudioSource>().Play();
                }
                //if (flyttObjekt.färg == "Vit")
                //{
                //    botDragCount = 0;
                //    if (Rekursiv.checkSchackmatt(currentBoard, 1, 0))
                //    {
                //        Debug.Log("Schackmatt");
                //        botDragCount = 10;
                //    }
                //}
                //else
                //{
                //    if (Rekursiv.checkSchackmatt(currentBoard, 1, 1))
                //        Debug.Log("Schackmatt");
                //}
                turnFärg++;
                turnIndicator.GetComponent<SpriteRenderer>().color = new Color(turnFärg%2, (turnFärg+1) % 2, 0);
                flyttaPjäs = false;
                GameObject.Find("Scoreboard").GetComponentInChildren<TextMesh>().text = "Score: " + -currentBoard.boardVärde(true)/100;
                if(Rekursiv.checkSchackmatt(currentBoard, 2, 0) || Rekursiv.checkSchackmatt(currentBoard, 2, 1))
                    GameObject.Find("chessIndicator").transform.position = new Vector2(5.63f, -3.47f);
            }
        }
        if(Input.GetButton("Fire1"))
        {
            klick = true;
        }
        else if(klick)
        {
            klick = false;
            Vector2 musPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (musPosition.x > -4.5f && musPosition.x < 3.5f && musPosition.y > -4.5f && musPosition.y < 3.5f)
            {
                selectPjäs((int)(musPosition.x + 4.5), (int)(3.5f - musPosition.y));
            }
        }

	}
    List<GameObject> allaPjäser = new List<GameObject>();
    private void skapaBräda()
    {
        for (int i = 0; i < 8; i++)
        {
            currentBoard.Pjäser[6, i] = new Bonde("Vit", new Vector2(i - 4, -3));
            currentBoard.Pjäser[1, i] = new Bonde("Svart", new Vector2(i - 4, 2));
        }
        for (int i = 0; i < 8; i += 7)
        {
            currentBoard.Pjäser[7, i] = new Torn("Vit", new Vector2(i - 4, -4));
            currentBoard.Pjäser[0, i] = new Torn("Svart", new Vector2(i - 4, 3));
        }
        for (int i = 1; i < 7; i += 5)
        {
            currentBoard.Pjäser[7, i] = new Häst("Vit", new Vector2(i - 4, -4));
            currentBoard.Pjäser[0, i] = new Häst("Svart", new Vector2(i - 4, 3));
        }
        for (int i = 2; i < 6; i += 3)
        {
            currentBoard.Pjäser[7, i] = new Löpare("Vit", new Vector2(i - 4, -4));
            currentBoard.Pjäser[0, i] = new Löpare("Svart", new Vector2(i - 4, 3));
        }
        currentBoard.Pjäser[7, 3] = new Dam("Vit", new Vector2(-1, -4));
        currentBoard.Pjäser[0, 3] = new Dam("Svart", new Vector2(-1, 3));
        currentBoard.Pjäser[7, 4] = new Kung("Vit", new Vector2(0, -4));
        currentBoard.Pjäser[0, 4] = new Kung("Svart", new Vector2(0, 3));
        turnIndicator.transform.position = new Vector2(5,0.34f);
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject ruta = skapaGameObject(new Vector2(x-4,y-4), "Ruta", new Vector2(1,1), "Square", -1);
                SpriteRenderer render = ruta.GetComponent<SpriteRenderer>();
                if ((y + x) % 2 == 0)
                    render.color = Color.black;
                else
                    render.color = Color.white;
            }
        }
    }

    private GameObject skapaGameObject(Vector2 location, string namn, Vector2 size, string sprite, int sortingOrder)
    {
        GameObject obj = new GameObject();
        SpriteRenderer render = obj.AddComponent<SpriteRenderer>();
        render.sprite = Resources.Load<Sprite>(sprite);
        render.sortingOrder = sortingOrder;
        obj.transform.position = location;
        obj.transform.localScale = size;
        obj.name = namn;
        return obj;
    }

    public void ändraBoard(Board originalBoard, Board slutBoard)
    {
        int xStart=0;
        int yStart=0;
        int xSlut=0;
        int ySlut=0;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (originalBoard.Pjäser[y, x] != null && slutBoard.Pjäser[y, x] == null) //om pjäs finns på original men inte på slut
                {
                    xStart = x;
                    yStart = y;
                }
                else if (slutBoard.Pjäser[y, x] != null && originalBoard.Pjäser[y, x] == null) //om pjäs finns på slut men inte på original
                {
                    xSlut = x;
                    ySlut = y;
                }
                else if (slutBoard.Pjäser[y, x] != null && originalBoard.Pjäser[y, x] != null)
                {
                    if (!slutBoard.Pjäser[y, x].färg.Equals(originalBoard.Pjäser[y, x].färg)) //om pjäs har en färg på original och anna färg på slut 
                    {
                        xSlut = x;
                        ySlut = y;
                        tagenPjäs = originalBoard.Pjäser[ySlut, xSlut];
                    }
                }
            }
        }
        flyttObjekt = originalBoard.Pjäser[yStart, xStart];
        flyttObjekt.objekt.GetComponent<SpriteRenderer>().sortingOrder = 3;
        originalBoard.Pjäser[ySlut, xSlut] = originalBoard.Pjäser[yStart, xStart];
        flyttPosition = new Vector2(xSlut - 4, 3 - ySlut);
        startPosition = flyttObjekt.objekt.transform.position;
        flyttaPjäs = true;
        originalBoard.Pjäser[yStart, xStart] = null;
    }

    Board currentBoard = new Board();
    private void botDrag()
    {

        Board bästaDrag = Rekursiv.BästaDrag(currentBoard, 1, Variabler.svårighetsGrad, 15000);
        ändraBoard(currentBoard,bästaDrag);
        GameObject.Find("ValdBit").GetComponentInChildren<TextMesh>().text = "Bot move: " + beräknaPosition(startPosition) + "-" + beräknaPosition(flyttPosition);
    }
    Pjäs markeradPjäs = null;
    int markeradIndexX = 0;
    int markeradIndexY = 0;
    private void selectPjäs(int x, int y)
    {
        if (flyttaPjäs)
            return;
        if (currentBoard.Pjäser[y,x] == null || currentBoard.Pjäser[y,x].färg == "Svart") //ska flytta egen pjäs
        {
            if (markeradPjäs != null)
            {
                Board testBoard = new Board();
                testBoard.copyBoard(currentBoard);
                testBoard.Pjäser[y, x] = testBoard.Pjäser[markeradIndexY, markeradIndexX];
                testBoard.Pjäser[markeradIndexY, markeradIndexX] = null;
                markeradPjäs = null;
                markeradIndexY = 0;
                markeradIndexX = 0;
                selectRam.transform.position = new Vector3(20, 20);

                if(Rekursiv.godkäntDrag(currentBoard, testBoard, 2, 0))
                ändraBoard(currentBoard, testBoard);
            }
        }
        else //markerar egen pjäs
        {
            if (markeradPjäs == currentBoard.Pjäser[y, x]) //klickar på markerad pjäs
            {
                markeradPjäs = null;
                markeradIndexY = 0;
                markeradIndexX = 0;
                selectRam.transform.position = new Vector3(20, 20);
                return;
            }
            markeradPjäs = currentBoard.Pjäser[y, x];
            markeradIndexY = y;
            markeradIndexX = x;
            selectRam.transform.position = currentBoard.Pjäser[y, x].objekt.transform.position - new Vector3(0.5f, 0);
        }
    }
    private string beräknaPosition(Vector2 position)
    {
        string positionRuta = "";
        positionRuta += char.ConvertFromUtf32(69 + (int)(position.x));
        positionRuta += (position.y + 5);

        return positionRuta;
    }
}
