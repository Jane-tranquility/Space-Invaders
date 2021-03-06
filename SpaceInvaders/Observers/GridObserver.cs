﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridObserver : ColObserver
    {
        public GridObserver()
        {

        }
        public override void Notify()
        {
            Debug.WriteLine("Grid_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // OK do some magic
            AlienGrid pGrid = (AlienGrid)this.pSubject.pObjA;

            WallCategory pWall = (WallCategory)this.pSubject.pObjB;
            if (pWall.GetCategoryType() == WallCategory.Type.Right)
            {
                if (pGrid.flag == true)
                {
                    pGrid.SetDelta(-20.0f);
                    pGrid.MoveDownGrid();
                    pGrid.flag = false;
                }

            }
            else if (pWall.GetCategoryType() == WallCategory.Type.Left)
            {
                if (pGrid.flag == false)
                {
                    pGrid.SetDelta(20.0f);
                    pGrid.MoveDownGrid();
                    pGrid.flag = true;
                }
            }
            else
            {
                Debug.Assert(false);
            }

        }
    }
}
