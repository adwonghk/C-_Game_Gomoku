using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Gomoku
{
    class Chess : PictureBox
    {
        public enum ChessType {Black, White, None};

        private static readonly int IMAGE_WIDTH = 50;
        private ChessType _type = ChessType.None;

        public Chess(int x, int y, ChessType type)
        {
            this._type = type;
            this.BackColor = Color.Transparent;
            this.Location = new Point(x - IMAGE_WIDTH / 2, y - IMAGE_WIDTH / 2);
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);

            if (type == ChessType.Black)
            {
                this.Image = Properties.Resources.black;
            } 
            else if (type == ChessType.White)
            {
                this.Image = Properties.Resources.white;
            }
        }

        public ChessType GetChessType()
        {
            return _type;
        }
    }
}
