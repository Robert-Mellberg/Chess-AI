using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dam : Pjäs
{
    public Dam(string färg) : base(färg, 90) 
    {

    }
    public Dam(string färg, Vector2 location) : base(färg, location, "dam", 900)
    {

    }

    int[] förflyttning = { 0, 1, 1, 1, 0, -1, -1, -1, 0, 1 };
    public override List<Board> allaMoves(Board originalBoard, int x, int y)
    {
        List<Board> allaBoard = new List<Board>();

        for (int count = 0; count < 8; count++) //vektor som snurrar 45 grader
        {
            for (int magnitude = 1; magnitude < 8; magnitude++)
            {

                string färgRuta = positionFärg(originalBoard, x + förflyttning[count + 2]*magnitude, y + förflyttning[count]*magnitude); // x har count + 2 eftersom sinus har 90 grader fasförskjutning mot cos
                if (färgRuta == färg)
                    connectionVärde += riktning*2;
                if (färgRuta == "" || färgRuta == färg) //om ruta inte existerar eller samma färg står där 
                    break;
                Board nyboard = new Board();
                nyboard.copyBoard(originalBoard);

                //flytta pjäs och ta bort gammal plats
                nyboard.Pjäser[y + förflyttning[count]*magnitude, x + förflyttning[count + 2]*magnitude] = nyboard.Pjäser[y, x];
                nyboard.Pjäser[y, x] = null;
                allaBoard.Add(nyboard);
                if (färgRuta != "Tom") //om rutan är av annan färg än pjäs
                {
                    connectionVärde += riktning*2;
                    break;
                }
            }
        }
        return allaBoard;
    }
}

