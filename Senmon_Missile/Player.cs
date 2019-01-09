using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace Senmon_Missile
{
    class Player
    {
        public fk_Model pmodel;
        private fk_Cone cone;
        public fk_Vector Pos//player位置取得
        {
            get { return pmodel.Position; }
            set { pmodel.GlMoveTo(value); }
        }
        private double Speed;
        public Player()
        {
            pmodel = new fk_Model();
            cone = new fk_Cone(4, 1.5, 3.5);
            Speed = 0.2;
        }
        public void Entry(fk_AppWindow argWin)
        {
            pmodel.Shape = cone;
            pmodel.Material = fk_Material.Red;
            pmodel.GlVec(0.0, 1.0, 0.0);
            Pos = new fk_Vector(0.0, -20.0, 0.0);
            pmodel.BMode = fk_BoundaryMode.AABB;
            pmodel.AdjustAABB();
            argWin.Entry(pmodel);
        }
        public void Move(fk_AppWindow argWin)
        {
            fk_Vector origin = new fk_Vector(0.0,0.0,-2.0);
            if (argWin.GetSpecialKeyStatus(fk_SpecialKey.LEFT, fk_SwitchStatus.PRESS))
            {
                pmodel.LoTranslate(-Speed, 0.0, 0.0);
            }
            if (argWin.GetSpecialKeyStatus(fk_SpecialKey.RIGHT, fk_SwitchStatus.PRESS))
            {
                pmodel.LoTranslate(Speed, 0.0,0.0);
            }
            if (argWin.GetSpecialKeyStatus(fk_SpecialKey.UP, fk_SwitchStatus.PRESS))
            {
                pmodel.LoTranslate(0.0, 0.0, -Speed);
            }
            if (argWin.GetSpecialKeyStatus(fk_SpecialKey.DOWN, fk_SwitchStatus.PRESS))
            {
                pmodel.LoTranslate(0.0, 0.0, Speed * 3.0);
            }
        }
        
    }
}
