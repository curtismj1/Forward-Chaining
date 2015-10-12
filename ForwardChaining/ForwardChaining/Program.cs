using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForwardChaining
{
    class Program
    {
        
        struct chainStructure
        {
           public List<char[]> clauses;
           public List<char> implications;
           public List<int> counts;
        }
        //This one uses hashing to decrement counts. O(n)
       static bool isTrue(chainStructure myStruct, List<char> known_symbols, Dictionary<char, List<int>> positions)
       {
            //Initialize counts
            char query = 'd';
            while (known_symbols.Count != 0)
            {

                if (known_symbols[0] == query)
                {
                    return true;
                }
                int index = 0;
                foreach (var proposition in myStruct.clauses)
                {

                    foreach(var decrement in positions[known_symbols[0]])
                    {
                        myStruct.counts[decrement]--;
                        if(myStruct.counts[decrement] == 0)
                        {
                            known_symbols.Add(myStruct.implications[index]);
                            myStruct.clauses.RemoveAt(index);
                            myStruct.implications.RemoveAt(index);
                            myStruct.counts.RemoveAt(index);
                        }
                    }
                    index++;
                }
               
                known_symbols.RemoveAt(0);
            }
            return false;

       }
        //This one accomplishes the task via looping and deleting things. O(nm) where n is the number of propositions and m is the number of characters in each proposition. 
       static  bool isTrue(chainStructure myStruct, List<char> known_symbols)
        {
            char query = 'd'; //This is the Character we are searching for to be true
            while (known_symbols.Count != 0)
            {
                if (known_symbols[0] == query)
                {
                    return true;
                }
                for (int index = 0; index < myStruct.clauses.Count; index++)
                {

                    myStruct.clauses[index] = myStruct.clauses[index].Where(val => val != known_symbols[0]).ToArray();
                    
                    if (myStruct.clauses[index].Length == 0)
                    {
                        known_symbols.Add(myStruct.implications[index]);
                        myStruct.clauses.RemoveAt(index);
                        myStruct.implications.RemoveAt(index);
                        index--;
                    }
                   
                }
              
                known_symbols.RemoveAt(0);
            }
            return false;
        }
        static void Main(string[] args)
        {
            chainStructure myStruct = new chainStructure();
            myStruct.clauses = new List<char[]>();
            myStruct.implications = new List<char>();
            myStruct.counts = new List<int>();
            string FILE_NAME = "..\\..\\input.txt";
            List<char> known_symbols = new List<char>();
          
            string line;
            Dictionary<char, List<int>> positions = new Dictionary<char, List<int>>();
            System.IO.StreamReader file = new System.IO.StreamReader(FILE_NAME);
            int h = 0; // first index of position array
            while ((line = file.ReadLine()) != null) 
            {
                char[] temp  = line.ToCharArray();
                if(temp.Length == 0)
                {

                }
                else if (temp.Length == 1)
                {
                    known_symbols.Add(temp[0]);
                }
                else
                {
                    for(int i = 0; i < temp.Length; i++)
                    {
                        if (!positions.ContainsKey(temp[i]))
                        {
                            positions.Add(temp[i], new List<int>());
                        }
                        positions[temp[i]].Add(h);
                    }
                    var temp2 = new char[temp.Length - 1];
                    Array.Copy(temp, temp2, temp.Length - 1);
                    myStruct.clauses.Add(temp2);
                    myStruct.implications.Add(temp[temp.Length-1]);
                    myStruct.counts.Add(temp2.Length);
                }
                h++;
            }
            foreach(var key in positions.Keys)
            {
                Console.WriteLine(key);
                foreach (var val in positions[key])
                {
                    Console.Write(val);
                }
            }
            bool result = isTrue(myStruct, known_symbols);
            bool result2 = isTrue(myStruct, known_symbols, positions);
            Console.WriteLine(result);
            Console.WriteLine(result2);
           
           
            
            Console.ReadLine();
        }
    }
}
