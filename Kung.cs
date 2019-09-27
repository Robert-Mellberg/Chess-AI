using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Kung : Pjäs
{
    public Kung(string färg) : base(färg, 10000)
    {
        if (färg == "Svart")
            värde += 100;
    }
    public Kung(string färg, Vector2 location) : base(färg, location, "kungen", 10000)
    {

    }

    int[] förflyttning = {0, 1, 1, 1, 0, -1, -1, -1, 0, 1}; 
    public override List<Board> allaMoves(Board originalBoard, int x, int y)
    {
        List<Board> allaBoard = new List<Board>();

        for (int count = 0; count < 8; count++) //vektor som snurrar 45 grader
        {
            string färgRuta = positionFärg(originalBoard, x + förflyttning[count + 2], y + förflyttning[count]); // x har count + 2 eftersom sinus har 90 grader fasförskjutning mot cos
            if (färgRuta == färg)
                connectionVärde += riktning;
            if (färgRuta == "" || färgRuta == färg) //om ruta inte existerar eller samma färg står där 
                continue;
            if (färgRuta != "Tom")
                connectionVärde += riktning * 2;
            Board nyboard = new Board();
            nyboard.copyBoard(originalBoard);

            //flytta pjäs och ta bort gammal plats
            nyboard.Pjäser[y + förflyttning[count], x + förflyttning[count + 2]] = nyboard.Pjäser[y, x];
            nyboard.Pjäser[y, x] = null;
            allaBoard.Add(nyboard);
        }
        return allaBoard;
    }
}
