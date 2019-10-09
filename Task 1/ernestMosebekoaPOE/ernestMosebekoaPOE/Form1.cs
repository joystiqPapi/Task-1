using System;
using System.Windows.Forms;

namespace ernestMosebekoaPOE
{
    public partial class Form1 : Form, FormView
    {
        private const int numberOfUnits = 8;
        private gameEngine engine;
        private Map map;
        public Form1()
        {
            InitializeComponent();
            map = new Map(this,numberOfUnits);
            engine = new gameEngine(map);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //START timer
            tmrTimer.Enabled = true;
            map.displayUnitsOnBattlefield();
            engine.initiateBattle();
        }

        private void timerTrigger(object sender, EventArgs e)
        {
            engine.initiateBattle();
        }

        public void invalidateView(String battleField)
        {
            lblMap.Text = battleField;
        }

        public void showRound(int round)
        {
            lblTimer.Text = "Round " + round;
        }
    }
}
