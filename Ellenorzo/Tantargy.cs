using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ellenorzo
{
    [Serializable()]
    public class Tantargy : ISerializable
    {
        string nev;
        List<Jegy> jegyek = new List<Jegy>();
        public Tantargy(string nev)
        {
            Nev = nev;
        }
        public string Nev { get => nev; set => nev = value; }
        public string NevKicsi { get => nev.ToLower(); }
        public List<Jegy> Jegyek { get => jegyek; set => jegyek = value; }
        public void UjJegy(int jegy)
        {
            jegyek.Add(new Jegy(jegy));
        }
        public void TorolJegy(int index)
        {
            jegyek.RemoveAt(index);
        }
        public double Atlag()
        {
            int sum = 0;
            double atlag = 0.0;

            foreach (var item in jegyek)
            {
                sum += item.Erdemjegy;
            }

            if (jegyek.Count > 0)
            {
                atlag = (double) sum / jegyek.Count;
            }

            return atlag;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Nev", Nev);
        }

        public Tantargy() { }

        public Tantargy(SerializationInfo info, StreamingContext context)
        {
             Nev = (string) info.GetValue( "Nev", typeof( string ) );
        }

    }
}
