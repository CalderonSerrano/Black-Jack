using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("B I E N V E N I D O    A L    B L A C K    J A C K    C A S I N O");
            Console.WriteLine();
            Random rnd = new Random();
            int random, num;
            int randomCPU = 0;
            int playerPoints = 0;
            int cpuPoints = 0;
            double bankMoney = 500000;
            double playerMoney = 5000;
            string answer;
            double bet;
            bool draw = false;
            bool cpuSticks = true;
            do
            {
                Console.WriteLine("Dinero Casino: " + bankMoney);
                Console.WriteLine("Saldo disponible: " + playerMoney);
                if (playerMoney <= 0)
                {
                    playerMoney += 1000;
                    Console.WriteLine("Has quebrado y se te ha concedido un préstamo de: 1000 euros.");
                    Console.WriteLine("Nuevo saldo: " + playerMoney);
                }
                do
                {
                    Console.Write("¿Cuánto quieres apostar?: ");
                    bet = double.Parse(Console.ReadLine());
                } while (!Apuesta(bet, playerMoney));
                playerMoney -= bet;
                Console.WriteLine();
                do
                {
                    Console.WriteLine("Elija una opción: ");
                    Console.WriteLine("1. Reglas.");
                    Console.WriteLine("2. Pedir carta.");
                    Console.WriteLine("3. Plantarse.");
                    Console.WriteLine();
                    num = int.Parse(Console.ReadLine());
                    switch (num)
                    {
                        case 1:
                            playerMoney += bet;
                            Console.WriteLine();
                            Console.WriteLine("El juego consiste en aproximarse a 21 sin pasarse, el que más se acerque gana.");
                            Console.WriteLine("Si consigues todo el dinero del Banco, ganas.");
                            Console.WriteLine();
                            break;
                        case 2:
                            Console.WriteLine();
                            random = Card(rnd);
                            playerPoints += random;
                            Console.WriteLine("Jugador coge carta y sale: " + random);
                            if (!CPU(cpuPoints))
                            {
                                randomCPU = Card(rnd);
                                cpuPoints += randomCPU;
                                Console.WriteLine("CPU coge carta y sale: " + randomCPU);
                                cpuSticks = false;
                            }
                            else
                            {
                                Console.WriteLine("La CPU se planta");
                            }
                            if (Busted(playerPoints))
                            {
                                Console.WriteLine("Te has pasado!");
                            }
                            Console.WriteLine("Puntos Jugador: " + playerPoints);
                            Console.WriteLine("Puntos CPU: " + cpuPoints);
                            Console.WriteLine();
                            break;
                        case 3:
                            Console.WriteLine("Te plantaste con: " + playerPoints);
                            while ((cpuPoints <= playerPoints) && (!CPU(cpuPoints)))
                            {
                                randomCPU = Card(rnd);
                                cpuPoints += randomCPU;
                                Console.WriteLine("CPU coge carta y sale: " + randomCPU);
                                Console.WriteLine("Puntos CPU: " + cpuPoints);
                            }
                            cpuSticks = true;
                            break;
                        default:
                            Console.WriteLine("Has introducido un valor no válido.");
                            break;
                    }
                } while ((playerPoints <= 21) && (cpuPoints <= 21) && (!cpuSticks));
                if ((num > 1) && (num < 4))
                {
                    if (((playerPoints > cpuPoints) || (cpuPoints > 21)) && (playerPoints <= 21))
                    {
                        Console.WriteLine();
                        Console.WriteLine("P L A Y E R   1   W I N S !");
                        Console.WriteLine();
                        bankMoney -= bet;
                        playerMoney += bet * 2;
                        Console.WriteLine("Dinero Banca: " + bankMoney + "\n" + "Dinero Jugador: " + playerMoney);
                    }
                    if (((cpuPoints > playerPoints) || (playerPoints > 21)) && (cpuPoints <= 21))
                    {
                        Console.WriteLine();
                        Console.WriteLine("C P U    W I N S");
                        Console.WriteLine();
                        bankMoney += bet;
                        Console.WriteLine("Dinero Banca: " + bankMoney + "\n" + "Dinero Jugador: " + playerMoney);
                    }
                    if (playerPoints == cpuPoints)
                    {
                        draw = true;
                        Console.WriteLine();
                        Console.WriteLine("D R A W");
                        Console.WriteLine();
                        playerMoney += bet;
                        Console.WriteLine("Dinero Banca: " + bankMoney + "\n" + "Dinero Jugador: " + playerMoney);
                    }
                    if ((playerPoints > 21) && (cpuPoints > 21))
                    {
                        Console.WriteLine("Nadie gana");
                        if (!draw)
                        {
                            playerMoney += bet;
                        }
                    }
                }
                Console.WriteLine("¿Quieres seguir jugando? s/n");
                Refresh(ref playerPoints, ref cpuPoints);
                answer = Console.ReadLine();
                Console.Clear();
            } while (answer == "s");
            Console.ReadKey();
        }
        public static bool Apuesta(double bet, double playerMoney)
        {
            bool correctBet = false;
            if ((bet > 0) && (bet <= playerMoney))
            {
                correctBet = true;
            }
            return correctBet;
        }
        public static int Card(Random rnd)
        {
            int random = rnd.Next(1, 14);
            if ((random == 1) || (random >= 10))
            {
                random = 10;
            }
            return random;
        }
        public static bool Busted(int playerPoints)
        {
            bool busted = false;
            if (playerPoints > 21)
            {
                busted = true;
            }
            return busted;
        }
        public static bool CPU(int cpuPoints)
        {
            bool cpu = false;
            if ((cpuPoints >= 19) && (cpuPoints <= 21))
            {
                cpu = true;
            }
            return cpu;
        }
        public static void Refresh(ref int playerPoints, ref int cpuPoints)
        {
            playerPoints = 0;
            cpuPoints = 0;
        }
    }
}
