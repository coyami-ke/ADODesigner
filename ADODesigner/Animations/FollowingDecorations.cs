using ADODesigner.Models;
using ADODesigner.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public class FollowingDecorationArgs
    {
        public List<KeyFrame> KeyFrames { get; set; }
        public List<Decoration> Decorations { get; set; }
        public int Intensivity { get; set; }
        public FollowingDecorationArgs(KeyFrame[] keyFrames, Decoration[] decorations)
        {
            KeyFrames = new(keyFrames);
            Decorations = new(decorations);
        }
    }
    /// <summary>
    /// Class for creating procedural animation of Following the Decorations.
    /// </summary>
    public class FollowingDecorations : BaseAnimation<FollowingDecorationArgs>
    {
        public FollowingDecorations(FollowingDecorationArgs args) : base(args)
        {
        }

        public override void CreateAnimation()
        {
            for (int i = 0; i < Args.Decorations.Count; i++)
            {
                for (int s = 0; s < Args.KeyFrames.Count; s++)
                {
                    if (Args.Decorations[i].Tag == Args.KeyFrames[s].Tag)
                    {
                        KeyFrame keyFrame = Args.KeyFrames[s];
                        keyFrame.AngleOffset += 90 * Args.Intensivity * s;
                        EditorView.Editor.AddKeyFrame(keyFrame);
                    }
                }
            }
        }
    }
}
