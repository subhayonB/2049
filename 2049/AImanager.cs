using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2049
{
    public class AImanager
    {
        int m_pop;
        int gen = 1;
        int surs;
        public int GENERATION
        {
            get
            {
                return gen;
            }
        }
        double m_var;
        Random z = new Random();
        AI[] a;
        AI[] sur;
        public AI[] SUR
        {
            get
            {
                return sur;
            }
        }
        int ci=0;
        public int CI
        {
            get
            {
                return ci;
            }
            set
            {
                ci = value;
            }
        }
        AI best;
        AI second;
        public AImanager(int pop = 100, double var = .005)
        {
            m_pop = pop;
            m_var = var;
            surs = (int)(m_pop / 4);
            a = new AI[m_pop];
            sur = new AI[surs];
        }
        public void populate(int[] scores)
        {
            if (gen == 1)
            {
                for (int i = 0; i < m_pop; i++)
                {
                    double[] weights = new double[1200];
                    for (int j = 0; j < 1200; j++)
                    {
                        weights[j] = z.NextDouble() * 10 - 5;
                    }
                    a[i] = new AI(weights);
                }
            }
            else
            {
                survivors(scores);
                for (int i = 0; i < m_pop; i++)
                {
                    best = sur[(int)(z.NextDouble() * surs)];
                    second = sur[(int)(z.NextDouble() * surs)];
                    double[] weights = new double[1200];
                    for (int j = 0; j < 1200; j++)
                    {
                        if (z.NextDouble() < m_var)
                        {
                            weights[j] = z.NextDouble() * 10 - 5;
                        }
                        else
                        {
                            weights[j] = z.NextDouble() <= .5 ? best.WEIGHTS[j] : second.WEIGHTS[j];
                        }
                    }
                    a[i] = new AI(weights);
                }
            }
        }
            public int move(int[] inputs)
        {
             return a[ci].move(inputs);
        }
        public void gOver(int[] scores)
        {
            if (ci==m_pop-1)
            {
                populate(scores);
                ci = 0;
                gen++;
            }
            else
            {
                ci++;
            }
        }
        public void survivors(int[] scores)
        {
            int[] ns = new int[surs];
            for (int i=0; i<m_pop;i++)
            {
                for (int j=0;j< surs;j++)
                {
                    if (scores[i] > ns[j])
                    {
                        isur(j, a[i]);
                        for (int v = surs - 1; v >j; v--)
                        {
                            ns[v] = ns[v - 1];
                        }
                        ns[j] = scores[i];
                    }
                }
            }
        }
        public void isur(int z, AI b)
        {
            for (int i=surs-1;i>z;i--)
            {
                sur[i] = sur[i - 1];
            }
            sur[z] = b;
        }
    }
}
