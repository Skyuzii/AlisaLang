using System;
using AlisaLang.Reader.Models;

namespace AlisaLang.Reader
{
    public abstract class AbstractReader<T, T2>
    {
        public T Content { get; set; }
        
        public int Position { get; protected set; }
        
        public abstract bool IsEmpty(int offset = 0);
        
        public abstract T2 Read(int offset = 0);
        
        protected abstract T CreateT();
        
        protected abstract T AddToT(object part, T item);

        public AbstractReader(T data)
        {
            Content = data;
        }

        public virtual void Next(int count = 1) => Position += count;

        public T ReadWhile(Func<T2, ReadWhileResponse> readWhile)
        {
            var item = CreateT();

            while (!IsEmpty())
            {
                var part = Read();
                if (part == null) break;

                var response = readWhile.Invoke(part);
                if (response.Value != null)
                {
                    if (response.Value is not T && response.Value is not T2)
                        throw new InvalidCastException("ReadWhile вернул неверное значение");
                    
                    item = AddToT(response.Value, item);
                }
                
                if (!response.IsContinue) break;
                Next();
            }

            return item;
        }
    }
}