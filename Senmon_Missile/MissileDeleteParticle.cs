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
        const int MaxParticle = 50;
        Random rand = new Random();
        fk_Polyline[] points;
        fk_Model[] pmodel;
        double deletetime = 3.5;
        fk_Vector randPos;

        public MissileDeleteParticle(fk_AppWindow argWin)
        {
            double R, G, B;
            points = new fk_Polyline[MaxParticle];
            pmodel = new fk_Model[MaxParticle];
            randPos = new fk_Vector();
            for (int n = 0; n < MaxParticle; n++)
            {
                points[n] = new fk_Polyline();
                pmodel[n].Shape = points[n];
                pmodel[n].PointSize = 4.5;

                R = rand.NextDouble();
                G = rand.NextDouble();
                B = rand.NextDouble();

                Console.WriteLine("R : " + R + " G : " + G + " B : " + B);

                pmodel[n].PointColor = new fk_Color(R * (155.0 / 256.0) + (100.0 / 256.0), G * (155.0 / 256.0) + (100.0 / 256.0), B * (155.0 / 256.0) + (100.0 / 256.0));
                argWin.Entry(pmodel[n]);
            }
        }
      
    }
}
