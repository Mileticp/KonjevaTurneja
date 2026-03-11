using System;

namespace Konjeva_Turneja
{
    internal class Program
    {
        // mozni premiki
        static int[] premikX = { 2, 2, -2, -2, 1, 1, -1, -1 };
        static int[] premikY = { 1, -1, 1, -1, 2, -2, 2, -2 };

        static void Main(string[] args)
        {
            Console.WriteLine("Velikost šahovnice:");
            int velikost = int.Parse(Console.ReadLine());

            Console.WriteLine("Začetni X:");
            int zacetniX = int.Parse(Console.ReadLine());

            Console.WriteLine("Začetni Y:");
            int zacetniY = int.Parse(Console.ReadLine());

            find_knights_tour(zacetniX, zacetniY, velikost);
        }

        public static void find_knights_tour(int start_x, int start_y, int board_size)
        {
            int[,] sahovnica = new int[board_size, board_size];


            for (int i = 0; i < board_size; i++)
            {
                for (int j = 0; j < board_size; j++)
                {
                    sahovnica[i, j] = -1;
                }
            }


            sahovnica[start_x, start_y] = 0;

            bool uspelo = NajdiPot(sahovnica, start_x, start_y, 1, board_size);

            if (uspelo)
            {
                Izpis(sahovnica, board_size);
            }
            else
            {
                Console.WriteLine("Ni mogoče narediti konjeve turneje iz te pozicije.");
            }
        }


        static bool NajdiPot(int[,] sahovnica, int x, int y, int korak, int velikost)
        {
            if (korak == velikost * velikost)
                return true;

            for (int i = 0; i < 8; i++)
            {
                int nx = x + premikX[i];
                int ny = y + premikY[i];

                if (Veljavno(nx, ny, sahovnica, velikost))
                {
                    sahovnica[nx, ny] = korak;

                    if (NajdiPot(sahovnica, nx, ny, korak + 1, velikost))
                        return true;

                    // rollback
                    sahovnica[nx, ny] = -1;
                }
            }

            return false;
        }


        static bool Veljavno(int x, int y, int[,] sahovnica, int velikost)
        {
            if (x < 0 || y < 0 || x >= velikost || y >= velikost)
                return false;

            if (sahovnica[x, y] != -1)
                return false;

            return true;
        }

        // izpis poti
        static void Izpis(int[,] sahovnica, int velikost)
        {
            Console.WriteLine("\nSeznam potez:");

            for (int i = 0; i < velikost; i++)
            {
                for (int j = 0; j < velikost; j++)
                {
                    Console.Write(sahovnica[i, j].ToString().PadLeft(3) + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nZaporedje koordinat:");

            for (int korak = 0; korak < velikost * velikost; korak++)
            {
                for (int i = 0; i < velikost; i++)
                {
                    for (int j = 0; j < velikost; j++)
                    {
                        if (sahovnica[i, j] == korak)
                        {
                            Console.WriteLine("(" + i + "," + j + ")");
                        }
                    }
                }
            }
        }
    }
}