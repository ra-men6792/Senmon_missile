using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace Senmon_Missile
{
    class MissileDeleteParticle
    {
        Random rand = new Random();
        fk_Point points;
        fk_Model pmodel;
        public MissileDeleteParticle(fk_AppWindow argWin)
        {
            double R, G, B;
            points = new fk_Point();
            pmodel = new fk_Model();
            pmodel.Shape = points;
            pmodel.PointSize = 4.5;
            R = rand.NextDouble();
            G = rand.NextDouble();
            B = rand.NextDouble();

            Console.WriteLine("R : " + R + " G : " + G + " B : " + B);

            pmodel.PointColor = new fk_Color(R * (155.0 / 256.0) + (100.0 / 256.0), G * (155.0 / 256.0) + (100.0 / 256.0), B * (155.0 / 256.0) + (100.0 / 256.0));
            argWin.Entry(pmodel);

        }
        public void PushPoints(fk_Vector pos)
        {
            points.PushVertex(pos);
        }
    }
}
