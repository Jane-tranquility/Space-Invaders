using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Composite : GameObject
    {
        public Composite(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {
            this.poHead = null;
            this.poLast = null;

            this.holder = Container.COMPOSITE;
        }

        override public void Add(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.AddToLast(ref this.poHead, ref this.poLast, pComponent);
            //DLink.AddToFront(ref this.poHead, pComponent);

            pComponent.pParent = this;
        }

        override public void Remove(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            //DLink.RemoveNode(ref this.poHead, pComponent);
            DLink.RemoveNode(ref this.poHead, ref this.poLast, pComponent);
        }

        override public Component GetFirstChild()
        {
            DLink pNode = this.poHead;

            // Sometimes it returns null... that's ok
            // Scenario - we have a group without a child
            // i.e. composite with no children
            // Debug.Assert(pNode != null);

            return (Component)pNode;
        }

        override public void DumpNode()
        {
            Debug.WriteLine(" GameObject Name: {0} ({1}) <---- Composite", this.GetName(), this.GetHashCode());
        }

        public override void Print()
        {
            Debug.WriteLine(" GameObject Name: {0} ({1})", this.GetName(), this.GetHashCode());

            DLink pNode = this.poHead;

            while (pNode != null)
            {
                Component pComponent = (Component)pNode;
                pComponent.Print();

                pNode = pNode.pNext;
            }
        }

        //public override void Move()
        //{
        //    DLink pNode = this.poHead;

        //    while (pNode != null)
        //    {
        //        Component pComponent = (Component)pNode;
        //        pComponent.Move();

        //        pNode = pNode.pNext;
        //    }
        //}
        //public override void MoveDown()
        //{
        //    DLink pNode = this.poHead;

        //    while (pNode != null)
        //    {
        //        Component pComponent = (Component)pNode;
        //        pComponent.MoveDown();

        //        pNode = pNode.pNext;
        //    }
        //}
        public virtual void MoveGrid()
        {
            Debug.Assert(false);
        }

        public virtual void MoveDownGrid()
        {
            Debug.Assert(false);
        }


        public DLink poHead;
        public DLink poLast;

    }
}