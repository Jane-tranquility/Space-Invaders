using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class MissileCategory : Leaf
    {
        public enum Type
        {
            Missile,
            MissileGroup,
            Unitialized
        }

        public MissileCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {

        }


        // Data: ---------------
        //~MissileCategory()
        //{

        //}



    }
}