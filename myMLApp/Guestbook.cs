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

        public (int positiveRemarks, int length, int approvalRate) ApprovalData() // metod för att räkna ut godkännandegrad etc
        {
            int length = this.Length(); // räknar antal inlägg i gästboken
            int positiveRemarks = 0; 

            foreach (var entry in guestbookEntries) // loopar igenom inlägg i gästboken
            {
                if (entry.Assessment == '+') // om bedömning är positiv
                {
                    positiveRemarks++; // ökar räknare för positiva inlägg
                }
            }
            int approvalRate = (int)Math.Round(((double)positiveRemarks / length) * 100, 0); // räknar ut godkännandegrad

            return (positiveRemarks, length, approvalRate); // returnerar antal positiva inlägg, antal inlägg och godkännandegrad
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