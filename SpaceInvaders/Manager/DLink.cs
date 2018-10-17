using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class DLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        protected DLink()
        {
            this.Clear();
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public void Clear()
        {
            this.pNext = null;
            this.pPrev = null;
        }

        //----------------------------------------------------------------------
        // Static methods
        //----------------------------------------------------------------------
        public static void AddToFront(ref DLink pHead, DLink pNode)
        {
            // Will work for Active or Reserve List

            // add to front
            Debug.Assert(pNode != null);

            // add node
            if (pHead == null)
            {
                // push to the front
                pHead = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // push to front
                pNode.pPrev = null;
                pNode.pNext = pHead;

                pHead.pPrev = pNode;
                pHead = pNode;
            }

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }

        public static void AddToLast(ref DLink pHead, ref DLink pLast, DLink pNode)
        {
            // Will work for Active or Reserve List

            // add to front
            Debug.Assert(pNode != null);

            // add node
            if (pLast == pHead && pHead == null)
            {
                // push to the front
                pHead = pNode;
                pLast = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                Debug.Assert(pLast != null);
                // add to end
                pLast.pNext = pNode;
                pNode.pPrev = pLast;
                pNode.pNext = null;

                pLast = pNode;
                // ---> no change for pHead
            }

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
            Debug.Assert(pLast != null);
        }

        public static void addSorted(ref DLink head, DLink link, float time)
        {
            if (head == null)
            {
                head = link;
            }
            else
            {
                DLink current = head;
                if (((TimeEvent)current).triggerTime > time)
                {
                    link.pNext = head;
                    head.pPrev = link;
                    head = link;
                }
                else
                {
                    while (current.pNext != null && ((TimeEvent)current.pNext).triggerTime < time)
                    {
                        current = current.pNext;
                    }
                    if (current.pNext == null)
                    {
                        current.pNext = link;
                        link.pPrev = current;
                    }
                    else
                    {
                        link.pNext = current.pNext;
                        current.pNext.pPrev = link;
                        link.pPrev = current;
                        current.pNext = link;
                    }
                }


            }
        }
        public static DLink PopFromFront(ref DLink pHead)
        {
            // There should always be something on list
            Debug.Assert(pHead != null);

            // return node
            DLink pNode = pHead;

            // Update head (OK if it points to NULL)
            pHead = pHead.pNext;
            if (pHead != null)
            {
                pHead.pPrev = null;
            }

            // HUGELY important - otherwise its crossed linked 
            //      Very hard to figure out
            pNode.Clear();

            return pNode;
        }
        public static void RemoveNode(ref DLink pHead, DLink pNode)
        {
            // protection
            Debug.Assert(pNode != null);

            // 4 different conditions... 
            if (pNode.pPrev != null)
            {	// middle or last node
                pNode.pPrev.pNext = pNode.pNext;
            }
            else
            {  // first
                pHead = pNode.pNext;
            }

            if (pNode.pNext != null)
            {	// middle node
                pNode.pNext.pPrev = pNode.pPrev;
            }
        }

        public static void RemoveNode(ref DLink pHead, ref DLink pLast, DLink pNode)
        {
            // protection
            Debug.Assert(pNode != null);

            // Quick HACK... might be a bug... need to diagram

            // 4 different conditions... 
            if (pNode.pPrev != null)
            {	// middle or last node
                pNode.pPrev.pNext = pNode.pNext;

                if (pNode == pLast)
                {
                    pLast = pNode.pPrev;
                }
            }
            else
            {  // first
                pHead = pNode.pNext;

                if (pNode == pLast)
                {
                    // Only one node
                    pLast = pNode.pNext;
                }
                else
                {
                    // Only first not the last
                    // do nothing more
                }
            }

            if (pNode.pNext != null)
            {	// middle node
                pNode.pNext.pPrev = pNode.pPrev;
            }

        }


        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        public DLink pNext;
        public DLink pPrev;
    }
}
