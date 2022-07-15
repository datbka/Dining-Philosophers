using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dining_Philosophers
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Ini();
        }
        void Ini()
        {
            picPhilo0.Image = Resources.ThinkingIcon;
            picPhilo1.Image = Resources.ThinkingIcon;
            picPhilo2.Image = Resources.ThinkingIcon;
            picPhilo3.Image = Resources.ThinkingIcon;
            picPhilo4.Image = Resources.ThinkingIcon;
            picPhilo3.Image = Resources.ThinkingIcon;
            forks = new Fork[5];
            forks[0] = new Fork(0) { Pic = picFork0 };
            forks[1] = new Fork(1) { Pic = picFork1 };
            forks[2] = new Fork(2) { Pic = picFork2 };
            forks[3] = new Fork(3) { Pic = picFork3 };
            forks[4] = new Fork(4) { Pic = picFork4 };
            philosophes = new Philosopher[5];
            philosophes[0] = new Philosopher(this) { Index = 0, Pic = picPhilo0, RightFork = forks[0], LeftFork = forks[4], Lb = lbPhil0 };
            philosophes[1] = new Philosopher(this) { Index = 1, Pic = picPhilo1, RightFork = forks[1], LeftFork = forks[0], Lb = lbPhil1 };
            philosophes[2] = new Philosopher(this) { Index = 2, Pic = picPhilo2, RightFork = forks[2], LeftFork = forks[1], Lb = lbPhil2 };
            philosophes[3] = new Philosopher(this) { Index = 3, Pic = picPhilo3, RightFork = forks[3], LeftFork = forks[2], Lb = lbPhil3 };
            philosophes[4] = new Philosopher(this) { Index = 4, Pic = picPhilo4, RightFork = forks[4], LeftFork = forks[3], Lb = lbPhil4 };

        }

        Philosopher[] philosophes;
        Fork[] forks;
        bool IsRun = false;
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!IsRun)
            {
                foreach (var phi in philosophes)
                {
                    phi.Run();
                }
                IsRun = true;
            }
            
           
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(btnPause.Text == "Pause")
            {
                btnPause.Text = "Continue";
                foreach(var item in philosophes)
                {
                    item.thread.Suspend();

                }
            }
            else
            {
                btnPause.Text = "Pause";
                foreach (var item in philosophes)
                {
                    item.thread.Resume();

                }
            }
        }
     public   bool IsDeadLock()
        {
            foreach(var item in philosophes)
            {
                if (item.Status != Philosopher.STATUS.WAITING) return false;
            }
            return true;
        }
        bool IsAllThinking()
        {
            foreach (var item in philosophes)
            {
                if (item.Status != Philosopher.STATUS.THINKING) return false;
            }
            return true;
        }
      public  void SolveDeadlock()
        {
            foreach(var item in philosophes)
            {
                if(item.Status == Philosopher.STATUS.WAITING)
                {
                    item.PutDownLeftFork();
                }
            }
        }
        private void btnDeadlock_Click(object sender, EventArgs e)
        {
            if (philosophes[0].thread != null) 
            if (philosophes[0].thread.IsAlive) return;
            while (!IsAllThinking())
            {
                foreach(var item in philosophes)
                {
                    if (item.Status == Philosopher.STATUS.EATING)
                    {
                        item.PutDownLeftFork();
                        item.PutDownRightFork();
                    }

                    if (item.Status == Philosopher.STATUS.WAITING)
                        item.PutDownLeftFork();
                }
              
            }
            
               foreach(var item in philosophes)
            {
                item.PickUpLeftFork();
            }
         
        }
    }
}
