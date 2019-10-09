using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ernestMosebekoaPOE
{
    class gameEngine
    { //class variable
        int numberOfRoundsCompleted = 0;
        private Map battlefield;

        public gameEngine(Map battleField)
        {
            this.battlefield = battleField;
        }

        public void initiateBattle()
        {
            Unit[] unitList = battlefield.getUnits();
            for (int count = 0; count < unitList.Length; count++)
            {
                Unit unit = unitList[count];
                if (!unit.isDead())
                {
                    if (isFitForBattle(unit.returnAttackStrength()))
                    {
                        attackClosestEnemy(unit, unitList);
                    }
                    else
                    {
                        battlefield.unitRunAway(unit);
                    }
                }
            }
            numberOfRoundsCompleted++;
            battlefield.showRounds(numberOfRoundsCompleted);
        }

        private void attackClosestEnemy(Unit attackingUnit, Unit [] unitList)
        {
            int[] closestEnemyDetails = battlefield.getClosestEnemy(attackingUnit);
            Unit closestEnemy = unitList[closestEnemyDetails[0]];
            int closestEnemyDistance = closestEnemyDetails[1];

            if (attackingUnit.withinRange(closestEnemyDistance))
            {
                closestEnemy.combat(attackingUnit.returnAttackStrength());
                battlefield.getUnits()[closestEnemyDetails[0]] = closestEnemy;
            }
        }

        private bool isFitForBattle(int unitHealth)
        {
            return unitHealth >= 25;
        }
    }
}
