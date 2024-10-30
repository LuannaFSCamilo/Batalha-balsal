using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public const int SIZE = 10; // Tamanho do tabuleiro
    private char[,] board; // Matriz do tabuleiro
    public const char WATER = 'O'; // Representa água
    public const char SHIP = 'S'; // Representa navio
    public const char HIT = 'X'; // Representa acerto
    public const char MISS = '-'; // Representa erro

    private List<Ship> ships; // Lista de navios
    private bool[,] attacks; // Matriz para registrar ataques

    public Board()
    {
        this.board = new char[SIZE, SIZE];
        this.attacks = new bool[SIZE, SIZE];
        this.ships = new List<Ship>();
        InitializeBoard();
    }

    // Inicializa o tabuleiro com água
    private void InitializeBoard()
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                board[i, j] = WATER;
            }
        }
    }

    // Verifica se há navios no tabuleiro
    public bool HasShips()
    {
        return ships.Count > 0;
    }

    // Coloca um navio no tabuleiro
    public bool PlaceShip(int row, int col, int size, bool horizontal)
    {
        if (horizontal)
        {
            if (col + size > SIZE) return false; // Verifica se cabe no tabuleiro

            // Verifica se a posição está livre
            for (int i = 0; i < size; i++)
            {
                if (board[row, col + i] != WATER) return false;
            }

            // Verifica se a posição acima e abaixo está livre
            if (row > 0 && board[row - 1, col] != WATER) return false;
            if (row + 1 < SIZE && board[row + 1, col + size - 1] != WATER) return false;

            // Coloca o navio
            for (int i = 0; i < size; i++)
            {
                board[row, col + i] = SHIP;
            }
        }
        else
        {
            if (row + size > SIZE) return false; // Verifica se cabe no tabuleiro

            // Verifica se a posição está livre
            for (int i = 0; i < size; i++)
            {
                if (board[row + i, col] != WATER) return false;
            }

            // Verifica se a posição à esquerda e à direita está livre
            if (col > 0 && board[row, col - 1] != WATER) return false;
            if (col + 1 < SIZE && board[row + size - 1, col] != WATER) return false;

            // Coloca o navio
            for (int i = 0; i < size; i++)
            {
                board[row + i, col] = SHIP;
            }
        }

        ships.Add(new Ship(size, horizontal, row, col)); // Adiciona o navio à lista
        return true;
    }

    // Realiza um ataque em uma posição
    public bool Attack(int row, int col)
    {
        if (board[row, col] == SHIP)
        {
            board[row, col] = HIT; // Marca o acerto
            foreach (var ship in ships)
            {
                if (IsHitShip(ship, row, col))
                {
                    ship.Hit(); // Marca que o navio foi atingido
                    break;
                }
            }
            attacks[row, col] = true; // Marca que a posição foi atacada
            return true;
        }
        else if (board[row, col] == WATER)
        {
            board[row, col] = MISS; // Marca o erro
            attacks[row, col] = true; // Marca que a posição foi atacada
            return false;
        }
        return false;
    }

    // Verifica se um navio foi atingido
    private bool IsHitShip(Ship ship, int row, int col)
    {
        if (ship.IsHorizontal())
        {
            return ship.GetRow() == row && col >= ship.GetCol() && col < ship.GetCol() + ship.GetSize();
        }
        else
        {
            return ship.GetCol() == col && row >= ship.GetRow() && row < ship.GetRow() + ship.GetSize();
        }
    }

    // Verifica se a posição foi atingida
    public bool IsHit(int row, int col)
    {
        return board[row, col] == HIT;
    }

    // Verifica se todos os navios foram afundados
    public bool AllShipsSunk()
    {
        foreach (var ship in ships)
        {
            if (!ship.IsSunk())
            {
                return false;
            }
        }
        return true;
    }

    // Coloca navios aleatoriamente no tabuleiro
    public void RandomPlaceShips()
    {
        PlaceRandomShip(4);
        for (int i = 0; i < 2; i++)
        {
            PlaceRandomShip(3);
        }
        for (int i = 0; i < 3; i++)
        {
            PlaceRandomShip(2);
        }
        for (int i = 0; i < 4; i++)
        {
            PlaceRandomShip(1);
        }
    }

    // Método auxiliar para colocar navios aleatoriamente
    private void PlaceRandomShip(int size)
    {
        bool placed = false;
        while (!placed)
        {
            int row = Random.Range(0, SIZE);
            int col = Random.Range(0, SIZE);
            bool horizontal = Random.value > 0.5f; // Aleatoriamente horizontal ou vertical
            placed = PlaceShip(row, col, size, horizontal);
        }
    }

    // Getters
    public bool[,] GetAttacks()
    {
        return attacks;
    }

    public List<Ship> GetShips()
    {
        return ships;
    }
}

