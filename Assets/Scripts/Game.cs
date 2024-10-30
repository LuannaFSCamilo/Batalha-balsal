using System;

public class Game
{
    public Player Player1 { get; }
    public Player Player2 { get; }
    private bool currentPlayer;

    // Construtor da classe Game
    public Game(string player1Name, string player2Name)
    {
        Player1 = new Player(player1Name);
        Player2 = new Player(player2Name);
        currentPlayer = true;
    }

    // Método para iniciar o jogo
    public void Start()
    {
        Player1.PlaceShips();
        Player2.PlaceShips();

        while (true)
        {
            PrintBoards();
            if (currentPlayer)
            {
                Console.WriteLine($"É a vez de {Player1.GetName()}");
                if (Player1.Attack(Player2))
                {
                    if (Player2.AllShipsSunk())
                    {
                        Console.WriteLine($"{Player1.GetName()} venceu!");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine($"É a vez de {Player2.GetName()}");
                if (Player2.Attack(Player1))
                {
                    if (Player1.AllShipsSunk())
                    {
                        Console.WriteLine($"{Player2.GetName()} venceu!");
                        break;
                    }
                }
            }
            currentPlayer = !currentPlayer;
        }
    }

    // Método para obter o resultado do jogo
    public string GetGameResult()
    {
        if (Player1.AllShipsSunk())
        {
            return $"{Player2.GetName()} venceu!";
        }
        else if (Player2.AllShipsSunk())
        {
            return $"{Player1.GetName()} venceu!";
        }
        else
        {
            return "Jogo em andamento...";
        }
    }

    // Método para imprimir os tabuleiros dos jogadores
    public void PrintBoards()
    {
        Console.WriteLine($"\nTabuleiro de {Player1.GetName()}:");
        Player1.GetBoard().PrintBoard(false);
        Console.WriteLine($"\nTabuleiro de {Player2.GetName()}:");
        Player2.GetBoard().PrintBoard(false);
    }
}
