import clr
from System.Numerics import Vector2
from ADODesigner.Models import KeyFrame
from ADODesigner.Models import Decoration
from ADODesigner.Animations import *

class following_decorations:
    def __init__(self, argkeyframes, argdecorations):
        self.decorations = argdecorations
        self.key_frames = argkeyframes
        self.intensivity = 1
    def animate(self):
        for i in self.decorations.count:
            for s in self.keyframes.count:
                if self.decorations[i].Tag == self.key_frames[s].Tag:
                    key_frame = KeyFrame()
                    key_frame.AngleOffset += 90 * self.intensivity * s