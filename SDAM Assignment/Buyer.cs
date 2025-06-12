using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDAM_Assignment
{
    public class Buyer : User
    {
        public override void OpenDashboard()
        {
            BuyerDash dashboard = new BuyerDash(this);  // Pass the Buyer object itself
            dashboard.Show();
        }
    }


}
