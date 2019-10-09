﻿namespace ernestMosebekoaPOE
{
    class MeleeUnit : Unit
    {
        private const int MeleeAttackRange = 1;
        private const int MeleeAttack = 5;
        private const int MeleeSpeed = 1;
        private const int InitialMelleHealth = 100;
        private const bool InMeleeUnitAttacking = false;
        private const string MeleeTeamName = "MeleeTeam";
        private const char MeleeUnitSymbol = 'M';
        
        private string unitActivity;

        public int XPosition
        {
            get => base.xPosition;
            set => base.xPosition = value;
        }

        public int YPosition
        {
            get => base.yPosition;
            set => base.yPosition = value;
        }

        public int Health
        {
            get => base.health;
            set => base.health = value;
        }

        public int MaxHealth
        {
            get => base.maxHealth;
        }

        public int Speed
        {
            get => base.speed;
            set => base.speed = value;
        }

        public int Attack
        {
            get => base.attack;
            set => base.attack = value;
        }

        public int AttackRange
        {
            get => base.attackRange;
            set => base.attackRange = value;
        }

        public string Team
        {
            get => base.team;
            set => base.team = value;
        }

        public char UnitSymbol
        {
            get => base.unitSymbol;
            set => base.unitSymbol = value;
        }

        public bool IsAttacking
        {
            get => base.isAttacking;
            set => base.isAttacking = value;
        }

        public MeleeUnit(int xPosition, int yPosition) : base(xPosition, yPosition, InitialMelleHealth, MeleeSpeed,
            MeleeAttack, MeleeAttackRange, MeleeTeamName, MeleeUnitSymbol, InMeleeUnitAttacking)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }

        public override void combat(int enemyAttackStrength)
        {
            int newHealth = Health - enemyAttackStrength;
            unitActivity += " has taken a blow of " + enemyAttackStrength + " which drops it's health from" + Health + "to " + newHealth + "\n";
            Health = newHealth;
        }

        public override bool isDead()
        {
            bool isDead = Health <= 0;
            if (isDead)
            {
                unitActivity += " has been destroyed\n";
            }
            return Health <= 0;
        }

        public override void move(int newXPosition, int newYPosition)
        {
            unitActivity += " has moved from (" + XPosition + "," + YPosition + ") position to (" + newXPosition + "," + newXPosition + ")\n";
            XPosition = newXPosition;
            YPosition = newYPosition;
        }

        public override int[] returnPosition()
        {
            int[] unitPosition = new int[2];
            unitPosition[Map.X_POSITION_IN_UNIT_POSITION_LIST] = XPosition;
            unitPosition[Map.Y_POSITION_IN_UNIT_POSITION_LIST] = YPosition;
            return unitPosition;
        }

        public override string toString()
        {
            return Team + unitActivity;
        }

        public override int returnAttackStrength()
        {
            unitActivity += " has initiated an attack on " + Attack + "\n";
            isAttacking = true;
            return Attack;
        }

        public override bool withinRange(int distance)
        {
            return AttackRange <= distance;
        }
        public override char returnSymbol()
        {
            return UnitSymbol;
        }
    }
}