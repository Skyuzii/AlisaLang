namespace AlisaLang.Reader
{
    public class StringReader : AbstractReader<string, char>
    {
        public int Line { get; set; }
        public int CharIndex { get; set; }

        public StringReader(string data) : base(data)
        {
        }

        public override bool IsEmpty(int offset = 0)
            => Position + offset < 0 || Position + offset >= Content.Length;

        public override char Read(int offset = 0)
            => IsEmpty(offset) ? '\0' : Content[Position + offset];

        protected override string CreateT()
            => string.Empty;

        protected override string AddToT(object part, string item)
            => item += part;

        public override void Next(int count = 1)
        {
            base.Next(count);

            if (Read() != '\n')
            {
                CharIndex++;
                return;
            }

            Line++;
            CharIndex = 0;
        }
    }
}