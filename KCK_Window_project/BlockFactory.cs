using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KCK_Window_project
{
    public class BlockFactory
    {
        // Pula istniejacych pylkow (Typy wiezyczek).
        private List<BlockType> blockTypes = new List<BlockType>();
        private List<Block> blocks = new List<Block>();

        public BlockFactory()
        {
            blockTypes.Add(new BlockType(1, Properties.Resources.brick_orange));
            blockTypes.Add(new BlockType(2, Properties.Resources.grassBlock));
        }

        private BlockType GetBlockType(int index)
        {
            foreach(BlockType t in blockTypes)
            {
                if (t.GetIndex() == index)
                {
                    return t;
                }
            }
            return null;
        }

        public void AddBlock(int index, int posX, int posY)
        {
            BlockType type = this.GetBlockType(index);
            if (type == null)
            {
                return;
            }
            Block block = new Block(type, posX, posY);
            blocks.Add(block);
        }

        public List<Block> GetAllBlocks()
        {
            return this.blocks;
        }
        
    }
}
