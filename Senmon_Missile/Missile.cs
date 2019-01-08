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

        //ミサイル生存確認
        public bool liveflag;

        private fk_Model mmodel;
        private fk_Model mbody;
        private fk_Model mwing;
        private fk_Capsule bodyshape;
        private fk_Prism wingshape;

        private MissileMoveParticle moveParticle;

        //randamMoveの時用当たらない位置
        fk_Vector randPos;

        enum MoveMode 
        {
            Line,
            Curve,
            RandCurve,
            RandLine

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


        public Missile(int randNum)
        {
            
            liveflag = true;
            mmodel = new fk_Model();
            mbody = new fk_Model();
            mwing = new fk_Model();
            bodyshape = new fk_Capsule(2, 1.5, 0.5);
            wingshape = new fk_Prism(3, 1.25, 1.25, 0.25);
            rand = new Random();
            randPos = new fk_Vector();
            accel = new fk_Vector();
            velocity = new fk_Vector();
            position = new fk_Vector();
            diff = new fk_Vector();

            //ミサイルの移動パターン決定
            moveMode = randNum;
            if (moveMode < 10)
            {
                moveMode = (int)MoveMode.Line;
            }else if(moveMode<50){
                moveMode = (int)MoveMode.Curve;
            }
            else if(moveMode<80)
            {
                moveMode = (int)MoveMode.RandCurve;
            }
            else
            {
                moveMode = (int)MoveMode.RandLine;
            }
            //ミサイルの消滅時間
            period = rand.Next(5, 8);

        }

        public void Entry(fk_AppWindow argWin, fk_Vector pos,int randSeed)
        {
            moveParticle = new MissileMoveParticle(argWin,randSeed);
            rand = new Random(randSeed);

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
            if (moveMode == (int)MoveMode.RandCurve||moveMode==(int)MoveMode.RandLine)
            {
                randPos.x = rand.NextDouble() * 20.0 -10.0;
                randPos.y = rand.NextDouble() * 20.0 - 10.0;
                randPos.z = rand.NextDouble()*20.0-10.0;
            }
        }

        public void LookVec(fk_Vector Target, fk_AppWindow argWin)
        {
            diff = Target - mmodel.Position + randPos;
            mmodel.GlVec(diff);
            moveParticle.PushPoints(mmodel.Position);
            Move(diff, argWin);
            if (period < 0.0)
            {
                argWin.Remove(mmodel);
                argWin.Remove(mwing);
                argWin.Remove(mbody);
                liveflag = false;
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
                       
                        velocity += accel * deltatime;
                        position += velocity * deltatime;
                        mmodel.GlMoveTo(position);
                        break;
                    case (int)MoveMode.Curve:

                        accel = new fk_Vector();
                        accel += 2.0 * (Diff - velocity * period) / (period * period);
                        accel += new fk_Vector(rand.NextDouble() *20 - 10.0, rand.NextDouble() * 3.0, rand.NextDouble()*100.0-50.1);

                        period -= deltatime;
                       
                        velocity += accel * deltatime;
                        position += velocity * deltatime;
                        mmodel.GlMoveTo(position);
                        break;
                    case (int)MoveMode.RandCurve:

                        accel = new fk_Vector();
                        accel += 2.0 * (Diff - velocity * period) / (period * period);
                        accel += new fk_Vector(rand.NextDouble() * 80.0 - 40.5, 0.0, rand.NextDouble()*50.0-25.1);

                        period -= deltatime;

                       
                        velocity += accel * deltatime;
                        position += velocity * deltatime;
                        mmodel.GlMoveTo(position);
                        break;
                    case (int)MoveMode.RandLine:
                        accel = new fk_Vector();
                        accel += 2.0 * (Diff - velocity * period) / (period * period);

                        period -= deltatime;

                    
                        velocity += accel * deltatime;
                        position += velocity * deltatime;
                        mmodel.GlMoveTo(position);
                        break;

                }
            }
        }
       
    }
}
