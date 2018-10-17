using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBatchMan_MLink : Manager
    {
        public SpriteBatch_Link poActive;
        public SpriteBatch_Link poReserve;
    }
    public class SpriteBatchMan : SpriteBatchMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SpriteBatchMan(int reserveNum = 1, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point SpriteBatchMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            this.poNodeCompare = new SpriteBatch();
        }

//        ~SpriteBatchMan()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~SpriteBatchMan():{0} ", this.GetHashCode());
//#endif
//            //SpriteBatchMan.pInstance = null;
//            this.poNodeCompare = null;
//        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        //public static void Create(int reserveNum = 1, int reserveGrow = 1)
        //{
        //    // make sure values are ressonable 
        //    Debug.Assert(reserveNum > 0);
        //    Debug.Assert(reserveGrow > 0);

        //    // initialize the singleton here
        //    Debug.Assert(pInstance == null);

        //    // Do the initialization
        //    if (pInstance == null)
        //    {
        //        pInstance = new SpriteBatchMan(reserveNum, reserveGrow);
        //    }


        //}
        override protected void DerivedDestroyNode(DLink pLink)
        {
            // default: do nothing
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pLink, pLink.GetHashCode());
#endif
            SpriteBatch pNode = (SpriteBatch)pLink;
            Debug.Assert(pNode != null);
            pNode.Destroy();
        }

        public void Destroy()
        {
            // Get the instance
            //SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            //Debug.Assert(pMan != null);

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->SpriteBatchMan.Destroy()");
#endif
            //pMan.BaseDestroy();
            this.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", SpriteBatchMan.pInstance, SpriteBatchMan.pInstance.GetHashCode());
#endif

            //pMan.poNodeCompare = null;
            this.poNodeCompare = null;
            //SpriteBatchMan.pInstance = null;
        }

        public SpriteBatch Add(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            //SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            //Debug.Assert(pMan != null);

            SpriteBatch pNode = (SpriteBatch)this.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, reserveNum, reserveGrow);
            return pNode;
        }

        public void Draw()
        {
            //SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            //Debug.Assert(pMan != null);

            // walk through the list and render
            SpriteBatch pSpriteBatch = (SpriteBatch)this.BaseGetActive();

            //int count = 0;
            while (pSpriteBatch != null)
            {
                //count++;
                if (pSpriteBatch.display == true)
                {
                    SBNodeMan pSBNodeMan = pSpriteBatch.GetSBNodeMan();
                    Debug.Assert(pSBNodeMan != null);

                    pSBNodeMan.Draw();
                }
                pSpriteBatch = (SpriteBatch)pSpriteBatch.pNext;
            }
            //Debug.WriteLine("DrawCount:{0}", count);

        }


        public SpriteBatch Find(SpriteBatch.Name name)
        {
           // SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
           // Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            this.poNodeCompare.SetName(name);

            SpriteBatch pData = (SpriteBatch)this.BaseFind(this.poNodeCompare);
            return pData;
        }
        public void Remove(SpriteBatch pNode)
        {
            //SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            //Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            this.BaseRemove(pNode);
        }

        public void Remove(SBNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SBNodeMan pSBNodeMan = pSpriteBatchNode.GetSBNodeMan();

            Debug.Assert(pSBNodeMan != null);
            pSBNodeMan.Remove(pSpriteBatchNode);
        }

        public void Dump()
        {
            //SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            //Debug.Assert(pMan != null);

            this.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new SpriteBatch();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SpriteBatch pDataA = (SpriteBatch)pLinkA;
            SpriteBatch pDataB = (SpriteBatch)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pNode = (SpriteBatch)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pData = (SpriteBatch)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        //private static SpriteBatchMan PrivGetInstance()
        //{
        //    // Safety - this forces users to call Create() first before using class
        //    Debug.Assert(pInstance != null);

        //    return pInstance;
        //}

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        //private static SpriteBatchMan pInstance = null;
        private SpriteBatch poNodeCompare;
    }
}

