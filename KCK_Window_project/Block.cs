using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KCK_Window_project
{
    public class Block
    {
        private BlockType blockType;
        private int posX;
        private int posY;

        /* Konstruktor */
        public Block(BlockType t, int posX, int posY)
        {
            this.blockType = t;
            this.posX = posX;
            this.posY = posY;
        }

        /* Gettery */
        public int GetX()
        {
            return posX;
        }
        public int GetY()
        {
            return posY;
        }
        public BlockType GetBlockType()
        {
            return blockType;
        }
    }
}
