using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace Senmon_Missile
{
    class Missile
    {
        private bool liveflag;

        private fk_Model mmodel;
        private fk_Model mbody;
        private fk_Model mwing;
        private fk_Capsule bodyshape;
        private fk_Prism wingshape;

        private MissileMoveParticle moveParticle;

        private enum MoveMode
        {
            Line,
            Curve,
            Random
        }
        public int moveMode;

        //移動系
        private double period;
        private Random rand;
        private fk_Vector accel;
        private fk_Vector velocity;
        private fk_Vector position;
        fk_Vector diff;
        private double deltatime = 0.03;
        private double maxAccel = 30;

        public Missile()
        {
            liveflag = true;
            mmodel = new fk_Model();
            mbody = new fk_Model();
            mwing = new fk_Model();
            bodyshape = new fk_Capsule(2, 1.5, 0.5);
            wingshape = new fk_Prism(3, 1.25, 1.25, 0.25);
            rand = new Random();

            accel = new fk_Vector();
            velocity = new fk_Vector();
            position = new fk_Vector();
            diff = new fk_Vector();

            //ミサイルの移動パターン決定
            moveMode = rand.Next(0, 3);

            //ミサイルの消滅時間
            period = rand.Next(5, 20);

        }

        public void Entry(fk_AppWindow argWin, fk_Vector pos, int seed)
        {
            moveParticle = new MissileMoveParticle(argWin);
            rand = new Random(seed);

            mbody.Shape = bodyshape;
            mwing.Shape = wingshape;

            mbody.Material = fk_Material.White;
            mwing.Material = fk_Material.LavaRed;

            mbody.GlVec(0.0, 0.0, -1.0);
            mwing.GlAngle(3.0 * FK.PI / 2.0, FK.PI / 2.0, 0.0);
            mwing.GlMoveTo(0.0, -0.125, 0.5);

            mbody.BMode = fk_BoundaryMode.AABB;
            mbody.AdjustAABB();

            argWin.Entry(mbody);
            argWin.Entry(mwing);

            mmodel.EntryChild(mbody);
            mbody.EntryChild(mwing);


            argWin.Entry(mmodel);
            position = pos;
            mmodel.GlMoveTo(pos);
        }

        public void LookVec(fk_Vector Target, fk_AppWindow argWin)
        {
            diff = Target - mmodel.Position;
            mmodel.GlVec(diff);
            moveParticle.PushPoints(mmodel.Position);
            Move(diff, argWin);
            if (period < 0.0)
            {
                fk_Vector pos = new fk_Vector();
                pos.y = rand.NextDouble() * 50.0;
                moveMode = rand.Next(0, 3);
                mmodel.GlMoveTo(Target + pos);
                period = rand.Next(5, 15);
            }
        }

        public void Move(fk_Vector Diff, fk_AppWindow argWin)
        {
            if (liveflag == true)
            {
                switch (moveMode)
                {
                    case (int)MoveMode.Line:
                        accel = new fk_Vector();
                        accel += 2.0 * (Diff - velocity * period) / (period * period);

                        period -= deltatime;
                        if (accel.Dist() > maxAccel)
                        {
                            accel = (accel / accel.Dist()) * maxAccel;
                        }
                        velocity += accel * deltatime;
                        position += velocity * deltatime;
                        mmodel.GlMoveTo(position);
                        break;
                    case (int)MoveMode.Curve:

                        accel = new fk_Vector();
                        accel += 2.0 * (Diff - velocity * period) / (period * period);
                        accel += new fk_Vector(rand.NextDouble() * 60.0 - 30.0, rand.NextDouble() * 3.0 - 1.5, 0.0);

                        period -= deltatime;
                        if (accel.Dist() > maxAccel)
                        {
                            accel = (accel / accel.Dist()) * maxAccel;
                        }
                        velocity += accel * deltatime;
                        position += velocity * deltatime;
                        mmodel.GlMoveTo(position);
                        break;
                    case (int)MoveMode.Random:
                        Diff.x = Diff.x + rand.NextDouble() * 5.0 - 2.5;
                        accel = new fk_Vector();
                        accel += 2.0 * (Diff - velocity * period) / (period * period);
                        accel += new fk_Vector(rand.NextDouble() * 80.0 - 40.0, 0.0, 0.0);

                        period -= deltatime;

                        if (accel.Dist() > maxAccel)
                        {
                            accel = (accel / accel.Dist()) * maxAccel;
                        }
                        velocity += accel * deltatime;
                        position += velocity * deltatime;
                        mmodel.GlMoveTo(position);
                        break;
                }
            }
        }
        public void HitCheck(fk_Model tmodel, fk_AppWindow argWin)
        {
            if (tmodel.IsInter(mbody) == true)
            {
                argWin.Remove(mbody);
                argWin.Remove(mwing);
                argWin.Remove(mmodel);
                liveflag = false;
            }
        }
    }
}
