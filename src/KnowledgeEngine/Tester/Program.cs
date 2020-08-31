using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using KnowledgeEngineRules;
using KnowledgeEngineRules.Assembler;

namespace RulesEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DateTime outerstart = DateTime.Now;

                string logfile = AppDomain.CurrentDomain.BaseDirectory + @"\Output.log";
                TextWriterTraceListener twtl = new TextWriterTraceListener(new StreamWriter(logfile, false));
                Debug.AutoFlush = true;
                Debug.Listeners.Add(twtl);

                MyEngine e = new MyEngine();
                e.CompileError += new CompilerErrorEventHandler(MyEngine_CompileError);
                e.Compile(new StreamReader(@"C:\Work\Additional\KnowledgeEngine\ai.kbe"));

                DateTime start = DateTime.Now;

                if (e.HasCompiledEngine)
                {
                    e.Run();

                    foreach (KnowledgeItem item in e.GetKnowledgeItems())
                    {
                        Console.WriteLine("{0}", item.ToString());
                    }
                }

                DateTime end = DateTime.Now;
                TimeSpan ts = end - start;
                float f = System.Environment.WorkingSet / 1024f / 1000;
                //Console.WriteLine("Time       : {0:0.000} s {1:0} Mb", ts.TotalSeconds, f);

                DateTime outerend = DateTime.Now;
                TimeSpan ts1 = outerend - outerstart;
                Console.WriteLine("Time       : {0:0.000} s", ts1.TotalSeconds);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine(exp.StackTrace);
            }
        }

        static List<KnowledgeItem> Run(MyEngine e)
        {
            e.Run();

            List<KnowledgeItem> list = e.GetKnowledgeItems();

            foreach (KnowledgeItem item in list)
            {
                Console.WriteLine("           : " + item.ToString());
            }

            Console.WriteLine();

            Console.WriteLine("Crew (END) : {0}", e.Ship.Crew);
            Console.WriteLine("{0:0} Mb", System.Environment.WorkingSet / 1024f / 1000);

            Console.WriteLine();

            return list;
        }

        static void MyEngine_CompileError(object sender, CompilerErrorEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

