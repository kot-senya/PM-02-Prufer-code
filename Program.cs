using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        struct Tree
        {
            public int begin;
            public int end;
        }
        static void read (List<Tree> l)
        {
            using (StreamReader sr = new StreamReader ("Ребра.txt"))
            {
                while(sr.EndOfStream != true)
                {
                    string[] line = sr.ReadLine().Split('-');
                    Tree t = new Tree ();
                    t.begin = int.Parse (line[0]);
                    t.end = int.Parse (line[1]);
                    l.Add(t);
                }
            }
        }
        static int[] read ()
        {
            using (StreamReader sr = new StreamReader("Код прюфера.txt"))
            {
                string[] line = sr.ReadLine().Trim().Split(' ');
                int[] arr = new int[line.Length];
                for(int i = 0; i < line.Length; i++)
                {
                    arr[i] = int.Parse (line[i]);
                }                
                return arr;
            }
        }
        static string writePrufer(List<Tree> l)
        {
            string code = "";
            int n = l.Count - 1;
            for(int i = 0; i < n; i++)
            {
                int a = min(l);
                code += l[a].begin + " ";
                l.RemoveAt(a);
            }
            return code;  
        }
        static int min(List<Tree> l)//возвращает номер ребра с минимальным листом
        {
            bool flag = false;
            bool mark = true;
            int min = 0;
            foreach (Tree t in l) //поиск минимального листа
            {
                if (!flag)//первое значение
                {
                    for (int i = 0; i < l.Count; i++)
                    {
                        if (l[i].begin == t.end)
                        {
                            mark = false;
                        }
                    }
                    if (mark)
                    {
                        min = t.end;
                        flag = true;
                    }
                }
                else //2 и последующее
                {
                    if (t.end < min)
                    {
                        for (int i = 0; i < l.Count; i++)
                        {
                            if (l[i].begin == t.end)
                            {
                                mark = false;
                            }
                        }
                        if (mark)
                        {
                            min = t.end;
                            
                        }
                    }
                }
                mark = true;
            }
            int num = 0;
            for (int i = 0; i < l.Count; i++)
            {
                if(min == l[i].end)
                {
                    num = i;
                }
            }
            return num;
        }
        static void display(List<Tree> l)
        {
            foreach(Tree t in l)
            {
                Console.WriteLine("{0}-{1}", t.begin, t.end);
            }
        }
        static void writeVerhPruf(List<Tree> t, int[] a)
        {
            for (int i = 0; i < a.Length; i++) 
            {
               Tree x = new Tree();
                x.begin = a[i];
                x.end = 0;
                t.Add(x);
            }
            List<int> num = new List<int>();
            for(int i = 0; i < a.Length + 2; i++)
            {
                num.Add(i+1);
            }
            for(int i = 0; i < t.Count; i++)
            {
                bool flag = false;
                int k = 0;
                for(int j = 0; j < num.Count; j++)
                {
                    bool mark = true;
                    for(int z = i; z < a.Length; z++)
                    {
                        if (t[z].begin == num[j])
                        {
                            mark = false;
                        }
                    }
                    if (t[i].begin != num[j] && mark)
                    {
                        flag = true;
                        k = j;
                        break;
                    }
                }    
                    if (flag)
                    {
                        Tree m = new Tree() { begin = t[i].begin, end = num[k] };
                        t.RemoveAt(i);
                        t.Insert(i, m);
                        num.RemoveAt(k);
                    }
               

            }
            /*
             for (int i = 1; i <= a.Length+2; i++)
            {
                bool flag = true;
                
                if(flag)
                {
                    for (int j = 0; j < t.Count; j++)
                    {
                        if (t[j].end == 0)
                        {
                            Tree m = new Tree() { begin = t[j].begin, end = i};
                            t.RemoveAt(j);
                            t.Add(m);
                        }
                    }
                }
            }
            */
        }
        static void Main(string[] args)
        {
            List<Tree> l = new List<Tree>();
            read(l);
            //display(l);
            using (StreamWriter sw = new StreamWriter ("Код прюфера.txt", false))
            {
                sw.WriteLine(writePrufer(l));
            }
            int[] code = read();
            List<Tree> t = new List <Tree>();
            writeVerhPruf(t, code);
            using (StreamWriter sw = new StreamWriter("Ребра new.txt.txt", false))
            {
                foreach(Tree a in t)
                {
                    sw.WriteLine(a.begin+"-"+a.end);
                }                
            }




        }
    }
}