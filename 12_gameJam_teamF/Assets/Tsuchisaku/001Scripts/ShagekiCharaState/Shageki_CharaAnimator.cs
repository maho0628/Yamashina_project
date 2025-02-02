using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;


namespace Toshiki
{
    public enum Anim
    {
        Hold,
        Shoot,
        Reload,
        Start,
        End,
        EndLoop
    }

    public class Shageki_CharaAnimator : MonoBehaviour
    {
        public SkeletonAnimation skeletonAnimation;

        public bool endReloadTrigger;

        TrackEntry entry;

        private void Awake()
        {
            skeletonAnimation.state.ClearTracks();
        }


        public void SetAnim(Anim _anim)
        {
            if (_anim == Anim.Hold)
            {
                skeletonAnimation.state.SetAnimation(0, "Hold", false);
            }
            else if (_anim == Anim.Shoot)
            {
                skeletonAnimation.state.SetAnimation(0, "Shoot", false);
            }
            else if (_anim == Anim.Reload)
            {
                entry = skeletonAnimation.state.SetAnimation(0, "IReload", false);
                entry.Complete += OnReloadComplate;
                entry.Event += OnEvent;
            }
            else if (_anim == Anim.Start)
            {
                entry = skeletonAnimation.state.SetAnimation(0, "IStart", false);
                entry.Complete += OnStartComplete;
                entry.Event += OnEvent;
            }
            else if (_anim == Anim.End)
            {
                skeletonAnimation.skeleton.SetToSetupPose();
                entry = skeletonAnimation.state.SetAnimation(0, "IEnd", false);
                entry.Complete += OnEndComplete;
            }

        }

        public void SetBoltStop(bool _true)
        {
            if (_true)
            {
                TrackEntry trackEntry = skeletonAnimation.state.SetAnimation(1, "BoltStop", false);
                trackEntry.MixDuration = 0;
                //‰¹‚à–Â‚ç‚·
                SoundManager.instance.Play("chember");
            }
            else
            {
                skeletonAnimation.state.SetEmptyAnimation(1, 0f);
            }


        }

        public void OnReloadComplate(TrackEntry trackEntry)
        {
            endReloadTrigger = true;
            Debug.Log($"endReload");
        }

        public void OnEndComplete(TrackEntry trackEntry)
        {
            skeletonAnimation.state.SetAnimation(0, "IEndLoop", true);
        }
        public void OnStartComplete(TrackEntry trackEntry)
        {
            skeletonAnimation.state.SetAnimation(0, "Hold", false);
        }

        private void OnEvent(TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == "Chember")
            {
                //chember‰¹‚ð–Â‚ç‚·
                SoundManager.instance.Play("magIn");
            }
            if (e.Data.Name == "magIn")
            {
                //ƒ}ƒKƒWƒ“‰¹‚ð–Â‚ç‚·
                SoundManager.instance.Play("chember");

            }
        }
    }
}