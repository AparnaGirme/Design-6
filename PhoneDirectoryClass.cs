// TC => O(n) for constructor else O(1)
// SC => O(n)
public class PhoneDirectory {

    HashSet<int> hashset;
    Queue<int> queue;
    public PhoneDirectory(int maxNumbers) {
        hashset = new HashSet<int>();
        queue = new Queue<int>();
        for(int i = 0; i< maxNumbers;i++){
            hashset.Add(i);
            queue.Enqueue(i);
        }
    }
    
    public int Get() {
        if(queue.Count == 0){
            return -1;
        }
        var num = queue.Dequeue();
        hashset.Remove(num);
        return num;
    }
    
    public bool Check(int number) {
        return hashset.Contains(number);
    }
    
    public void Release(int number) {
        if(!hashset.Contains(number)){
            hashset.Add(number);
            queue.Enqueue(number);
        }
    }
}

/**
 * Your PhoneDirectory object will be instantiated and called as such:
 * PhoneDirectory obj = new PhoneDirectory(maxNumbers);
 * int param_1 = obj.Get();
 * bool param_2 = obj.Check(number);
 * obj.Release(number);
 */