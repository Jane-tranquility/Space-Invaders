using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SndObserver : ColObserver
    {
        public SndObserver(IrrKlang.ISoundEngine pEng,string soundFileName )
        {
            Debug.Assert(pEng != null);
            this.pSndEngine = pEng;
            this.soundFileName = soundFileName;
        }
        public override void Notify()
        {
            Debug.WriteLine(" Snd_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            pSndEngine.SoundVolume = 0.2f;
            IrrKlang.ISound pSnd = pSndEngine.Play2D(this.soundFileName);
        }

        // Data
        IrrKlang.ISoundEngine pSndEngine;
        string soundFileName;
    }
}
