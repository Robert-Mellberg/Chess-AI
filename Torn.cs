using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Torn : Pjäs
{
    public Torn(string färg) : base(färg, 50)
    {

    }
    public Torn(string färg, Vector2 location) : base(färg, location, "torn", 500)
    {

    }

    int[] förflyttning = { 0,1,0,-1,0};
    public override List<Board> allaMoves(Board originalBoard, int x, int y)
    {
        List<Board> allaBoard = new List<Board>();

        for (int count = 0; count < 4; count++) //vektor som snurrar 90 grader
        {
            for (int magnitude = 1; magnitude < 8; magnitude++)
            {

                string färgRuta = positionFärg(originalBoard, x + förflyttning[count + 1] * magnitude, y + förflyttning[count] * magnitude); // x har count + 1 eftersom sinus har 90 grader fasförskjutning mot cos
                if (färgRuta == färg)
                    connectionVärde += riktning;
                if (färgRuta == "" || färgRuta == färg) //om ruta inte existerar eller samma färg står där 
                    break;
                Board nyboard = new Board();
                nyboard.copyBoard(originalBoard);

                //flytta pjäs och ta bort gammal plats
                nyboard.Pjäser[y + förflyttning[count] * magnitude, x + förflyttning[count + 1] * magnitude] = nyboard.Pjäser[y, x];
                nyboard.Pjäser[y, x] = null;
                allaBoard.Add(nyboard);
                if (färgRuta != "Tom") //om rutan är av annan färg än pjäs
                {
                    connectionVärde += riktning * 2;
                    break;
                }
            }
        }
        return allaBoard;
    }
}
