using System.Daemon.Utils;
using System.Daemon.Interface;

namespace System.Daemon
{
    public class Pericia : IPericia
    {        
        public Pericia(string nome, NomeAtributo? nomeAtributoBase = null, string nomeLimitador = null, IAtributo atributoBase = null, IPericia limitador = null, byte incremento = 0)
        {
            Nome = nome;
            NomeAtributoBase = nomeAtributoBase;
            NomeLimitador = nomeLimitador;
            VincularPericia(atributoBase, limitador, incremento);
        }

        public void VincularPericia(IAtributo atributoBase = null, IPericia limitador = null, byte incremento = 0)
        {
            Incremento = incremento;
            AtributoBase = (atributoBase != null && atributoBase.Nome == NomeAtributoBase.Value) ? atributoBase : throw new RuleException("Atributo incompativel com a pericia");
            Limitador = (limitador != null && limitador.Nome == NomeLimitador) ? limitador : throw new RuleException("Limitador incompativel com a pericia");
        }

        public string Nome { get; private set; }
        public byte Valor { 
            get
            {
                var valor = Convert.ToByte(AtributoBase == null ? Incremento : AtributoBase.Valor + Incremento);
                if (Limitador == null)
                    return valor;
                return Limitador.Valor < valor ? valor : Limitador.Valor;
            } 
        }
        public float Teste 
        { 
            get
            {
                return (Valor * 0.01f);
            } 
        }
        public byte Incremento { get; set; }
        public NomeAtributo? NomeAtributoBase { get; private set; }
        public IAtributo AtributoBase { get; private set; }
        public string NomeLimitador { get; private set;}
        public IPericia Limitador { get; private set; }
    }
}