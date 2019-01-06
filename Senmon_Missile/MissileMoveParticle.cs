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
        fk_Point points;
        fk_Model pmodel;
        public MissileMoveParticle(fk_AppWindow argWin)
        {
            points = new fk_Point();
            pmodel = new fk_Model();
            pmodel.Shape = points;
            pmodel.PointSize = 5.0;
            pmodel.Material = fk_Material.White;
            argWin.Entry(pmodel);
            
        }
        public void PushPoints(fk_Vector pos)
        {
            points.PushVertex(pos);
        }
    }
}
