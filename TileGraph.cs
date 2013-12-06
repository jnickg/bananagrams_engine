using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bananagrams;

namespace Bananagrams
{
    /// <summary>
    /// The Tile Graph stores several tiles and uses the
    /// Bananagram static class to check words from the
    /// loaded dictionary for the game in which it was
    /// made. The Tile class is contained in a separate
    /// file and contains a char, and all of its
    /// adjacent letters.
    /// 
    /// Every player has one Tile Grid.
    /// 
    /// The structure structure of the Tile is also defined
    /// within this file
    /// </summary>
    class TileGraph
    {
        Tile[,] graph; // Actual 2d array of tiles, for TileGraph to manage
        
        /// <summary>
        /// These values represent the x- and y-coordinates of the
        /// currently-selected tile. Value 0,0 represents the top left
        /// corner of the graph; higher x values down; higher y values
        /// to the right.
        /// </summary>
        int sel_x, sel_y; // The x and y coordinate of the selected tile;

        public TileGraph(int nt)
        {
            // Place in approximate center
            sel_x = nt/2;
            sel_y = nt/2;

            graph = new Tile[nt, nt];
        }

        /// <summary>
        /// One of the elementary moves. Assigns tile
        /// of graph[x,y] to value l, if the tile is
        /// unused. Returns whether it was successful.
        /// </summary>
        /// <param name="l">The letter</param>
        /// <param name="x">x-index of the tile</param>
        /// <param name="y">y-index of the tile</param>
        /// <returns>True if the tile was placed, false if that tile space was used</returns>
        private bool e_place(char l, int x, int y)
        {
            if (graph[x, y].IsUsed) return false;
            else
            {
                graph[x, y].ltr_put(l);
                return true;
            }
        }

        /// <summary>
        /// One of the elementary moves. Resets tile of
        /// graph[x,y] to "unused" value. Returns the
        /// value of the tile that was there
        /// </summary>
        /// <param name="x">x-index of the tile</param>
        /// <param name="y">y-index of the tile</param>
        /// <returns>The character removed</returns>
        private char e_rmv(int x, int y)
        {
            return graph[x, y].ltr_rmv();
        }

        /// <summary>
        /// One of the elementary moves. Attempts to
        /// move the tile at graph[x,y] up one space,
        /// returning its success.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool e_move_u(int x, int y)
        {
            if (graph[x, y - 1].IsUsed) return false;
            else
            {
                char tmp = graph[x, y].ltr_rmv();
                graph[x, y - 1].ltr_put(tmp);
                return true;
            }
        }

        /// <summary>
        /// One of the elementary moves. Attempts to
        /// move the tile at graph[x,y] down one space,
        /// returning its success.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool e_move_d(int x, int y)
        {
            if (graph[x, y + 1].IsUsed) return false;
            else
            {
                char tmp = graph[x, y].ltr_rmv();
                graph[x, y + 1].ltr_put(tmp);
                return true;
            }
        }

        /// <summary>
        /// One of the elementary moves. Attempts to
        /// move the tile at graph[x,y] left one space,
        /// returning its success.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool e_move_l(int x, int y)
        {
            if (graph[x - 1, y].IsUsed) return false;
            else
            {
                char tmp = graph[x, y].ltr_rmv();
                graph[x - 1, y].ltr_put(tmp);
                return true;
            }
        }

        /// <summary>
        /// One of the elementary moves. Attempts to
        /// move the tile at graph[x,y] right one space,
        /// returning its success.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool e_move_r(int x, int y)
        {
            if (graph[x + 1, y].IsUsed) return false;
            else
            {
                char tmp = graph[x, y].ltr_rmv();
                graph[x + 1, y].ltr_put(tmp);
                return true;
            }
        }
        
        /// <summary>
        /// Attempts to slide a column of tiles up
        /// from base tile graph[x,y].
        /// </summary>
        /// <param name="x">x-coordinate of the column</param>
        /// <param name="y">y-coordinate of the base of the column</param>
        /// <returns>The number of tiles moved</returns>
        public int slide_up(int x, int y)
        {
            if (graph[x, y].IsUnused) return 0;
            else
            {
                int y2 = y;
                int num_slid = 0;

                // Attempt to slide tiles up higher and higher until
                // One is able to slide
                while (!e_move_u(x, y2))
                    --y2;

                // Slide all tiles from first successful slide
                // back down to y
                do //while(e_move_u(x, y2) && y2>y);
                {
                    ++y2;
                    ++num_slid;
                } while(e_move_u(x, y2) && y2<=y);

                return num_slid;
            }
        }

    } // End of TileGraph class


    internal class Tile
    {
        char ltr; // The letter of the tile
        bool[] val; // Validated flags (0=vertical, 1=horizontal)
        bool[] vis; // Visited flags (0=vertical, 1=horizontal)


        public bool IsUsed
        {
            get
            {
                return (!(ltr == '\0'));
            }
        }

        public bool IsUnused
        {
            get
            {
                return ((ltr == '\0'));
            }
        }

        public bool IsVisited_V
        {
            get
            {
                if (this.IsUsed) return vis[0];
                else return true;
            }
        }

        public bool IsVisited_H
        {
            get
            {
                if (this.IsUsed) return vis[1];
                else return true;
            }
        }

        public bool IsValid_V
        {
            get
            {
                if(this.IsUsed) return val[0];
                else return true;
            }
        }

        public bool IsValid_H
        {
            get
            {
                if (this.IsUsed) return val[1];
                else return true;
            }
        }

        public Tile()
        {
            ltr = '\0';
            val = new bool[2];
            vis = new bool[2];
            
        }

        /// <summary>
        /// Assigns the tile to contain a "used" value, resetting
        /// all flags as well.
        /// </summary>
        /// <param name="l">The letter to which Tile will be assigned</param>
        public void ltr_put(char l)
        {
            ltr = l;
            this.reset_flags();
        }

        /// <summary>
        /// Resets the tile to the "unused" value.
        /// This indicates to the Tile that it is not used,
        /// and will ensure all other properties return
        /// false regardless of their internal state.
        /// <returns>The letter previously present</returns>
        /// </summary>
        public char ltr_rmv()
        {
            if (this.IsUsed)
            {
                char rtn = ltr;
                ltr = '\0';
                return rtn;
            }
            else return ltr;
        }

        /// <summary>
        /// Swaps out existing tile with another existing
        /// one, resetting all validation flags as well.
        /// equivalent to executing ltr_rmv() and ltr_put(l)
        /// without resetting flags both times.
        /// </summary>
        /// <param name="l">The new letter</param>
        /// <returns>The letter previously present</returns>
        public char ltr_swap(char l)
        {
            char rtn = ltr;
            ltr = l;
            this.reset_flags();
            return rtn;
        }

        /// <summary>
        /// If this tile is considered "used" it will flag
        /// the tile as visited
        /// </summary>
        /// <returns></returns>
        public char visit_v()
        {
            vis[0] = true;
            return ltr;
        }

        public char visit_h()
        {
            vis[1] = true; // Protects against accidental visitation
            return ltr;
        }

        /// <summary>
        /// Sets the vertical validation flag to true
        /// </summary>
        public void validate_v()
        {
            val[0] = true;
        }

        /// <summary>
        /// Sets the horizontal validation flag to true
        /// </summary>
        public void validate_h()
        {
            val[1] = true;
        }

        private void reset_flags()
        {
            val[0] = false;
            val[1] = false;
            vis[0] = false;
            vis[1] = false;
        }
    } // End of Tile class

}
