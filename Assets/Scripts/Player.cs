using System;

public class Player
{
    private string name;
    private Board board;

    // Construtor da classe Player
    public Player(string name)
    {
        this.name = name;
        this.board = new Board();
    }

    // M�todo para obter o nome do jogador
    public string GetName()
    {
        return name;
    }

    // M�todo para obter o tabuleiro do jogador
    public Board GetBoard()
    {
        return board;
    }

    // M�todo para colocar os navios no tabuleiro
    public void PlaceShips()
    {
        Console.WriteLine($"{name}, coloque seus navios.");
        int[] shipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        foreach (int size in shipSizes)
        {
            bool placed = false;
            while (!placed)
            {
                Console.Write("Insira a coluna (ex: A): ");
                string columnInput = Console.ReadLine().Trim().ToUpper();
                int col = ConvertColumnToIndex(columnInput);

                if (col == -1)
                {
                    Console.WriteLine("Coluna inv�lida. Tente novamente.");
                    continue;
                }

                Console.Write("Insira a linha (ex: 1): ");
                int row;
                if (!int.TryParse(Console.ReadLine().Trim(), out row) || row < 1 || row > Board.SIZE)
                {
                    Console.WriteLine("Linha inv�lida. Tente novamente.");
                    continue;
                }
                row -= 1; // Ajusta para zero-based index

                Console.Write("Insira a orienta��o (H para horizontal, V para vertical): ");
                string orientationInput = Console.ReadLine().Trim().ToUpper();
                bool horizontal = orientationInput.Equals("H");

                if (board.PlaceShip(row, col, size, horizontal))
                {
                    Console.WriteLine("Navio colocado com sucesso!");
                    placed = true;
                }
                else
                {
                    Console.WriteLine("N�o foi poss�vel colocar o navio. Tente novamente.");
                }

                board.PrintBoard(false);
            }
        }
    }

    // M�todo auxiliar para converter a coluna de letra para �ndice
    private int ConvertColumnToIndex(string column)
    {
        if (column.Length != 1) return -1;
        char colChar = column[0];
        if (colChar < 'A' || colChar > 'J') return -1;
        return colChar - 'A';
    }

    // M�todo para atacar um oponente
    public bool Attack(Player opponent)
    {
        Console.Write($"{name}, insira a linha e a coluna para atacar (ex: 1 1): ");
        int row, col;

        string input = Console.ReadLine();
        string[] inputs = input.Split(' ');

        if (inputs.Length != 2 ||
            !int.TryParse(inputs[0], out row) || !int.TryParse(inputs[1], out col) ||
            row < 1 || row > Board.SIZE || col < 1 || col > Board.SIZE)
        {
            Console.WriteLine("Entrada inv�lida. Tente novamente.");
            return false;
        }

        row -= 1; // Ajusta para zero-based index
        col -= 1; // Ajusta para zero-based index

        bool result = opponent.GetBoard().Attack(row, col);
        Console.WriteLine(result ? "Ataque bem-sucedido!" : "Ataque falhou!");
        return result;
    }

    // M�todo para verificar se todos os navios est�o afundados
    public bool AllShipsSunk()
    {
        return board.AllShipsSunk();
    }
}
