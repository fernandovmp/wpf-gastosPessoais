namespace wpf_gastosPessoais.Models
{
    public struct EntryGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static EntryGroup[] DefaultGroups()
        {
            EntryGroup[] entryGroups = 
            {
                new EntryGroup
                {
                    Name = "Alimentação",
                    Type = -1
                },
                new EntryGroup
                {
                    Name = "Estudos",
                    Type = -1
                },
                new EntryGroup
                {
                    Name = "Moradia",
                    Type = -1
                },
                new EntryGroup
                {
                    Name = "Pagamentos",
                    Type = -1
                },
                new EntryGroup
                {
                    Name = "Transporte",
                    Type = -1
                },
                new EntryGroup
                {
                    Name = "Saúde",
                    Type = -1
                },
                new EntryGroup
                {
                    Name = "Salário",
                    Type = 1
                },
                new EntryGroup
                {
                    Name = "Presente",
                    Type = 1
                },
                new EntryGroup
                {
                    Name = "Investimento",
                    Type = 1
                },
                new EntryGroup
                {
                    Name = "Outro",
                    Type = 1
                }
            };
            return entryGroups;
        }

    }
}
