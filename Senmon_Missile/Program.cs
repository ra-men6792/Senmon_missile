using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;
namespace Senmon_Missile
{
    class Program
    {
        static void Main(string[] args)
        {
            fk_Material.InitDefault();
            var win =new fk_AppWindow();
            win.ShowGuide(fk_GuideMode.GRID_XY);
            win.TrackBallMode = true;
            MakeWindow(win);

            Player player;
            player = new Player();
            player.Entry(win);

            Missile missile;
            missile = new Missile();
            missile.Entry(win);

            win.Open();
            while (win.Update() == true)
            {
                player.Move(win);
                missile.LookVec(player.Pos);
                
            }
        }
        static void MakeWindow(fk_AppWindow argWin)
        {
            argWin.Size = new fk_Dimension(950, 700);
            argWin.ShowGuide(fk_GuideMode.GRID_XY);
        }
    }
}
