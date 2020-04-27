using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ellenorzo
{
    [Serializable()]
    public class Jegy : ISerializable
    {
        int erdemjegy;

        public Jegy(int jegy)
        {
            Erdemjegy = jegy;
        }

        public int Erdemjegy { get => erdemjegy; set => erdemjegy = value; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Erdemjegy", Erdemjegy);
        }

        public Jegy() { }

        public Jegy(SerializationInfo info, StreamingContext context)
        {
            Erdemjegy = (int) info.GetValue("Erdemjegy", typeof( int ) );
        }
    }
}
