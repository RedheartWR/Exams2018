using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary1
{
    public class Data
    {
        private int[] V;

        public double M
        { get
            {
                try
                {
                    int sum = 0;
                    for (int i = 0; i < V.Length; i++) sum += V[i];
                    return sum / V.Length;
                }
                catch (DivideByZeroException) { return 0; }
            }
        }

        public double D
        {
            get
            {
                try
                {
                    double sum = 0;
                    for (int i = 0; i < V.Length; i++) sum += (V[i] - this.M) * (V[i] - this.M);
                    return sum / V.Length;
                }
                catch(DivideByZeroException) { return 0; }
            }
        }

        public Data(string str)
        {
            string[] s = str.Split(' ');
            V = new int[s.Length - 1];
            for (int i = 0; i < s.Length - 1; i++)
            {
                int.TryParse(s[i], out int a);
                V[i] = a;
            }
        }

        public override string ToString()
        {
            string str = "";
            foreach (int a in V) str += " "+ a;
            return str;
        }
    }

    public class Processing : IEnumerable<Data>
    {
        private string _fileName;
        private TextReader _textReader;

        public Processing(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerator<Data> GetEnumerator()
        {
            return new InternalClass(this);
        }

        class InternalClass : IEnumerator<Data>
        {
            private Data _element;
            private Processing _processing;

            public InternalClass(Processing processing)
            {
                _processing = processing;
                processing._textReader = new StreamReader(_processing._fileName);
            }

            public void Dispose()
            {
                _processing._textReader.Close();
            }

            public bool MoveNext()
            {
                string s = _processing._textReader.ReadLine();
                if (s != null)
                {
                    _element = new Data(s);
                    return true;
                }
                Reset();
                return false;
            }

            public void Reset()
            {
                _processing._textReader = new StreamReader(_processing._fileName);
            }

            public Data Current => _element;

            object IEnumerator.Current => Current;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new InternalClass(this);
        }
    }
}
