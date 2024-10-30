public class Ship
{
    private readonly int size; // Tamanho do navio
    private readonly bool horizontal; // Orienta��o do navio (horizontal ou vertical)
    private readonly int row; // Posi��o da linha do navio
    private readonly int col; // Posi��o da coluna do navio
    private int hits; // N�mero de acertos no navio

    // Construtor da classe Ship
    public Ship(int size, bool horizontal, int row, int col)
    {
        this.size = size;
        this.horizontal = horizontal;
        this.row = row;
        this.col = col;
        this.hits = 0;
    }

    // Verifica se o navio est� afundado
    public bool IsSunk()
    {
        return hits >= size;
    }

    // Registra um acerto no navio
    public void Hit()
    {
        if (hits < size)
        {
            hits++;
        }
    }

    // M�todos getters
    public int GetSize() => size;
    public bool IsHorizontal() => horizontal;
    public int GetRow() => row;
    public int GetCol() => col;
    public int GetHits() => hits; // Retorna o n�mero de acertos

    // Verifica se a posi��o est� ocupada pelo navio
    public bool IsPositionOccupied(int row, int col)
    {
        if (horizontal)
        {
            return this.row == row && col >= this.col && col < this.col + size;
        }
        else
        {
            return this.col == col && row >= this.row && row < this.row + size;
        }
    }

    // Representa��o em string do navio
    public override string ToString()
    {
        return $"Ship{{ size = {size}, horizontal = {horizontal}, row = {row}, col = {col}, hits = {hits} }}";
    }
}
