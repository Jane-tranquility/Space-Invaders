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
    //  * Create one - NULL Object - BoxSprite Default
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class BoxSpriteMan_MLink : Manager
    {
        public BoxSprite_Base poActive;
        public BoxSprite_Base poReserve;
    }
    public class BoxSpriteMan : BoxSpriteMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private BoxSpriteMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new BoxSprite();
        }

//        ~BoxSpriteMan()
//        {
//#if(TRACK_DESTRUCTOR)
//            Debug.WriteLine("~BoxSpriteMan():{0}", this.GetHashCode());
//#endif
//            this.poNodeCompare = null;
//            BoxSpriteMan.pInstance = null;
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
                pInstance = new BoxSpriteMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            // Get the instance
            BoxSpriteMan pMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->BoxSpriteMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", BoxSpriteMan.pInstance, BoxSpriteMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            BoxSpriteMan.pInstance = null;
        }
        public static BoxSprite Add(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            BoxSpriteMan pMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            BoxSprite pNode = (BoxSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, x, y, width, height, pColor);

            return pNode;
        }
        public static BoxSprite Find(BoxSprite.Name name)
        {
            BoxSpriteMan pMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            BoxSprite pData = (BoxSprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(BoxSprite pNode)
        {
            BoxSpriteMan pMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            BoxSpriteMan pMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new BoxSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            BoxSprite pDataA = (BoxSprite)pLinkA;
            BoxSprite pDataB = (BoxSprite)pLinkB;

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
            BoxSprite pNode = (BoxSprite)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            BoxSprite pData = (BoxSprite)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static BoxSpriteMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static BoxSpriteMan pInstance = null;
        private BoxSprite poNodeCompare;
    }
}
