using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBatch_Link : DLink
    {

    }
    public class SpriteBatch : SpriteBatch_Link
    {
        public enum Name
        {
            Aliens,
            Boxes,
            Texts,
            Shields,
            Walls,
            UFOs,
            Bombs,

            Uninitialized
        }

        public SpriteBatch()
            : base()
        {
            this.name = SpriteBatch.Name.Uninitialized;
            this.poSBNodeMan = new SBNodeMan();
            Debug.Assert(this.poSBNodeMan != null);
            display = true;
        }

//        ~SpriteBatch()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~SpriteBatch():{0} ", this.GetHashCode());
//#endif
//            this.name = Name.Uninitialized;
//            this.poSBNodeMan = null;
//        }

        public void Destroy()
        {
            Debug.Assert(this.poSBNodeMan != null);
            this.poSBNodeMan.Destroy();
        }
        public void Set(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(this.poSBNodeMan != null);
            this.name = name;
            this.poSBNodeMan.Set(name, reserveNum, reserveGrow);
        }

        public void Attach(SpriteBase pNode)
        {
            Debug.Assert(pNode != null);

            // Go to Man, get a node from reserve, add to active, return it
            SBNode pSBNode = (SBNode)this.poSBNodeMan.Attach(pNode);
            Debug.Assert(pSBNode != null);

            // Initialize SpriteBatchNode
            pSBNode.Set(pNode, this.poSBNodeMan);

            // Back pointer
            this.poSBNodeMan.SetSpriteBatch(this);
        }

        public void Wash()
        {
        }

        public void Dump()
        {
        }

        public void SetName(SpriteBatch.Name inName)
        {
            this.name = inName;
        }

        public SpriteBatch.Name GetName()
        {
            return this.name;
        }

        public SBNodeMan GetSBNodeMan()
        {
            return this.poSBNodeMan;
        }

        // Data -------------------------------
        private Name name;
        private SBNodeMan poSBNodeMan;
        public bool display;
    }
}
