using System.Collections.Generic;

namespace South_Park
{

    // Структура содержит матрицу игровых клеток
    // GameMap - позволяет получить доступ к элементу матрицы

    sealed class Map 
    {
        private static Cell[,] GMap; // Матрица игровых клеток(игровая карта)

        public Map()
        {
            GMap = new Cell[13, 23];
        }

        // Возвращает весь массив игровых клеток
        static public Cell[,] WorldMatrix
        {
            get { return GMap; }
        }

        static public void Construct(char ch, sbyte rowIndex, sbyte columnIndex)
        {
            switch (ch)
            {
                case '+': 
                    Map.WorldMatrix[rowIndex, columnIndex].CellCondition = CellsCondition.Free;
                    Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionAbsent;
                    return;
                case 'F': Map.WorldMatrix[rowIndex, columnIndex].CellCondition = CellsCondition.Finish; return;
                case 'S': Map.WorldMatrix[rowIndex, columnIndex].CellCondition = CellsCondition.Start; return;
                case '0': Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionLeft; return;
                case '1': Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionLeftUp; return;
                case '2': Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionUp; return;
                case '3': Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionRightUp; return;
                case '4': Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionRight; return;
                case '5': Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionRightDown; return;
                case '6': Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionDown; return;
                case '7': Map.WorldMatrix[rowIndex, columnIndex].CellMovementDirectionCondition = CellsMovementDirectionCondition.DirectionLeftDown; return;


                    

                    
                    
               
            }
        }

    }
}
