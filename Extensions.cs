using wpf_gastosPessoais.Models;

namespace wpf_gastosPessoais
{
    public static class Extensions
    {
        public static string GetString(this EntryType entryType)
        {
            return entryType == EntryType.Credit ? "Crédito" : "Débito";
        }
    }
}
