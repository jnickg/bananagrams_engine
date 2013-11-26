using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bananagrams
{
    class Tile
    {
        private char ltr; // The letter of this tile

        // Adjacency pointers: up, down, left, right
        private Tile adj_u;
        private Tile adj_d;
        private Tile adj_l;
        private Tile adj_r;

        private Tile Up
        {
            get
            {
                return adj_u;
            }
            set
            {
                adj_u = value;
            }
        }

        // Traversal flags
        private bool val_v; // vertical validated flag
        private bool val_h; // horizontal
        private bool vis_v; // vertical visited flag
        private bool vis_h; // horizontal

        /// <summary>
        /// Every tile MUST contain a letter
        /// </summary>
        /// <param name="l">The letter of the tile</param>
        public Tile(char l)
        {
            ltr = l;

            // All adjacent are null
            adj_u = adj_d =
            adj_l = adj_r = null;

            // All flags invalid
            val_v = val_h = false;
            vis_v = vis_h = false;
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
            vis_v = true;
            l = ltr;
            return adj_r;
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
            vis_h = true;
            l = ltr;
            return adj_d;
        }

        /// <summary>
        /// Returns whether the tile has been visited in the
        /// vertical direction.
        /// </summary>
        /// <returns>Whether it's been visited.</returns>
        public bool is_visited_v()
        {
            return vis_v;
        }

        /// <summary>
        /// Returns whether the tile has been visited in the
        /// horizontal direction.
        /// </summary>
        /// <returns>Whether it's been visited.</returns>
        public bool is_visited_h()
        {
            return vis_h;
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
            return ((!(null == adj_u)) || (!(null == adj_d)));
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
            return ((!(null == adj_l)) || (!(null == adj_r)));
        }

        /// <summary>
        /// Recursively calls itself until an adjacent tile
        /// is found that itself has no adjacent tiles, in
        /// the vertical direction.
        /// </summary>
        /// <returns>The head tile of the word in the vertical direction</returns>
        public Tile get_head_v()
        {
            if (null == adj_u)
                return this;
            else return adj_u.get_head_v();
        }

        /// <summary>
        /// Recursively calls itself until an adjacent tile
        /// is found that itself has no adjacent tiles, in
        /// the horizontal direction.
        /// </summary>
        /// <returns>The head tile of the word in the horizontal direction</returns>
        public Tile get_head_h()
        {
            if (null == adj_l)
                return this;
            else return adj_l.get_head_h();
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
            val_v = true;
        }

        /// <summary>
        /// Sets the horizontal validation flag to true
        /// </summary>
        public void validate_h()
        {
            val_h = true;
        }

        /// <summary>
        /// Returns whether this tile has been validated
        /// in the vertical direction.
        /// </summary>
        /// <returns>Whether it has been validated.</returns>
        public bool is_valid_v()
        {
            return val_v;
        }

        /// <summary>
        /// Returns whether this tile has been validated
        /// in the horizontal direction.
        /// </summary>
        /// <returns>Whether it has been validated.</returns>
        public bool is_valid_h()
        {
            return val_h;
        }
    }
}
