// Decompiled with JetBrains decompiler
// Type: web_servisi.Service1
// Assembly: web_servisi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21540CE4-A506-4EC1-A7DB-398E2A438F81
// Assembly location: E:\ersay\bin\web_servisi.dll

using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web.Services;

namespace web_servisi
{
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [ToolboxItem(false)]
  [WebService(Namespace = "http://tempuri.org/")]
  public class Service1 : WebService
  {
    private parametre param = new parametre();
    private DataSet ds_hastalar = new DataSet();

    [WebMethod]
    public DataTable gonder(string gelen, string sifreee, string id)
    {
      StreamReader streamReader = new StreamReader((Stream) new FileStream("C:\\veritabani.txt", FileMode.Open, FileAccess.Read));
      parametre.conn.ConnectionString = streamReader.ReadLine();
      if (this.param.sql_bool("select * from kod_protez_lab kpl where kpl.id=" + id + " and kpl.web_sifre=" + sifreee))
      {
        parametre.komut.Connection = parametre.conn;
        parametre.adap.SelectCommand = parametre.komut;
        parametre.komut.CommandText = gelen;
        parametre.adap.Fill(this.ds_hastalar, "hastalar");
      }
      return this.ds_hastalar.Tables["hastalar"];
    }
  }
}
