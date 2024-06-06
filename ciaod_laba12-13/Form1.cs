using System;
using System.Diagnostics;
using System.Drawing;
using static ciaod_laba12_13.Form1;

namespace ciaod_laba12_13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(8);
            dataGridView1.Rows[0].Cells[1].Value = "Обмен";
            dataGridView1.Rows[1].Cells[1].Value = "Выбор";
            dataGridView1.Rows[2].Cells[1].Value = "Включение";
            dataGridView1.Rows[3].Cells[1].Value = "Быстрая";
            dataGridView1.Rows[4].Cells[1].Value = "Шелла";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";
            dataGridView1.Rows[7].Cells[1].Value = "Пирамидальная";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = true;
            }

        }
        public struct result
        {
            public ulong time;
            public ulong comp;
            public ulong reinst;
            public int[] newArray;
            public result()
            {
                time =comp=reinst= 0;
            }
            public result(long _time, int _comp,int _reinst, int[] _newArray)
            {
                time= (ulong)_time;
                comp= (ulong)_comp;
                reinst= (ulong)_reinst;
                newArray = _newArray;
            }
            public result(long _time, ulong _comp, ulong _reinst, int[] _newArray)
            {
                time = (ulong)_time;
                comp = _comp;
                reinst = _reinst;
                newArray = _newArray;
            }
        }
        public bool Check(int[] Array)
        {
            for (int i = 1;i< Array.Length;i++)
            {
                if (Array[i] < Array[i-1]) {
                    return false;
                }
            }
            return true;
        }
        
        public static result BubbleSort(int[] Array)
        {
            Stopwatch time = new Stopwatch();
            int comp  = 0;
            int reinst = 0;
            time.Start();
            bool have_reinst=true;
            for (int i = 0; i < Array.Length - 1; i++)
            {
                have_reinst = false;
                for (int j = 0; j < Array.Length-i - 1; j++)
                {
                    comp++;
                    if (Array[j] > Array[j + 1])
                    {
                        have_reinst = true;
                        reinst++;
                        int temp = Array[j];
                        Array[j] = Array[j + 1];
                        Array[j + 1] = temp;
                    }
                }
                if (have_reinst == false)
                {
                    break;
                }
                if (have_reinst == false)
                {
                    break;
                }
            }
            time.Stop();
            return new result(time.ElapsedMilliseconds,comp, reinst,Array);
        }

        static public result ViborSort(int[] Array)
        {
            Stopwatch time = new Stopwatch();
            int comp = 0;
            int reinst = 0;
            time.Start();
            for (int i = 0; i < Array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Array.Length; j++)
                {
                    comp++;
                    if (Array[j] < Array[min])
                    {
                        min = j;
                    }
                }
                reinst++;
                int temp = Array[min];
                Array[min] = Array[i];
                Array[i] = temp;
            }
            time.Stop();
            return new result(time.ElapsedMilliseconds, comp, reinst, Array);
        }

        static public result SortingByDirectInclusions(int[] Array)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            int comp = 0;
            int reinst = 0;
            int min = Array[0];
            int j = 0;
            for (int i = 1; i < Array.Length; i++)
            {
                comp++;
                if (Array[i] < min)
                {
                    min = Array[i];
                    j = i;
                }
            }
            Array[j] = Array[0];
            Array[0] = min;
            reinst += 2;
            for (int i = 1; i < Array.Length; i++)
            {
                int value = Array[i]; 
                int index = i;     
                while ((index > 0) && (Array[index - 1] > value))
                {
                    comp++;
                    reinst++;
                    Array[index] = Array[index - 1];
                    index--;    
                }
                comp++;
                reinst++;
                Array[index] = value;
            }
            time.Stop();
            return new result(time.ElapsedMilliseconds,comp, reinst, Array);
        }

        static public result Quicksort(int[] array,int startindex,int endindex)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            int index = startindex;
            ulong comp = 0;
            ulong reinst = 0;
            for (int i = startindex+1; i < endindex; i++)
            {
                comp++;
                if (array[i] < array[index])
                {
                    reinst += 2;
                    int tmp = array[index];
                    array[index] = array[i];
                    array[i]= array[index+1];
                    array[index+1] = tmp;
                    index++;
                }
            }

            result res1 = QuicksortWT(array,startindex,index);
            result res2 = QuicksortWT(array,index+1,endindex);
            time.Stop();
            return new result(time.ElapsedMilliseconds, comp + res1.comp+res2.comp, reinst+res1.reinst+res2.reinst, array);
        }
        static public result QuicksortWT(int[] array, int startindex, int endindex)
        {
            if (startindex >= endindex)
            {
                result result = new result();
                result.comp = 1;
                return result;
            }
            int index = startindex;
            ulong comp = 0;
            ulong reinst = 0;
            for (int i = startindex + 1; i < endindex; i++)
            {
                comp++;
                if (array[i] < array[index])
                {
                    reinst += 2;
                    int tmp = array[index];
                    array[index] = array[i];
                    array[i] = array[index + 1];
                    array[index + 1] = tmp;
                    index++;
                }
            }

            result res1 = Quicksort(array, startindex, index);
            result res2 = Quicksort(array, index + 1, endindex);
            result res = new result();
            res.comp = comp+res1.comp+res2.comp;
            res.reinst=reinst+res1.reinst+res2.reinst;
            return res;
        }

        result ShellSort(int[] array)
        {
            ulong comp = 0;
            ulong reinst = 0;
            var sw = new Stopwatch();
            sw.Start();
            int j;
            int step = 1;
            while (step <= array.Length / 9)
            {
                step = 3 * step + 1;
            }
            while (step > 0)
            {
                for (int i = 0; i < (array.Length - step); i++)
                {
                    j = i;
                    int tmp = array[j + step];
                    while ((j >= 0) && (array[j] > tmp))
                    {
                        comp++;
                        array[j + step] = array[j];
                        j -= step;
                        reinst++;
                    }
                    array[j + step] = tmp;

                    comp++;
                }
                step = (step - 1) / 3;
            }

            sw.Stop();
            return new result(sw.ElapsedMilliseconds, comp, reinst, array);
        }
        result LinearSort(int[] array)
        {
            ulong comp = 0;
            ulong reinst = 0;
            int[] tmpArray = new int[array.Length];
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < array.Length; i++)
            {
                tmpArray[i] = 0;
            }
            for (int i = 0; i < array.Length; i++)
            {
                comp++;
                reinst++;
                tmpArray[array[i]]++;
            }
            int index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < tmpArray[i]; j++)
                {
                    reinst++;
                    array[index] = i;
                    index++;
                }
            }
            sw.Stop();
            return new result(sw.ElapsedMilliseconds, comp, reinst, array);
        }


        void fixDown(int[] Array, int n, int i, ref int comp, ref int swaps)
        {
            while (2 * i + 1 < n)
            {
                int j = 2 * i + 1;
                comp++;
                if (j + 1 < n && Array[j] < Array[j + 1]) j++;
                comp++;
                if (Array[i] >= Array[j]) break;
                swaps++;
                (Array[i], Array[j]) = (Array[j], Array[i]);
                i = j;
            }
        }

        result Pyrsort(int[] array)
        {
            int comp = 0;
            int swaps = 0;
            var sw = new Stopwatch();

            sw.Start();
            for (int i = array.Length / 2 - 1; i >= 0; i--)
                fixDown(array, array.Length, i, ref comp, ref swaps);

            for (int i = array.Length - 1; i >= 0; i--)
            {
                swaps++;
                (array[0], array[i]) = (array[i], array[0]);
                fixDown(array, i, 0, ref comp, ref swaps);
            }
            sw.Stop();
            return new result(sw.ElapsedMilliseconds, comp, swaps, array);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            label2.Text= string.Empty;
            for(int i=0;i< dataGridView1.Rows.Count; i++)
            {
                for (int j = 2; j < 6; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
            }
            Random rnd = new Random();
            int[] array = new int[Convert.ToInt32( numericUpDown1.Value)];
            for(int i=0; i<array.Length; i++)
            {
                array[i] = rnd.Next(0, array.Length);
            }
            result res;
            if (dataGridView1.Rows[0].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                res = BubbleSort(array_tmp);
                dataGridView1.Rows[0].Cells[2].Value = Convert.ToString(res.comp);
                dataGridView1.Rows[0].Cells[3].Value = Convert.ToString(res.reinst);
                dataGridView1.Rows[0].Cells[4].Value = Convert.ToString(res.time);
                if (Check(res.newArray))
                {
                    dataGridView1.Rows[0].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[0].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[1].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                res = ViborSort(array_tmp);
                dataGridView1.Rows[1].Cells[2].Value = Convert.ToString(res.comp);
                dataGridView1.Rows[1].Cells[3].Value = Convert.ToString(res.reinst);
                dataGridView1.Rows[1].Cells[4].Value = Convert.ToString(res.time);
                if (Check(res.newArray))
                {
                    dataGridView1.Rows[1].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[1].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[2].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                res =SortingByDirectInclusions(array_tmp);
                dataGridView1.Rows[2].Cells[2].Value = Convert.ToString(res.comp);
                dataGridView1.Rows[2].Cells[3].Value = Convert.ToString(res.reinst);
                dataGridView1.Rows[2].Cells[4].Value = Convert.ToString(res.time);
                if (Check(res.newArray))
                {
                    dataGridView1.Rows[2].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[2].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[3].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                 res = Quicksort(array_tmp,0,array_tmp.Length);
                dataGridView1.Rows[3].Cells[2].Value = Convert.ToString(res.comp);
                dataGridView1.Rows[3].Cells[3].Value = Convert.ToString(res.reinst);
                dataGridView1.Rows[3].Cells[4].Value = Convert.ToString(res.time);
                if (Check(res.newArray))
                {
                    dataGridView1.Rows[3].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[3].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[4].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                res = ShellSort(array_tmp);
                dataGridView1.Rows[4].Cells[2].Value = Convert.ToString(res.comp);
                dataGridView1.Rows[4].Cells[3].Value = Convert.ToString(res.reinst);
                dataGridView1.Rows[4].Cells[4].Value = Convert.ToString(res.time);
                if (Check(res.newArray))
                {
                    dataGridView1.Rows[4].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[4].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[5].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                res = LinearSort(array_tmp);
                dataGridView1.Rows[5].Cells[2].Value = Convert.ToString(res.comp);
                dataGridView1.Rows[5].Cells[3].Value = Convert.ToString(res.reinst);
                dataGridView1.Rows[5].Cells[4].Value = Convert.ToString(res.time);
                if (Check(res.newArray))
                {
                    dataGridView1.Rows[5].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[5].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[6].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                var sw = new Stopwatch();
                sw.Start();
                Array.Sort(array_tmp);
                sw.Stop();
                dataGridView1.Rows[6].Cells[2].Value = "-";
                dataGridView1.Rows[6].Cells[3].Value = "-";
                dataGridView1.Rows[6].Cells[4].Value = Convert.ToString(sw.ElapsedMilliseconds);
                if (Check(array_tmp))
                {
                    dataGridView1.Rows[6].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[6].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[7].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                res = Pyrsort(array_tmp);
                dataGridView1.Rows[7].Cells[2].Value = Convert.ToString(res.comp);
                dataGridView1.Rows[7].Cells[3].Value = Convert.ToString(res.reinst);
                dataGridView1.Rows[7].Cells[4].Value = Convert.ToString(res.time);
                if (Check(array_tmp))
                {
                    dataGridView1.Rows[7].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[7].Cells[5].Value = "нет";
                }
            }
            bool cheked = false;
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.Equals(true))
                {
                    cheked = true;
                    break;
                }

            }
            if (!cheked) {
                label2.Visible = true;
                label2.Text = "Ничего не выбрано";
            }

        }
    }
}