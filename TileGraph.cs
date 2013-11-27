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
    /// The structure of the graph ("Tiles" aka nodes & edges)
    /// are defined below.
    /// </summary>
    class TileGraph
    {
        
    } // End of TileGraph class

    /// <summary>
    /// The node of the TileGraph, called Tile
    /// 
    /// Each tile will be linked to up to four other
    /// 'adjacent' tiles, which exist regardless of distance,
    /// IFF there is a tile precisely in any straight direction
    /// from it. The weight of the edge (below) represents the
    /// tile distance, meaning that truly adjacent tiles will have
    /// an edge weight of 1.
    /// 
    /// Each tile has two validation flags, representing whether
    /// that tile has been checked for being part of a valid word,
    /// in the vertical and horizontal direction. Each tile also has
    /// two visitation flags for the purpose of TileGraph traversal.
    /// All of these flags are initialized to false.
    /// 
    /// Each tile must have a letter.
    /// </summary>
    internal class Tile
    {
        char ltr; // The letter of the tile
        edge[] adj; // 0=up, 1=right, 2=down, 3=left
        bool[] val; // Validated flags (0=vertical, 1=horizontal)
        bool[] vis; // Visited flags (0=vertical, 1=horizontal)

        public Tile(char l)
        {
            ltr = l;
            adj = new edge[4];
            val = new bool[2];
            vis = new bool[2];
        }

        /// <summary>
        /// Spits out the tile's current letter to l, while
        /// returning a pointer to the next adjacent tile in
        /// the vertical direction. Return will be null if 
        /// it is the last tile in that direction.
        /// </summary>
        /// <param name="l">Gets assigned to this tile's letter</param>
        /// <returns>The next adjacent tile in the vertical direction</returns>
        public Tile visit_v(out char l)
        {
            vis[0] = true;
            l = ltr;
            if (1 != adj[1].weight) return null;
            else return adj[1].dest;
        }

        /// <summary>
        /// Spits out the tile's current letter to l, while
        /// returning a pointer to the next adjacent tile in
        /// the horizontal direction. Return will be null if 
        /// it is the last tile in that direction.
        /// </summary>
        /// <param name="l">Gets assigned to this tile's letter</param>
        /// <returns>The next adjacent tile in the horizontal direction</returns>
        public Tile visit_h(out char l)
        {
            vis[1] = true;
            l = ltr;
            if (1 != adj[2].weight) return null;
            else return adj[2].dest;
        }

        /// <summary>
        /// Returns whether the tile has been visited in the
        /// vertical direction.
        /// </summary>
        /// <returns>Whether it's been visited.</returns>
        public bool is_visited_v()
        {
            return vis[0];
        }

        /// <summary>
        /// Returns whether the tile has been visited in the
        /// horizontal direction.
        /// </summary>
        /// <returns>Whether it's been visited.</returns>
        public bool is_visited_h()
        {
            return vis[1];
        }

        /// <summary>
        /// Recursively calls itself until an adjacent tile
        /// is found that itself has no adjacent tiles, in
        /// the vertical direction.
        /// </summary>
        /// <returns>The head tile of the word in the vertical direction</returns>
        public Tile get_head_v()
        {
            if (1 != adj[0].weight)
                return this;
            else return adj[0].dest.get_head_v();
        }

        /// <summary>
        /// Recursively calls itself until an adjacent tile
        /// is found that itself has no adjacent tiles, in
        /// the horizontal direction.
        /// </summary>
        /// <returns>The head tile of the word in the horizontal direction</returns>
        public Tile get_head_h()
        {
            if (1 != adj[3].weight)
                return this;
            else return adj[3].dest.get_head_h();
        }

        /// <summary>
        /// Returns true if there is either a tile preceding
        /// or proceeding this one, in the vertical direction.
        /// Does not check whether the word is valid, only
        /// whether it is part of a word.
        /// </summary>
        /// <returns>Whether it is part of a word</returns>
        public bool is_word_v()
        {
            return ((1 == adj[0].weight) || (1 == adj[2].weight));
        }

        /// <summary>
        /// Returns true if there is either a tile preceding
        /// or proceeding this one, in the horizontal direction.
        /// Does not check whether the word is valid, only
        /// whether it is part of a word.
        /// </summary>
        /// <returns>Whether it is part of a word</returns>
        public bool is_word_h()
        {
            return ((1 == adj[3].weight) || (1 == adj[1].weight));
        }

        /// <summary>
        /// Finds the head tile for this one, in the vertical
        /// direction, visits each following tile until there
        /// are no more, and returns a string representing the
        /// entire vertical word which this tile is a part of.
        /// </summary>
        /// <returns>A string representation of the word.</returns>
        public String get_word_v()
        {
            char l;
            StringBuilder rtn = new StringBuilder();
            Tile tmp = this.get_head_v();
            while (!(null == tmp))
            {
                tmp = tmp.visit_v(out l);
                rtn.Append(l);
            }
            return rtn.ToString();
        }

        /// <summary>
        /// Finds the head tile for this one, in the horizontal
        /// direction, visits each following tile until there
        /// are no more, and returns a string representing the
        /// entire horizontal word which this tile is a part of.
        /// </summary>
        /// <returns>A string representation of the word.</returns>
        public String get_word_h()
        {
            char l;
            StringBuilder rtn = new StringBuilder();
            Tile tmp = this.get_head_h();
            while (!(null == tmp))
            {
                tmp = tmp.visit_h(out l);
                rtn.Append(l);
            }
            return rtn.ToString();
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

        /// <summary>
        /// Returns whether this tile has been validated
        /// in the vertical direction.
        /// </summary>
        /// <returns>Whether it has been validated.</returns>
        public bool is_valid_v()
        {
            return val[0];
        }

        /// <summary>
        /// Returns whether this tile has been validated
        /// in the horizontal direction.
        /// </summary>
        /// <returns>Whether it has been validated.</returns>
        public bool is_valid_h()
        {
            return val[1];
        }
    } // End of Tile class

    /// <summary>
    /// The monodirectional edges of each Tile. "dest" represents
    /// to the Tile to which the edge points, and weight represents
    /// its simulated physical distance from the adjacent tile.
    /// 
    /// Truly adjacent tiles will have a weight of 1, while tiles with
    /// a gap between them will have a weight equal to the number of
    /// tiels that could fit in between them.
    /// 
    /// Weight is initialized to 0, meaning any non-1 weight represents
    /// an edge pointing either to nothing or to something not truly
    /// adjacent.
    /// </summary>
    internal struct edge
    {
        public Tile dest; // points to the edge's destination
        public int weight; // the distance between 'adjacent' tiles

        public edge()
        {
            dest = null;
            weight = 0;
        }
    } // End of edge struct
}
