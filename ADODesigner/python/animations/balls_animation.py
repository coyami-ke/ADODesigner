import clr
from System.Numerics import Vector2
from ADODesigner.Models import KeyFrame
from ADODesigner.Models import Decoration
from ADODesigner.Animations import *

class balls_animation:
    def __init__(self):
        clr.AddReference("ADODesigner.Models")
        clr.AddReference("System.Numerics")
        self.iscurve = True
        self.duration = 10
        self.count = 8
        self.floor = 0
        self.first_frame = KeyFrame()
        self.second_frame = KeyFrame()
        self.intensivity = 0
        self.framerate = 60
    def animate(self):
        decorations = list()
        key_frames = list()
        count_frames = self.duration * self.framerate

        for i in self.count:
            decoration = Decoration()
            decoration.Position = self.FirstFrame.PositionOffset
            decoration.Tag = "Ball" + i
            decoration.Opacity -= i * 5 * self.intensivity
            decoration.Scale -= Vector2(i * 10, i * 10)
            decorations.add()
        if self.iscurve:
            for i in decorations.count:
                for s in count_frames:
                    key_frame = KeyFrame()
                    key_frame.Duration = self.frameRate / count_frames
                    key_frame.AngleOffset = 180 / self.frameRate * (s + 1)
                    key_frame.Tag = "Ball" + i
                    key_frame.Key = "BallsAnimation" + decorations[i].Tag + s.ToString()
                    key_frame.Floor = KeyFrame.Floor
                    key_frames.append(key_frame)
        return (key_frames, decorations)
                

