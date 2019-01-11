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
        const int Line = 0;
        const int Curve = 1;
        const int RandCurve = 2;
        const int RandLine = 3;

        private enum DrawMode
        {
            Normal,
            Line,
            Curve,
            RandCurve,
            RandLine,
            Lines,
            Curves
        }

        static void Main(string[] args)
        {
            fk_Material.InitDefault();
            var win = new fk_AppWindow();
            win.TrackBallMode = true;
            MakeWindow(win);

            Player player;
            player = new Player();
            player.Entry(win);

            Enemy enemy;
            enemy = new Enemy();
            enemy.Entry(win);
                
            Random rand = new Random();

            Missile missile;
            List<Missile> missiles = new List<Missile>();

            int DMode = 0;//初期通常描画

            fk_SpriteModel ModeSprite = new fk_SpriteModel();
            string mtxt;
            if (ModeSprite.InitFont("mona.ttf") == false)
            {
                Console.WriteLine("Error");
            }
            ModeSprite.SetPositionLT(-300, 250);
            win.Entry(ModeSprite);
            mtxt = "1 : Normal";
            int t = 0;
            win.Open();
            while (win.Update() == true)
            {
                enemy.ChangePos(win);
                t++;
                rand = new Random(t);
                int nNum = rand.Next(3, 10);
                //CameraSet(win, player.Pos);
                player.Move(win);

                //描画モード選択
                if (win.GetKeyStatus('1', fk_SwitchStatus.DOWN))
                {
                    DMode = (int)DrawMode.Normal;
                    mtxt = "1 : Normal";
                }
                if (win.GetKeyStatus('2', fk_SwitchStatus.DOWN))
                {
                    DMode = (int)DrawMode.Line;
                    mtxt = "2 : Line";
                }
                if (win.GetKeyStatus('3', fk_SwitchStatus.DOWN))
                {
                    DMode = (int)DrawMode.Curve;
                    mtxt = "3 : Curve";
                }
                if (win.GetKeyStatus('4', fk_SwitchStatus.DOWN))
                {
                    DMode = (int)DrawMode.RandLine;
                    mtxt = "4 : RandLine";
                }
                if (win.GetKeyStatus('5', fk_SwitchStatus.DOWN))
                {
                    DMode = (int)DrawMode.RandCurve;
                    mtxt = "5 : RandCurve";
                }
                if (win.GetKeyStatus('6', fk_SwitchStatus.DOWN))
                {
                    DMode = (int)DrawMode.Lines;
                    mtxt = "6 : Lines";
                }
                if (win.GetKeyStatus('7', fk_SwitchStatus.DOWN))
                {
                    DMode = (int)DrawMode.Curves;
                    mtxt = "7 : Curves";
                }
                ModeSprite.DrawText("モード(1～7) = " + mtxt,true);

                //発射
                if (win.GetKeyStatus(' ', fk_SwitchStatus.DOWN))
                {
                    Console.WriteLine("発射数 : " + nNum);
                    for (int n = 0; n < nNum; n++)
                    {
                        missile = new Missile(rand.Next(0, 100));
                        //描画モードに合わせる
                        switch (DMode)
                        {
                            case (int)DrawMode.Normal:
                                break;
                            case (int)DrawMode.Line:
                                missile.moveMode = Line;
                                break;
                            case (int)DrawMode.Curve:
                                missile.moveMode = Curve;
                                break;
                            case (int)DrawMode.RandCurve:
                                missile.moveMode = RandCurve;
                                break;
                            case (int)DrawMode.RandLine:
                                missile.moveMode = RandLine;
                                break;
                            case (int)DrawMode.Lines:
                                if (rand.NextDouble() > 0.81)
                                {
                                    missile.moveMode = Line;
                                }
                                else
                                {
                                    missile.moveMode = RandLine;
                                }
                                break;
                            case (int)DrawMode.Curves:
                                if (rand.NextDouble() > 0.65)
                                {
                                    missile.moveMode = Curve;
                                }
                                else
                                {
                                    missile.moveMode = RandCurve;
                                }
                                break;

                        }
                        missiles.Add(missile);
                        missiles[missiles.Count - 1].Entry(win, player.Pos, rand.Next());
                    }

                }
                //reset
                if (win.GetKeyStatus('R', fk_SwitchStatus.DOWN))
                {
                    win.ClearModel(true);
                    player.Entry(win);
                    win.Entry(ModeSprite);
                    enemy.Entry(win);

                }
                for (int n = 0; n < missiles.Count; n++)
                {
                    //ミサイル生存確認
                    if (missiles[n].liveflag == false)
                    {
                        missiles.Remove(missiles[n]);

                    }
                    else
                    {
                        missiles[n].LookVec(enemy.Pos, win);
                    }
                }
            }
        }
        static void MakeWindow(fk_AppWindow argWin)
        {
            argWin.Size = new fk_Dimension(950, 700);
            argWin.FPS = 60;
        }
        static void CameraSet(fk_AppWindow argWin, fk_Vector parentPos)
        {
            argWin.CameraPos = parentPos + new fk_Vector(0.0, -30.0, 0.0);
            argWin.CameraFocus=parentPos;
        }
    }
}
