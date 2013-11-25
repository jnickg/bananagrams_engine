using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace Bananagrams
{
    class Bananagram
    {
        /// <summary>
        /// The data for a game of Bananagrams is contained within
        /// these static members, including:
        ///     The bag of tiles (later versions will read from config file)
        ///     A list of all created players
        ///     the number of players currently in the game
        ///     whether the game has started
        /// </summary>
        public static Dictionary<char, int> bananabag;
        public static List<Bananagram> players;
        public static int nplay;
        public static bool gstart;

        /// <summary>
        /// Every instance of Bananagram is one player, each of whom has:
        ///     a collection of letters, drawn from bananabag
        ///     a name
        /// </summary>
        private Dictionary<char, int> letters;
        private String pname;

        // Fix this shite; might need to make child class that has "down" and
        // "right" adjacents???
        private AdjacencyGraph<char, IEdge<char>> words;

        /// <summary>
        /// Static constructor--later versions will read tiles from a
        /// config file
        /// </summary>
        static Bananagram()
        {
            bananabag.Add('A', 13);
            bananabag.Add('B', 3);
            bananabag.Add('C', 3);
            bananabag.Add('D', 6);
            bananabag.Add('E', 18);
            bananabag.Add('F', 3);
            bananabag.Add('G', 4);
            bananabag.Add('H', 3);
            bananabag.Add('I', 12);
            bananabag.Add('J', 2);
            bananabag.Add('K', 2);
            bananabag.Add('L', 5);
            bananabag.Add('M', 3);
            bananabag.Add('N', 8);
            bananabag.Add('O', 11);
            bananabag.Add('P', 3);
            bananabag.Add('Q', 2);
            bananabag.Add('R', 9);
            bananabag.Add('S', 6);
            bananabag.Add('T', 9);
            bananabag.Add('U', 6);
            bananabag.Add('V', 3);
            bananabag.Add('W', 3);
            bananabag.Add('X', 2);
            bananabag.Add('Y', 3);
            bananabag.Add('Z', 2);

            nplay = 0;
            players = new List<Bananagram>();

            gstart = false;
        }

        /// <summary>
        /// Draws one letter from bananabag for the game manager to give to
        /// the player
        /// </summary>
        /// <returns>A letter from bananabag</returns>
        public static char Peel()
        {
            // Get a char from bananabag; reduce its count
            // Return it
            return ' ';
        }

        /// <summary>
        /// Returns an undesired letter, adding it to the bananabag, and
        /// draws three more letters to give to the player
        /// </summary>
        /// <param name="d">The letter to be returned</param>
        /// <returns>A list of new letters to replace d</returns>
        public static List<char> Dump(char d)
        {
            // Add each char in d back to bananabag; increase their count
            // return Peel() x 3
            return null;
        }

        /// <summary>
        /// Draws tiles for each player and starts the game
        /// </summary>
        public static void Split()
        {
            // Based on nplay, give each player some tiles
            // Set gstart to true
        }

        /// <summary>
        /// Checks word against internal dictionary (later versions to use
        /// dictionary files) and returns whether it is a valid word to
        /// place.
        /// </summary>
        /// <param name="w">The word to be checked</param>
        /// <returns>Whether this word is considered valid</returns>
        public static bool CheckWord(String w)
        {
            return true;
        }

        /// <summary>
        /// Adds a new player to the game, if a game is not in session.
        /// </summary>
        /// <param name="name">The name of the new player</param>
        /// <returns>A pointer to the instance of the new player</returns>
        public static Bananagram addPlayer(String name)
        {
            if (!gstart)
            {
                ++nplay;
                Bananagram NuPl = new Bananagram(name);
                players.Add(NuPl);
                return NuPl;
            }
            else
            {
                return null;
            }

        }


        Bananagram(String name)
        {
            pname = name;
            letters = new Dictionary<char,int>();
            words = new AdjacencyGraph<char, IEdge<char>>();
        }

        ~Bananagram()
        {

        }

        /// <summary>
        /// Adds new tiles to a player's letters pile
        /// </summary>
        /// <param name="NuTil">The letters to be added</param>
        public void addTiles(List<char> NuTil)
        {

        }

        /// <summary>
        /// Adds this word to the player's words board; assumes
        /// it has already been checked for validity.
        /// 
        /// If this word has only one possible placement on words,
        /// it will be placed there. If there are many, the user will
        /// be prompted to pick. If there are no possible placements,
        /// the word will be rejected.
        /// </summary>
        /// <param name="w">The word to be added to the board</param>
        /// <returns>Whether this word can be made on the board</returns>
        public bool makeWord(String w)
        {
            return true;
        }
    }
}
