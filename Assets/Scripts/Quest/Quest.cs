public class Quest
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsCompleted { get; set; }

    public Quest(string id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        IsCompleted = false;
    }
}
