using DatabaseGenarator.Model;
using System.Text;

namespace DatabaseGenarator;
public class DataSeeder
{
    public static void SeedData(HubDbContext context)
    {
        var totalRows = 300000;
        var endDate = DateTime.Now;
        var startDate = endDate.AddMonths(-1);
        var totalSeconds = (endDate - startDate).TotalSeconds;
        var intervalSeconds = totalSeconds / totalRows;

        var entities = new List<Packet>();

        for (int i = 0; i < totalRows; i++)
        {
            var entity = new Packet
            {
                BinaryData = GetRandomBinaryData(),
                Channel = GetRandomChannel(),
                Status = GetRandomPacketStatus(),
                DateCreated = startDate.AddSeconds(intervalSeconds * i),
            };
            entities.Add(entity);
        }

        context.AddRange(entities);
        context.SaveChanges();
    }

    private static PacketStatus GetRandomPacketStatus()
    {
        var values = Enum.GetValues(typeof(PacketStatus));
        var random = new Random();
        return (PacketStatus?)values?.GetValue(random.Next(values.Length)) ?? PacketStatus.Enqueued;
    }

    private static string GetRandomChannel()
    {
        var random = new Random();
        var randomNumber = random.Next(2); // Generates a random number between 0 and 1

        if (randomNumber == 0)
        {
            return "Device01ToDevice02_Data";
        }
        else
        {
            return "Device02ToDevice01_Data";
        }
    }

    private static byte[] GetRandomBinaryData()
    {
        var random = new Random();
        var length = random.Next(1, 21); // Random length between 1 and 20
        var bytes = new byte[length];
        random.NextBytes(bytes);
        var randomString = Encoding.UTF8.GetString(bytes);
        var filteredString = new string(randomString.Where(c => char.IsLetter(c) || c == ' ').ToArray());
        return Encoding.UTF8.GetBytes(filteredString); ;
    }
}