using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Dining_Philosophers
{
   public class Philosopher
    {
        public Philosopher(MainForm fm)
        {
            this.mainform = fm;
        }
        public enum STATUS
        {
            THINKING,
            EATING,
            WAITING
        }
        static Random RandomInstance = new Random();
        MainForm mainform;
        public int Index;
        public Fork LeftFork;
        public Fork RightFork;
        public PictureBox Pic;
        public Label Lb;
        int forks = 0;
        public STATUS Status;
       public Thread thread;
        int maxTimeEat = 10;
        int maxTimeThink = 10;
       public bool IsLeftFork;
      public  bool IsRightFork;
        void UpdateImage()
        {
            if (forks == 0)
            {
                this.Pic.Invoke((MethodInvoker)(() => this.Pic.Image = Resources.ThinkingIcon));
                this.Status = STATUS.THINKING;
            }
               
            else if (forks == 1)
            {
                this.Pic.Invoke((MethodInvoker)(() => this.Pic.Image = Resources.OneFork));
                this.Status = STATUS.WAITING;
            }
               
            else if (forks == 2)
            {
                this.Pic.Invoke((MethodInvoker)(() => this.Pic.Image = Resources.Eating));
                this.Status = STATUS.EATING;
            }   
            UpdateLabel();
        }
        void UpdateLabel()
        {
           Lb.Invoke((MethodInvoker)(()=> Lb.Text = $@"Forks Left: {IsLeftFork}
Forks Right: {IsRightFork}
Status: {Status.ToString()}"));
        }
        public void PickUpRightFork()
        {
            if (mainform.IsDeadLock())
                mainform.SolveDeadlock();
            LeftFork.PickedUp();
            IsRightFork = true;
            forks++;
            UpdateImage();

        }
        public void PickUpLeftFork()
        {
            if (mainform.IsDeadLock())
                mainform.SolveDeadlock();
            RightFork.PickedUp();
            IsLeftFork = true;
            forks++;
            UpdateImage();

        }

        public void PutDownRightFork()
        {
            RightFork.PutDown();
            IsRightFork = false;
            forks--;
            UpdateImage();
        }
        public void PutDownLeftFork()
        {
            LeftFork.PutDown();
            IsLeftFork = false;
            forks--;
            UpdateImage();
        }
        public void Eat()
        {
            
            PickUpLeftFork();
            PickUpRightFork();
            
        }
        public void Think()
        {
            PutDownLeftFork();
            PutDownRightFork();
           
        }
        public  void Run()
        {
            thread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        int thinking = Philosopher.RandomInstance.Next(0, maxTimeThink);
                        Task.Delay(thinking * 1000).Wait();
                        Eat();
                        int eating = Philosopher.RandomInstance.Next(0, maxTimeEat);
                        Task.Delay(eating * 1000).Wait();
                        Think();
                    }
                }
                catch
                {

                }
              
            });
            thread.Start();
            
        }
        
    }
}
