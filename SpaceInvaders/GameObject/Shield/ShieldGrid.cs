using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldGrid : Composite
    {
        public ShieldGrid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        //~ShieldGrid()
        //{
        //}

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldGrid(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldGrid
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void VisitBomb(Bomb m)
        {
            // Missile vs ShieldGrid
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }
        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitAlienGrid(AlienGrid a)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        //public override void Remove()
        //{
        //    ForwardIterator pFor = new ForwardIterator(this);
        //    Component pCurrent = pFor.First();
        //    Component pNode = pFor.Next();

        //    while (!pFor.IsDone())
        //    {
        //        GameObject pGameObj = (GameObject)pNode;
        //        pGameObj.Remove();

        //        pNode = pFor.Next();
        //    }

        //    pCurrent.
        //}
        // Data: ---------------


    }
}
