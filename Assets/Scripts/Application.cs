using System;

class Application
{
    static void Main(string[] args)
    {
        Console.Write("Nome do Jogador 1: ");
        string player1Name = Console.ReadLine();
        Console.Write("Nome do Jogador 2: ");
        string player2Name = Console.ReadLine();

        Game game = new Game(player1Name, player2Name);
        game.Start();
    }
}
