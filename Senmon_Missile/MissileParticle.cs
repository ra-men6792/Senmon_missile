using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace Senmon_Missile
{
    class MissileParticle : fk_ParticleSet
    {
        private Random rand;
        private fk_Color white, gray;
        private double maxSpeed, minSpeed;
        private fk_Vector Pos;

        //コンストラクタ
        //ここで初期設定
        public MissileParticle()
        {
            Pos = new fk_Vector();
            MaxSize = 9000;//パーティクルの最大表示数
            IndivMode = true;
            AllMode = true;
            for (int i = 0; i < MaxSize; i++)
            {
                SetColorPalette(i, 0.0, 1.0, 0.6);
            }
            rand = new Random();
            white = new fk_Color(1.0, 1.0, 1.0);
            gray = new fk_Color(0.5, 0.5, 0.5);
            maxSpeed = 0.3;//これより早いパーティクルはGray
            minSpeed = 0.1;//これより遅いパーティクルはWhite
        }

        public override void GenMethod(fk_Particle p)
        {
            fk_Vector randPos = new fk_Vector();
            randPos.x = rand.NextDouble() * 1.0 - 0.5;
            randPos.y = rand.NextDouble() * 1.0 - 0.5;
            randPos.z = rand.NextDouble() * 1.0 - 0.5;
            p.Position = new fk_Vector(Pos+randPos);
            p.ColorID = p.ID;
        }

        public override void AllMethod()
        {
            for (int i = 0; i < 20; i++)
            {
                if (rand.NextDouble() < 0.9)
                {
                    NewParticle();//パーティクル生成処理
                }
            }
        }

        public override void IndivMethod(fk_Particle p)
        {
            fk_Vector pos, vec, tmp1, tmp2;
            var water = new fk_Vector(-0.2, 0.0, 0.0);
            double R = 15.0;
            double r;

            pos = p.Position;
            pos.z = 0.0;
            r = pos.Dist();

            //パーティクル速度計算
            tmp1 = water / (r * r * r);
            tmp2 = ((3.0 * (water * pos)) / (r * r * r * r)) * pos;
            vec = water + ((R * R * R) / 2.0) * (tmp1 - tmp2);
            p.Velocity = new fk_Vector(0.1, 0.0, 0.0);

            //パーティクル色計算
            double speed = vec.Dist();
            double t = (speed - minSpeed) / (maxSpeed - minSpeed);
            if (t > 1.0) t = 1.0;
            if (t < 0.0) t = 0.0;
            fk_Color newCol = (1.0 - t) * gray + t * white;
            SetColorPalette(p.ID, newCol);

            //パーティクルのX成分が‐50以下で消去
            if (pos.x < -50.0||pos.x>50.0)
            {
                RemoveParticle(p);
            }
        }

        public void getPos(fk_Vector mPos)
        {
            Pos = mPos;
        }
        
    }
}
