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

            Missile missile = new Missile(0);
            List<Missile> missiles = new List<Missile>();
            int t = 0;
            win.Open();
            while (win.Update() == true)
            {
                t++;
                rand = new Random(t);
                //CameraSet(win, player.Pos);
                player.Move(win);
             
                if(win.GetKeyStatus(' ', fk_SwitchStatus.DOWN))
                {
                    for (int n = 0; n < rand.Next(5,20); n++)
                    {
                        missile = new Missile(n*t);
                        missiles.Add(missile);
                        missiles[missiles.Count - 1].Entry(win, player.Pos);
                    }
                }
                for(int n = 0; n < missiles.Count; n++)
                {
                    //ミサイル生存確認
                    if (missiles[n].liveflag == false)
                    {
                        missiles.Remove(missiles[n]);
                        break;
                    }
                    missiles[n].LookVec(new fk_Vector(), win);
                }
            }
        }
        static void MakeWindow(fk_AppWindow argWin)
        {
            argWin.Size = new fk_Dimension(950, 700);
           // argWin.ShowGuide(fk_GuideMode.GRID_XY);
            argWin.FPS = 60;
        }
        static void CameraSet(fk_AppWindow argWin,fk_Vector parentPos)
        {
            argWin.CameraPos = parentPos + new fk_Vector(0.0, -30.0, 200.0);
        }
    }
}
