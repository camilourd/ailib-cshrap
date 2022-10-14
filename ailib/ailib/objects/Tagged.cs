using System.Collections.Generic;

namespace ailib.objects
{
    public class Tagged<S>
    {
        S value;
        Dictionary<string, object> elements = new Dictionary<string, object>();

        public Tagged(S value)
        {
            this.value = value;
        }

        public S unwrap()
        {
            return value;
        }

        public object find(string tag)
        {
            return elements[tag];
        }

        public void store(string tag, object result)
        {
            elements.Add(tag, result);
        }

        public bool contains(string tag)
        {
            return elements.ContainsKey(tag);
        }

        public bool remove(string tag)
        {
            return elements.Remove(tag);
        }
    }
}
