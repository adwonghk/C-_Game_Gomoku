using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Board
    {
        public static readonly int NODE_COUNT = 9;

        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);

        public static readonly int OFFSET = 75;
        private static readonly int NODE_RADIUS = 10;
        private static readonly int NODE_DISTANCE = 75;

        private Chess[,] chesses = new Chess[NODE_COUNT, NODE_COUNT];

        private Point _lastPlaceNode = NO_MATCH_NODE;
        public Point LastPlacedNode { get { return _lastPlaceNode; } }

        public void Reset()
        {
            for (int i = 0; i < NODE_COUNT; i++)
            {
                for (int j = 0; j < NODE_COUNT; j++)
                {
                    chesses[i, j] = null;
                }
            }
        }

        public Chess.ChessType GetChessType(int nodeIdX, int nodeIdY)
        {
            if (chesses[nodeIdX, nodeIdY] == null) return Chess.ChessType.None;
            return chesses[nodeIdX, nodeIdY].GetChessType();
        }


        public bool CanBePlaced(int x, int y)
        {
            Point nodeID = FindTheClosetNode(x, y);

            if (nodeID == NO_MATCH_NODE)
            {
                return false;
            }

            if (chesses[nodeID.X, nodeID.Y] != null)
            {
                return false;
            }

            return true;
        }

        public Chess PlaceAChess(int x, int y, Chess.ChessType type)
        {
            Point nodeId = FindTheClosetNode(x, y);

            if (nodeId == NO_MATCH_NODE)
            {
                return null;
            }

            if (chesses[nodeId.X, nodeId.Y] != null)
            {
                return null;
            }
            Point formPos = ConvertToFormPos(nodeId);
            chesses[nodeId.X, nodeId.Y] = new Chess(formPos.X, formPos.Y, type);

            _lastPlaceNode = nodeId;

            return chesses[nodeId.X, nodeId.Y];
        }

        private Point ConvertToFormPos(Point nodeID)
        {
            Point formPos = new Point();
            formPos.X = nodeID.X * NODE_DISTANCE + OFFSET;
            formPos.Y = nodeID.Y * NODE_DISTANCE + OFFSET;

            return formPos;
        }

        private Point FindTheClosetNode(int x, int y)
        {
            int nodeIdX = FindTheClosetNode(x);
            if (nodeIdX == -1 || nodeIdX >= NODE_COUNT)
            {
                return NO_MATCH_NODE;
            }

            int nodeIdY = FindTheClosetNode(y);
            if (nodeIdY == -1 || nodeIdY >= NODE_COUNT)
            {
                return NO_MATCH_NODE;
            }
            return new Point(nodeIdX, nodeIdY);
        }

        private int FindTheClosetNode(int pos)
        {

            if (pos < OFFSET - NODE_RADIUS) return -1;

            pos -= OFFSET;

            int quotient = pos / NODE_DISTANCE;
            int remainder = pos % NODE_DISTANCE;

            if (remainder <= NODE_RADIUS)
            {
                return quotient;
            }
            else if (remainder >= NODE_DISTANCE - NODE_RADIUS)
            {
                return quotient + 1;
            }
            else
            {
                return -1;
            }
        }

        public bool CheckLineUp(Chess.ChessType type)
        {
            int centerX = _lastPlaceNode.X;
            int centerY = _lastPlaceNode.Y;

            int total = 1;
            int count = 1;
            // \
            while (count <= 5)
            {
                if (centerX - count < 0 || centerY - count < 0) break;
                if (chesses[centerX - count, centerY - count] == null) break;
                if (chesses[centerX - count, centerY - count].GetChessType() != type) break;

                total++;
                count++;
            }

            count = -1;

            while (count >= -5)
            {
                if (centerX - count >= Board.NODE_COUNT || centerY - count >= Board.NODE_COUNT) break;
                if (chesses[centerX - count, centerY - count] == null) break;

                if (chesses[centerX - count, centerY - count].GetChessType() != type) break;

                total++;
                count--;
            }

            if (total >= 5)
            {
                return true;
            }

            total = 1;
            count = 1;

            // /
            while (count <= 5)
            {
                if (centerX - count < 0 || centerY + count >= Board.NODE_COUNT) break;
                if (chesses[centerX - count, centerY + count] == null) break;

                if (chesses[centerX - count, centerY + count].GetChessType() != type) break;

                total++;
                count++;
            }

            count = 1;

            while (count <= 5)
            {
                if (centerX + count >= Board.NODE_COUNT || centerY - count < 0) break;
                if (chesses[centerX + count, centerY - count] == null) break;

                if (chesses[centerX + count, centerY - count].GetChessType() != type) break;

                total++;
                count++;
            }

            if (total >= 5)
            {
                return true;
            }

            total = 1;
            count = 1;

            // |
            while (count <= 5)
            {
                if (centerY - count < 0) break;
                if (chesses[centerX, centerY - count] == null) break;

                if (chesses[centerX, centerY - count].GetChessType() != type) break;

                total++;
                count++;
            }

            count = 1;

            while (count <= 5)
            {
                if (centerY + count >= Board.NODE_COUNT) break;
                if (chesses[centerX, centerY + count] == null) break;

                if (chesses[centerX, centerY + count].GetChessType() != type) break;

                total++;
                count++;
            }

            if (total >= 5)
            {
                return true;
            }

            total = 1;
            count = 1;

            // -

            while (count <= 5)
            {
                if (centerX + count >= Board.NODE_COUNT) break;
                if (chesses[centerX + count, centerY] == null) break;

                if (chesses[centerX + count, centerY].GetChessType() != type) break;

                total++;
                count++;
            }

            count = 1;

            while (count <= 5)
            {
                if (centerX - count < 0) break;
                if (chesses[centerX - count, centerY] == null) break;

                if (chesses[centerX - count, centerY].GetChessType() != type) break;

                total++;
                count++;
            }

            if (total >= 5)
            {
                return true;
            }

            return false;
        }
    }
}
