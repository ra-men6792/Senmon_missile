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
        private fk_Model mmodel;
        private fk_Model mbody;
        private fk_Model mwing;
        private fk_Capsule bodyshape;
        private fk_Prism wingshape;
        public Missile()
        {
            mmodel = new fk_Model();
            mbody = new fk_Model();
            mwing = new fk_Model();
            bodyshape = new fk_Capsule(32, 1.5, 0.5);
            wingshape = new fk_Prism(3, 1.25, 1.25, 0.25);
        }

        public void Entry(fk_AppWindow argWin)
        {
            mbody.Shape = bodyshape;
            mwing.Shape = wingshape;

            mbody.Material = fk_Material.White;
            mwing.Material = fk_Material.LavaRed;

            mbody.GlVec(0.0, 0.0, -1.0);
            mwing.GlAngle(3.0*FK.PI/2.0,FK.PI/2.0, 0.0);
            mwing.GlMoveTo(0.0, -0.125, 0.5);

            argWin.Entry(mbody);
            argWin.Entry(mwing);

            mmodel.EntryChild(mbody);
            mbody.EntryChild(mwing);

            argWin.Entry(mmodel);
                      
        }

        public void LookVec(fk_Vector Target)
        {
            mmodel.GlVec(Target);
        }
    }
}
