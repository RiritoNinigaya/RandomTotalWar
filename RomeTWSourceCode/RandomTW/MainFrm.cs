using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
 
namespace RandomTW
{
    public partial class MainFrm : Form
    {
        public struct UInt24
        {
            public static explicit operator UInt24(int value)
            {
                return new UInt24(value);
            }
            public UInt24(int value)
            {
                _mostSignificant = (byte)(value >> 16);
                _leastSignificant = (ushort)value;
            }
            public readonly ushort _leastSignificant;
            public readonly byte _mostSignificant;
        }

        struct TWPort
        {
            public int X, Y;
            public bool Is;
        };

        struct TWReg
        {
            public Color C;
            public int X, Y;
            public bool Is;
            public TWPort Port;
        };

        string IntDir;
        string UnitDir;
        public StratMap SMap = new StratMap();
        string hmappath = "";

        int nN = 26;
        int unit4faction = 4;
        int unit4AOR = 5;
        UnitInf[] UnitTypes;
        int[] utypes_f;
        string[] unames_f;
        int[] utypes_l;
        string[] unames_l;
        int[] utypes_g;
        string[] unames_g;
        ArrayList unit_detail;

        Random Rnd = new Random();
        FileStream FS;
        StreamWriter SW;
        BinaryWriter BW;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OFDlg.ShowDialog();
            //OFDlg_FileOk(object sender, CancelEventArgs e)
            //{
            //    string ss = OFDlg.FileName;
            //    tgaImage = new Paloma.TargaImage(ss);
            //    this.pbx1.Image = tgaImage.Image;
            //    this.Width = pbx1.Image.Width+this.Width-pbx1.Width;
            //    this.Height = pbx1.Image.Height + this.Height - pbx1.Height;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SFDlg.ShowDialog();
        }

        int TGAColorToInt(Color C)
        {
            return (C.B << 16) + (C.G) + (C.R << 8);
        }

        public Image LoadTGAImage(string path)
        {
            Image img = null;
            FileStream fs = null;
            int Width, Height, bpp, bytesPerPixel, stride;
            byte[] header = new byte[18]; // header
            // ----
                fs = new FileStream(path, FileMode.Open);
                fs.Read(header, 0, 18); // Маркер сдвинут на 18 байтов, так как произошло чтение;
                // ----
                // Определяем ширину TGA (старший байт * 256 + младший байт);
                Width = (header[13] * 256) + header[12];
                // Определяем высоту TGA (старший байт * 256 + младший байт);
                Height = (header[15] * 256) + header[14];
                // ----
                // Получаем TGA бит/пиксель (24 or 32);
                bpp = header[16];
                // Делим на 8 для получения байт/пиксель;
                bytesPerPixel = bpp / 8;
                // Вычисляем количество байт на строке;
                stride = Width * bytesPerPixel;
                // ----
                int x;
                byte temp;
                byte[] line = new byte[stride];
                Bitmap bm = new Bitmap(Width, Height);
                // ----
                for (int y = Height - 1; y >= 0; y--)  // проход по всем строкам
                {
                    fs.Read(line, 0, stride);
                    x = 0;
                    for (int pt = 0; pt < stride; pt += bytesPerPixel)
                    {
                        temp = line[pt];
                        line[pt] = line[pt + 2];
                        line[pt + 2] = temp;
                        bm.SetPixel(x, y, Color.FromArgb(line[pt], line[pt + 1], line[pt + 2]));
                        x++;
                    }
                }
                // ----
                img = bm;
            return img;
        }

        private void SFDlg_FileOk(object sender, CancelEventArgs e)
        {
            //string ss = SFDlg.FileName;
            //WriteTGA(tgaImage, ss);
            //SW.Close();
            //BW.Close();
            //FS.Close();
        }

        void WriteNewTGA(int W, int H, Color[,] clrData, string ss){
            FS = new FileStream(ss, FileMode.Create, FileAccess.Write);
            BW = new BinaryWriter(FS);

            FS.WriteByte(0);
            FS.WriteByte(0);
            FS.WriteByte(2); //(byte)Paloma.ImageType.UNCOMPRESSED_TRUE_COLOR);

            BW.Write((UInt16)0);
            BW.Write((UInt16)0);
            FS.WriteByte(0);

            BW.Write((UInt16)0);
            BW.Write((UInt16)0);
            BW.Write((UInt16)W);
            BW.Write((UInt16)H);
            FS.WriteByte(24);
            FS.WriteByte(0);

            int i, j;
            UInt24 i24;
            int colorInt;
            Color clr;
            for (j = H - 1; j >= 0; j--)
            {
                for (i = 0; i < W; i++)
                {
                    clr = clrData[i, j];
                    colorInt = TGAColorToInt(clr);
                    i24 = (UInt24)colorInt;
                    BW.Write(i24._mostSignificant);
                    BW.Write(i24._leastSignificant);
                }
            }
            FS.Close();
            BW.Close();
        }

        void CreateEmptyTxtFile(string ss)
        {
            FS = new FileStream(ss, FileMode.Create, FileAccess.Write);
            FS.Close();
        }

        void GenNames()
        {
            int i, j;
            FS = new FileStream(IntDir + "//data//text//names.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS, Encoding.Unicode);
            SW.WriteLine("¬ Names for characters");
            for (i = 0; i < 19; i++)
            {
                SMap.Names[i] = "" + (char)((int)'a' + i) + (char)((int)'a' + i) + "a";
                for (j = 0; j < nN; j++)
                {
                    SW.WriteLine("{" + (char)((int)'a' + i) + (char)((int)'a' + i) + (char)((int)'a' + j) + "}\t\t\t" + SMap.RndName());
                }
                SW.WriteLine("{x" + (char)((int)'a' + i) + (char)((int)'a' + i) + "}\t\t\t" + " ");
                SW.WriteLine("{w" + (char)((int)'a' + i) + (char)((int)'a' + i) + "}\t\t\t" + "A");
            }
            SW.WriteLine();
            SW.Close();
            FS.Close();

            FS = new FileStream(IntDir + "//data//descr_names.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS);
            for (i = 0; i < 19; i++)
            {
                SW.WriteLine("faction: " + SMap.Facs[i] + Environment.NewLine);
                SW.WriteLine("\tcharacters");
                for (j = 0; j < nN; j++)
                {
                    SW.WriteLine("\t\t" + (char)((int)'a' + i) + (char)((int)'a' + i) + (char)((int)'a' + j));
                }
                SW.WriteLine();
                SW.WriteLine("\tsurnames");
                SW.WriteLine("\t\tx" + (char)((int)'a' + i) + (char)((int)'a' + i));
                SW.WriteLine();
                SW.WriteLine("\twomen");
                SW.WriteLine("\t\tw" + (char)((int)'a' + i) + (char)((int)'a' + i));
                SW.WriteLine();
            }
            SW.Close();
            FS.Close();

            FS = new FileStream(IntDir + "//data//descr_names_lookup.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS);
            for (i = 0; i < 19; i++)
            {
                for (j = 0; j < nN; j++)
                {
                    SW.WriteLine("" + (char)((int)'a' + i) + (char)((int)'a' + i) + (char)((int)'a' + j));
                }
                SW.WriteLine("x" + (char)((int)'a' + i) + (char)((int)'a' + i));
                SW.WriteLine("w" + (char)((int)'a' + i) + (char)((int)'a' + i));
            }
            SW.Close();
            FS.Close();

            //factions
            if (SMap.New)
            {
                for (i = 0; i < 19; i++)
                {
                    SMap.FacsNames[i] = SMap.RndName();
                }

                if (File.Exists(UnitDir + "//expanded_bi.txt"))
                {
                    string sfs;
                    FS = new FileStream(IntDir + "//data//text//expanded_bi.txt", FileMode.Create, FileAccess.Write);
                    SW = new StreamWriter(FS, Encoding.Unicode);
                    FileStream FSR = new FileStream(UnitDir + "//expanded_bi.txt", FileMode.Open, FileAccess.Read);
                    StreamReader SR = new StreamReader(FSR);
                    while (!SR.EndOfStream)
                    {
                        sfs = SR.ReadLine();
                        for (i = 0; i < 19; i++)
                        {
                            sfs = sfs.Replace("" + (char)((int)'A' + i) + (char)((int)'A' + i) + (char)((int)'A' + i), SMap.FacsNames[i]);
                        }
                        SW.WriteLine(sfs);
                    }
                    SR.Close();
                    FSR.Close();
                    SW.Close();
                    FS.Close();
                }
            }
        }

        string ReadLineFromScndWord(ref StreamReader SR)
        {
            string s; int i = 0;
            s = SR.ReadLine();
            while (s[i] != ' ') i++;
            while ((s[i] == ' ') || (s[i] == '\t')) i++;
            s = s.Substring(i);
            return s;
        }

        string WriteVarString(string s)
        {
            string ss = "";
            int i1, i2;
            double d1, d2;
            string s1, s2;
            ArrayList S;
            int i = 0;
            while (i < s.Length)
            {
                if (s[i] == '[')
                {
                    S = new ArrayList();
                    i++;
                    s1 = "";
                    while (s[i] != ']')
                    {
                        if (s[i] == '|')
                        {
                            S.Add(s1);
                            s1 = "";
                        }
                        else
                        {
                            s1 += s[i];
                        }
                        i++;
                    }
                    S.Add(s1);
                    ss += (S[Rnd.Next(S.Count)]).ToString();
                }
                else if (s[i] == '(')
                {
                    i++;
                    s1 = "";
                    while (s[i] != '|')
                    {
                        s1 += s[i]; i++;
                    }
                    i++;
                    s2 = "";
                    while (s[i] != ')')
                    {
                        s2 += s[i]; i++;
                    }
                    i1 = int.Parse(s1);
                    i2 = int.Parse(s2);
                    ss += (Rnd.Next(i1, i2 + 1)).ToString();
                }
                else if (s[i] == '{')
                {
                    i++;
                    s1 = "";
                    while (s[i] != '|')
                    {
                        s1 += s[i]; i++;
                    }
                    i++;
                    s2 = "";
                    while (s[i] != '}')
                    {
                        s2 += s[i]; i++;
                    }
                    d1 = double.Parse(s1);
                    d2 = double.Parse(s2);
                    ss += (d1 + (d2 - d1) * Rnd.NextDouble()).ToString();
                }
                else
                {
                    ss += s[i];
                }
                i++;
            }
            return ss;
        }

        void GenUnits()
        {
            int AORs = SMap.AORs;
            if (unit4AOR > 100 / AORs) unit4AOR = 100 / AORs;
            int i, j, k, l;
            string UU;
            string uu;
            utypes_g = new int[19];
            unames_g = new string[19];
            utypes_f = new int[unit4faction * 19];
            unames_f = new string[unit4faction * 19];
            utypes_l = new int[unit4AOR * AORs];
            unames_l = new string[unit4AOR * AORs];

            //ships, if are
            StreamReader SR;
            ArrayList ShDataList = new ArrayList();
            ArrayList ShNameList = new ArrayList();
            ArrayList ShTypeList = new ArrayList();
            if (File.Exists(UnitDir + "//ships.txt"))
            {
                string shs, Shs = ""; bool shb = false;
                FS = new FileStream(UnitDir + "//ships.txt", FileMode.Open, FileAccess.Read);
                SR = new StreamReader(FS);
                while (!SR.EndOfStream)
                {
                    shs = SR.ReadLine();
                    if (shs.StartsWith("type"))
                    {
                        if (shb) ShDataList.Add(Shs);
                        shb = true;
                        Shs = shs + Environment.NewLine;
                        shs = shs.Replace("type", "");
                        shs = shs.Trim();
                        ShTypeList.Add(shs);
                    }
                    else
                    {
                        if (shb)
                        {
                            Shs += shs + Environment.NewLine;
                            if (shs.StartsWith("dictionary"))
                            {
                                shs = shs.Replace("dictionary", "");
                                shs = shs.Trim();
                                ShNameList.Add(shs);
                            }
                        }
                    }
                }
                if (shb) ShDataList.Add(Shs);
                SR.Close();
                FS.Close();
            }
            
            //open & read unit_inf
            FS = new FileStream(UnitDir + "//unit_inf.txt", FileMode.Open, FileAccess.Read);
            SR = new StreamReader(FS);
            j = int.Parse(SR.ReadLine());//.
            uu = SR.ReadLine();//detalization
            unit_detail = new ArrayList();
            while (uu != "")
            {
                uu = uu.Trim();
                i = uu.IndexOf(" ");
                if (i == -1)
                {
                    unit_detail.Add(uu);
                    uu = "";
                }
                else
                {
                    unit_detail.Add(uu.Substring(0, i));
                    uu = uu.Substring(i, uu.Length - i);
                }
            }
            i = 0;
            UnitTypes = new UnitInf[j];
            while (! SR.EndOfStream){
                uu = SR.ReadLine();
                if (uu == "#")
                {   //not use j
                    UnitTypes[i] = new UnitInf();
                    UnitTypes[i].name = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].name_text = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].model1 = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].model2 = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].model3 = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].model4 = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].texture = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].pic_units = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].pic_unitinfo = ReadLineFromScndWord(ref SR);
                    uu = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].climates = new double[12];
                    for (k = 0; k < 12; k++)
                    {
                        UU = "";
                        while ((uu[0] != ' ') && (uu[0] != '\t'))
                        {
                            if (uu[0] == '.') UU += ','; else UU += uu[0]; 
                            uu = uu.Remove(0, 1);
                            if (uu == "") break;
                        }
                        UnitTypes[i].climates[k] = double.Parse(UU, System.Globalization.NumberStyles.AllowDecimalPoint);
                        if (uu!="")
                            while ((uu[0] == ' ') || (uu[0] == '\t'))
                            {
                                uu = uu.Remove(0, 1);
                                if (uu == "") break;
                            }
                    }
                    uu = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].genfacloc = new double[3];
                    for (k = 0; k < 3; k++)
                    {
                        UU = "";
                        while ((uu[0] != ' ') && (uu[0] != '\t'))
                        {
                            if (uu[0] == '.') UU += ','; else UU += uu[0];
                            uu = uu.Remove(0, 1);
                            if (uu == "") break;
                        }
                        UnitTypes[i].genfacloc[k] = double.Parse(UU, System.Globalization.NumberStyles.AllowDecimalPoint);
                        if (uu != "") while ((uu[0] == ' ') || (uu[0] == '\t'))
                            {
                                uu = uu.Remove(0, 1);
                                if (uu == "") break;
                            }
                    }
                    UnitTypes[i].skeleton1 = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].skeleton2 = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].category = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].cclass = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].voice_type = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].soldier = ReadLineFromScndWord(ref SR);
                    //UnitTypes[i].mount = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].mount = SR.ReadLine();
                    UnitTypes[i].mount_effect = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].attributes = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].formation = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_health = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_pri = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_pri_attr = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_sec = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_sec_attr = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_pri_armour = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_sec_armour = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_heat = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_ground = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_mental = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_charge_dist = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_fire_delay = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_food = ReadLineFromScndWord(ref SR);
                    UnitTypes[i].stat_cost = ReadLineFromScndWord(ref SR);
                    i++;
                }
            }
            SR.Close();
            FS.Close();
            bool b; int kk;
            for (i = 0; i < 19; i++)
            {
                b = true;
                kk = 0;
                while (b)
                {
                    kk++;
                    l = Rnd.Next(j);
                    if ((SMap.FIs[i]) && (kk < 1000))
                    {
                        if ((UnitTypes[l].genfacloc[0] > Rnd.NextDouble())
                                && (UnitTypes[l].climates[SMap.ClmOfAORs[SMap.AORsOfRegs[SMap.RegsOfFacs[i]]]] > Rnd.NextDouble()))
                        {
                            b = false;
                            utypes_g[i] = l;
                        }
                    }
                    else if (kk < 1000)
                    {
                        if ((UnitTypes[l].genfacloc[0] > Rnd.NextDouble()))
                        {
                            b = false;
                            utypes_g[i] = l;
                        }
                    }
                    else
                    {
                        b = false;
                        utypes_g[i] = l;
                    }
                }
            }
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    for (k = 0; k < unit4faction; k++)
                    {
                        b = true;
                        kk = 0;
                        while (b)
                        {
                            kk++;
                            l = Rnd.Next(j);
                            if ((SMap.FIs[i]) && (kk < 1000))
                            {
                                if ((UnitTypes[l].genfacloc[1] > Rnd.NextDouble())
                                    && (UnitTypes[l].climates[SMap.ClmOfAORs[SMap.AORsOfRegs[SMap.RegsOfFacs[i]]]] > Rnd.NextDouble()))
                                {
                                    b = false;
                                    utypes_f[i * unit4faction + k] = l;
                                }
                            }
                            else if (kk > 1000)
                            {
                                b = false;
                                utypes_f[i * unit4faction + k] = l;
                            }
                        }
                    }
                }
            }
            for (i = 0; i < AORs; i++)
            {
                for (k = 0; k < unit4AOR; k++)
                {
                    b = true;
                    kk = 0;
                    while (b)
                    {
                        kk++;
                        l = Rnd.Next(j);
                        if ((UnitTypes[l].genfacloc[2] > Rnd.NextDouble())
                            && (UnitTypes[l].climates[SMap.ClmOfAORs[i]] > Rnd.NextDouble()))
                        {
                            b = false;
                            utypes_l[i * unit4AOR + k] = l;
                        }
                        else if (kk > 1000)
                        {
                            b = false;
                            utypes_l[i * unit4AOR + k] = l;
                        }
                    }
                }
            }

            //text_export_units
            FS = new FileStream(IntDir + "//data//text//export_units.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS, Encoding.Unicode);
            //gereral_units
            SW.WriteLine("¬ ololo");
            for (i = 0; i < 19; i++)
            {
                UU = SMap.Facs[i] + "_general_unit";
                //uu = SMap.Facs[i] + " general";
                uu = UnitTypes[utypes_g[i]].name_text;
                uu = uu.Replace("%", SMap.Facs[i]);
                SMap.Generals[i] = SMap.Facs[i] + " general unit";
                SW.Write("{" + UU + "}\t"); 
                SW.WriteLine(uu);//unit_name
                SW.WriteLine();
                SW.WriteLine("{" + UU + "_descr}");
                SW.WriteLine(UnitTypes[utypes_g[i]].name);//descr
                SW.WriteLine();
                SW.WriteLine("{" + UU + "_descr_short}");
                SW.WriteLine(uu);//descr_short
                SW.WriteLine();
                SW.WriteLine();
                SW.WriteLine();
            }
            //faction_units
            SW.WriteLine("¬ ololo");
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    for (k = 0; k < unit4faction; k++)
                    {
                        UU = SMap.Facs[i] + "_" + (char)((int)'a' + k) + "_" + UnitTypes[utypes_f[i * unit4faction + k]].name;
                        //uu = SMap.Facs[i] + " " + (char)((int)'a' + k) + " " + UnitTypes[utypes_f[i * unit4faction + k]].name;
                        uu = UnitTypes[utypes_f[i * unit4faction + k]].name_text;
                        uu = uu.Replace("%", SMap.Facs[i]);
                        unames_f[i * unit4faction + k] = uu;
                        SW.Write("{" + UU + "}\t");
                        SW.WriteLine(uu);//unit_name
                        SW.WriteLine();
                        SW.WriteLine("{" + UU + "_descr}");
                        SW.WriteLine(UnitTypes[utypes_f[i * unit4faction + k]].name);//descr
                        SW.WriteLine();
                        SW.WriteLine("{" + UU + "_descr_short}");
                        SW.WriteLine(uu);//descr_short
                        SW.WriteLine();
                        SW.WriteLine();
                        SW.WriteLine();
                    }
                }
            }
            //local_units
            SW.WriteLine("¬ ololo");
            for (i = 0; i < AORs; i++)
            {
                for (k = 0; k < unit4AOR; k++)
                {
                    UU = "local_" + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26) + "_" + (char)((int)'a' + k) + "_" + UnitTypes[utypes_l[i * unit4AOR + k]].name;
                    //uu = "local " + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26) + " " + (char)((int)'a' + k) + " " + UnitTypes[utypes_l[i * unit4AOR + k]].name;
                    uu = UnitTypes[utypes_l[i * unit4AOR + k]].name_text;
                    uu = uu.Replace("%", "" + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26));
                    unames_l[i * unit4AOR + k] = uu;
                    SW.Write("{" + UU + "}\t");
                    SW.WriteLine(uu);//unit_name
                    SW.WriteLine();
                    SW.WriteLine("{" + UU + "_descr}");
                    SW.WriteLine(UnitTypes[utypes_l[i * unit4AOR + k]].name);//descr
                    SW.WriteLine();
                    SW.WriteLine("{" + UU + "_descr_short}");
                    SW.WriteLine(uu);//descr_short
                    SW.WriteLine();
                    SW.WriteLine();
                    SW.WriteLine();
                }
            }
            for (i = 0; i < ShNameList.Count; i++)
            {
                SW.Write("{" + (string)ShNameList[i] + "}\t");
                SW.WriteLine((string)ShNameList[i]);
                SW.WriteLine();
                SW.WriteLine("{" + (string)ShNameList[i] + "_descr}");
                SW.WriteLine((string)ShNameList[i]);//descr
                SW.WriteLine();
                SW.WriteLine("{" + (string)ShNameList[i] + "_descr_short}");
                SW.WriteLine((string)ShNameList[i]);
                SW.WriteLine();
                SW.WriteLine();
                SW.WriteLine();
            }
            SW.Close();
            FS.Close();

            //export_descr_unit_enums
            FS = new FileStream(IntDir + "//data//export_descr_unit_enums.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS);
            //general_units
            for (i = 0; i < 19; i++)
            {
                UU = SMap.Facs[i] + "_general_unit";
                SW.WriteLine(UU);
                SW.WriteLine(UU + "_descr");
                SW.WriteLine(UU + "_descr_short");
                SW.WriteLine();
            }
            //faction_units
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    for (k = 0; k < unit4faction; k++)
                    {
                        UU = SMap.Facs[i] + "_" + (char)((int)'a' + k) + "_" + UnitTypes[utypes_f[i * unit4faction + k]].name;
                        SW.WriteLine(UU);
                        SW.WriteLine(UU + "_descr");
                        SW.WriteLine(UU + "_descr_short");
                        SW.WriteLine();
                    }
                }
            }
            //local_units
            for (i = 0; i < AORs; i++)
            {
                for (k = 0; k < unit4AOR; k++)
                {
                    UU = "local_" + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26) + "_" + (char)((int)'a' + k) + "_" + UnitTypes[utypes_l[i * unit4AOR + k]].name;
                    SW.WriteLine(UU);
                    SW.WriteLine(UU + "_descr");
                    SW.WriteLine(UU + "_descr_short");
                    SW.WriteLine();
                }
            }
            for (i = 0; i < ShNameList.Count; i++)
            {
                SW.WriteLine((string)ShNameList[i]);
                SW.WriteLine((string)ShNameList[i] + "_descr");
                SW.WriteLine((string)ShNameList[i] + "_descr_short");
                SW.WriteLine();
            }
            SW.Close();
            FS.Close();

            //descr_model_battle
            FileInfo f = new FileInfo(UnitDir + "//descr_model_battle.txt");
            f.CopyTo(IntDir + "//data//descr_model_battle.txt", true);
            FS = new FileStream(IntDir + "//data//descr_model_battle.txt", FileMode.Append, FileAccess.Write);
            SW = new StreamWriter(FS);
            //general_units
            for (i = 0; i < 19; i++)
            {
                UU = SMap.Facs[i] + "_general_unit";
                SW.WriteLine("type\t\t\t\t" + UU);
                SW.WriteLine("skeleton\t\t\t" + UnitTypes[utypes_g[i]].skeleton1 + ", " + UnitTypes[utypes_g[i]].skeleton2);
                SW.WriteLine("indiv_range\t\t\t40");
                string sss = WriteVarString(UnitTypes[utypes_g[i]].texture);
                SW.WriteLine("texture\t\t\t\t" + SMap.Facs[i] + ", " + //UnitDir + @"/textures/" + 
                    sss);
                SW.WriteLine("texture\t\t\t\t" + "slave" + ", " + //UnitDir + @"/textures/" + 
                    sss);
                for (j = 0; j < unit_detail.Count; j++)
                {
                    switch(j){
                        case 0:
                            SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                            UnitTypes[utypes_g[i]].model1 + ", " + (string)unit_detail[j]);
                            break;
                        case 1:
                            SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                            UnitTypes[utypes_g[i]].model2 + ", " + (string)unit_detail[j]);
                            break;
                        case 2:
                            SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                            UnitTypes[utypes_g[i]].model3 + ", " + (string)unit_detail[j]);
                            break;
                        default:
                            SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                            UnitTypes[utypes_g[i]].model4 + ", " + (string)unit_detail[j]);
                            break;
                    }
                }
                SW.WriteLine("model_tri\t\t\t" + "400, 0.5f, 0.5f, 0.5");
                SW.WriteLine();
                SW.WriteLine();
            }
            //faction_units
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    for (k = 0; k < unit4faction; k++)
                    {
                        UU = SMap.Facs[i] + "_" + (char)((int)'a' + k) + "_" + UnitTypes[utypes_f[i * unit4faction + k]].name;
                        SW.WriteLine("type\t\t\t\t" + UU);
                        SW.WriteLine("skeleton\t\t\t" + UnitTypes[utypes_f[i * unit4faction + k]].skeleton1 + ", " + UnitTypes[utypes_f[i * unit4faction + k]].skeleton2);
                        SW.WriteLine("indiv_range\t\t\t40");
                        string sss = WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].texture);
                        for (j = 0; j < 19; j++)
                        {
                            SW.WriteLine("texture\t\t\t\t" + SMap.Facs[j] + ", " + //UnitDir + @"/textures/" + 
                                sss);
                        }
                        SW.WriteLine("texture\t\t\t\t" + "slave" + ", " + //UnitDir + @"/textures/" + 
                            sss);
                        SW.WriteLine("texture\t\t\t\t" + "merc" + ", " + //UnitDir + @"/textures/" + 
                            sss);
                        for (l = 0; l < unit_detail.Count; l++)
                        {
                            switch (l)
                            {
                                case 0:
                                    SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                                    UnitTypes[utypes_f[i * unit4faction + k]].model1 + ", " + (string)unit_detail[l]);
                                    break;
                                case 1:
                                    SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                                    UnitTypes[utypes_f[i * unit4faction + k]].model2 + ", " + (string)unit_detail[l]);
                                    break;
                                case 2:
                                    SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                                    UnitTypes[utypes_f[i * unit4faction + k]].model3 + ", " + (string)unit_detail[l]);
                                    break;
                                default:
                                    SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                                    UnitTypes[utypes_f[i * unit4faction + k]].model4 + ", " + (string)unit_detail[l]);
                                    break;
                            }
                        }
                        SW.WriteLine("model_tri\t\t\t" + "400, 0.5f, 0.5f, 0.5");
                        SW.WriteLine();
                        SW.WriteLine();
                    }
                }
            }
            //local_units
            for (i = 0; i < AORs; i++)
            {
                for (k = 0; k < unit4AOR; k++)
                {
                    UU = "local_" + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26) + "_" + (char)((int)'a' + k) + "_" + UnitTypes[utypes_l[i * unit4AOR + k]].name;
                    SW.WriteLine("type\t\t\t\t" + UU);
                    SW.WriteLine("skeleton\t\t\t" + UnitTypes[utypes_l[i * unit4AOR + k]].skeleton1 + ", " + UnitTypes[utypes_l[i * unit4AOR + k]].skeleton2);
                    SW.WriteLine("indiv_range\t\t\t40");
                    string sss = WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].texture);
                    for (j = 0; j < 19; j++)
                    {
                        SW.WriteLine("texture\t\t\t\t" + SMap.Facs[j] + ", " + //UnitDir + @"/textures/" + 
                            sss);
                    }
                    SW.WriteLine("texture\t\t\t\t" + "slave" + ", " + //UnitDir + @"/textures/" + 
                        sss);
                    SW.WriteLine("texture\t\t\t\t" + "merc" + ", " + //UnitDir + @"/textures/" + 
                        sss);
                    for (l = 0; l < unit_detail.Count; l++)
                    {
                        switch (l)
                        {
                            case 0:
                                SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                                UnitTypes[utypes_l[i * unit4AOR + k]].model1 + ", " + (string)unit_detail[l]);
                                break;
                            case 1:
                                SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                                UnitTypes[utypes_l[i * unit4AOR + k]].model2 + ", " + (string)unit_detail[l]);
                                break;
                            case 2:
                                SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                                UnitTypes[utypes_l[i * unit4AOR + k]].model3 + ", " + (string)unit_detail[l]);
                                break;
                            default:
                                SW.WriteLine("model_flexi\t\t\t" + //UnitDir + @"/models/" + 
                                UnitTypes[utypes_l[i * unit4AOR + k]].model4 + ", " + (string)unit_detail[l]);
                                break;
                        }
                    }
                    SW.WriteLine("model_tri\t\t\t" + "400, 0.5f, 0.5f, 0.5");
                    SW.WriteLine();
                    SW.WriteLine();
                }
            }
            SW.Close();
            FS.Close();

            //export_descr_unit
            FS = new FileStream(IntDir + "//data//export_descr_unit.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS);
            //general_units
            for (i = 0; i < 19; i++)
            {
                UU = SMap.Facs[i] + "_general_unit";
                uu = SMap.Facs[i] + " general unit";
                unames_g[i] = uu;
                SW.WriteLine("type             " + uu);
                SW.WriteLine("dictionary       " + UU);
                SW.WriteLine("category         " + UnitTypes[utypes_g[i]].category);
                SW.WriteLine("class            " + UnitTypes[utypes_g[i]].cclass);
                SW.WriteLine("voice_type       " + WriteVarString(UnitTypes[utypes_g[i]].voice_type));
                SW.WriteLine("soldier          " + UU + WriteVarString(UnitTypes[utypes_g[i]].soldier));
                if (!UnitTypes[utypes_g[i]].mount.EndsWith("!"))
                    SW.WriteLine("" + WriteVarString(UnitTypes[utypes_g[i]].mount));
                if (UnitTypes[utypes_g[i]].mount_effect != "!")
                    SW.WriteLine("mount_effect      " + WriteVarString(UnitTypes[utypes_g[i]].mount_effect));
                SW.WriteLine("attributes       " + "general_unit, " + WriteVarString(UnitTypes[utypes_g[i]].attributes));
                SW.WriteLine("formation        " + WriteVarString(UnitTypes[utypes_g[i]].formation));
                SW.WriteLine("stat_health      " + WriteVarString(UnitTypes[utypes_g[i]].stat_health));
                SW.WriteLine("stat_pri         " + WriteVarString(UnitTypes[utypes_g[i]].stat_pri));
                SW.WriteLine("stat_pri_attr    " + WriteVarString(UnitTypes[utypes_g[i]].stat_pri_attr));
                SW.WriteLine("stat_sec         " + WriteVarString(UnitTypes[utypes_g[i]].stat_sec));
                SW.WriteLine("stat_sec_attr    " + WriteVarString(UnitTypes[utypes_g[i]].stat_sec_attr));
                SW.WriteLine("stat_pri_armour  " + WriteVarString(UnitTypes[utypes_g[i]].stat_pri_armour));
                SW.WriteLine("stat_sec_armour  " + WriteVarString(UnitTypes[utypes_g[i]].stat_sec_armour));
                SW.WriteLine("stat_heat        " + WriteVarString(UnitTypes[utypes_g[i]].stat_heat));
                SW.WriteLine("stat_ground      " + WriteVarString(UnitTypes[utypes_g[i]].stat_ground));
                SW.WriteLine("stat_mental      " + WriteVarString(UnitTypes[utypes_g[i]].stat_mental));
                SW.WriteLine("stat_charge_dist " + WriteVarString(UnitTypes[utypes_g[i]].stat_charge_dist));
                SW.WriteLine("stat_fire_delay  " + WriteVarString(UnitTypes[utypes_g[i]].stat_fire_delay));
                SW.WriteLine("stat_food        " + WriteVarString(UnitTypes[utypes_g[i]].stat_food));
                SW.WriteLine("stat_cost        " + WriteVarString(UnitTypes[utypes_g[i]].stat_cost));
                SW.WriteLine("ownership        " + SMap.Facs[i] + ", slave");
                SW.WriteLine();
            }
            //faction_units
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    for (k = 0; k < unit4faction; k++)
                    {
                        UU = SMap.Facs[i] + "_" + (char)((int)'a' + k) + "_" + UnitTypes[utypes_f[i * unit4faction + k]].name;
                        uu = SMap.Facs[i] + " " + (char)((int)'a' + k) + " " + UnitTypes[utypes_f[i * unit4faction + k]].name;
                        unames_f[i * unit4faction + k] = uu;
                        SW.WriteLine("type             " + uu);
                        SW.WriteLine("dictionary       " + UU);
                        SW.WriteLine("category         " + UnitTypes[utypes_f[i * unit4faction + k]].category);
                        SW.WriteLine("class            " + UnitTypes[utypes_f[i * unit4faction + k]].cclass);
                        SW.WriteLine("voice_type       " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].voice_type));
                        SW.WriteLine("soldier          " + UU + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].soldier));
                        if (!UnitTypes[utypes_f[i * unit4faction + k]].mount.EndsWith("!"))
                            SW.WriteLine("" + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].mount));
                        if (UnitTypes[utypes_f[i * unit4faction + k]].mount_effect != "!")
                            SW.WriteLine("mount_effect      " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].mount_effect));
                        SW.WriteLine("attributes       " + UnitTypes[utypes_f[i * unit4faction + k]].attributes);
                        SW.WriteLine("formation        " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].formation));
                        SW.WriteLine("stat_health      " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_health));
                        SW.WriteLine("stat_pri         " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_pri));
                        SW.WriteLine("stat_pri_attr    " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_pri_attr));
                        SW.WriteLine("stat_sec         " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_sec));
                        SW.WriteLine("stat_sec_attr    " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_sec_attr));
                        SW.WriteLine("stat_pri_armour  " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_pri_armour));
                        SW.WriteLine("stat_sec_armour  " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_sec_armour));
                        SW.WriteLine("stat_heat        " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_heat));
                        SW.WriteLine("stat_ground      " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_ground));
                        SW.WriteLine("stat_mental      " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_mental));
                        SW.WriteLine("stat_charge_dist " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_charge_dist));
                        SW.WriteLine("stat_fire_delay  " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_fire_delay));
                        SW.WriteLine("stat_food        " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_food));
                        SW.WriteLine("stat_cost        " + WriteVarString(UnitTypes[utypes_f[i * unit4faction + k]].stat_cost));
                        SW.WriteLine("ownership        " + "all");
                        SW.WriteLine();
                    }
                }
            }
            //local_units
            for (i = 0; i < AORs; i++)
            {
                for (k = 0; k < unit4AOR; k++)
                {
                    UU = "local_" + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26) + "_" + (char)((int)'a' + k) + "_" + UnitTypes[utypes_l[i * unit4AOR + k]].name;
                    uu = "local " + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26) + " " + (char)((int)'a' + k) + " " + UnitTypes[utypes_l[i * unit4AOR + k]].name;
                    unames_l[i * unit4AOR + k] = uu;
                    SW.WriteLine("type             " + uu);
                    SW.WriteLine("dictionary       " + UU);
                    SW.WriteLine("category         " + UnitTypes[utypes_l[i * unit4AOR + k]].category);
                    SW.WriteLine("class            " + UnitTypes[utypes_l[i * unit4AOR + k]].cclass);
                    SW.WriteLine("voice_type       " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].voice_type));
                    SW.WriteLine("soldier          " + UU + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].soldier));
                    if (!UnitTypes[utypes_l[i * unit4AOR + k]].mount.EndsWith("!"))
                        SW.WriteLine("" + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].mount));
                    if (UnitTypes[utypes_l[i * unit4AOR + k]].mount_effect != "!")
                        SW.WriteLine("mount_effect      " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].mount_effect));
                    SW.WriteLine("attributes       " + "mercenary_unit, " + UnitTypes[utypes_l[i * unit4AOR + k]].attributes);
                    SW.WriteLine("formation        " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].formation));
                    SW.WriteLine("stat_health      " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_health));
                    SW.WriteLine("stat_pri         " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_pri));
                    SW.WriteLine("stat_pri_attr    " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_pri_attr));
                    SW.WriteLine("stat_sec         " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_sec));
                    SW.WriteLine("stat_sec_attr    " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_sec_attr));
                    SW.WriteLine("stat_pri_armour  " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_pri_armour));
                    SW.WriteLine("stat_sec_armour  " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_sec_armour));
                    SW.WriteLine("stat_heat        " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_heat));
                    SW.WriteLine("stat_ground      " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_ground));
                    SW.WriteLine("stat_mental      " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_mental));
                    SW.WriteLine("stat_charge_dist " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_charge_dist));
                    SW.WriteLine("stat_fire_delay  " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_fire_delay));
                    SW.WriteLine("stat_food        " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_food));
                    SW.WriteLine("stat_cost        " + WriteVarString(UnitTypes[utypes_l[i * unit4AOR + k]].stat_cost));
                    SW.WriteLine("ownership        " + "all");
                    SW.WriteLine();
                }
            }
            for (i = 0; i < ShDataList.Count; i++)
            {
                SW.Write((string)ShDataList[i]);
            }
            SW.Close();
            FS.Close();

            //export_descr_buildings
            string SS = "";
            string lSS = "";
            string ss = "";
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    for (k = 0; k < unit4faction; k++)
                    {
                        SS += ("\t\trecruit \"" + unames_f[i * unit4faction + k] +
                            "\"  0  requires factions { " + SMap.Facs[i] + ", } " + Environment.NewLine);
                    }
                }
            }
            for (i = 0; i < AORs; i++)
            {
                for (k = 0; k < unit4AOR; k++)
                {
                    lSS += ("\t\trecruit \"" + unames_l[i * unit4AOR + k] +
                        "\"  0  requires factions { " + "all" + ", } and hidden_resource " + "hres_" +
                        i +  Environment.NewLine);
                }
            }
            FileStream FS2 = new FileStream(UnitDir + "//export_descr_buildings.txt", FileMode.Open, FileAccess.Read);
            FS = new FileStream(IntDir + "//data//export_descr_buildings.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS);
            SR = new StreamReader(FS2);
            while (! SR.EndOfStream){
                uu = SR.ReadLine();
                if (uu.StartsWith("QQQQQ"))
                {
                    //uu = uu.Replace("QQQQQ ", " ");
                    //SW.Write(SS.Replace("; @@@", uu));
                    SW.Write(SS);
                }
                else if (uu.StartsWith("WWWWW"))
                {
                    //uu = uu.Replace("QQQQQ ", " ");
                    //SW.Write(lSS.Replace("; @@@", uu));
                    SW.Write(lSS);
                }
                else if (uu.StartsWith("NNNNN"))
                {
                    for (i = 0; i < ShTypeList.Count; i++)
                    {
                        SW.WriteLine("\t\trecruit \"" + ShTypeList[i] + "\"  0\"");
                    }
                }
                else if (uu.StartsWith("hidden_resources"))
                {
                    SW.Write("hidden_resources");
                    for (j = 0; j < 12; j++)
                    {
                        //SW.Write(" clmres_" + j);
                    }
                    for (j = 0; j < AORs; j++)
                    {
                        SW.Write(" hres_" + j);
                    }
                }
                else
                {
                    SW.WriteLine(uu);
                }
            }
            SR.Close();
            SW.Close();
            FS.Close();
            FS2.Close();

            //descr_rebel_factions
            ss = "";
            SS = "";
            for (i = 0; i < SMap.AORs; i++)
            {
                SS += "rebel_type\t\t\t" +
                    "" + (char)((int)'A' + i / 26) + (char)((int)'a' + i % 26) + "rebels" + Environment.NewLine;
                SS += "category\t\t\tpeasant_revolt" + Environment.NewLine;
                SS += "chance\t\t\t\t3" + Environment.NewLine;
                SS += "description\t\t\t" +
                    "" + (char)((int)'A' + i / 26) + (char)((int)'a' + i % 26) + "rebels" + Environment.NewLine;
                for (j = 0; j < unit4AOR; j++)
                {
                    SS += "unit\t\t\t\t" + unames_l[i * unit4AOR + j] + Environment.NewLine;
                }
                SS += Environment.NewLine;
            }
            ss = ("unit\t\t\t\t" + unames_l[0] + Environment.NewLine);
            //FS = new FileStream(IntDir + "//data//descr_rebel_factions.txt", FileMode.Create, FileAccess.Write);
            FS = new FileStream(IntDir + "//data//descr_rebel_factions.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS);
            SW.WriteLine(SS);
            SW.WriteLine("rebel_type\t\t\tgladiator_uprising");
            SW.WriteLine("category\t\t\tgladiator_revolt");
            SW.WriteLine("chance\t\t\t\t100");
            SW.WriteLine("description\t\t\tgladiator_uprising");
            SW.WriteLine(ss);
            SW.WriteLine("rebel_type\t\t\tbrigands");
            SW.WriteLine("category\t\t\tbrigands");
            SW.WriteLine("chance\t\t\t\t50");
            SW.WriteLine("description\t\t\tbrigands");
            SW.WriteLine(ss);
            SW.WriteLine("rebel_type\t\t\tpirates");
            SW.WriteLine("category\t\t\tpirates");
            SW.WriteLine("chance\t\t\t\t50");
            SW.WriteLine("description\t\t\tpirates");
            for (i = 0; i < ShNameList.Count; i++)
            {
                SW.WriteLine("unit\t\t\t\t" + ShTypeList[i]);
            }

            //while (!SR.EndOfStream)
            //{
            //    uu = SR.ReadLine();
            //    if (uu == "QQQQQ")
            //    {
            //        SW.Write(SS);
            //        //SW.Write(ss);
            //    }
            //    else 
            //    if (uu == "qqqqq")
            //    {
            //        SW.Write(ss);
            //    }
            //    else
            //    {
            //        SW.WriteLine(uu);
            //    }
            //}
            SW.Close();
            FS.Close();

            //rebel_faction_descr_enums
            //f = new FileInfo("RndData//rebel_faction_descr_enums.txt");
            //f.CopyTo(IntDir + "//data//rebel_faction_descr_enums.txt", true);
            FS = new FileStream(IntDir + "//data//rebel_faction_descr_enums.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS);
            for (i = 0; i < SMap.AORs; i++)
            {
                SW.WriteLine("" + (char)((int)'A' + i / 26) + (char)((int)'a' + i % 26) + "rebels");
            }
            SW.WriteLine("slave_uprising");
            SW.WriteLine("gladiator_uprising");
            SW.WriteLine("brigands");
            SW.WriteLine("pirates");
            SW.Close();
            FS.Close();

            //text/rebel_faction_descr.txt
            FS = new FileStream(IntDir + "//data//text//rebel_faction_descr.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS, Encoding.Unicode);
            SW.WriteLine("¬ Text file converted with loc_parser");
            for (i = 0; i < SMap.AORs; i++)
            {
                SW.WriteLine("{" + (char)((int)'A' + i / 26) + (char)((int)'a' + i % 26) + "rebels}\t\t\t\t\t" +
                    (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26)); // add here names of aors(?)
            }
            SW.WriteLine("{slave_uprising}\t\t\t\t\tslave uprising");
            SW.WriteLine("{gladiator_uprising}\t\t\t\t\tgladiator uprising");
            SW.WriteLine("{brigands}\t\t\t\t\tbrigands");
            SW.WriteLine("{pirates}\t\t\t\t\tpirates");
            SW.Close();
            FS.Close();

            //descr_mercenaries.txt
            FS = new FileStream(IntDir + "//data//world//maps//campaign//imperial_campaign//descr_mercenaries.txt", FileMode.Create, FileAccess.Write);
            SW = new StreamWriter(FS);
            for (i = 0; i < SMap.AORs; i++)
            {
                SW.WriteLine();
                SW.WriteLine("pool " + (char)((int)'A' + i / 26) + (char)((int)'a' + i % 26));
                SW.Write("\tregions");
                for (j = 0; j < SMap.RN; j++)
                {
                    if (SMap.AORsOfRegs[j] == i)
                    {
                        SW.Write(" P" + j);
                    }
                }
                SW.WriteLine("\t\t\t");
                for (j = 0; j < unit4AOR; j++)
                {
                    SW.WriteLine("\tunit " + unames_l[i * unit4AOR + j] + ",\t\t\texp 0 cost 1500 replenish 0.11 - 0.33 max 3 initial 1");
                }
            }
            SW.WriteLine();
            SW.Close();
            FS.Close();

            //for SMap.descr_strat.txt
            SMap.egUnitNames = new string[SMap.AORs];
            for (i = 0; i < SMap.AORs; i++)
            {
                SMap.egUnitNames[i] = unames_l[i];
            }

            //ui/units && ui/unit_info
            if (!Directory.Exists(IntDir + "//data//ui"))
            {
                Directory.CreateDirectory(IntDir + "//data//ui");
            }
            if (!Directory.Exists(IntDir + "//data//ui//units"))
            {
                Directory.CreateDirectory(IntDir + "//data//ui//units");
            }
            if (!Directory.Exists(IntDir + "//data//ui//unit_info"))
            {
                Directory.CreateDirectory(IntDir + "//data//ui//unit_info");
            }
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    if (!Directory.Exists(IntDir + "//data//ui//units//" + SMap.Facs[i]))
                    {
                        Directory.CreateDirectory(IntDir + "//data//ui//units//" + SMap.Facs[i]);
                    }
                    if (!Directory.Exists(IntDir + "//data//ui//unit_info//" + SMap.Facs[i]))
                    {
                        Directory.CreateDirectory(IntDir + "//data//ui//unit_info//" + SMap.Facs[i]);
                    }
                }
            }
            if (!Directory.Exists(IntDir + "//data//ui//units//" + "slave"))
            {
                Directory.CreateDirectory(IntDir + "//data//ui//units//" + "slave");
            }
            if (!Directory.Exists(IntDir + "//data//ui//unit_info//" + "slave"))
            {
                Directory.CreateDirectory(IntDir + "//data//ui//unit_info//" + "slave");
            }
            if (!Directory.Exists(IntDir + "//data//ui//units//" + "mercs"))
            {
                Directory.CreateDirectory(IntDir + "//data//ui//units//" + "mercs");
            }
            if (!Directory.Exists(IntDir + "//data//ui//unit_info//" + "merc"))
            {
                Directory.CreateDirectory(IntDir + "//data//ui//unit_info//" + "merc");
            }
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    if (!UnitTypes[utypes_g[i]].pic_units.StartsWith("!"))
                    {
                        if (File.Exists(UnitTypes[utypes_g[i]].pic_units))
                        {
                            File.Copy(UnitTypes[utypes_g[i]].pic_units,
                                IntDir + "//data//ui//units//" + SMap.Facs[i] + "//#" +
                                SMap.Facs[i] + "_general_unit" + ".tga");
                        }
                    }
                    if (!UnitTypes[utypes_g[i]].pic_unitinfo.StartsWith("!"))
                    {
                        if (File.Exists(UnitTypes[utypes_g[i]].pic_unitinfo))
                        {
                            File.Copy(UnitTypes[utypes_g[i]].pic_unitinfo,
                                IntDir + "//data//ui//unit_info//" + SMap.Facs[i] + "//#" +
                                SMap.Facs[i] + "_general_unit" + ".tga");
                        }
                    }
                }
            }
            for (i = 0; i < 19; i++)
            {
                if (SMap.FIs[i])
                {
                    for (j = 0; j < unit4faction; j++)
                    {
                        if (!UnitTypes[utypes_f[i * unit4faction + j]].pic_units.StartsWith("!"))
                        {
                            if (File.Exists(UnitTypes[utypes_f[i * unit4faction + j]].pic_units))
                            {
                                File.Copy(UnitTypes[utypes_f[i * unit4faction + j]].pic_units,
                                    IntDir + "//data//ui//units//" + SMap.Facs[i] + "//#" + 
                                    SMap.Facs[i] + "_" + (char)((int)'a' + j) + "_" + UnitTypes[utypes_f[i * unit4faction + j]].name + ".tga");
                            }
                        }
                        if (!UnitTypes[utypes_f[i * unit4faction + j]].pic_unitinfo.StartsWith("!"))
                        {
                            if (File.Exists(UnitTypes[utypes_f[i * unit4faction + j]].pic_unitinfo))
                            {
                                File.Copy(UnitTypes[utypes_f[i * unit4faction + j]].pic_unitinfo,
                                    IntDir + "//data//ui//unit_info//" + SMap.Facs[i] + "//#" +
                                    SMap.Facs[i] + "_" + (char)((int)'a' + j) + "_" + UnitTypes[utypes_f[i * unit4faction + j]].name + ".tga");
                            }
                        }
                    }
                }
            }
            for (i = 0; i < SMap.AORs; i++)
            {
                for (j = 0; j < unit4AOR; j++)
                {
                    if (!UnitTypes[utypes_l[i * unit4AOR + j]].pic_units.StartsWith("!"))
                    {
                        if (File.Exists(UnitTypes[utypes_l[i * unit4AOR + j]].pic_units))
                        {
                            File.Copy(UnitTypes[utypes_l[i * unit4AOR + j]].pic_units,
                                IntDir + "//data//ui//units//" + "mercs" + "//#" +
                                "local_" + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26) + "_" + (char)((int)'a' + j) + "_" + UnitTypes[utypes_l[i * unit4AOR + j]].name + ".tga");
                        }
                    }
                    if (!UnitTypes[utypes_l[i * unit4AOR + j]].pic_unitinfo.StartsWith("!"))
                    {
                        if (File.Exists(UnitTypes[utypes_l[i * unit4AOR + j]].pic_unitinfo))
                        {
                            File.Copy(UnitTypes[utypes_l[i * unit4AOR + j]].pic_unitinfo,
                                IntDir + "//data//ui//unit_info//" + "merc" + "//#" +
                                "local_" + (char)((int)'a' + i / 26) + (char)((int)'a' + i % 26) + "_" + (char)((int)'a' + j) + "_" + UnitTypes[utypes_l[i * unit4AOR + j]].name + ".tga");
                        }
                    }
                }
            }
        }

        void CopyFolder(string begin_dir, string end_dir)
        {
            if (Directory.Exists(end_dir) != true)
            {
                Directory.CreateDirectory(end_dir);
            }
            DirectoryInfo dir_inf = new DirectoryInfo(begin_dir);
            foreach (DirectoryInfo dir in dir_inf.GetDirectories())
            {
                if (Directory.Exists(end_dir + "\\" + dir.Name) != true)
                {
                    Directory.CreateDirectory(end_dir + "\\" + dir.Name);
                }
                CopyFolder(dir.FullName, end_dir + "\\" + dir.Name);
            }
            foreach (string file in Directory.GetFiles(begin_dir))
            {
                string filik = file.Substring(file.LastIndexOf('\\'), file.Length - file.LastIndexOf('\\'));
                File.Copy(file, end_dir + "\\" + filik, true);
            }
        }

        void CopyFiles(string dir)
        {
            int i, m; string s;
            if (Directory.Exists(dir))
            {
                string[] allfiles = Directory.GetFiles(dir);
                for (i = 0; i < allfiles.Length; i++)
                {
                    m = allfiles[i].LastIndexOf("\\");
                    s = allfiles[i].Substring(m, allfiles[i].Length - m);
                    s = s.Replace("\\", "");
                    if (s.StartsWith("~"))
                    {
                        File.Copy(allfiles[i], IntDir + "//data//" + s.Substring(1, s.Length - 1), true);
                    }
                }
                allfiles = Directory.GetDirectories(dir);
                for (i = 0; i < allfiles.Length; i++)
                {
                    m = allfiles[i].LastIndexOf("\\");
                    s = allfiles[i].Substring(m, allfiles[i].Length - m);
                    s = s.Replace("\\", "");
                    if (s.StartsWith("~"))
                    {
                        CopyFolder(allfiles[i], IntDir + "//data//" + s.Substring(1, s.Length - 1));
                    }
                }
            }
        }

        bool LoadLetters()
        {
            int i, j, k, l; string s;
            ArrayList AL = new ArrayList();
            ArrayList Al = new ArrayList();
            ArrayList al = new ArrayList();
            if (File.Exists(UnitDir + "//letters.txt"))
            {
                FileStream FS = new FileStream(UnitDir + "//letters.txt", FileMode.Open);
                StreamReader SR = new StreamReader(FS);
                while (!SR.EndOfStream)
                {
                    AL.Add(SR.ReadLine());
                }
                for (i = AL.Count - 1; i >= 0; i--)
                {
                    if (!((string)AL[i]).StartsWith("["))
                    {
                        AL.RemoveAt(i);
                    }
                }
                if (AL.Count < 1) return false;
                SMap.Letters = new string[AL.Count][][];

                for (i = 0; i < AL.Count; i++)
                {
                    k = 0; l = 0;
                    for (j = 0; j < ((string)AL[i]).Length; j++)
                    {
                        if (((string)AL[i])[j] == '[') k++;
                        if (((string)AL[i])[j] == ']') l++;
                    }
                    if (k != l) return false;
                    SMap.Letters[i] = new string[k][];

                    Al = new ArrayList();
                    s = "";
                    for (j = 0; j < ((string)AL[i]).Length; j++)
                    {
                        if (((string)AL[i])[j] == ' ')
                        {
                            if (s != "")
                            {
                                Al.Add(s);
                                s = "";
                            }
                        }
                        else
                        {
                            s += ((string)AL[i])[j];
                        }
                    }
                    Al.Add(s);

                    k = 0;
                    for (j = 0; j < Al.Count; j++)
                    {
                        if (((string)Al[j]).StartsWith("["))
                        {
                            if (j > 0)
                            {
                                SMap.Letters[i][k] = new string[al.Count];
                                SMap.Letters[i][k][0] = ((string)al[0]).Substring(1, ((string)al[0]).Length - 2);
                                for (l = 1; l < al.Count; l++)
                                {
                                    SMap.Letters[i][k][l] = (string)al[l];
                                }
                                k++;
                            }
                            al = new ArrayList();
                            al.Add(Al[j]);
                        }
                        else
                        {
                            al.Add(Al[j]);
                        }
                    }
                    SMap.Letters[i][k] = new string[al.Count];
                    SMap.Letters[i][k][0] = ((string)al[0]).Substring(1, ((string)al[0]).Length - 2);
                    for (l = 1; l < al.Count; l++)
                    {
                        SMap.Letters[i][k][l] = (string)al[l];
                    }
                }

                SR.Close();
                FS.Close();
                return true;
            }
            return false;
        }

        int[] StringToIntArray(string s, int k)
        {
            string ss = s, js; int j = 0;
            bool b = true;
            ArrayList A = new ArrayList();
            int i;
            while (ss != "")
            {
                ss = ss.Trim();
                i = ss.IndexOf(" ");
                if (i == -1)
                {
                    js = ss;
                    ss = "";
                }
                else
                {
                    js = ss.Substring(0, i);
                    ss = ss.Substring(i, ss.Length - i);
                }
                b = b && int.TryParse(js, out j);
                if (b) A.Add(j);
            }
            int[] Res = new int[19];
            if (b)
            {
                for (i = 0; i < 19; i++)
                {
                    Res[i] = (int)A[i];
                }
            }
            else
            {
                if (k == 0)
                    return SMap.def_clms;
                else
                    return SMap.def_clms2;
            }
            return Res;
        }
         
        double[] StringToDoubleArray(string s, int k)
        {
            string ss = s, js; double j = 0;
            bool b = true, bb = true;
            ArrayList A = new ArrayList();
            int i;
            while (ss != "")
            {
                ss = ss.Trim();
                i = ss.IndexOf(" ");
                if (i == -1)
                {
                    js = ss;
                    ss = "";
                }
                else
                {
                    js = ss.Substring(0, i);
                    ss = ss.Substring(i, ss.Length - i);
                }
                bb = double.TryParse(js, out j);
                if (!bb) bb = double.TryParse(js.Replace('.',','), out j);
                b = b && bb;
                if (b) A.Add(j);
            }
            double[] Res = new double[12];
            if (b)
            {
                for (i = 0; i < 12; i++)
                {
                    Res[i] = (int)A[i];
                }
            }
            else
            {
                if (k == 2)
                    return SMap.vers;
                else if (k == 3)
                    return SMap.forest_vers;
                else
                    return SMap.clmpopkf;
            }
            return Res;
        }

        void ReadGeographyInf()
        {
            string ss;
            int i = 0;
            FS = new FileStream(UnitDir + "//ships.txt", FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            while (!SR.EndOfStream)
            {
                ss = SR.ReadLine();
                if ((ss != "") && (!ss.StartsWith("//")))
                {
                    switch (i)
                    {
                        case 0: SMap.def_clms = StringToIntArray(ss, i);  break;
                        case 1: SMap.def_clms2 = StringToIntArray(ss, i);  break;
                        case 2: SMap.vers = StringToDoubleArray(ss, i);  break;
                        case 3: SMap.forest_vers = StringToDoubleArray(ss, i);  break;
                        case 4: SMap.clmpopkf = StringToDoubleArray(ss, i);  break;
                        default: break;
                    }
                }
            }
            SR.Close();
            FS.Close();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            tlStrSttLbl.Visible = true; tlStrPrgrssBr.Width = this.Width * 2 / 3;
            tlStrPrgrssBr.Visible = true;
            sttStr.Show(); //sttbar max = 100;
            tlStrSttLbl.Text = "initialization"; Application.DoEvents(); tlStrPrgrssBr.Value = 0;
            if (chbx_New.Checked) SMap.New = true; else SMap.New = false;
            if (chbx_hist.Checked) SMap.hist = true; else SMap.hist = false;
            //if (chbx_Pangea.Checked) SMap.island = true; else SMap.island = false;
            SMap.land_type = cbx_land.SelectedIndex;
            SMap.garrs = true;
            //this.Cursor = Cursors.WaitCursor;
            SMap.pw = double.Parse(tbx_1_pw.Text);
            SMap.r = int.Parse(tbx_1_r.Text);
            SMap.cP = true; // chbx_P.Checked;
            SMap.Rivers = int.Parse(tbx_rvr.Text);
            IntDir = tbx_Name.Text;
            UnitDir = tbxUnits.Text;
            int W = int.Parse(tbx_x.Text);
            int H = int.Parse(tbx_y.Text);
            SMap.W = W;
            SMap.H = H;
            SMap.RN = int.Parse(tbx_Regs.Text);
            SMap.FN = int.Parse(tbx_F.Text);
            SMap.Dph = int.Parse(tbx_Dph.Text);
            SMap.money = int.Parse(tbx_money.Text);
            if (File.Exists(UnitDir + "//geography_inf.txt"))
            {
                ReadGeographyInf();
            }
            tlStrSttLbl.Text = "creating directories"; Application.DoEvents(); tlStrPrgrssBr.Value = 5;
            if (hmappath != "")
            {
                SMap.hmapfrompath = LoadTGAImage(hmappath);
                W = (SMap.hmapfrompath.Width - 1) / 2;
                H = (SMap.hmapfrompath.Height - 1) / 2;
                SMap.W = W;
                SMap.H = H;
                SMap.GenHgtsfrompath();
            }
            if (!Directory.Exists(IntDir))
            {
                Directory.CreateDirectory(IntDir);
            }
            if (!Directory.Exists(IntDir + "//data"))
            {
                Directory.CreateDirectory(IntDir + "//data");
            }
            if (!Directory.Exists(IntDir + "//data//text"))
            {
                Directory.CreateDirectory(IntDir + "//data//text");
            }
            if (!Directory.Exists(IntDir + "//data//world"))
            {
                Directory.CreateDirectory(IntDir + "//data//world");
            }
            if (!Directory.Exists(IntDir + "//data//world//maps"))
            {
                Directory.CreateDirectory(IntDir + "//data//world//maps");
            }
            if (!Directory.Exists(IntDir + "//data//world//maps//base"))
            {
                Directory.CreateDirectory(IntDir + "//data//world//maps//base");
            }
            if (!Directory.Exists(IntDir + "//data//world//maps//campaign"))
            {
                Directory.CreateDirectory(IntDir + "//data//world//maps//campaign");
            }
            if (!Directory.Exists(IntDir + "//data//world//maps//campaign//imperial_campaign"))
            {
                Directory.CreateDirectory(IntDir + "//data//world//maps//campaign//imperial_campaign");
            }
            if (Directory.Exists(IntDir + "//data//ui"))
            {
                Directory.Delete(IntDir + "//data//ui", true);
            }
            CopyFiles(UnitDir);
            
                //maps
                tlStrSttLbl.Text = "heights"; Application.DoEvents(); tlStrPrgrssBr.Value = 10;
                if (hmappath == "")
                {
                    SMap.GenHgts1(2 * W + 1, 2 * H + 1, ref SMap.myHgtData);
                    SMap.HgtToClr(2 * W + 1, 2 * H + 1, SMap.myHgtData, ref SMap.myHgtClr);//heights
                }
                tlStrSttLbl.Text = "ground_types"; Application.DoEvents(); tlStrPrgrssBr.Value = 15;
                SMap.HgtToGTpClr(2 * W + 1, 2 * H + 1, SMap.myHgtData, ref SMap.myGTpClr);//ground_types
                tlStrSttLbl.Text = "climates"; Application.DoEvents(); tlStrPrgrssBr.Value = 20;
                SMap.GenClmClr(2 * W + 1, 2 * H + 1, ref SMap.myClmClr);//climates
                tlStrSttLbl.Text = "roughness"; Application.DoEvents(); tlStrPrgrssBr.Value = 25;
                SMap.GenRghClr(2 * W, 2 * H, ref SMap.myRghClr);//roughness
                tlStrSttLbl.Text = "features"; Application.DoEvents(); tlStrPrgrssBr.Value = 30;
                SMap.GenFtrClr(W, H, ref SMap.myFtrClr);//features
                tlStrSttLbl.Text = "forests"; Application.DoEvents(); tlStrPrgrssBr.Value = 35;
                SMap.GenForests(2 * W + 1, 2 * H + 1);//forests
                tlStrSttLbl.Text = "water_surface"; Application.DoEvents(); tlStrPrgrssBr.Value = 40;
                SMap.GenWSfClr(256, 256, ref SMap.myWSfClr);//water_surface
                tlStrSttLbl.Text = "trade_routes"; Application.DoEvents(); tlStrPrgrssBr.Value = 40;
                SMap.GenTRtClr(W, H, ref SMap.myTRtClr);//trade_routes
                tlStrSttLbl.Text = "disasters"; Application.DoEvents(); tlStrPrgrssBr.Value = 40;
                SMap.GenDisClr(W, H, ref SMap.myDisClr);//disasters
                tlStrSttLbl.Text = "map of available lands"; Application.DoEvents(); tlStrPrgrssBr.Value = 40;
                SMap.GenAvMap(W, H, SMap.myGTpClr, ref SMap.AvMap);
                tlStrSttLbl.Text = "writing maps"; Application.DoEvents(); tlStrPrgrssBr.Value = 45;

                WriteNewTGA(2 * W + 1, 2 * H + 1, SMap.myHgtClr, IntDir + "//data//world//maps//base//map_heights" + ".tga");
                WriteNewTGA(2 * W + 1, 2 * H + 1, SMap.myGTpClr, IntDir + "//data//world//maps//base//map_ground_types" + ".tga");
                WriteNewTGA(2 * W + 1, 2 * H + 1, SMap.myClmClr, IntDir + "//data//world//maps//base//map_climates" + ".tga");
                WriteNewTGA(2 * W, 2 * H, SMap.myRghClr, IntDir + "//data//world//maps//base//map_roughness" + ".tga");
                WriteNewTGA(W, H, SMap.myFtrClr, IntDir + "//data//world//maps//base//map_features" + ".tga");
                WriteNewTGA(256, 256, SMap.myWSfClr, IntDir + "//data//world//maps//base//water_surface" + ".tga");
                WriteNewTGA(W, H, SMap.myTRtClr, IntDir + "//data//world//maps//base//map_trade_routes" + ".tga");
                WriteNewTGA(W, H, SMap.myDisClr, IntDir + "//data//world//maps//campaign//imperial_campaign//disasters" + ".tga");
                WriteNewTGA(2 * W + 1, 2 * H + 1, SMap.myHmdClr, IntDir + "//data//world//maps//base//map_rainfall" + ".tga");
                WriteNewTGA(2 * W + 1, 2 * H + 1, SMap.myTmpClr, IntDir + "//data//world//maps//base//map_temperatures" + ".tga");
                WriteNewTGA(W, H, SMap.myAvClr, IntDir + "//data//world//maps//base//AvMap" + ".tga");

                //text files
                tlStrSttLbl.Text = "names"; Application.DoEvents(); tlStrPrgrssBr.Value = 50;
                SMap.lettersloaded = LoadLetters();
                GenNames();
                tlStrSttLbl.Text = "empty txt files"; Application.DoEvents(); tlStrPrgrssBr.Value = 55;
                CreateEmptyTxtFile(IntDir + "//data//world//maps//base//descr_disasters.txt");
                CreateEmptyTxtFile(IntDir + "//data//world//maps//campaign//imperial_campaign//descr_events.txt");
                if (!SMap.New)
                    CreateEmptyTxtFile(IntDir + "//data//world//maps//campaign//imperial_campaign//descr_mercenaries.txt");
                CreateEmptyTxtFile(IntDir + "//data//world//maps//campaign//imperial_campaign//descr_win_conditions.txt");
                SMap.CreateDscTrn(W, H, IntDir + "//data//world//maps//base//descr_terrain.txt");

                //regions
                tlStrSttLbl.Text = "regions"; Application.DoEvents(); tlStrPrgrssBr.Value = 55;
                SMap.GenRegions(W, H, SMap.RN, ref SMap.Regs, ref SMap.myRegClr);
                //units
                tlStrSttLbl.Text = "units"; Application.DoEvents(); tlStrPrgrssBr.Value = 75;
                if (chbx_New.Checked)
                {
                    GenUnits();
                }
                tlStrSttLbl.Text = "writing text files"; Application.DoEvents(); tlStrPrgrssBr.Value = 85;
                WriteNewTGA(W, H, SMap.myRegClr, IntDir + "//data//world//maps//base//map_regions" + ".tga");
                SMap.CreateDscReg(SMap.RN, SMap.Regs, IntDir + "//data//world//maps//base//descr_regions.txt");
                SMap.CreateDscStrat(H, SMap.RN, SMap.FN, SMap.Regs, IntDir + "//data//world//maps//campaign//imperial_campaign//descr_strat.txt");
                SMap.CreateRegsTxt(SMap.RN, IntDir + "//data//text//imperial_campaign_regions_and_settlement_names.txt");
                SMap.CreateRegsLookup(SMap.RN, IntDir + "//data//world//maps//campaign//imperial_campaign//descr_regions_and_settlement_name_lookup.txt");

                tlStrSttLbl.Text = "drawing maps"; Application.DoEvents(); tlStrPrgrssBr.Value = 85;
                string olds = Directory.GetCurrentDirectory();
                string news = IntDir + "//data//world//maps//base";
                //Directory.SetCurrentDirectory(news);
                //pbx1.Image = Paloma.TargaImage.LoadTargaImage(IntDir + "/data/world/maps/base/map_heights.tga");
                pbx1.Image = LoadTGAImage(IntDir + "/data/world/maps/base/map_heights.tga");
                //pbx2.Image = Paloma.TargaImage.LoadTargaImage(IntDir + "/data/world/maps/base/map_ground_types.tga");
                pbx2.Image = LoadTGAImage(IntDir + "/data/world/maps/base/map_ground_types.tga");
                //pbx3.Image = Paloma.TargaImage.LoadTargaImage(IntDir + "/data/world/maps/base/map_climates.tga");
                pbx3.Image = LoadTGAImage(IntDir + "/data/world/maps/base/map_climates.tga");
                //pbx4.BackgroundImage = Paloma.TargaImage.LoadTargaImage(IntDir + "/data/world/maps/base/map_regions.tga");
                pbx4.BackgroundImage = LoadTGAImage(IntDir + "/data/world/maps/base/map_regions.tga");
                pbx4.BackgroundImageLayout = ImageLayout.Stretch;
                //pbx5.BackgroundImage = Paloma.TargaImage.LoadTargaImage(IntDir + "/data/world/maps/base/map_features.tga");
                pbx5.BackgroundImage = LoadTGAImage(IntDir + "/data/world/maps/base/map_features.tga");
                pbx5.BackgroundImageLayout = ImageLayout.Stretch;
                //pbx6.BackgroundImage = Paloma.TargaImage.LoadTargaImage(IntDir + "/data/world/maps/base/map_temperatures.tga");
                pbx6.BackgroundImage = LoadTGAImage(IntDir + "/data/world/maps/base/map_temperatures.tga");
                pbx6.BackgroundImageLayout = ImageLayout.Stretch;
                //pbx7.BackgroundImage = Paloma.TargaImage.LoadTargaImage(IntDir + "/data/world/maps/base/map_rainfall.tga");
                pbx7.BackgroundImage = LoadTGAImage(IntDir + "/data/world/maps/base/map_rainfall.tga");
                pbx7.BackgroundImageLayout = ImageLayout.Stretch;
                //Directory.SetCurrentDirectory(olds);
                int oldy = pbx1.Height;
                pbx1.Height = pbx1.Image.Height;
                pbx1.Width = pbx1.Image.Width;
                pbx2.Height = pbx1.Image.Height;
                pbx2.Width = pbx1.Image.Width;
                pbx3.Height = pbx1.Image.Height;
                pbx3.Width = pbx1.Image.Width;
                pbx4.Height = pbx1.Image.Height - 1;
                pbx4.Width = pbx1.Image.Width - 1;
                pbx5.Height = pbx1.Image.Height - 1;
                pbx5.Width = pbx1.Image.Width - 1;
                pbx6.Height = pbx1.Image.Height;
                pbx6.Width = pbx1.Image.Width;
                pbx7.Height = pbx1.Image.Height;
                pbx7.Width = pbx1.Image.Width;
                tabMaps.Width = pbx1.Width + 8;
                tabMaps.Height = pbx1.Height + 26;
                this.Height += pbx1.Height - oldy;
            //this.Cursor = Cursors.Arrow;
            tlStrSttLbl.Text = "done^^"; Application.DoEvents(); tlStrPrgrssBr.Value = 100;
            tlStrPrgrssBr.Visible = false; //sttStr.Hide();
            hmappath = "";
        }

        private void FFbtn_OK_Click(object sender, EventArgs e)
        {
            int i, k = 0;
            for (i = 0; i < 19; i++)
            {
                if (((CheckBox)((Button)sender).Parent.Controls[i]).Checked)
                {
                    k++;
                    SMap.FIs[i] = true;
                }
                else
                {
                    SMap.FIs[i] = false;
                }
            }
            tbx_F.Text = k.ToString();
            ((Form)((Button)sender).Parent).Close();
        }

        private void btn_CstmFacs_Click(object sender, EventArgs e)
        {
            Form FacsForm = new Form();
            int i;
            CheckBox[] FFchbxs = new CheckBox[19];
            for (i = 0; i < 19; i++)
            {
                FFchbxs[i] = new CheckBox();
                FFchbxs[i].Left = 35 + 120*(i/10);
                FFchbxs[i].Top = 20 + 20 * (i%10);
                FFchbxs[i].Text = SMap.Facs[i];
                if (SMap.FIs[i]) FFchbxs[i].Checked = true;
                FacsForm.Controls.Add(FFchbxs[i]);
            }
            Button FFbtn_OK = new Button();
            FFbtn_OK.Left = 150;
            FFbtn_OK.Top = 220;
            FFbtn_OK.Text = "OK";
            FFbtn_OK.Click += FFbtn_OK_Click;
            FacsForm.Controls.Add(FFbtn_OK);
            SMap.fautodef = false;
            FacsForm.ShowDialog();
            
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

        }

        private void tbx_F_TextChanged(object sender, EventArgs e)
        {
            int x, i;
            if (int.TryParse(tbx_F.Text, out x) && SMap.fautodef)
            {
                if ((x > 2) && (x < 20))
                {
                    for (i = 0; i < x; i++)
                    {
                        SMap.FIs[i] = true;
                    }
                    for (i = x; i < 19; i++)
                    {
                        SMap.FIs[i] = false;
                    }
                }
            }
            SMap.fautodef = true;
        }

        private void btn_fExHMap_Click(object sender, EventArgs e)
        {
            if (OFDlg.ShowDialog() == DialogResult.OK)
            {
                hmappath = OFDlg.FileName;
                btnGen.PerformClick();
            }
        }

        private void CCbtn_OK_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < 7; i++)
            {
                SMap.clm_hmd_Dw[i] = int.Parse((((TextBox)((Button)sender).Parent.Controls[i]).Text));
                SMap.clm_hmd_De[i] = int.Parse((((TextBox)((Button)sender).Parent.Controls[7 + i]).Text));
            }
            SMap.clm_tmp_c = int.Parse((((TextBox)((Button)sender).Parent.Controls[14]).Text));
            SMap.clm_tmp_C = int.Parse((((TextBox)((Button)sender).Parent.Controls[15]).Text));
            ((Form)((Button)sender).Parent).Close();
        }

        private void btn_Clm_Click(object sender, EventArgs e)
        {
            Form ClmForm = new Form();
            ClmForm.Height = 350;
            int i;

            TextBox[] HmdTbxs = new TextBox[14];
            for (i = 0; i < 7; i++)
            {
                HmdTbxs[i] = new TextBox();
                HmdTbxs[i].Left = 20;
                HmdTbxs[i].Top = 50 + 20 * i;
                HmdTbxs[i].Text = SMap.clm_hmd_Dw[i].ToString();
                ClmForm.Controls.Add(HmdTbxs[i]);
            }
            for (i = 7; i < 14; i++)
            {
                HmdTbxs[i] = new TextBox();
                HmdTbxs[i].Left = 130;
                HmdTbxs[i].Top = 50 + 20 * (i - 7);
                HmdTbxs[i].Text = SMap.clm_hmd_De[i - 7].ToString();
                ClmForm.Controls.Add(HmdTbxs[i]);
            }

            TextBox tbx_c = new TextBox();
            tbx_c.Left = 20;
            tbx_c.Top = 240;
            tbx_c.Text = SMap.clm_tmp_c.ToString();
            ClmForm.Controls.Add(tbx_c);

            TextBox tbx_C = new TextBox();
            tbx_C.Left = 130;
            tbx_C.Top = 240;
            tbx_C.Text = SMap.clm_tmp_C.ToString();
            ClmForm.Controls.Add(tbx_C);

            Button CCbtn_OK = new Button();
            CCbtn_OK.Left = 150;
            CCbtn_OK.Top = 270;
            CCbtn_OK.Text = "OK";
            CCbtn_OK.Click += CCbtn_OK_Click;
            ClmForm.Controls.Add(CCbtn_OK);

            Label CClbl_r = new Label();
            CClbl_r.Left = 70;
            CClbl_r.Top = 10;
            CClbl_r.Text = "cyclones power";
            ClmForm.Controls.Add(CClbl_r);

            Label CClbl_w = new Label();
            CClbl_w.Left = 20;
            CClbl_w.Top = 30;
            CClbl_w.Text = "west";
            ClmForm.Controls.Add(CClbl_w);

            Label CClbl_e = new Label();
            CClbl_e.Left = 130;
            CClbl_e.Top = 30;
            CClbl_e.Text = "east";
            ClmForm.Controls.Add(CClbl_e);

            Label CClbl_t = new Label();
            CClbl_t.Left = 70;
            CClbl_t.Top = 200;
            CClbl_t.Text = "temperature";
            ClmForm.Controls.Add(CClbl_t);

            Label CClbl_n = new Label();
            CClbl_n.Left = 20;
            CClbl_n.Top = 220;
            CClbl_n.Text = "north";
            ClmForm.Controls.Add(CClbl_n);

            Label CClbl_s = new Label();
            CClbl_s.Left = 130;
            CClbl_s.Top = 220;
            CClbl_s.Text = "south";
            ClmForm.Controls.Add(CClbl_s);

            SMap.fautodef = false;
            ClmForm.ShowDialog();
        }
    }
}

