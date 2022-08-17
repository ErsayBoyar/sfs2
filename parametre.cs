// Decompiled with JetBrains decompiler
// Type: web_servisi.parametre
// Assembly: web_servisi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21540CE4-A506-4EC1-A7DB-398E2A438F81
// Assembly location: E:\ersay\bin\web_servisi.dll

using System;
using System.Data;
using System.Data.OracleClient;

namespace web_servisi
{
  internal class parametre
  {
    public static OracleConnection conn = new OracleConnection();
    public static OracleDataAdapter adap = new OracleDataAdapter();
    public static OracleCommand komut = parametre.conn.CreateCommand();
    private OracleDataReader okuyucu;
    public static DataSet ds = new DataSet();
    private DataTable dt_sql_tek_deger = new DataTable();
    private DataTable dt_oylesine = new DataTable();

    public bool sayimi(char tusbas) => (tusbas >= ':' || tusbas <= '/') && tusbas != '\b';

    public void sql_calistir(string sql)
    {
      try
      {
        parametre.conn.Open();
        parametre.komut.CommandText = sql;
        parametre.komut.ExecuteNonQuery();
        parametre.conn.Close();
      }
      catch
      {
      }
      finally
      {
        parametre.conn.Close();
      }
    }

    public bool sql_bool(string sql)
    {
      bool flag = false;
      try
      {
        parametre.conn.Open();
        parametre.komut.CommandText = sql;
        this.okuyucu = parametre.komut.ExecuteReader();
        if (this.okuyucu.Read())
          flag = true;
        parametre.conn.Close();
      }
      catch
      {
        parametre.conn.Close();
      }
      return flag;
    }

    public string sql_tek_deger(string sql)
    {
      try
      {
        this.dt_sql_tek_deger.Clear();
        this.dt_sql_tek_deger.Columns.Clear();
        parametre.komut.CommandText = sql;
        parametre.adap.Fill(this.dt_sql_tek_deger);
      }
      catch
      {
      }
      if (this.dt_sql_tek_deger.Rows.Count > 0)
      {
        sql = this.dt_sql_tek_deger.Rows[0][0].ToString();
        if (sql == "")
          sql = "0";
      }
      else
        sql = "0";
      return sql;
    }

    public string yaziya_cevir(string sayi)
    {
      string str1 = "";
      try
      {
        string[] strArray1 = new string[10]
        {
          "",
          "bir",
          "iki",
          "üç",
          "dört",
          "beş",
          "altı",
          "yedi",
          "sekiz",
          "dokuz"
        };
        string[] strArray2 = new string[10]
        {
          "",
          "on",
          "yirmi",
          "otuz",
          "kırk",
          "elli",
          "altmış",
          "yetmiş",
          "seksen",
          "doksan"
        };
        string[] strArray3 = sayi.Split(',');
        string str2 = strArray3[0].PadLeft(6, '0');
        char ch;
        if (str2[0] != '0' && str2[0] != '1')
        {
          string[] strArray4 = strArray1;
          ch = str2[0];
          int int32 = Convert.ToInt32(ch.ToString());
          str1 = strArray4[int32];
        }
        if (str2[0] != '0')
          str1 += "yüz";
        string str3 = str1;
        string[] strArray5 = strArray2;
        ch = str2[1];
        int int32_1 = Convert.ToInt32(ch.ToString());
        string str4 = strArray5[int32_1];
        str1 = str3 + str4;
        string str5 = str1;
        string[] strArray6 = strArray1;
        ch = str2[2];
        int int32_2 = Convert.ToInt32(ch.ToString());
        string str6 = strArray6[int32_2];
        str1 = str5 + str6;
        if (str2[0] != '0' || str2[1] != '0' || str2[2] != '0')
          str1 += " bin ";
        if (str1 == "bir bin ")
          str1 = "bin ";
        if (str2[3] != '0' && str2[3] != '1')
        {
          string str7 = str1;
          string[] strArray7 = strArray1;
          ch = str2[3];
          int int32_3 = Convert.ToInt32(ch.ToString());
          string str8 = strArray7[int32_3];
          str1 = str7 + str8;
        }
        if (str2[3] != '0')
          str1 += "yüz";
        string str9 = str1;
        string[] strArray8 = strArray2;
        ch = str2[4];
        int int32_4 = Convert.ToInt32(ch.ToString());
        string str10 = strArray8[int32_4];
        str1 = str9 + str10;
        string str11 = str1;
        string[] strArray9 = strArray1;
        ch = str2[5];
        int int32_5 = Convert.ToInt32(ch.ToString());
        string str12 = strArray9[int32_5];
        str1 = str11 + str12;
        if (str2[3] != '0' || str2[4] != '0' || str2[5] != '0')
          str1 += " lira ";
        string str13 = strArray3[1].PadRight(3, '0');
        string str14 = str1;
        string[] strArray10 = strArray2;
        ch = str13[0];
        int int32_6 = Convert.ToInt32(ch.ToString());
        string str15 = strArray10[int32_6];
        str1 = str14 + str15;
        string str16 = str1;
        string[] strArray11 = strArray1;
        ch = str13[1];
        int int32_7 = Convert.ToInt32(ch.ToString());
        string str17 = strArray11[int32_7];
        str1 = str16 + str17;
        if (str13[0] != '0' || str13[1] != '0')
          str1 += " kuruş";
      }
      catch
      {
      }
      return str1;
    }

    public bool kaydet(string tablo, string select_command)
    {
      bool flag = false;
      try
      {
        if (parametre.ds.Tables[tablo].Rows[parametre.ds.Tables[tablo].Rows.Count - 1].RowState == DataRowState.Added)
          flag = true;
        parametre.komut.CommandText = select_command;
        OracleCommandBuilder oracleCommandBuilder = new OracleCommandBuilder(parametre.adap);
        parametre.adap.InsertCommand = oracleCommandBuilder.GetInsertCommand();
        parametre.adap.UpdateCommand = oracleCommandBuilder.GetUpdateCommand();
        parametre.adap.DeleteCommand = oracleCommandBuilder.GetDeleteCommand();
        parametre.adap.Update(parametre.ds.Tables[tablo]);
      }
      catch (Exception ex)
      {
        string str = ex.Message.Substring(0, 9);
        flag = true;
        if (str == "ORA-01400")
          flag = false;
      }
      return flag;
    }

    public void kaydet(DataTable tablo, string select_command)
    {
      try
      {
        parametre.komut.CommandText = select_command;
        OracleCommandBuilder oracleCommandBuilder = new OracleCommandBuilder(parametre.adap);
        parametre.adap.InsertCommand = oracleCommandBuilder.GetInsertCommand();
        parametre.adap.UpdateCommand = oracleCommandBuilder.GetUpdateCommand();
        parametre.adap.DeleteCommand = oracleCommandBuilder.GetDeleteCommand();
        parametre.adap.Update(tablo);
      }
      catch
      {
      }
    }

    public void tablo_doldur(string sql, string tablo_ad)
    {
      try
      {
        parametre.ds.Tables[tablo_ad].Clear();
        parametre.komut.CommandText = sql;
        parametre.adap.Fill(parametre.ds, tablo_ad);
      }
      catch
      {
      }
    }

    public DataTable tablo_gonder(string sql)
    {
      try
      {
        parametre.komut.Connection = parametre.conn;
        parametre.adap.SelectCommand = parametre.komut;
        this.dt_oylesine.Dispose();
        this.dt_oylesine = new DataTable();
        parametre.komut.CommandText = sql;
        parametre.adap.Fill(this.dt_oylesine);
      }
      catch
      {
      }
      return this.dt_oylesine;
    }

    public double xRound(double number)
    {
      string[] strArray1 = new string[2];
      string[] strArray2 = number.ToString().Split(',');
      string[] strArray3;
      (strArray3 = strArray2)[1] = strArray3[1] + "00";
      strArray2[1] = strArray2[1].Substring(0, 2);
      return !(strArray2[1].Substring(strArray2[1].Length - 1, 1) == "0") ? Convert.ToDouble(strArray2[0] + "," + Convert.ToString(Convert.ToDouble(strArray2[1].Substring(0, 1)) + 1.0)) : Convert.ToDouble(strArray2[0] + "," + strArray2[1].Substring(0, 2));
    }
  }
}
