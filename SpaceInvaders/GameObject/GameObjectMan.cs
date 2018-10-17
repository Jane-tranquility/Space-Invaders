using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //---------------------------------------------------------------------------------------------------------
    abstract public class GameObjectMan_MLink : Manager
    {
        public GameObjectNode_Link poActive;
        public GameObjectNode_Link poReserve;
    }
    public class GameObjectMan : GameObjectMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private GameObjectMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new GameObjectNode();
            this.poNullGameObject = new NullGameObject();

            this.poNodeCompare.poGameObj = this.poNullGameObject; ;
        }



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
                pInstance = new GameObjectMan(reserveNum, reserveGrow);
            }
            
        }

        public static void Destroy()
        {
            // Get the instance
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);
#if (TRACK_DESTRUCTOR_MAN)
                        Debug.WriteLine("--->GameObjectMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
                        Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
                        Debug.WriteLine("     {0} ({1})", GameObjectMan.pInstance, GameObjectMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            
            GameObjectMan.pInstance = null;
        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pNode = (GameObjectNode)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }


        public static GameObject Find(GameObject.Name name)
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes
            pMan.poNodeCompare.poGameObj.name = name;

            GameObjectNode pNode = (GameObjectNode)pMan.BaseFind(pMan.poNodeCompare);
            Debug.Assert(pNode != null);

            return pNode.poGameObj;
        }

        public static void Remove(GameObjectNode pNode)
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void Remove(GameObject pNode, SpriteBatchMan pSpriteBatchMan)
        {
            Debug.Assert(pNode != null);
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();

            GameObject pSafetyNode = pNode;

            // OK so we have a linked list of trees (Remember that)

            // 1) find the tree root (we already know its the most parent)

            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)Iterator.GetParent(pTmp);
            }

            // 2) pRoot is the tree we are looking for
            // now walk the active list looking for pRoot

            GameObjectNode pTree = (GameObjectNode)pMan.BaseGetActive();

            while (pTree != null)
            {
                if (pTree.poGameObj == pRoot)
                {
                    // found it
                    break;
                }
                // Goto Next tree
                pTree = (GameObjectNode)pTree.pNext;
            }

            // 3) pTree is the tree that holds pNode
            //  Now remove the node from that tree

            Debug.Assert(pTree != null);
            Debug.Assert(pTree.poGameObj != null);

            // Is pTree.poGameObj same as the node we are trying to delete?
            // Answer: should be no... since we always have a group (that was a good idea)

            Debug.Assert(pTree.poGameObj != pNode);

            GameObject pParent = (GameObject)Iterator.GetParent(pNode);
            Debug.Assert(pParent != null);

            GameObject pChild = (GameObject)Iterator.GetChild(pNode);
            //Debug.Assert(pChild == null);
            if (pChild == null)
            {
                // remove the node
                pParent.Remove(pNode);
            }
            else
            {
                GameObject pSibling = (GameObject)Iterator.GetSibling(pChild);
                while (pSibling != null)
                {
                    pChild.Remove(pSpriteBatchMan);
                    pChild = pSibling;
                    pSibling = (GameObject)Iterator.GetSibling(pChild);
                }
                pChild.Remove(pSpriteBatchMan);
                pParent.Remove(pNode);
            }
            

            // TODO - Recycle pNode

        }

        public static void Update()
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            //Debug.WriteLine("---------------");

            GameObjectNode pGameObjectNode = (GameObjectNode)pMan.BaseGetActive();

            while (pGameObjectNode != null)
            {
                //Debug.WriteLine("update: GameObjectTree {0} ({1})", pGameObjectNode.poGameObj, pGameObjectNode.poGameObj.GetHashCode());
                // Debug.WriteLine("   +++++");
                ReverseIterator pRev = new ReverseIterator(pGameObjectNode.poGameObj);

                Component pNode = pRev.First();
                while (!pRev.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;

                    //Debug.WriteLine("update: {0} ({1})", pGameObj, pGameObj.GetHashCode());
                    pGameObj.Update();

                    pNode = pRev.Next();
                }

                //Debug.WriteLine("   ------");
                pGameObjectNode = (GameObjectNode)pGameObjectNode.pNext;
            }

        }

        public static void Dump()
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new GameObjectNode();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameObjectNode pDataA = (GameObjectNode)pLinkA;
            GameObjectNode pDataB = (GameObjectNode)pLinkB;

            Boolean status = false;

            if (pDataA.poGameObj.GetName() == pDataB.poGameObj.GetName())
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pNode = (GameObjectNode)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pData = (GameObjectNode)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GameObjectMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        public static GameObjectMan pInstance = null;
        private GameObjectNode poNodeCompare;
        private NullGameObject poNullGameObject;
    }
}
