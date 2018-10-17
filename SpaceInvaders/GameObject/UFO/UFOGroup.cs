using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOGroup : Composite
    {
        public UFOGroup(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        //~UFOGroup()
        //{

        //}

        public override void Accept(ColVisitor other)
        {         
            other.VisitUFOGroup(this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }


        // Data: ---------------


    }
}
