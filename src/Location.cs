namespace STiles;

public class FileLocation
{
    public bool IsTextured {get; set;} 
    public string Location {get; set;}
    public int Index {get; set;}

    public FileLocation(bool isTextured, int index)
    {
        IsTextured = isTextured;
        Location = "";
        Index = index;
    }

    private FileLocation()
    {
        IsTextured = false;
        Index = 2;
        Location = "";
    }
}