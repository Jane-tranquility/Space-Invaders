using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Singleton class - use only public static methods for customers
    //
    //  * One single compare node is owned by this singleton - used for find, prevent extra news
    //  * Create one - NULL Object - Sprite Default
    //  * Dependency - ImageMan needs to be initialized before SpriteMan
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class ProxySpriteMan_MLink : Manager
    {
        public ProxySprite_Base poActive;
        public ProxySprite_Base poReserve;
    }
    public class ProxySpriteMan : ProxySpriteMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private ProxySpriteMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new ProxySprite();
        }

//        ~ProxySpriteMan()
//        {
//#if(TRACK_DESTRUCTOR)
//            Debug.WriteLine("~GameSpriteMan():{0}", this.GetHashCode());
//#endif
//            this.poNodeCompare = null;
//            ProxySpriteMan.pInstance = null;
//        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new ProxySpriteMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            // Get the instance
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->GameSpriteMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", GameSpriteMan.pInstance, GameSpriteMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            ProxySpriteMan.pInstance = null;
        }

        public static ProxySprite Add(GameSprite.Name name)
        {
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            ProxySprite pNode = (ProxySprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);

            return pNode;
        }
        public static ProxySprite Find(ProxySprite.Name name)
        {
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            ProxySprite pData = (ProxySprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(GameSprite pNode)
        {
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new ProxySprite();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            ProxySprite pDataA = (ProxySprite)pLinkA;
            ProxySprite pDataB = (ProxySprite)pLinkB;

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
            ProxySprite pNode = (ProxySprite)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            ProxySprite pData = (ProxySprite)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static ProxySpriteMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static ProxySpriteMan pInstance = null;
        private ProxySprite poNodeCompare;
    }
}
