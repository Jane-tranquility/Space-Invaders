using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileGroup : Composite
    {
        public MissileGroup(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        //~MissileGroup()
        //{

        //}

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an MissileGroup
            // Call the appropriate collision reaction            
            other.VisitMissileGroup(this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitAlienRoot(AlienRoot a)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(a);
            ColPair.Collide(pGameObj,this);
        }
        public override void VisitAlienGrid(AlienGrid a)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(a, pGameObj);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(b);
            ColPair.Collide(this,pGameObj);
        }

        // Data: ---------------


    }
}

