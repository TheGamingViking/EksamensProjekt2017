using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    public class Animation
    {
        //Fields
        int frames;
        float fps;
        Vector2 offset;
        Rectangle[] rectangles;

        //Properties
        public float Fps
        {
            get
            {
                return fps;
            }
        }
        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }
        public Rectangle[] Rectangles
        {
            get
            {
                return rectangles;
            }
        }
        public int Frames
        {
            get { return frames; }
            set { frames = value; }
        }

        //Constructor
        public Animation(int frames, int yPos, int xStartFrame, int width, int height, float fps, Vector2 offset)
        {
            rectangles = new Rectangle[frames];
            this.offset = offset;
            this.fps = fps;
            this.frames = frames;

            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }
        }
    }
}
