using System;
using System.Linq;

namespace ernestMosebekoaPOE
{
    class Map
    {
        //class variables
        private const int MAP_SIZE = 20;
        public const int X_POSITION_IN_UNIT_POSITION_LIST = 0;
        public const int Y_POSITION_IN_UNIT_POSITION_LIST = 1;

        char[,] mapArray;
        private Unit[] unitList;
        private Random randomizer;
        private FormView view;

        public Map(FormView view, int numberOfUnits)
        {
            this.view = view;
            this.randomizer = new Random();
            mapArray = new char[MAP_SIZE, MAP_SIZE];
            generateUnits(numberOfUnits);
        }

        private void generateUnits(int numberOfUnits)
        {
            unitList = new Unit[numberOfUnits];
            for (int i = 0; i < numberOfUnits; i++)
            {
                unitList[i] = createRandomUnit();
            }
        }

        private Unit createRandomUnit()
        {
            int randomNumber = randomizer.Next(0, 2);
            int unitXPosition = generateRandomUnitPosition();
            int unitYPosition = generateRandomUnitPosition();
            
            if (randomNumber == 1)
            {
                return new MeleeUnit(unitXPosition, unitYPosition);
            }
            else
            {
                return new RangedUnit(unitXPosition, unitYPosition);
            }
        }

        private int generateRandomUnitPosition()
        {
            return randomizer.Next(0, MAP_SIZE);
        }

        public void displayUnitsOnBattlefield()
        {
            int [][] layout = new int [unitList.Length][];
            char[] symbol = new char [unitList.Length];
            for (int count = 0; count < unitList.Length; count++)
            {
                Unit unit = unitList[count];
                layout[count] = unit.returnPosition();
                symbol[count] = unit.returnSymbol();
            }
            view.invalidateView(buildBattleField(layout, symbol));
        }

        public void updateBattlefield(int unitPosition, int xPosition, int yPosition)
        {
            Unit unit = unitList[unitPosition];
            unit.move(xPosition, yPosition);
            unitList[unitPosition] = unit;
            displayUnitsOnBattlefield();
        }

        public Unit[] getUnits() {
            return unitList; 
        }
        
        public int[] getClosestEnemy(Unit engagingUnit)
        {
            int[] engagingUnitPosition = engagingUnit.returnPosition();
            int engagingXPosition = engagingUnitPosition[X_POSITION_IN_UNIT_POSITION_LIST];
            int engagingYPosition = engagingUnitPosition[Y_POSITION_IN_UNIT_POSITION_LIST];
            int [] unitDistances = new int[unitList.Length];

            for (int count = 0; count <  unitList.Length; count++)
            {
                Unit enemyUnit = unitList[count];
                int[] enemyUnitPosition = enemyUnit.returnPosition();
                unitDistances[count] = distanceBetweenPoints(engagingXPosition, engagingYPosition,
                    enemyUnitPosition[X_POSITION_IN_UNIT_POSITION_LIST],
                    enemyUnitPosition[Y_POSITION_IN_UNIT_POSITION_LIST]);
            }

            int closestEnemyDistance = (from number in unitDistances orderby number ascending select number).Distinct().Skip(1).First();
            int closetEnemyListPosition = Array.IndexOf(unitDistances, closestEnemyDistance);
            return new []{closetEnemyListPosition, closestEnemyDistance};
        }

        private int distanceBetweenPoints(int x1, int y1, int x2, int y2)
        {
            int xAxis = x2 - x1;
            int xAxisSquared = xAxis * xAxis;

            int yAxis = y2 - y1;
            int yAxisSquared = yAxis * yAxis;

            return (int)Math.Sqrt(xAxisSquared + yAxisSquared);
        }

        public void unitRunAway(Unit unit)
        {
            int[] unitPosition = unit.returnPosition();
            int unitXPosition = unitPosition[X_POSITION_IN_UNIT_POSITION_LIST];
            int unitYPosition = unitPosition[Y_POSITION_IN_UNIT_POSITION_LIST];
            if (unitXPosition != 0)
            {
                int minX = unitXPosition - 1;
                int maxX = unitXPosition + 1;
                int minY = unitYPosition - 1;
                int maxY = unitYPosition + 1;
                Random random = new Random();
                unitXPosition = random.Next(minX, maxX);
                unitYPosition = random.Next(minY, maxY);
                unit.move(unitXPosition, unitYPosition);
            }

            displayUnitsOnBattlefield();
        }

        private string buildBattleField(int [][] battleFieldLayout, char [] symbol)
        {
            string field = "";
            for (int count = 0; count < MAP_SIZE; count++)
            {
                for (int i = 0; i < MAP_SIZE; i++)
                {
                    field += buildUnits(battleFieldLayout, symbol, count, i);
                }
                field += "\n";
            }
            return field;
        }

        private string buildUnits(int[][] battleFieldLayout, char[] symbol, int xCord, int yCord)
        {
            for (int count = 0; count < battleFieldLayout.Length; count++)
            {
                int[] coodinates = battleFieldLayout[count];
                if (xCord == coodinates[0] && yCord == coodinates[1])
                {
                    return symbol[count].ToString();
                }
            }

            return "    ";
        }

        public void showRounds(int round)
        {
            view.showRound(round);
        }
    }
}