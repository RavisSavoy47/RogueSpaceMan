﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        public int FontSize;
        public Font Font;
        public Color FontColor;

        /// <summary>
        /// Sets the starting value for the text box
        /// </summary>
        /// <param name="x">The x position for the text box</param>
        /// <param name="y">The y position for the text box</param>
        /// <param name="name">The name </param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="text"></param>
        public UIText(float x, float y, float z, string name, Color color, int width, int height, int fontSize, string text = "") : base(x, y, z, name)
        {
            Text = text;
            Width = width;
            Height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
            SetColor(color);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Draw()
        {
            //Create a new rectangel that will act as the borders of the text box
            Rectangle rec = new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, Width, Height);
            Raylib.DrawRectangleRec(rec, Color.BLACK);
            //Draw text box
            Raylib.DrawTextRec(Font, Text, rec, FontSize, 1, true, FontColor);
        }
    }
}
