public class AutocompleteSystem{
    public class TrieNode{
        public Dictionary<char, TrieNode> children;
        public List<string> pq;
        public TrieNode(){
            this.children = new Dictionary<char, TrieNode>();
            this.pq = new List<string>();
        }
    }
    TrieNode root;
    StringBuilder sb;
    Dictionary<string, int> map;

    public void Insert(string word, int times){
        TrieNode current = root;
        for(int i = 0; i< word.Length; i++){
            var c = word[i];
            if(!current.children.ContainsKey(c)){
                current.children.Add(c, new TrieNode()); 
            }
            current = current.children[c];
            List<string> temp = new List<string>();
            temp = current.pq;
            if(!temp.Contains(word)){
                temp.Add(word);
            }
            temp.Sort((a, b) => {
                if(map[a] == map[b]){
                    return a.CompareTo(b);
                }
                return map[b] - map[a];
            });
            
            if(temp.Count > 3){
                temp.RemoveAt(temp.Count - 1);
            }
            current.pq = temp;
        }
    }

    public List<string> StartsWith(string word){
        TrieNode current = root;
        for(int i = 0; i< word.Length; i++){
            var c = word[i];
            if(!current.children.ContainsKey(c)){
                return new List<string>();
            }
            current = current.children[c];
        }
        return current.pq;
    }

    public AutocompleteSystem(string[] sentences, int[] times) {
        root = new TrieNode();
        sb = new StringBuilder();
        map = new Dictionary<string, int>();
        for(int i = 0; i < sentences.Length; i++){
            map.TryAdd(sentences[i], 0);
            map[sentences[i]] += times[i];
            Insert(sentences[i], times[i]);
        }
    }

    public IList<string> Input(char c) {
        if(c == '#'){
            string s = sb.ToString();
            map.TryAdd(s, 0);
            map[s] += 1;
            Insert(s, 1);
            sb = new StringBuilder();
            return new List<string>();
        }
        sb.Append(c);
        return StartsWith(sb.ToString());
    }
}
///////////////////////////////////////////////////////////////
public class AutocompleteSystem2 {
    public class TrieNode{
        public Dictionary<char, TrieNode> children;
        public Dictionary<string, int> map;
        public TrieNode(){
            this.children = new Dictionary<char, TrieNode>();
            this.map = new Dictionary<string, int>();
        }
    }

    TrieNode root;
    StringBuilder sb;

    public void Insert(string word, int times){
        TrieNode current = root;
        for(int i = 0; i< word.Length; i++){
            var c = word[i];
            if(!current.children.ContainsKey(c)){
                current.children.Add(c, new TrieNode()); 
            }
            current = current.children[c];
            current.map.TryAdd(word, 0);
            current.map[word] += times;
        }
    }

    public Dictionary<string, int> StartsWith(string word){
        TrieNode current = root;
        for(int i = 0; i< word.Length; i++){
            var c = word[i];
            if(!current.children.ContainsKey(c)){
                return new Dictionary<string, int>();
            }
            current = current.children[c];
        }
        return current.map;
    }
    public AutocompleteSystem2(string[] sentences, int[] times) {
        root = new TrieNode();
        sb = new StringBuilder();
        for(int i = 0; i < sentences.Length; i++){
            Insert(sentences[i], times[i]);
        }
    }
    public IList<string> Input(char c) {
        if(c == '#'){
            string s = sb.ToString();
            Insert(s, 1);
            sb = new StringBuilder();
        }
        else{
            sb.Append(c);
        }
        
        
        Dictionary<string, int> map = StartsWith(sb.ToString());
        PriorityQueue<string, string> pqueue = new PriorityQueue<string, string>(Comparer<string>.Create((a,b) => {
            if(map[a] == map[b]){
                return b.CompareTo(a);
            }
            return map[a] - map[b];
        }));
        foreach(var kv in map){
            if(kv.Key.StartsWith(sb.ToString())){
                pqueue.Enqueue(kv.Key, kv.Key);
                if(pqueue.Count > 3){
                    pqueue.Dequeue();
                }
            }
        }

        List<string> result = new List<string>();
        while(pqueue.Count > 0){
            result.Insert(0, pqueue.Dequeue());
        }
        return result;
    }
}

///////////////////////////////////////////////////////////////
public class AutocompleteSystem1 {
    Dictionary<string, int> map;
    StringBuilder sb;
    public AutocompleteSystem1(string[] sentences, int[] times) {
        map = new Dictionary<string, int>();
        sb = new StringBuilder();
        for(int i = 0; i< sentences.Length; i++){
            map.TryAdd(sentences[i], 0);
            map[sentences[i]] += times[i];
        }
    }
    
    public IList<string> Input(char c) {
        if(c == '#'){
            string s = sb.ToString();
            map.TryAdd(s, 0);
            map[s] += 1;
            sb = new StringBuilder();
            return new List<string>();
        }
        sb.Append(c);
        PriorityQueue<string, string> pqueue = new PriorityQueue<string, string>(Comparer<string>.Create((a,b) => {
            if(map[a] == map[b]){
                return b.CompareTo(a);
            }
            return map[a] - map[b];
        }));

        foreach(var kv in map){
            if(kv.Key.StartsWith(sb.ToString())){
                pqueue.Enqueue(kv.Key, kv.Key);
                if(pqueue.Count > 3){
                    pqueue.Dequeue();
                }
            }
        }
        List<string> result = new List<string>();
        while(pqueue.Count > 0){
            result.Insert(0, pqueue.Dequeue());
        }
        return result;
    }
}

/**
 * Your AutocompleteSystem object will be instantiated and called as such:
 * AutocompleteSystem obj = new AutocompleteSystem(sentences, times);
 * IList<string> param_1 = obj.Input(c);
 */
