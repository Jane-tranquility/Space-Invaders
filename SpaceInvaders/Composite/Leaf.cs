using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Leaf : GameObject
    {
        public Leaf(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {
            this.holder = Container.LEAF;
        }

        override public void Add(Component c)
        {
            Debug.Assert(false);
        }

        override public void Remove(Component c)
        {
            Debug.Assert(false);
        }

        override public void Print()
        {
            Debug.WriteLine(" GameObject Name: {0} ({1})", this.GetName(), this.GetHashCode());
        }

        override public void DumpNode()
        {
            Debug.WriteLine(" GameObject Name: {0} ({1})", this.GetName(), this.GetHashCode());
        }


        override public Component GetFirstChild()
        {
            Debug.Assert(false);
            return null;
        }

        //override public void MoveDownGrid()
        //{
        //    Debug.Assert(false);
        //}

        //override public void Move()
        //{
        //    Debug.Assert(false);
        //}


    }
}
