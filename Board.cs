using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board
{
    public Board()
    {

    }
    public Pjäs[,] Pjäser = new Pjäs[,] {
        {null, null, null, null, null, null, null, null},
        {null, null, null, null, null, null, null, null},
        {null, null, null, null, null, null, null, null},
        {null, null, null, null, null, null, null, null},
        {null, null, null, null, null, null, null, null},
        {null, null, null, null, null, null, null, null},
        {null, null, null, null, null, null, null, null},
        {null, null, null, null, null, null, null, null}
    };

    int[] positionValue = {2,2,4,5,5,4,2,2};
    public int boardVärde(bool endastPjäsVärde)
    {
        int totalVärde = 0;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (Pjäser[y, x] != null)
                {
                    totalVärde += Pjäser[y, x].värde;
                    if(Pjäser[y, x] is Bonde)
                    {
                        if(!endastPjäsVärde)
                        totalVärde += Pjäser[y, x].riktning * positionValue[y] * positionValue[x];
                    }
                }
            }
        }
        if (endastPjäsVärde)
            return totalVärde;
        totalVärde += rörelsehetsVärde;
        totalVärde += connectionVärde;
        return totalVärde;
    }
    public int rörelsehetsVärde = 0;
    public int connectionVärde = 0;

    public int beräknaConnectionVärde()
    {
        int totalVärde = 0;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (Pjäser[y, x] != null)
                {
                    totalVärde += 2*Pjäser[y, x].connectionVärde;
                }
            }
        }
        return totalVärde;
    }


    public void copyBoard(Board originalBoard)
    {
        for(int y = 0; y < 8; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                Pjäs pjäs = originalBoard.Pjäser[y, x];
                if (pjäs == null)
                    continue;
                else if (pjäs is Bonde)
                    Pjäser[y, x] = new Bonde(pjäs.färg);
                else if (pjäs is Torn)
                    Pjäser[y, x] = new Torn(pjäs.färg);
                else if (pjäs is Häst)
                    Pjäser[y, x] = new Häst(pjäs.färg);
                else if (pjäs is Löpare)
                    Pjäser[y, x] = new Löpare(pjäs.färg);
                else if (pjäs is Kung)
                    Pjäser[y, x] = new Kung(pjäs.färg);
                else if (pjäs is Dam)
                    Pjäser[y, x] = new Dam(pjäs.färg);
            }
        }
    }
}
