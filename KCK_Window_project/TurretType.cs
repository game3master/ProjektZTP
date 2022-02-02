﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public class TurretType
    {
        private int index;
        private Image image;

        /* Konstruktor */
        public TurretType(int index, Image image)
        {
            this.index = index;
            this.image = image;
        }

        public int GetIndex()
        {
            return this.index;
        }
        public Image GetImage()
        {
            return this.image;
        }
    }
}
