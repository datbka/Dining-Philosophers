using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Dining_Philosophers
{
   public class Fork
    {
        public Fork(int index)
        {
            this.Index = index;
            semaphore = new Semaphore(1, 1);
        }
        public int Index;

        public PictureBox Pic;
        
        Semaphore semaphore;
        public void PickedUp()
        {
            semaphore.WaitOne();

          this.Pic.Invoke((MethodInvoker)(()=>  this.Pic.Visible = false));
        }
        public void PutDown()
        {
            
            this.Pic.Invoke((MethodInvoker)(() => this.Pic.Visible = true));
            semaphore.Release();
        }
    }
}
