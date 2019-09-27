using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Bonde : Pjäs
{
    public Bonde(string färg) : base(färg, 10)
    {
        if (färg == "Vit")
            riktning *= -1;
    }
    public Bonde(string färg, Vector2 location) : base(färg, location, "bonde", 100)
    {
        if (färg == "Vit")
            riktning *= -1;
    }
    public override List<Board> allaMoves(Board originalBoard, int x, int y)
    {
        List<Board> allaBoard = new List<Board>();

        for (int count = -1; count < 2; count+=2)
        {
            string färgRuta = positionFärg(originalBoard, x+count, y + riktning);
            if (färgRuta == färg)
                connectionVärde += riktning*2;
            if (färgRuta == "" || färgRuta == färg || färgRuta == "Tom") //om ruta inte existerar eller samma färg står där 
                continue;
            Board nyboard = new Board();
            nyboard.copyBoard(originalBoard);
            //flytta pjäs och ta bort gammal plats
            nyboard.Pjäser[y + riktning, x+count] = nyboard.Pjäser[y, x];
            nyboard.Pjäser[y, x] = null;
            connectionVärde += riktning * 2;
            allaBoard.Add(nyboard);
        }
        int antalSteg = 1;
        if (y == 1 || y == 6)
            antalSteg = 2;
        for (int count = 1; count <= antalSteg; count++)
        {
            string färgRuta = positionFärg(originalBoard, x, y + count * riktning);
            if (färgRuta != "Tom") //om nån står på rutan
                break;
            Board nyboard = new Board();
            nyboard.copyBoard(originalBoard);

            //flytta pjäs och ta bort gammal plats
            nyboard.Pjäser[y + count * riktning, x] = nyboard.Pjäser[y, x];
            nyboard.Pjäser[y, x] = null;
            allaBoard.Add(nyboard);
        }
        return allaBoard;
    }
}