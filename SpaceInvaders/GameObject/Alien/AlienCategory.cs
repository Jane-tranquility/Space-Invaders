using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienCategory : Leaf
    {
        public enum Type
        {
            Squid,
            Octopus,
            Crab,
            AlienGrid,
            AlienColumn,

            Missile,
            MissileGroup
        }

        public AlienCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {
           // this.alienSpeed = 30f;
        }


        //public override void MoveDownGrid()
        //{
        //    this.y -= 50;
        //}

        //public override void Move()
        //{
        //    this.x += alienSpeed;
        //}


        //public float alienSpeed;
    }
}