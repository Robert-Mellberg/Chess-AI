using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Häst : Pjäs
{
    public Häst(string färg) : base(färg, 30)
    {

    }
    public Häst(string färg, Vector2 location) : base(färg, location, "häst", 300)
    {

    }

    int[] förflyttningY = { 1,-1, 2, 2, 1,-1, -2, -2 };
    int[] förflyttningX = { 2, 2, 1, -1, -2, -2, 1, -1};
    public override List<Board> allaMoves(Board originalBoard, int x, int y)
    {
        List<Board> allaBoard = new List<Board>();

        for (int count = 0; count < 8; count++)
        {
            string färgRuta = positionFärg(originalBoard, x + förflyttningX[count], y + förflyttningY[count]);
            if (färgRuta == färg)
                connectionVärde += riktning*2;
            if (färgRuta == "" || färgRuta == färg) //om ruta inte existerar eller samma färg står där 
                continue;
            if(färgRuta != "Tom")
                connectionVärde += riktning*2;
            Board nyboard = new Board();
            nyboard.copyBoard(originalBoard);

            //flytta pjäs och ta bort gammal plats
            nyboard.Pjäser[y + förflyttningY[count], x + förflyttningX[count]] = nyboard.Pjäser[y, x];
            nyboard.Pjäser[y, x] = null;
            allaBoard.Add(nyboard);
        }
        return allaBoard;
    }
}
