using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class UFOCategory : Leaf
    {
        public enum Type
        {
            RightMovingUFO,
            LeftMovingUFO,
            Unitialized
        }

        public UFOCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {

        }


        // Data: ---------------
        //~UFOCategory()
        //{

        //}



    }
}