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
            points = new fk_Point();
            pmodel = new fk_Model();
            pmodel.Shape = points;
            pmodel.PointSize = 4.5;
            pmodel.PointColor = new fk_Color(rand.NextDouble() * (155.0 / 256.0) + (100.0 / 256.0), rand.NextDouble() * (155.0 / 256.0) + (100.0 / 256.0), rand.NextDouble() * (155.0 / 256.0) + (100.0 / 256.0));
            argWin.Entry(pmodel);

        }
        public void PushPoints(fk_Vector pos)
        {
            points.PushVertex(pos);
        }
    }
}
