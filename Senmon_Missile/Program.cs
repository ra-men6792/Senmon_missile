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
        const int MissileMaxNum = 5;
        static void Main(string[] args)
        {
            fk_Material.InitDefault();
            var win = new fk_AppWindow();
            win.ShowGuide(fk_GuideMode.GRID_XY);
            win.TrackBallMode = true;
            MakeWindow(win);

            Player player;
            player = new Player();
            player.Entry(win);
            player.Pos = new fk_Vector(20.0, -30.0, 0.0);

            Missile[] missile = new Missile[MissileMaxNum];
            for (int n = 0; n < missile.Length; n++)
            {
                fk_Vector instPos = new fk_Vector(10 * n - 30, 50, 0);
                missile[n] = new Missile();
                missile[n].Entry(win,instPos);
                Console.WriteLine(missile[n].moveMode);
            }

            win.Open();
            while (win.Update() == true)
            {
                player.Move(win);
                for(int n = 0; n < missile.Length; n++)
                {
                    missile[n].LookVec(player.Pos, win);
                }
            }
        }
        static void MakeWindow(fk_AppWindow argWin)
        {
            argWin.Size = new fk_Dimension(950, 700);
            argWin.ShowGuide(fk_GuideMode.GRID_XY);
            argWin.FPS = 55;
        }
        static void CameraSet()
        {
            fk_Model camera = new fk_Model();
            
        }
    }
}
