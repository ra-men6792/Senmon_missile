using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace Senmon_Missile
{
    class MissileMoveParticle
    {
       
        fk_Polyline points;
        fk_Model pmodel;
        public MissileMoveParticle(fk_AppWindow argWin,int randSeed)
        {
            Random rand = new Random(randSeed);
            double R, G, B;
            points = new fk_Polyline();
            pmodel = new fk_Model();
            pmodel.Shape = points;
            pmodel.LineWidth = 3.5;
            R = rand.NextDouble();
            G = rand.NextDouble();
            B = rand.NextDouble();

            pmodel.LineColor = new fk_Color(R * (155.0 / 256.0) + (100.0 / 256.0), G * (155.0 / 256.0) + (100.0 / 256.0), B * (155.0 / 256.0) + (100.0 / 256.0));

            argWin.Entry(pmodel);
            
        }
        public void PushPoints(fk_Vector pos)
        {
            points.PushVertex(pos);
        }
    }
}
