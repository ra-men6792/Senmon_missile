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
        const int MissileMaxNum = 300;
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
            player.Pos = new fk_Vector(0.0, -30.0, 0.0);

            Random rand = new Random();

            Missile[] missile = new Missile[MissileMaxNum];
            for (int n = 0; n < missile.Length; n++)
            {
                fk_Vector instPos = new fk_Vector(rand.NextDouble()*30.0-15.0, -30, 0);
                missile[n] = new Missile();
                missile[n].Entry(win,instPos,n*rand.Next());
               // Console.WriteLine(missile[n].moveMode);
            }
            CameraSet(win, player.Pos);
            win.Open();
            while (win.Update() == true)
            {
                CameraSet(win, player.Pos);
                player.Move(win);
                for(int n = 0; n < missile.Length; n++)
                {
                    missile[n].HitCheck(player.pmodel, win);
                    missile[n].LookVec(player.Pos, win);
                }
            }
        }
        static void MakeWindow(fk_AppWindow argWin)
        {
            argWin.Size = new fk_Dimension(950, 700);
            argWin.ShowGuide(fk_GuideMode.GRID_XY);
            argWin.FPS = 60;
        }
        static void CameraSet(fk_AppWindow argWin,fk_Vector parentPos)
        {
            argWin.CameraPos = parentPos + new fk_Vector(0.0, -30.0, 200.0);
        }
    }
}
