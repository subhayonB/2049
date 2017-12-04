using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2049
{
    public class AI
    {
        double[] m_weights = new double[1200];
        public AI(double[] weights)
        {
            m_weights = weights;
        }
        public double[] WEIGHTS
            {
                get
            {
                return m_weights;
            }
            }
        public int move(int[] inputs)
        {
            int w = 0;
            double[] hl = new double[8];
            double[] hl2 = new double[8];
            double[] hl3 = new double[8];
            double[] outputs = new double[4];
            for (int i=0;i<8;i++)
            {
                double c = 0;
                for (int j = 0; j<16;j++)
                {
                    c += m_weights[w] * inputs[j];
                    w++;
                }
                c = Sigmoid(c);
                hl[i] = c;
            }
            for (int i = 0; i <8; i++)
            {
                double c = 0;
                for (int j = 0; j < 8; j++)
                {
                    c += m_weights[w] * hl[j];
                    w++;
                }
                c = Sigmoid(c);
                hl2[i] = c;
            }
            for (int i = 0; i < 8; i++)
            {
                double c = 0;
                for (int j = 0; j < 8; j++)
                {
                    c += m_weights[w] * hl2[j];
                    w++;
                }
                c = Sigmoid(c);
                hl3[i] = c;
            }
            for (int i = 0; i < 4; i++)
            {
                double c = 0;
                for (int j = 0; j < 8; j++)
                {
                    c += m_weights[w] * hl3[j];
                    w++;
                }
                c = Sigmoid(c);
                outputs[i] = c;
            }
            double a = outputs.Max();
                for(int i=0;i<4; i++)
                if (outputs[i]==a)
            return i;
            return 0;
        }
        public float Sigmoid(double value)
        {
            float k = (float)Math.Exp(value);
            return k / (1.0f + k);
        }
    }
}
