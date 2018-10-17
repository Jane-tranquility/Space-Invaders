﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldRoot : Composite
    {
        public ShieldRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

        }

        //~ShieldRoot()
        //{
        //}

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldRoot(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs ShieldRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void VisitBombRoot(BombRoot m)
        {
            // MissileRoot vs ShieldRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitBomb(Bomb m)
        {
            // Missile vs ShieldRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void VisitAlienRoot(AlienRoot a)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(a);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitAlienGrid(AlienGrid a)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(a, pGameObj);
        }
        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }


        // Data: ---------------


    }
}

