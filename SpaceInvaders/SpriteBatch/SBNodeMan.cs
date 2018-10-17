using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SBNodeMan_MLink : Manager
    {
        public SBNode_Link poActive = null;
        public SBNode_Link poReserve = null;
    }

    public class SBNodeMan : SBNodeMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------

        public SBNodeMan(int reserveNum = 1, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point SBMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            //  Debug.WriteLine("x--->:SBNodeMan({0}) ", this.GetHashCode());

            this.poNodeCompare = new SBNode();

            //  Debug.WriteLine("    x--->:SBNode({0}) ", this.poNodeCompare.GetHashCode());

            this.pBackSpriteBatch = null;

        }

//        ~SBNodeMan()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~SBNodeMan():{0} ", this.GetHashCode());
//#endif
//            this.name = SpriteBatch.Name.Uninitialized;
//            this.poNodeCompare = null;
//            this.pBackSpriteBatch = null;
//        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------

        public void Destroy()
        {
            // Get the instance
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("         SBNodeMan.Destroy({0})",this.GetHashCode());
#endif
            this.BaseDestroy();

#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("             {0} ({1})", this.poNodeCompare, this.poNodeCompare.GetHashCode());
#endif
            this.name = SpriteBatch.Name.Uninitialized;
            this.poNodeCompare = null;
            this.pBackSpriteBatch = null;
        }
        override protected void DerivedDestroyNode(DLink pLink)
        {
            // default: do nothing
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("             {0} ({1})", pLink, pLink.GetHashCode());
#endif
            pLink = null;
        }

        public void Set(SpriteBatch.Name name, int reserveNum, int reserveGrow)
        {
            this.name = name;

            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            this.BaseSetReserve(reserveNum, reserveGrow);
        }

        //public SBNode Attach(GameSprite.Name name)
        //{
        //    SBNode pNode = (SBNode)this.BaseAdd();
        //    Debug.Assert(pNode != null);

        //    // Initialize SpriteBatchNode
        //    pNode.Set(name);

        //    return pNode;
        //}
        //public SBNode Attach(BoxSprite.Name name)
        //{

        //    SBNode pNode = (SBNode)this.BaseAdd();
        //    Debug.Assert(pNode != null);

        //    // Initialize SpriteBatchNode
        //    pNode.Set(name);

        //    return pNode;
        //}

        //public SBNode Attach(ProxySprite pNode)
        //{
        //    // Go to Man, get a node from reserve, add to active, return it
        //    SBNode pSBNode = (SBNode)this.BaseAdd();
        //    Debug.Assert(pSBNode != null);

        //    // Initialize SpriteBatchNode
        //    pSBNode.Set(pNode);

        //    return pSBNode;
        //}


        public SBNode Attach(SpriteBase pNode)
        {
            // Go to Man, get a node from reserve, add to active, return it
            SBNode pSBNode = (SBNode)this.BaseAdd();
            Debug.Assert(pSBNode != null);

            // Initialize SpriteBatchNode
            pSBNode.Set(pNode, this);

            return pSBNode;
        }

        public void Draw()
        {

            // walk through the list and render
            SBNode pNode = (SBNode)this.BaseGetActive();

            while (pNode != null)
            {
                // Assumes someone before here called update() on each sprite
                // Draw me.
                pNode.GetSpriteBase().Render();

                pNode = (SBNode)pNode.pNext;
            }
        }

        public void Remove(SBNode pNode)
        {
            Debug.Assert(pNode != null);
            this.BaseRemove(pNode);
        }
        public void Dump()
        {
            this.BaseDump();
        }

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }

        public void SetSpriteBatch(SpriteBatch _pSpriteBatch)
        {
            this.pBackSpriteBatch = _pSpriteBatch;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new SBNode();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SBNode pDataA = (SBNode)pLinkA;
            SBNode pDataB = (SBNode)pLinkB;

            Boolean status = false;

            // Stubbed this function out
            if (pLinkB == pLinkA)
            {
                status = false;
            }
            else
            {
                status = false;
            }

            return status;
        }
        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SBNode pNode = (SBNode)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SBNode pData = (SBNode)pLink;
            // pData.Dump();
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private SBNode poNodeCompare;
        private SpriteBatch.Name name;
        private SpriteBatch pBackSpriteBatch;
    }
}
