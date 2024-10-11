using System.Text.Json;

namespace MyMLApp
{
    public class Guestbook // klass för att skapa gästbok
    {
        private string filename = @"guestbook.json"; // filnamn för att spara gästboksinlägg
        private List<GuestbookEntry> guestbookEntries = new List<GuestbookEntry>(); // lista för att spara inlägg i gästboken

        public Guestbook()
        { // konstruktor för att skapa gästbok
            if (File.Exists(filename)==true){ // If stored json data exists then read
                string jsonString = File.ReadAllText(filename); // läser in json data från fil
                guestbookEntries = JsonSerializer.Deserialize<List<GuestbookEntry>>(jsonString)!; // deserialiserar json data till lista
            }
        }
        public GuestbookEntry AddGuestbookEntry(string name, string comment, char assessment)
        { // metod för att lägga till inlägg i gästboken
            GuestbookEntry obj = new GuestbookEntry(); // skapar nytt objekt av klassen GuestbookEntry
            obj.Comment = comment; // sätter kommentar
            obj.Name = name; // sätter namn
            obj.Assessment= assessment; // sätter bedömning
            guestbookEntries.Add(obj);// lägger till objekt i listan
            Marshal();// serialiserar objekt och sparar till fil
            return obj; // returnerar objekt
        }
        
        public int DelEntry(int index)
        { // metod för att ta bort inlägg i gästboken
            guestbookEntries.RemoveAt(index); // tar bort inlägg från listan
            Marshal(); // serialiserar objekt och sparar till fil
            return index; // returnerar index
        }

        public int Length()
        {
            int length = guestbookEntries.Count; // räknar antal inlägg i gästboken
            return length; // returnerar antal inlägg
        }

        public Tuple<int, int> ApprovalData()
        {
            int positiveRemarks = 0; // Börjar med att sätta antalet positiva bedömningar till 0

            foreach (var entry in guestbookEntries) // Loopar igenom alla inlägg i gästboken
            {
                if (entry.Assessment == '+') // Om bedömningen är positiv så öka antalet positiva bedömningar med 1
                {
                    positiveRemarks++; 
                }
            }

            return Tuple.Create(positiveRemarks, this.Length());
        }

        public List<GuestbookEntry> GetGuestbookEntries()
        { // metod för att hämta inlägg i gästboken
            return guestbookEntries; // returnerar lista med inlägg
        }

        private void Marshal() // metod för att serialisera objekt och spara till fil
        {
            // Serialize all the objects and save to file
            var jsonString = JsonSerializer.Serialize(guestbookEntries);// serialiserar lista med objekt till json
            File.WriteAllText(filename, jsonString); // skriver json data till fil
        }
    }
}

//bygger väldigt mycket på exempelfil från föreläsning