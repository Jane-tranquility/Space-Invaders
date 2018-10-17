using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AddScoreObserver : ColObserver
    {
        public AddScoreObserver()
        {
            
        }

        public override void Notify()
        {
            SpaceInvaders pSI = SpaceInvaders.GetInstance();
            GameObject pObject = this.pSubject.pObjB;
            GameObject.Name name = pObject.name;
            switch (name)
            {
                case GameObject.Name.Squid:
                    pSI.scoreOne += 60;
                    break;

                case GameObject.Name.Octopus:
                    pSI.scoreOne += 20;
                    break;

                case GameObject.Name.Crab:
                    pSI.scoreOne += 40;
                    break;

                case GameObject.Name.UFO:
                    pSI.scoreOne += 100;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }
    }
}
