
public static class ServiceManager
{
    private static string _filePath = "services.bin";

    public static void SetFilePath(string path)
    {
        _filePath = path;
    }

    public static List<Service> LoadFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Service>();
        }
        List<Service> services = new List<Service>();
        using (BinaryReader reader = new BinaryReader(File.Open(_filePath, FileMode.Open)))
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                int id = reader.ReadInt32();
                string name = reader.ReadString();
                string category = reader.ReadString();
                decimal price = reader.ReadDecimal();
                int duration = reader.ReadInt32();
                services.Add(new Service(id, name, category, price, duration));
            }
        }
        return services;
    }

    private static void SaveToFile(List<Service> services)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(_filePath, FileMode.Create)))
        {
            writer.Write(services.Count);
            foreach (Service s in services)
            {
                writer.Write(s.Id);
                writer.Write(s.Name);
                writer.Write(s.Category);
                writer.Write(s.Price);
                writer.Write(s.DurationMinutes);
            }
        }
    }

    public static List<Service> GetAll()
    {
        return LoadFromFile();
    }

    public static void AddService(Service service)
    {
        List<Service> services = LoadFromFile();
        int maxId = services.Count == 0 ? 0 : services.Max(s => s.Id);
        service.Id = maxId + 1;
        services.Add(service);
        SaveToFile(services);
    }

    public static bool DeleteServiceById(int id)
    {
        List<Service> services = LoadFromFile();
        Service toRemove = services.FirstOrDefault(s => s.Id == id);
        if (toRemove == null)
        {
            return false;
        }
        services.Remove(toRemove);
        SaveToFile(services);
        return true;
    }
}