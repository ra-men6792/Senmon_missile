using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace Senmon_Missile
{
    class Enemy
    {
        Random rand;
        private fk_Model emodel;
        private fk_Cone cone;
        public fk_Vector Pos
        {
            get { return emodel.Position; }
            set { emodel.GlMoveTo(value); }
        }

        public Enemy()
        {
            emodel = new fk_Model();
            cone = new fk_Cone(4, 1.5, 3.5);
            rand = new Random();
        }
        public void Entry(fk_AppWindow argWin)
        {
            emodel.Shape = cone;
            emodel.Material = fk_Material.LightCyan;
            emodel.GlVec(0.0, -1.0, 0.0);
            Pos = new fk_Vector(0.0, 20.0, 0.0);
            argWin.Entry(emodel);
        }

        public void ChangePos(fk_AppWindow argWin)
        {
            if (argWin.GetMouseStatus(fk_MouseButton.MOUSE1, fk_SwitchStatus.PRESS, false) == true)
            {
                var plane = new fk_Plane();
                plane.SetPosNormal(new fk_Vector(0.0, 0.0, 0.0), new fk_Vector(0.0, 0.0, 1.0));
                var pos3D = new fk_Vector();
                var pos2D = argWin.MousePosition;
                argWin.GetProjectPosition(pos2D.x, pos2D.y, plane, pos3D);
                Pos = pos3D;
            }
        }
       
    }
}
