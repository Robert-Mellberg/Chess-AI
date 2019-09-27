using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class Rekursiv
{
    static string[] färg = { "Vit", "Svart" };
    public static Board BästaDrag(Board originalBoard, int rekNivå, int svårhetsgrad, int alphaBetaVärde)
    {
        List<Board> allaBoard = new List<Board>();
        int multiplier = ((rekNivå % 2) * 2) - 1; // om rekNivå är jämnt (player1:s runda) antar multiplier -1
        int bästaVärde = -15000 * multiplier; //antar negativt värde om rekNivå är ojämt och positivt och rekNivå är jämnt
        int allaBoardTotalCount = 0;
        Board bästaBoard = new Board();
        //om rekNivå är jämnt (player1:s runda)
        //om fler boards ska framskaffas
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Pjäs pjäs = originalBoard.Pjäser[y, x];
                if (pjäs != null)
                {
                    allaBoard = allaDragFörPjäs(originalBoard, färg[rekNivå % 2], x, y);
                    allaBoardTotalCount += allaBoard.Count;
                    if (rekNivå < svårhetsgrad)
                    {
                        foreach (Board board in allaBoard)
                        {
                            Board nyBoard = BästaDrag(board, rekNivå + 1, svårhetsgrad, bästaVärde);
                            int nyVärde = nyBoard.boardVärde(false);
                            if (nyVärde * multiplier > bästaVärde * multiplier) //letar största värde, för player1 är negativt bättre värde
                            {
                                bästaVärde = nyVärde;
                                bästaBoard = nyBoard;
                                if (rekNivå == 1) //vid första nivån skickar tillbaka det boardet som gav upphov till bästa boardet
                                    bästaBoard = board;
                            }
                            if (bästaVärde * multiplier > alphaBetaVärde * multiplier)
                                goto skip;
                        }
                    }
                    //sista nivån, inte rekursiva fler boards
                    else
                    {
                        if (svårhetsgrad == 1)
                        {
                            foreach (Board board in allaBoard)
                            {
                                if (godkäntDrag(originalBoard, board, 2, 1))
                                {
                                    int nyVärde = board.boardVärde(false);
                                    if (nyVärde > bästaVärde) //letar största värde, för boten är negativt bättre värde
                                    {
                                        bästaBoard = board;
                                        bästaVärde = nyVärde;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (Board board in allaBoard)
                            {
                                int nyVärde = board.boardVärde(false);
                                if (nyVärde * multiplier > bästaVärde * multiplier) //letar största värde, för boten är negativt bättre värde
                                {
                                    bästaBoard = board;
                                    bästaVärde = nyVärde;
                                }
                                if (bästaVärde * multiplier > alphaBetaVärde * multiplier)
                                    goto skip;
                            }
                        }
                    }
                }
            }
        }
        skip:
        if (rekNivå == 3)
        {
            bästaBoard.rörelsehetsVärde = allaBoardTotalCount;
            bästaBoard.connectionVärde = originalBoard.beräknaConnectionVärde();
        }
        return bästaBoard;
    }

            public static bool godkäntDrag(Board originalBoard, Board slutBoard, int rekNivå, int svartPjäs)
            {
                List<Board> allaBoard = allaDrag(originalBoard, färg[(rekNivå + svartPjäs) % 2]);
                if (rekNivå == 2)
                {
                    foreach (Board b in allaBoard)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (b.Pjäser[y, x] == null || slutBoard.Pjäser[y, x] == null)
                                {
                                    if (b.Pjäser[y, x] != null || slutBoard.Pjäser[y, x] != null)
                                        goto skip;
                                    else
                                        continue;
                                }
                                else if (!b.Pjäser[y, x].GetType().Equals(slutBoard.Pjäser[y, x].GetType()) || !b.Pjäser[y, x].färg.Equals(slutBoard.Pjäser[y, x].färg))
                                    goto skip;
                            }
                        }
                        if (godkäntDrag(b, b, 3, svartPjäs))
                            return true;
                        skip:;
                    }
                    return false;
                }
                else
                {
                    foreach (Board b in allaBoard)
                    {
                        if (b.boardVärde(true) > 5000 || b.boardVärde(true) < -5000)
                            return false;
                    }
                    return true;
                }
            }
            public static bool checkSchackmatt(Board originalBoard, int rekNivå, int vitPjäs)
            {
                List<Board> allaBoard = allaDrag(originalBoard, färg[(rekNivå + vitPjäs) % 2]); // alla drag 
                bool schackMatt = true; //förutsätt att det är schackmatt innan motbevisat

                if (rekNivå == 1)
                {
                    foreach (Board b in allaBoard)
                    {
                        if (!checkSchackmatt(b, 2, vitPjäs))
                            schackMatt = false;
                    }
                    return schackMatt;
                }
                else
                {
                    foreach (Board b in allaBoard)
                    {
                        if (b.boardVärde(true) > 5000 || b.boardVärde(true) < -5000)
                            return true;
                    }
                    return false;
                }
            }

            private static List<Board> allaDrag(Board originalBoard, string färg)
            {
                List<Board> allaBoards = new List<Board>();
                //Kollar alla möjliga board states utifrån varje pjäs
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        Pjäs pjäs = originalBoard.Pjäser[y, x];
                        //Om rutan innehåller en pjäs och pjäsens färg stämmer överens med rundans spelare
                        if (pjäs != null && pjäs.färg == färg)
                        {
                            //lägger till alla möjliga board från pjäsen till totala listan
                            allaBoards.AddRange(pjäs.allaMoves(originalBoard, x, y));
                        }
                    }
                }
                return allaBoards;
            }
            private static List<Board> allaDragFörPjäs(Board originalBoard, string färg, int x, int y)
            {
                List<Board> allaBoards = new List<Board>();
                Pjäs pjäs = originalBoard.Pjäser[y, x];
                //Om rutan innehåller en pjäs och pjäsens färg stämmer överens med rundans spelare
                if (pjäs != null && pjäs.färg == färg)
                {
                    //lägger till alla möjliga board från pjäsen till totala listan
                    allaBoards.AddRange(pjäs.allaMoves(originalBoard, x, y));
                }
                return allaBoards;
            }
        }

