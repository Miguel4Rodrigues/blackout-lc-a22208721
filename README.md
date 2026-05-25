# Blackout

Projeto desenvolvido no âmbito da unidade curricular de LP1 (Linguagens de Programação I).

---

# Autores

## Margarida Teles
- Desenvolvimento do Game Loop (Controller)
- Auxilio no desenvolvimento do README

## Miguel Rodrigues
- Desenvolvimento da Game View
- Criação do README
- Auxilio no desenvolvimento do Game Loop (Controller)

---

# Repositório Git

GitHub: https://github.com/MargaridaTeles/Blackout.git

---

# Descrição do Projeto

Blackout é um jogo de puzzle jogado numa grelha de células. Cada célula pode estar ligada ou desligada.

Quando o jogador seleciona uma célula:
- O estado da célula é invertido (ON para OFF e OFF para ON);
- O estado das células adjacentes (cima, baixo, esquerda e direita) também é invertido.

O objetivo do jogo é desligar todas as células da grelha.

O jogo inclui diferentes níveis de dificuldade:
- 3x3 (Fácil)
- 5x5 (Médio)
- 8x8 (Dificil)

---

# Arquitetura da Solução

O projeto foi desenvolvido segundo o padrão MVC (Model-View-Controller).

## Model
Responsável pelos dados e pela lógica do jogo:
- Estado da grelha
- Regras do jogo
- Verificação de vitória
- Manipulação das células

## View
Responsável pela interface com o utilizador:
- Apresentação da grelha
- Menus
- Informação do jogo

A interface é desenvolvida utilizando a biblioteca Spectre.Console.

## Controller
Responsável pela comunicação entre a View e o Model:
- Receção de input do jogador
- Atualização do estado do jogo
- Gestão do fluxo do jogo

---

# Algoritmos Utilizados

## Inicialização da Grelha

A grelha começa com todas as células desligadas.

Depois disso, são realizados vários cliques aleatórios na grelha, dependendo da dificuldade escolhida, para gerar o estado inicial do puzzle.

## Atualização das Células

Quando o jogador seleciona uma posição:
1. A célula selecionada muda de estado;
2. As células adjacentes também mudam de estado;
3. O jogo verifica se todas as células estão desligadas.

## Verificação de Vitória

O jogo percorre todas as células da grelha para verificar se existem células ligadas.

Caso todas estejam desligadas, o jogador vence.

---

# Diagrama UML

```mermaid
classDiagram

class Grid {
  - Cell[ , ] cells
  + int Rows
  + int Columns
  - static readonly Random
  + Grid(int rows, int columns)
  + ApplyRandomClicks(int clicks)
  + ToggleVonNeumann(int row, int col)
  + Toggle(int r, int c)
  + IsVictory()
  + GetCell(int row, int col)
}

class Cell {
  - CellState State
  + Toggle()
}

class GameView {
  + StartGrid(grid)
  + UpdateGrid(grid, row, col)
  - DrawGridLines()
  - DrawCells(grid, row, col)
  + ShowMenu()
  + ShowExittMessage()
  + ShowInstructions()
  + SelectDifficulty()
  + ShowProgressBar(message)
  + ReadInputPlayer()
  + ShowVictory()
}

class Controller {
  - Grid grid
  - int selectedRow
  - int selectedCol
  - int size
  - bool isRunning
  + Run(view)
  + CreateGrid(size)
  - GetGameSize(view)
  - HandleInput(key)
}

class CellState {
  <<enumeration>>
  ON
  OFF
}

class Program {
  + Main()
}

Program --> Controller
Program --> GameView
Controller --> Grid
Grid --> Cell
Cell --> CellState
Controller --> GameView
```

---

# Bibliotecas Utilizadas

## Bibliotecas
- Spectre.Console
- .NET 10

## Ferramentas
- Git
- GitHub
- Visual Studio

---

# Referências

- Spectre.Console Documentation  
  https://spectreconsole.net/

---

# Utilização de IA

Foi utilizada IA generativa (ChatGPT) para:
- Esclarecimento de dúvidas;
- Apoio na organização do README;
- Apoio na documentação XML.

Toda a lógica e arquitetura do projeto foram desenvolvidas e compreendidas pelos elementos do grupo.